using MkvTests.Ebml;
using System;
using System.IO;

namespace MkvTests.Matroska
{
    internal class MatroskaBlock
    {
        private readonly MemoryStream data;
        protected int[] sizes;
        protected int headerSize;
        public int TrackNumber { get; private set; }
        public int BlockTimecode { get; private set; }
        public int FrameCount { get; set; }
        public bool IsKeyFrame { get; private set; }


        public MatroskaBlock(MemoryStream data)
        {
            this.data = data;
            sizes = null;
            headerSize = 0;
            BlockTimecode = 0;
            TrackNumber = 0;
        }

        public void ParseBlock()
        {
            int index = 0;
            TrackNumber = (int)EbmlReader.ReadEbmlCode(data);
            index = Utility.CodedSizeLength((ulong)TrackNumber, 0);
            headerSize += index;

            byte[] readBytes = new byte[2];
            data.Read(readBytes, 0, 2);
            BlockTimecode = BitConverter.ToInt16(readBytes, 0);

            byte flagsByte = (byte)data.ReadByte();
            int keyFlag = flagsByte & 0x80;
            IsKeyFrame = keyFlag > 0;

            int laceFlag = flagsByte & 0x06;
            index++;

            headerSize += 3;
            if(laceFlag != 0x00)
            {
                byte laceCount = (byte)data.ReadByte();
                headerSize += 1;
                if (laceFlag == 0x02)
                    sizes = readXiphLaceSizes(index, laceCount);
                else if (laceFlag == 0x06)
                    sizes = readEbmlLaceSizes(index, laceCount);
                else if (laceFlag == 0x04)
                {
                    sizes = new int[laceCount + 1];
                    sizes[0] = (int)((data.Length - data.Position) - headerSize) / (laceCount + 1);
                    for (int s = 0; s < laceCount; s++)
                        sizes[s + 1] = sizes[0];
                }
                else
                {
                    throw new Exception("Unsupported lacing type flag.");
                }
            }
            headerSize = (int)data.Position;
        }

        private int[] readEbmlLaceSizes(int index, short laceCount)
        {
            int[] laceSizes = new int[laceCount + 1];
            laceSizes[laceCount] = (int)(data.Length - data.Position);

            int startIndex = index;

            laceSizes[0] = (int)EbmlReader.ReadEbmlCode(data);
            index += Utility.CodedSizeLength((ulong)laceSizes[0], 0);
            laceSizes[laceCount] -= laceSizes[0];

            ulong firstEbmlSize = (ulong)laceSizes[0];
            ulong lastEbmlSize = 0;
            for(int l = 0; l < laceCount - 1; l++)
            {
                lastEbmlSize = EbmlReader.ReadSignedEbmlCode(data);
                index += Utility.CodedSizeLength(lastEbmlSize, 0);

                firstEbmlSize += lastEbmlSize;
                laceSizes[l + 1] = (int)firstEbmlSize;

                laceSizes[laceCount] -= laceSizes[l + 1];
            }

            headerSize += (index - startIndex);
            laceSizes[laceCount] -= headerSize;

            return laceSizes;
        }

        private int[] readXiphLaceSizes(int index, short laceCount)
        {
            int[] laceSizes = new int[laceCount + 1];
            laceSizes[laceCount] = (int)(data.Length - data.Position);

            // long ByteStartPos = source.getFilePointer();

            for (int l = 0; l < laceCount; l++)
            {
                short laceSizeByte = 255;
                while (laceSizeByte == 255)
                {
                    laceSizeByte = (short)(data.ReadByte() & 0xFF);
                    headerSize += 1;
                    laceSizes[l] += laceSizeByte;
                }
                // Update the size of the last block
                laceSizes[laceCount] -= laceSizes[l];
            }
            // long ByteEndPos = source.getFilePointer();

            laceSizes[laceCount] -= headerSize;

            return laceSizes;
        }

        public MemoryStream GetFrame(int frame)
        {
            int startOffset = headerSize;
            int endOffset;
            if(sizes == null)
            {
                if (frame != 0)
                    throw new Exception("Tried to read laced frame on non-laced Block. MatroskaBlock.getFrame(frame > 0)");
                endOffset = headerSize + (int)(data.Length - data.Position);
            }
            else
            {
                for(int s = 0; s < frame; s++)
                {
                    startOffset += sizes[s];
                }
                endOffset = sizes[frame] + startOffset;
            }

            MemoryStream frameData = new MemoryStream(data.ToArray());
            frameData.Position = (long)startOffset;
            return frameData;
        }

        public ulong AdjustedBlockTimecode(ulong clusterTimecode, long timecodeScale)
        {
            return clusterTimecode + (ulong)BlockTimecode;
        }

        public void SetFrameData(short trackNumber, int timecode, byte[] data) { }
    }
}