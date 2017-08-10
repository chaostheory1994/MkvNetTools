using System.Collections.Generic;
using System.Text;

namespace MkvTests.Matroska
{
    public class MatroskaFileTagEntry
    {
        public List<ulong> TrackUID { get; set; }
        public List<ulong> ChapterUID { get; set; }
        public List<ulong> AttachmentUID { get; set; }
        public List<MatroskaFileSimpleTag> SimpleTags { get; set; }

        public MatroskaFileTagEntry()
        {
            TrackUID = new List<ulong>();
            ChapterUID = new List<ulong>();
            AttachmentUID = new List<ulong>();
            SimpleTags = new List<MatroskaFileSimpleTag>();
        }

        public new string ToString()
        {
            var sb = new StringBuilder();

            if(TrackUID.Count > 0)
                sb.Append($"\t\tTrackUID: {TrackUID.ToString()}\n");
            if (ChapterUID.Count > 0)
                sb.Append($"\t\tChapterUID: {ChapterUID.ToString()}\n");
            if (AttachmentUID.Count > 0)
                sb.Append($"\t\tAttachmentUID: {AttachmentUID.ToString()}\n");

            for (int t = 0; t < SimpleTags.Count; t++)
                sb.Append(SimpleTags[t].ToString(2));

            return sb.ToString();
        }
    }
}