using System;
using MkvTests.Ebml.Elements;
using System.IO;
using MkvTests.Ebml.DocTypes;
using MkvTests.Ebml;

namespace MkvTests.Matroska
{
    public class MatroskaFileMetaSeek
    {
        private static ulong BLOCK_RESEVER_SIZE;
        private ulong myPosition;
        private MasterElement seekHeadElem;
        private VoidElement placeHolderElement;
        private ulong referencePosition;

        static MatroskaFileMetaSeek()
        {
            BLOCK_RESEVER_SIZE = 256;
        }

        public MatroskaFileMetaSeek(ulong referencePosition)
        {
            this.referencePosition = referencePosition;
            myPosition = referencePosition;
            seekHeadElem = MatroskaDocTypes.SeekHead.GetInstance();
            placeHolderElement = new VoidElement(BLOCK_RESEVER_SIZE - seekHeadElem.TotalSize);
        }

        /// <summary>
        /// Writes this object into the data stream at the current position. Note that this method
        /// reverses some spacefor further additions in the stream,
        /// if the stream is seekable. Following subsequent additions to the object, the update() 
        /// method can be used to update the originally written object.
        /// </summary>
        /// <param name="writer">FileStream to write to.</param>
        /// <returns>Length of data written</returns>
        public ulong Write(FileStream writer)
        {
            ulong elementLength = seekHeadElem.WriteElement(writer);
            ulong voidLength = placeHolderElement.WriteElement(writer);
            if (elementLength + voidLength != BLOCK_RESEVER_SIZE)
                throw new Exception("ElementLength + VoidLength != Reserved size. Something went wrong");
            return BLOCK_RESEVER_SIZE;
        }

        /// <summary>
        /// Updates the representation of the object in the FileStream to account for added indexed elements
        /// </summary>
        /// <param name="writer">The Data Stream containing this object.</param>
        public void Update(FileStream writer)
        {
            if (!writer.CanSeek)
                throw new Exception("The file was not seekable.");
            long position = writer.Position;
            writer.Seek((long)myPosition, SeekOrigin.Begin);
            Write(writer);
            writer.Seek(position, SeekOrigin.Current);
            Utility.LogDebug("Updated metaseek section.");
        }

        /// <summary>
        /// Adds elements to the seek index. These should be level 1 objects only. 
        /// If this object has already been written, you must use the update() method
        /// for changes to take effect.
        /// </summary>
        /// <param name="element">The element itself.</param>
        /// <param name="filePostition">Position in teh data stream where the element has been written.</param>
        public void AddIndexedElement(MasterElement element, ulong filePostition)
        {
            Utility.LogDebug($"Adding indexed element {element.TypeInfo.Name} @ {filePostition - referencePosition}");
            AddIndexedElement(element.Type, filePostition);
        }

        /// <summary>
        /// Adds elements to the seek index. These should be level 1 objects only.
        /// If this object has already been written, you must use the update() method
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="filePosition"></param>
        public void AddIndexedElement(MemoryStream stream, ulong filePosition)
        {
            Utility.LogDebug($"Adding indexed element @ {filePosition - referencePosition}");
            MasterElement seekEntryElement = MatroskaDocTypes.Seek.GetInstance();
            BinaryElement seekEntryIdElement = MatroskaDocTypes.SeekID.GetInstance();
            seekEntryIdElement.Data = stream;

            UnsignedIntegerElement seekEntryPositionElement = MatroskaDocTypes.SeekPosition.GetInstance();
            seekEntryPositionElement.Value = filePosition - referencePosition;

            seekEntryElement.AddChildElement(seekEntryIdElement);
            seekEntryElement.AddChildElement(seekEntryPositionElement);

            seekHeadElem.AddChildElement(seekEntryElement);
            placeHolderElement.Size = BLOCK_RESEVER_SIZE - seekHeadElem.TotalSize;
        }
    }
}