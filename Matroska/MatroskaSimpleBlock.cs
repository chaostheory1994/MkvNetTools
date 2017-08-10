using MkvTests.Ebml;
using MkvTests.Ebml.DocTypes;
using MkvTests.Ebml.Elements;
using MkvTests.Ebml.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace MkvTests.Matroska
{
    public class MatroskaSimpleBlock
    {
        public static int MAX_LACE_SIZE;

        static MatroskaSimpleBlock()
        {
            MAX_LACE_SIZE = 6 * 0xFF;
        }

        public int TrackNumber { get; set; }
        public short Timecode { get; set; }
        public bool KeyFrame { get; set; }
        public MatroskaLaceMode LaceMode { get; set; }
        public bool Invisible { get; set; }
        public bool Discardable { get; set; }
        public List<MatroskaFileFrame> Frames { get; set; }
        public int TotalSize { get; set; }
        public ulong Duration { get; set; }

        public MatroskaSimpleBlock()
        {
            TrackNumber = 0;
            Timecode = 0;
            KeyFrame = true;
            LaceMode = MatroskaLaceMode.EBML;
            Invisible = false;
            Discardable = false;
            Frames = new List<MatroskaFileFrame>();
            TotalSize = 18;
            Duration = ulong.MaxValue;
        }

        public MatroskaSimpleBlock(ulong duration)
        {
            TrackNumber = 0;
            Timecode = 0;
            KeyFrame = true;
            LaceMode = MatroskaLaceMode.EBML;
            Invisible = false;
            Discardable = false;
            Frames = new List<MatroskaFileFrame>();
            TotalSize = 18;
            Duration = ulong.MaxValue;
        }

        public Element ToElement()
        {
            if (IsSimpleBlock())
                return ToSimpleBlock();
            return ToBlockGroup();
        }

        public Element ToSimpleBlock()
        {
            BinaryElement blockElement = MatroskaDocTypes.SimpleBlock.GetInstance();
            blockElement.Data = createInnerData();
            return blockElement;
        }

        public static MatroskaSimpleBlock FromElement(Element level3, FileStream writer, EbmlReader reader)
        {
            return new MatroskaSimpleBlock();
        }

        public Element ToBlockGroup()
        {
            MasterElement blockGroupElement = MatroskaDocTypes.BlockGroup.GetInstance();
            BinaryElement blockElement = MatroskaDocTypes.Block.GetInstance();
            blockElement.Data = createInnerData();
            UnsignedIntegerElement durationElement = MatroskaDocTypes.BlockDuration.GetInstance();
            durationElement.Value = Duration;
            blockGroupElement.AddChildElement(blockElement);
            blockGroupElement.AddChildElement(durationElement);
            return blockGroupElement;
        }

        private MemoryStream createInnerData()
        {
            MemoryStream stream = new MemoryStream(TotalSize);

            if (TrackNumber >= 0x4000)
                throw new Exception();

            if (TrackNumber < 0x80)
            {
                stream.WriteByte((byte)(TrackNumber | 0x80));
            }
            else
            {
                stream.WriteByte((byte)(TrackNumber >> 8 | 0x40));
                stream.WriteByte((byte)(TrackNumber & 0xFF));
            }

            byte[] writeMe = BitConverter.GetBytes(Timecode);
            stream.Write(writeMe, 0, writeMe.Length);
            BitArray bs = new BitArray(8);
            bs.Set(0, KeyFrame);
            bs.Set(4, Invisible);
            MemoryStream sizes = null;
            LaceMode = pickBestLaceMode();
            // TODO: correctly calculate resultant buffer size for different lace modes
            switch (LaceMode)
            {
                case MatroskaLaceMode.EBML:
                    bs.Set(5, true);
                    bs.Set(6, true);
                    sizes = ebmlEncodeLaceSizes();
                    break;
                case MatroskaLaceMode.XIPH:
                    bs.Set(6, true);
                    sizes = xiphEncodeLaceSizes();
                    break;
                case MatroskaLaceMode.FIXED:
                    sizes = fixedEncodeLaceSizes();
                    bs.Set(5, true);
                    break;
                case MatroskaLaceMode.NONE:
                    break;
                default:
                    break;
            }
            bs.Set(7, Discardable);
            byte writeByte = 0;
            for(int i = 0; i < bs.Length; i++)
            {
                writeByte += (byte)(bs.Get(7-i) ? 1 : 0);
                writeByte <<= 1;
            }
            stream.WriteByte(writeByte);
            if (sizes != null)
            {
                sizes.CopyTo(stream);
            }
            foreach(MatroskaFileFrame frame in Frames)
            {
                frame.Data.CopyTo(stream);
            }
            stream.Flip();
            return stream;
        }

        private MatroskaLaceMode pickBestLaceMode()
        {
            if (Frames.Count == 1)
                return MatroskaLaceMode.NONE;
            return LaceMode;
        }

        private MemoryStream fixedEncodeLaceSizes()
        {
            MemoryStream ret = new MemoryStream(1);
            ret.WriteByte((byte)(Frames.Count - 1));
            return ret;
        }

        private MemoryStream xiphEncodeLaceSizes()
        {
            MemoryStream stream = new MemoryStream(30);
            stream.WriteByte((byte)(Frames.Count - 1));
            for(int i = 0; i < Frames.Count - 1; ++i)
            {
                int tmpSize = (int)(Frames[i].Data.Length - Frames[i].Data.Position);
                while(tmpSize >= 0xFF)
                {
                    stream.WriteByte(0xFF);
                    tmpSize -= 0xFF;
                }
                stream.WriteByte((byte)tmpSize);
            }
            return stream;
        }

        private MemoryStream ebmlEncodeLaceSizes()
        {
            MemoryStream stream = new MemoryStream(30);
            stream.WriteByte((byte)(Frames.Count - 1));
            for(int i = 0; i < Frames.Count - 1; ++i)
            {
                int tempSize = (int)(Frames[i].Data.Length - Frames[i].Data.Position);
                byte[] writeMe = Utility.MakeEbmlCodedSize((ulong)tempSize);
                stream.Write(writeMe, 0, writeMe.Length);
            }
            return stream;
        }

        public bool IsSimpleBlock()
        {
            return Duration == long.MaxValue;
        }

        public bool AddFrame(MatroskaFileFrame frame)
        {
            Timecode = (short)frame.Timecode;
            TrackNumber = frame.TrackNumber;
            TotalSize += (int)(frame.Data.Length - frame.Data.Position);
            Frames.Add(frame);
            if (frame.Duration != ulong.MinValue)
                Duration = frame.Duration;

            if(frame.Data.Position - frame.Data.Length > MAX_LACE_SIZE)
            {
                LaceMode = MatroskaLaceMode.NONE;
                return false;
            }

            TotalSize += 4;

            return !IsSimpleBlock() || !(LaceMode == MatroskaLaceMode.NONE) || Frames.Count > 8;
        }
    }
}