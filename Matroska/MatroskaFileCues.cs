using MkvTests.Ebml;
using MkvTests.Ebml.DocTypes;
using MkvTests.Ebml.Elements;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MkvTests.Matroska
{
    public class MatroskaFileCues
    {
        private MasterElement cues;
        private ulong endOfEbmlHeaderBytePosition;


        public MatroskaFileCues(ulong endOfHeaderBytePosition)
        {
            this.endOfEbmlHeaderBytePosition = endOfHeaderBytePosition;
            cues = MatroskaDocTypes.Cues.GetInstance();
        }

        public void AddCue(ulong positionInFile, ulong timecodeOfCluster, List<int> clusterTrackNumber)
        {
            Utility.LogDebug($"Adding matoska cue to cues element at postion [{positionInFile}], using timecode [{timecodeOfCluster}], for track numbers [{clusterTrackNumber}]");
            UnsignedIntegerElement cueTime = MatroskaDocTypes.CueTime.GetInstance();
            cueTime.Value = timecodeOfCluster;
            MasterElement cuePoint = MatroskaDocTypes.CuePoint.GetInstance();
            MasterElement cueTrackPosition = createCueTrackPositions(positionInFile, clusterTrackNumber);

            cues.AddChildElement(cuePoint);
            cuePoint.AddChildElement(cueTime);
            cuePoint.AddChildElement(cueTrackPosition);

            Utility.LogDebug("Finished adding matroska cue to cues element.");
        }

        private MasterElement createCueTrackPositions(ulong positionInFile, List<int> trackNumbers)
        {
            MasterElement cueTrackPosition = MatroskaDocTypes.CueTrackPositions.GetInstance();

            foreach(int trackNumber in trackNumbers)
            {
                UnsignedIntegerElement cueTrack = MatroskaDocTypes.CueTrack.GetInstance();
                cueTrack.Value = (ulong)trackNumber;

                UnsignedIntegerElement cueClusterPosition = MatroskaDocTypes.CueClusterPosition.GetInstance();
                cueClusterPosition.Value = getPositionRelativeToSegmentEbmlElement(positionInFile);

                cueTrackPosition.AddChildElement(cueTrack);
                cueTrackPosition.AddChildElement(cueClusterPosition);
            }
            return cueTrackPosition;
        }

        public Element Write(FileStream writer, MatroskaFileMetaSeek metaSeek)
        {
            ulong currentbytePositionInFile = (ulong)writer.Position;
            Utility.LogDebug($"Writing matroska cues at file byte position [{currentbytePositionInFile}]");
            ulong numberOfBytesInCueData = cues.WriteElement(writer);
            Utility.LogDebug($"Done writing matroska cues, number of bytes was [{numberOfBytesInCueData}]");

            metaSeek.AddIndexedElement(cues, getPositionRelativeToSegmentEbmlElement(currentbytePositionInFile));

            return cues;
        }

        private ulong getPositionRelativeToSegmentEbmlElement(ulong currentBytePositionInFile)
        {
            return currentBytePositionInFile - endOfEbmlHeaderBytePosition;
        }
    }
}
