using MkvTests.Ebml.Elements;

namespace MkvTests.Ebml.DocTypes
{
    public static class MatroskaDocTypes
    {
        public static ProtoType<MasterElement> EBML { get; }
        public static ProtoType<UnsignedIntegerElement> EBMLVersion { get; }
        public static ProtoType<UnsignedIntegerElement> EBMLReadVersion { get; }
        public static ProtoType<UnsignedIntegerElement> EBMLMaxIDLength { get; }
        public static ProtoType<UnsignedIntegerElement> EBMLMaxSizeLength { get; }
        public static ProtoType<StringElement> DocType { get; }
        public static ProtoType<UnsignedIntegerElement> DocTypeVersion { get; }
        public static ProtoType<UnsignedIntegerElement> DocTypeReadVersion { get; }
        public static ProtoType<BinaryElement> Void { get; }
        public static ProtoType<BinaryElement> CRC_32 { get; }
        public static ProtoType<MasterElement> SignatureSlot { get; }
        public static ProtoType<UnsignedIntegerElement> SignatureAlgo { get; }
        public static ProtoType<UnsignedIntegerElement> SignatureHash { get; }
        public static ProtoType<BinaryElement> SignaturePublicKey { get; }
        public static ProtoType<BinaryElement> Signature { get; }
        public static ProtoType<MasterElement> SignatureElements { get; }
        public static ProtoType<MasterElement> SignatureElementList { get; }
        public static ProtoType<BinaryElement> SignedElement { get; }
        public static ProtoType<MasterElement> Segment { get; }
        public static ProtoType<MasterElement> SeekHead { get; }
        public static ProtoType<MasterElement> Seek { get; }
        public static ProtoType<BinaryElement> SeekID { get; }
        public static ProtoType<UnsignedIntegerElement> SeekPosition { get; }
        public static ProtoType<MasterElement> Info { get; }
        public static ProtoType<BinaryElement> SegmentUID { get; }
        public static ProtoType<UTF8StringElement> SegmentFilename { get; }
        public static ProtoType<BinaryElement> PrevUID { get; }
        public static ProtoType<UTF8StringElement> PrevFilename { get; }
        public static ProtoType<BinaryElement> NextUID { get; }
        public static ProtoType<UTF8StringElement> NextFilename { get; }
        public static ProtoType<BinaryElement> SegmentFamily { get; }
        public static ProtoType<MasterElement> ChapterTranslate { get; }
        public static ProtoType<UnsignedIntegerElement> ChapterTranslateEditionUID { get; }
        public static ProtoType<UnsignedIntegerElement> ChapterTranslateCodec { get; }
        public static ProtoType<BinaryElement> ChapterTranslateID { get; }
        public static ProtoType<UnsignedIntegerElement> TimecodeScale { get; }
        public static ProtoType<FloatElement> Duration { get; }
        public static ProtoType<DateElement> DateUTC { get; }
        public static ProtoType<UTF8StringElement> Title { get; }
        public static ProtoType<UTF8StringElement> MuxingApp { get; }
        public static ProtoType<UTF8StringElement> WritingApp { get; }
        public static ProtoType<MasterElement> Cluster { get; }
        public static ProtoType<UnsignedIntegerElement> Timecode { get; }
        public static ProtoType<MasterElement> SilentTracks { get; }
        public static ProtoType<UnsignedIntegerElement> SilentTrackNumber { get; }
        public static ProtoType<UnsignedIntegerElement> Position { get; }
        public static ProtoType<UnsignedIntegerElement> PrevSize { get; }
        public static ProtoType<BinaryElement> SimpleBlock { get; }
        public static ProtoType<MasterElement> BlockGroup { get; }
        public static ProtoType<BinaryElement> Block { get; }
        public static ProtoType<BinaryElement> BlockVirtual { get; }
        public static ProtoType<MasterElement> BlockAdditions { get; }
        public static ProtoType<MasterElement> BlockMore { get; }
        public static ProtoType<UnsignedIntegerElement> BlockAddID { get; }
        public static ProtoType<BinaryElement> BlockAdditional { get; }
        public static ProtoType<UnsignedIntegerElement> BlockDuration { get; }
        public static ProtoType<UnsignedIntegerElement> ReferencePriority { get; }
        public static ProtoType<SignedIntegerElement> ReferenceBlock { get; }
        public static ProtoType<SignedIntegerElement> ReferenceVirtual { get; }
        public static ProtoType<BinaryElement> CodecState { get; }
        public static ProtoType<SignedIntegerElement> DiscardPadding { get; }
        public static ProtoType<MasterElement> Slices { get; }
        public static ProtoType<MasterElement> TimeSlice { get; }
        public static ProtoType<UnsignedIntegerElement> LaceNumber { get; }
        public static ProtoType<UnsignedIntegerElement> FrameNumber { get; }
        public static ProtoType<UnsignedIntegerElement> BlockAdditionID { get; }
        public static ProtoType<UnsignedIntegerElement> Delay { get; }
        public static ProtoType<UnsignedIntegerElement> SliceDuration { get; }
        public static ProtoType<MasterElement> ReferenceFrame { get; }
        public static ProtoType<UnsignedIntegerElement> ReferenceOffset { get; }
        public static ProtoType<UnsignedIntegerElement> ReferenceTimeCode { get; }
        public static ProtoType<BinaryElement> EncryptedBlock { get; }
        public static ProtoType<MasterElement> Tracks { get; }
        public static ProtoType<MasterElement> TrackEntry { get; }
        public static ProtoType<UnsignedIntegerElement> TrackNumber { get; }
        public static ProtoType<UnsignedIntegerElement> TrackUID { get; }
        public static ProtoType<UnsignedIntegerElement> TrackType { get; }
        public static ProtoType<UnsignedIntegerElement> FlagEnabled { get; }
        public static ProtoType<UnsignedIntegerElement> FlagDefault { get; }
        public static ProtoType<UnsignedIntegerElement> FlagForced { get; }
        public static ProtoType<UnsignedIntegerElement> FlagLacing { get; }
        public static ProtoType<UnsignedIntegerElement> MinCache { get; }
        public static ProtoType<UnsignedIntegerElement> MaxCache { get; }
        public static ProtoType<UnsignedIntegerElement> DefaultDuration { get; }
        public static ProtoType<UnsignedIntegerElement> DefaultDecodedFieldDuration { get; }
        public static ProtoType<FloatElement> TrackTimecodeScale { get; }
        public static ProtoType<SignedIntegerElement> TrackOffset { get; }
        public static ProtoType<UnsignedIntegerElement> MaxBlockAdditionID { get; }
        public static ProtoType<UTF8StringElement> Name { get; }
        public static ProtoType<StringElement> Language { get; }
        public static ProtoType<StringElement> CodecID { get; }
        public static ProtoType<BinaryElement> CodecPrivate { get; }
        public static ProtoType<UTF8StringElement> CodecName { get; }
        public static ProtoType<UnsignedIntegerElement> AttachmentLink { get; }
        public static ProtoType<UTF8StringElement> CodecSettings { get; }
        public static ProtoType<StringElement> CodecInfoURL { get; }
        public static ProtoType<StringElement> CodecDownloadURL { get; }
        public static ProtoType<UnsignedIntegerElement> CodecDecodeAll { get; }
        public static ProtoType<UnsignedIntegerElement> TrackOverlay { get; }
        public static ProtoType<UnsignedIntegerElement> CodecDelay { get; }
        public static ProtoType<UnsignedIntegerElement> SeekPreRoll { get; }
        public static ProtoType<MasterElement> TrackTranslate { get; }
        public static ProtoType<UnsignedIntegerElement> TrackTranslateEditionUID { get; }
        public static ProtoType<UnsignedIntegerElement> TrackTranslateCodec { get; }
        public static ProtoType<BinaryElement> TrackTranslateTrackID { get; }
        public static ProtoType<MasterElement> Video { get; }
        public static ProtoType<UnsignedIntegerElement> FlagInterlaced { get; }
        public static ProtoType<UnsignedIntegerElement> StereoMode { get; }
        public static ProtoType<UnsignedIntegerElement> AlphaMode { get; }
        public static ProtoType<UnsignedIntegerElement> OldStereoMode { get; }
        public static ProtoType<UnsignedIntegerElement> PixelWidth { get; }
        public static ProtoType<UnsignedIntegerElement> PixelHeight { get; }
        public static ProtoType<UnsignedIntegerElement> PixelCropBottom { get; }
        public static ProtoType<UnsignedIntegerElement> PixelCropTop { get; }
        public static ProtoType<UnsignedIntegerElement> PixelCropLeft { get; }
        public static ProtoType<UnsignedIntegerElement> PixelCropRight { get; }
        public static ProtoType<UnsignedIntegerElement> DisplayWidth { get; }
        public static ProtoType<UnsignedIntegerElement> DisplayHeight { get; }
        public static ProtoType<UnsignedIntegerElement> DisplayUnit { get; }
        public static ProtoType<UnsignedIntegerElement> AspectRatioType { get; }
        public static ProtoType<BinaryElement> ColourSpace { get; }
        public static ProtoType<FloatElement> GammaValue { get; }
        public static ProtoType<FloatElement> FrameRate { get; }
        public static ProtoType<MasterElement> Audio { get; }
        public static ProtoType<FloatElement> SamplingFrequency { get; }
        public static ProtoType<FloatElement> OutputSamplingFrequency { get; }
        public static ProtoType<UnsignedIntegerElement> Channels { get; }
        public static ProtoType<BinaryElement> ChannelPositions { get; }
        public static ProtoType<UnsignedIntegerElement> BitDepth { get; }
        public static ProtoType<MasterElement> TrackOperation { get; }
        public static ProtoType<MasterElement> TrackCombinePlanes { get; }
        public static ProtoType<MasterElement> TrackPlane { get; }
        public static ProtoType<UnsignedIntegerElement> TrackPlaneUID { get; }
        public static ProtoType<UnsignedIntegerElement> TrackPlaneType { get; }
        public static ProtoType<MasterElement> TrackJoinBlocks { get; }
        public static ProtoType<UnsignedIntegerElement> TrackJoinUID { get; }
        public static ProtoType<UnsignedIntegerElement> TrickTrackUID { get; }
        public static ProtoType<BinaryElement> TrickTrackSegmentUID { get; }
        public static ProtoType<UnsignedIntegerElement> TrickTrackFlag { get; }
        public static ProtoType<UnsignedIntegerElement> TrickMasterTrackUID { get; }
        public static ProtoType<BinaryElement> TrickMasterTrackSegmentUID { get; }
        public static ProtoType<MasterElement> ContentEncodings { get; }
        public static ProtoType<MasterElement> ContentEncoding { get; }
        public static ProtoType<UnsignedIntegerElement> ContentEncodingOrder { get; }
        public static ProtoType<UnsignedIntegerElement> ContentEncodingScope { get; }
        public static ProtoType<UnsignedIntegerElement> ContentEncodingType { get; }
        public static ProtoType<MasterElement> ContentCompression { get; }
        public static ProtoType<UnsignedIntegerElement> ContentCompAlgo { get; }
        public static ProtoType<BinaryElement> ContentCompSettings { get; }
        public static ProtoType<MasterElement> ContentEncryption { get; }
        public static ProtoType<UnsignedIntegerElement> ContentEncAlgo { get; }
        public static ProtoType<BinaryElement> ContentEncKeyID { get; }
        public static ProtoType<BinaryElement> ContentSignature { get; }
        public static ProtoType<BinaryElement> ContentSigKeyID { get; }
        public static ProtoType<UnsignedIntegerElement> ContentSigAlgo { get; }
        public static ProtoType<UnsignedIntegerElement> ContentSigHashAlgo { get; }
        public static ProtoType<MasterElement> Cues { get; }
        public static ProtoType<MasterElement> CuePoint { get; }
        public static ProtoType<UnsignedIntegerElement> CueTime { get; }
        public static ProtoType<MasterElement> CueTrackPositions { get; }
        public static ProtoType<UnsignedIntegerElement> CueTrack { get; }
        public static ProtoType<UnsignedIntegerElement> CueClusterPosition { get; }
        public static ProtoType<UnsignedIntegerElement> CueRelativePosition { get; }
        public static ProtoType<UnsignedIntegerElement> CueDuration { get; }
        public static ProtoType<UnsignedIntegerElement> CueBlockNumber { get; }
        public static ProtoType<UnsignedIntegerElement> CueCodecState { get; }
        public static ProtoType<MasterElement> CueReference { get; }
        public static ProtoType<UnsignedIntegerElement> CueRefTime { get; }
        public static ProtoType<UnsignedIntegerElement> CueRefCluster { get; }
        public static ProtoType<UnsignedIntegerElement> CueRefNumber { get; }
        public static ProtoType<UnsignedIntegerElement> CueRefCodecState { get; }
        public static ProtoType<MasterElement> Attachments { get; }
        public static ProtoType<MasterElement> AttachedFile { get; }
        public static ProtoType<UTF8StringElement> FileDescription { get; }
        public static ProtoType<UTF8StringElement> FileName { get; }
        public static ProtoType<StringElement> FileMimeType { get; }
        public static ProtoType<BinaryElement> FileData { get; }
        public static ProtoType<UnsignedIntegerElement> FileUID { get; }
        public static ProtoType<BinaryElement> FileReferral { get; }
        public static ProtoType<UnsignedIntegerElement> FileUsedStartTime { get; }
        public static ProtoType<UnsignedIntegerElement> FileUsedEndTime { get; }
        public static ProtoType<MasterElement> Chapters { get; }
        public static ProtoType<MasterElement> EditionEntry { get; }
        public static ProtoType<UnsignedIntegerElement> EditionUID { get; }
        public static ProtoType<UnsignedIntegerElement> EditionFlagHidden { get; }
        public static ProtoType<UnsignedIntegerElement> EditionFlagDefault { get; }
        public static ProtoType<UnsignedIntegerElement> EditionFlagOrdered { get; }
        public static ProtoType<MasterElement> ChapterAtom { get; }
        public static ProtoType<UnsignedIntegerElement> ChapterUID { get; }
        public static ProtoType<UTF8StringElement> ChapterStringUID { get; }
        public static ProtoType<UnsignedIntegerElement> ChapterTimeStart { get; }
        public static ProtoType<UnsignedIntegerElement> ChapterTimeEnd { get; }
        public static ProtoType<UnsignedIntegerElement> ChapterFlagHidden { get; }
        public static ProtoType<UnsignedIntegerElement> ChapterFlagEnabled { get; }
        public static ProtoType<BinaryElement> ChapterSegmentUID { get; }
        public static ProtoType<UnsignedIntegerElement> ChapterSegmentEditionUID { get; }
        public static ProtoType<UnsignedIntegerElement> ChapterPhysicalEquiv { get; }
        public static ProtoType<MasterElement> ChapterTrack { get; }
        public static ProtoType<UnsignedIntegerElement> ChapterTrackNumber { get; }
        public static ProtoType<MasterElement> ChapterDisplay { get; }
        public static ProtoType<UTF8StringElement> ChapString { get; }
        public static ProtoType<StringElement> ChapLanguage { get; }
        public static ProtoType<StringElement> ChapCountry { get; }
        public static ProtoType<MasterElement> ChapProcess { get; }
        public static ProtoType<UnsignedIntegerElement> ChapProcessCodecID { get; }
        public static ProtoType<BinaryElement> ChapProcessPrivate { get; }
        public static ProtoType<MasterElement> ChapProcessCommand { get; }
        public static ProtoType<UnsignedIntegerElement> ChapProcessTime { get; }
        public static ProtoType<BinaryElement> ChapProcessData { get; }
        public static ProtoType<MasterElement> Tags { get; }
        public static ProtoType<MasterElement> Tag { get; }
        public static ProtoType<MasterElement> Targets { get; }
        public static ProtoType<UnsignedIntegerElement> TargetTypeValue { get; }
        public static ProtoType<StringElement> TargetType { get; }
        public static ProtoType<UnsignedIntegerElement> TagTrackUID { get; }
        public static ProtoType<UnsignedIntegerElement> TagEditionUID { get; }
        public static ProtoType<UnsignedIntegerElement> TagChapterUID { get; }
        public static ProtoType<UnsignedIntegerElement> TagAttachmentUID { get; }
        public static ProtoType<MasterElement> SimpleTag { get; }
        public static ProtoType<UTF8StringElement> TagName { get; }
        public static ProtoType<StringElement> TagLanguage { get; }
        public static ProtoType<UnsignedIntegerElement> TagDefault { get; }
        public static ProtoType<UTF8StringElement> TagString { get; }
        public static ProtoType<BinaryElement> TagBinary { get; }
        static MatroskaDocTypes()
        {
            EBML = new ProtoType<MasterElement>("EBML", new byte[] { (byte)0x1A, (byte)0x45, (byte)0xDF, (byte)0xA3 }, 0);
            EBMLVersion = new ProtoType<UnsignedIntegerElement>("EBMLVersion", new byte[] { (byte)0x42, (byte)0x86 }, 1);
            EBMLReadVersion = new ProtoType<UnsignedIntegerElement>("EBMLReadVersion", new byte[] { (byte)0x42, (byte)0xF7 }, 1);
            EBMLMaxIDLength = new ProtoType<UnsignedIntegerElement>("EBMLMaxIDLength", new byte[] { (byte)0x42, (byte)0xF2 }, 1);
            EBMLMaxSizeLength = new ProtoType<UnsignedIntegerElement>("EBMLMaxSizeLength", new byte[] { (byte)0x42, (byte)0xF3 }, 1);
            DocType = new ProtoType<StringElement>("DocType", new byte[] { (byte)0x42, (byte)0x82 }, 1);
            DocTypeVersion = new ProtoType<UnsignedIntegerElement>("DocTypeVersion", new byte[] { (byte)0x42, (byte)0x87 }, 1);
            DocTypeReadVersion = new ProtoType<UnsignedIntegerElement>("DocTypeReadVersion", new byte[] { (byte)0x42, (byte)0x85 }, 1);
            Void = new ProtoType<BinaryElement>("Void", new byte[] { (byte)0xEC }, -1);
            CRC_32 = new ProtoType<BinaryElement>("CRC_32", new byte[] { (byte)0xBF }, -1);
            SignatureSlot = new ProtoType<MasterElement>("SignatureSlot", new byte[] { (byte)0x1B, (byte)0x53, (byte)0x86, (byte)0x67 }, -1);
            SignatureAlgo = new ProtoType<UnsignedIntegerElement>("SignatureAlgo", new byte[] { (byte)0x7E, (byte)0x8A }, 1);
            SignatureHash = new ProtoType<UnsignedIntegerElement>("SignatureHash", new byte[] { (byte)0x7E, (byte)0x9A }, 1);
            SignaturePublicKey = new ProtoType<BinaryElement>("SignaturePublicKey", new byte[] { (byte)0x7E, (byte)0xA5 }, 1);
            Signature = new ProtoType<BinaryElement>("Signature", new byte[] { (byte)0x7E, (byte)0xB5 }, 1);
            SignatureElements = new ProtoType<MasterElement>("SignatureElements", new byte[] { (byte)0x7E, (byte)0x5B }, 1);
            SignatureElementList = new ProtoType<MasterElement>("SignatureElementList", new byte[] { (byte)0x7E, (byte)0x7B }, 2);
            SignedElement = new ProtoType<BinaryElement>("SignedElement", new byte[] { (byte)0x65, (byte)0x32 }, 3);
            Segment = new ProtoType<MasterElement>("Segment", new byte[] { (byte)0x18, (byte)0x53, (byte)0x80, (byte)0x67 }, 0);
            SeekHead = new ProtoType<MasterElement>("SeekHead", new byte[] { (byte)0x11, (byte)0x4D, (byte)0x9B, (byte)0x74 }, 1);
            Seek = new ProtoType<MasterElement>("Seek", new byte[] { (byte)0x4D, (byte)0xBB }, 2);
            SeekID = new ProtoType<BinaryElement>("SeekID", new byte[] { (byte)0x53, (byte)0xAB }, 3);
            SeekPosition = new ProtoType<UnsignedIntegerElement>("SeekPosition", new byte[] { (byte)0x53, (byte)0xAC }, 3);
            Info = new ProtoType<MasterElement>("Info", new byte[] { (byte)0x15, (byte)0x49, (byte)0xA9, (byte)0x66 }, 1);
            SegmentUID = new ProtoType<BinaryElement>("SegmentUID", new byte[] { (byte)0x73, (byte)0xA4 }, 2);
            SegmentFilename = new ProtoType<UTF8StringElement>("SegmentFilename", new byte[] { (byte)0x73, (byte)0x84 }, 2);
            PrevUID = new ProtoType<BinaryElement>("PrevUID", new byte[] { (byte)0x3C, (byte)0xB9, (byte)0x23 }, 2);
            PrevFilename = new ProtoType<UTF8StringElement>("PrevFilename", new byte[] { (byte)0x3C, (byte)0x83, (byte)0xAB }, 2);
            NextUID = new ProtoType<BinaryElement>("NextUID", new byte[] { (byte)0x3E, (byte)0xB9, (byte)0x23 }, 2);
            NextFilename = new ProtoType<UTF8StringElement>("NextFilename", new byte[] { (byte)0x3E, (byte)0x83, (byte)0xBB }, 2);
            SegmentFamily = new ProtoType<BinaryElement>("SegmentFamily", new byte[] { (byte)0x44, (byte)0x44 }, 2);
            ChapterTranslate = new ProtoType<MasterElement>("ChapterTranslate", new byte[] { (byte)0x69, (byte)0x24 }, 2);
            ChapterTranslateEditionUID = new ProtoType<UnsignedIntegerElement>("ChapterTranslateEditionUID", new byte[] { (byte)0x69, (byte)0xFC }, 3);
            ChapterTranslateCodec = new ProtoType<UnsignedIntegerElement>("ChapterTranslateCodec", new byte[] { (byte)0x69, (byte)0xBF }, 3);
            ChapterTranslateID = new ProtoType<BinaryElement>("ChapterTranslateID", new byte[] { (byte)0x69, (byte)0xA5 }, 3);
            TimecodeScale = new ProtoType<UnsignedIntegerElement>("TimecodeScale", new byte[] { (byte)0x2A, (byte)0xD7, (byte)0xB1 }, 2);
            Duration = new ProtoType<FloatElement>("Duration", new byte[] { (byte)0x44, (byte)0x89 }, 2);
            DateUTC = new ProtoType<DateElement>("DateUTC", new byte[] { (byte)0x44, (byte)0x61 }, 2);
            Title = new ProtoType<UTF8StringElement>("Title", new byte[] { (byte)0x7B, (byte)0xA9 }, 2);
            MuxingApp = new ProtoType<UTF8StringElement>("MuxingApp", new byte[] { (byte)0x4D, (byte)0x80 }, 2);
            WritingApp = new ProtoType<UTF8StringElement>("WritingApp", new byte[] { (byte)0x57, (byte)0x41 }, 2);
            Cluster = new ProtoType<MasterElement>("Cluster", new byte[] { (byte)0x1F, (byte)0x43, (byte)0xB6, (byte)0x75 }, 1);
            Timecode = new ProtoType<UnsignedIntegerElement>("Timecode", new byte[] { (byte)0xE7 }, 2);
            SilentTracks = new ProtoType<MasterElement>("SilentTracks", new byte[] { (byte)0x58, (byte)0x54 }, 2);
            SilentTrackNumber = new ProtoType<UnsignedIntegerElement>("SilentTrackNumber", new byte[] { (byte)0x58, (byte)0xD7 }, 3);
            Position = new ProtoType<UnsignedIntegerElement>("Position", new byte[] { (byte)0xA7 }, 2);
            PrevSize = new ProtoType<UnsignedIntegerElement>("PrevSize", new byte[] { (byte)0xAB }, 2);
            SimpleBlock = new ProtoType<BinaryElement>("SimpleBlock", new byte[] { (byte)0xA3 }, 2);
            BlockGroup = new ProtoType<MasterElement>("BlockGroup", new byte[] { (byte)0xA0 }, 2);
            Block = new ProtoType<BinaryElement>("Block", new byte[] { (byte)0xA1 }, 3);
            BlockVirtual = new ProtoType<BinaryElement>("BlockVirtual", new byte[] { (byte)0xA2 }, 3);
            BlockAdditions = new ProtoType<MasterElement>("BlockAdditions", new byte[] { (byte)0x75, (byte)0xA1 }, 3);
            BlockMore = new ProtoType<MasterElement>("BlockMore", new byte[] { (byte)0xA6 }, 4);
            BlockAddID = new ProtoType<UnsignedIntegerElement>("BlockAddID", new byte[] { (byte)0xEE }, 5);
            BlockAdditional = new ProtoType<BinaryElement>("BlockAdditional", new byte[] { (byte)0xA5 }, 5);
            BlockDuration = new ProtoType<UnsignedIntegerElement>("BlockDuration", new byte[] { (byte)0x9B }, 3);
            ReferencePriority = new ProtoType<UnsignedIntegerElement>("ReferencePriority", new byte[] { (byte)0xFA }, 3);
            ReferenceBlock = new ProtoType<SignedIntegerElement>("ReferenceBlock", new byte[] { (byte)0xFB }, 3);
            ReferenceVirtual = new ProtoType<SignedIntegerElement>("ReferenceVirtual", new byte[] { (byte)0xFD }, 3);
            CodecState = new ProtoType<BinaryElement>("CodecState", new byte[] { (byte)0xA4 }, 3);
            DiscardPadding = new ProtoType<SignedIntegerElement>("DiscardPadding", new byte[] { (byte)0x75, (byte)0xA2 }, 3);
            Slices = new ProtoType<MasterElement>("Slices", new byte[] { (byte)0x8E }, 3);
            TimeSlice = new ProtoType<MasterElement>("TimeSlice", new byte[] { (byte)0xE8 }, 4);
            LaceNumber = new ProtoType<UnsignedIntegerElement>("LaceNumber", new byte[] { (byte)0xCC }, 5);
            FrameNumber = new ProtoType<UnsignedIntegerElement>("FrameNumber", new byte[] { (byte)0xCD }, 5);
            BlockAdditionID = new ProtoType<UnsignedIntegerElement>("BlockAdditionID", new byte[] { (byte)0xCB }, 5);
            Delay = new ProtoType<UnsignedIntegerElement>("Delay", new byte[] { (byte)0xCE }, 5);
            SliceDuration = new ProtoType<UnsignedIntegerElement>("SliceDuration", new byte[] { (byte)0xCF }, 5);
            ReferenceFrame = new ProtoType<MasterElement>("ReferenceFrame", new byte[] { (byte)0xC8 }, 3);
            ReferenceOffset = new ProtoType<UnsignedIntegerElement>("ReferenceOffset", new byte[] { (byte)0xC9 }, 4);
            ReferenceTimeCode = new ProtoType<UnsignedIntegerElement>("ReferenceTimeCode", new byte[] { (byte)0xCA }, 4);
            EncryptedBlock = new ProtoType<BinaryElement>("EncryptedBlock", new byte[] { (byte)0xAF }, 2);
            Tracks = new ProtoType<MasterElement>("Tracks", new byte[] { (byte)0x16, (byte)0x54, (byte)0xAE, (byte)0x6B }, 1);
            TrackEntry = new ProtoType<MasterElement>("TrackEntry", new byte[] { (byte)0xAE }, 2);
            TrackNumber = new ProtoType<UnsignedIntegerElement>("TrackNumber", new byte[] { (byte)0xD7 }, 3);
            TrackUID = new ProtoType<UnsignedIntegerElement>("TrackUID", new byte[] { (byte)0x73, (byte)0xC5 }, 3);
            TrackType = new ProtoType<UnsignedIntegerElement>("TrackType", new byte[] { (byte)0x83 }, 3);
            FlagEnabled = new ProtoType<UnsignedIntegerElement>("FlagEnabled", new byte[] { (byte)0xB9 }, 3);
            FlagDefault = new ProtoType<UnsignedIntegerElement>("FlagDefault", new byte[] { (byte)0x88 }, 3);
            FlagForced = new ProtoType<UnsignedIntegerElement>("FlagForced", new byte[] { (byte)0x55, (byte)0xAA }, 3);
            FlagLacing = new ProtoType<UnsignedIntegerElement>("FlagLacing", new byte[] { (byte)0x9C }, 3);
            MinCache = new ProtoType<UnsignedIntegerElement>("MinCache", new byte[] { (byte)0x6D, (byte)0xE7 }, 3);
            MaxCache = new ProtoType<UnsignedIntegerElement>("MaxCache", new byte[] { (byte)0x6D, (byte)0xF8 }, 3);
            DefaultDuration = new ProtoType<UnsignedIntegerElement>("DefaultDuration", new byte[] { (byte)0x23, (byte)0xE3, (byte)0x83 }, 3);
            DefaultDecodedFieldDuration = new ProtoType<UnsignedIntegerElement>("DefaultDecodedFieldDuration", new byte[] { (byte)0x23, (byte)0x4E, (byte)0x7A }, 3);
            TrackTimecodeScale = new ProtoType<FloatElement>("TrackTimecodeScale", new byte[] { (byte)0x23, (byte)0x31, (byte)0x4F }, 3);
            TrackOffset = new ProtoType<SignedIntegerElement>("TrackOffset", new byte[] { (byte)0x53, (byte)0x7F }, 3);
            MaxBlockAdditionID = new ProtoType<UnsignedIntegerElement>("MaxBlockAdditionID", new byte[] { (byte)0x55, (byte)0xEE }, 3);
            Name = new ProtoType<UTF8StringElement>("Name", new byte[] { (byte)0x53, (byte)0x6E }, 3);
            Language = new ProtoType<StringElement>("Language", new byte[] { (byte)0x22, (byte)0xB5, (byte)0x9C }, 3);
            CodecID = new ProtoType<StringElement>("CodecID", new byte[] { (byte)0x86 }, 3);
            CodecPrivate = new ProtoType<BinaryElement>("CodecPrivate", new byte[] { (byte)0x63, (byte)0xA2 }, 3);
            CodecName = new ProtoType<UTF8StringElement>("CodecName", new byte[] { (byte)0x25, (byte)0x86, (byte)0x88 }, 3);
            AttachmentLink = new ProtoType<UnsignedIntegerElement>("AttachmentLink", new byte[] { (byte)0x74, (byte)0x46 }, 3);
            CodecSettings = new ProtoType<UTF8StringElement>("CodecSettings", new byte[] { (byte)0x3A, (byte)0x96, (byte)0x97 }, 3);
            CodecInfoURL = new ProtoType<StringElement>("CodecInfoURL", new byte[] { (byte)0x3B, (byte)0x40, (byte)0x40 }, 3);
            CodecDownloadURL = new ProtoType<StringElement>("CodecDownloadURL", new byte[] { (byte)0x26, (byte)0xB2, (byte)0x40 }, 3);
            CodecDecodeAll = new ProtoType<UnsignedIntegerElement>("CodecDecodeAll", new byte[] { (byte)0xAA }, 3);
            TrackOverlay = new ProtoType<UnsignedIntegerElement>("TrackOverlay", new byte[] { (byte)0x6F, (byte)0xAB }, 3);
            CodecDelay = new ProtoType<UnsignedIntegerElement>("CodecDelay", new byte[] { (byte)0x56, (byte)0xAA }, 3);
            SeekPreRoll = new ProtoType<UnsignedIntegerElement>("SeekPreRoll", new byte[] { (byte)0x56, (byte)0xBB }, 3);
            TrackTranslate = new ProtoType<MasterElement>("TrackTranslate", new byte[] { (byte)0x66, (byte)0x24 }, 3);
            TrackTranslateEditionUID = new ProtoType<UnsignedIntegerElement>("TrackTranslateEditionUID", new byte[] { (byte)0x66, (byte)0xFC }, 4);
            TrackTranslateCodec = new ProtoType<UnsignedIntegerElement>("TrackTranslateCodec", new byte[] { (byte)0x66, (byte)0xBF }, 4);
            TrackTranslateTrackID = new ProtoType<BinaryElement>("TrackTranslateTrackID", new byte[] { (byte)0x66, (byte)0xA5 }, 4);
            Video = new ProtoType<MasterElement>("Video", new byte[] { (byte)0xE0 }, 3);
            FlagInterlaced = new ProtoType<UnsignedIntegerElement>("FlagInterlaced", new byte[] { (byte)0x9A }, 4);
            StereoMode = new ProtoType<UnsignedIntegerElement>("StereoMode", new byte[] { (byte)0x53, (byte)0xB8 }, 4);
            AlphaMode = new ProtoType<UnsignedIntegerElement>("AlphaMode", new byte[] { (byte)0x53, (byte)0xC0 }, 4);
            OldStereoMode = new ProtoType<UnsignedIntegerElement>("OldStereoMode", new byte[] { (byte)0x53, (byte)0xB9 }, 4);
            PixelWidth = new ProtoType<UnsignedIntegerElement>("PixelWidth", new byte[] { (byte)0xB0 }, 4);
            PixelHeight = new ProtoType<UnsignedIntegerElement>("PixelHeight", new byte[] { (byte)0xBA }, 4);
            PixelCropBottom = new ProtoType<UnsignedIntegerElement>("PixelCropBottom", new byte[] { (byte)0x54, (byte)0xAA }, 4);
            PixelCropTop = new ProtoType<UnsignedIntegerElement>("PixelCropTop", new byte[] { (byte)0x54, (byte)0xBB }, 4);
            PixelCropLeft = new ProtoType<UnsignedIntegerElement>("PixelCropLeft", new byte[] { (byte)0x54, (byte)0xCC }, 4);
            PixelCropRight = new ProtoType<UnsignedIntegerElement>("PixelCropRight", new byte[] { (byte)0x54, (byte)0xDD }, 4);
            DisplayWidth = new ProtoType<UnsignedIntegerElement>("DisplayWidth", new byte[] { (byte)0x54, (byte)0xB0 }, 4);
            DisplayHeight = new ProtoType<UnsignedIntegerElement>("DisplayHeight", new byte[] { (byte)0x54, (byte)0xBA }, 4);
            DisplayUnit = new ProtoType<UnsignedIntegerElement>("DisplayUnit", new byte[] { (byte)0x54, (byte)0xB2 }, 4);
            AspectRatioType = new ProtoType<UnsignedIntegerElement>("AspectRatioType", new byte[] { (byte)0x54, (byte)0xB3 }, 4);
            ColourSpace = new ProtoType<BinaryElement>("ColourSpace", new byte[] { (byte)0x2E, (byte)0xB5, (byte)0x24 }, 4);
            GammaValue = new ProtoType<FloatElement>("GammaValue", new byte[] { (byte)0x2F, (byte)0xB5, (byte)0x23 }, 4);
            FrameRate = new ProtoType<FloatElement>("FrameRate", new byte[] { (byte)0x23, (byte)0x83, (byte)0xE3 }, 4);
            Audio = new ProtoType<MasterElement>("Audio", new byte[] { (byte)0xE1 }, 3);
            SamplingFrequency = new ProtoType<FloatElement>("SamplingFrequency", new byte[] { (byte)0xB5 }, 4);
            OutputSamplingFrequency = new ProtoType<FloatElement>("OutputSamplingFrequency", new byte[] { (byte)0x78, (byte)0xB5 }, 4);
            Channels = new ProtoType<UnsignedIntegerElement>("Channels", new byte[] { (byte)0x9F }, 4);
            ChannelPositions = new ProtoType<BinaryElement>("ChannelPositions", new byte[] { (byte)0x7D, (byte)0x7B }, 4);
            BitDepth = new ProtoType<UnsignedIntegerElement>("BitDepth", new byte[] { (byte)0x62, (byte)0x64 }, 4);
            TrackOperation = new ProtoType<MasterElement>("TrackOperation", new byte[] { (byte)0xE2 }, 3);
            TrackCombinePlanes = new ProtoType<MasterElement>("TrackCombinePlanes", new byte[] { (byte)0xE3 }, 4);
            TrackPlane = new ProtoType<MasterElement>("TrackPlane", new byte[] { (byte)0xE4 }, 5);
            TrackPlaneUID = new ProtoType<UnsignedIntegerElement>("TrackPlaneUID", new byte[] { (byte)0xE5 }, 6);
            TrackPlaneType = new ProtoType<UnsignedIntegerElement>("TrackPlaneType", new byte[] { (byte)0xE6 }, 6);
            TrackJoinBlocks = new ProtoType<MasterElement>("TrackJoinBlocks", new byte[] { (byte)0xE9 }, 4);
            TrackJoinUID = new ProtoType<UnsignedIntegerElement>("TrackJoinUID", new byte[] { (byte)0xED }, 5);
            TrickTrackUID = new ProtoType<UnsignedIntegerElement>("TrickTrackUID", new byte[] { (byte)0xC0 }, 3);
            TrickTrackSegmentUID = new ProtoType<BinaryElement>("TrickTrackSegmentUID", new byte[] { (byte)0xC1 }, 3);
            TrickTrackFlag = new ProtoType<UnsignedIntegerElement>("TrickTrackFlag", new byte[] { (byte)0xC6 }, 3);
            TrickMasterTrackUID = new ProtoType<UnsignedIntegerElement>("TrickMasterTrackUID", new byte[] { (byte)0xC7 }, 3);
            TrickMasterTrackSegmentUID = new ProtoType<BinaryElement>("TrickMasterTrackSegmentUID", new byte[] { (byte)0xC4 }, 3);
            ContentEncodings = new ProtoType<MasterElement>("ContentEncodings", new byte[] { (byte)0x6D, (byte)0x80 }, 3);
            ContentEncoding = new ProtoType<MasterElement>("ContentEncoding", new byte[] { (byte)0x62, (byte)0x40 }, 4);
            ContentEncodingOrder = new ProtoType<UnsignedIntegerElement>("ContentEncodingOrder", new byte[] { (byte)0x50, (byte)0x31 }, 5);
            ContentEncodingScope = new ProtoType<UnsignedIntegerElement>("ContentEncodingScope", new byte[] { (byte)0x50, (byte)0x32 }, 5);
            ContentEncodingType = new ProtoType<UnsignedIntegerElement>("ContentEncodingType", new byte[] { (byte)0x50, (byte)0x33 }, 5);
            ContentCompression = new ProtoType<MasterElement>("ContentCompression", new byte[] { (byte)0x50, (byte)0x34 }, 5);
            ContentCompAlgo = new ProtoType<UnsignedIntegerElement>("ContentCompAlgo", new byte[] { (byte)0x42, (byte)0x54 }, 6);
            ContentCompSettings = new ProtoType<BinaryElement>("ContentCompSettings", new byte[] { (byte)0x42, (byte)0x55 }, 6);
            ContentEncryption = new ProtoType<MasterElement>("ContentEncryption", new byte[] { (byte)0x50, (byte)0x35 }, 5);
            ContentEncAlgo = new ProtoType<UnsignedIntegerElement>("ContentEncAlgo", new byte[] { (byte)0x47, (byte)0xE1 }, 6);
            ContentEncKeyID = new ProtoType<BinaryElement>("ContentEncKeyID", new byte[] { (byte)0x47, (byte)0xE2 }, 6);
            ContentSignature = new ProtoType<BinaryElement>("ContentSignature", new byte[] { (byte)0x47, (byte)0xE3 }, 6);
            ContentSigKeyID = new ProtoType<BinaryElement>("ContentSigKeyID", new byte[] { (byte)0x47, (byte)0xE4 }, 6);
            ContentSigAlgo = new ProtoType<UnsignedIntegerElement>("ContentSigAlgo", new byte[] { (byte)0x47, (byte)0xE5 }, 6);
            ContentSigHashAlgo = new ProtoType<UnsignedIntegerElement>("ContentSigHashAlgo", new byte[] { (byte)0x47, (byte)0xE6 }, 6);
            Cues = new ProtoType<MasterElement>("Cues", new byte[] { (byte)0x1C, (byte)0x53, (byte)0xBB, (byte)0x6B }, 1);
            CuePoint = new ProtoType<MasterElement>("CuePoint", new byte[] { (byte)0xBB }, 2);
            CueTime = new ProtoType<UnsignedIntegerElement>("CueTime", new byte[] { (byte)0xB3 }, 3);
            CueTrackPositions = new ProtoType<MasterElement>("CueTrackPositions", new byte[] { (byte)0xB7 }, 3);
            CueTrack = new ProtoType<UnsignedIntegerElement>("CueTrack", new byte[] { (byte)0xF7 }, 4);
            CueClusterPosition = new ProtoType<UnsignedIntegerElement>("CueClusterPosition", new byte[] { (byte)0xF1 }, 4);
            CueRelativePosition = new ProtoType<UnsignedIntegerElement>("CueRelativePosition", new byte[] { (byte)0xF0 }, 4);
            CueDuration = new ProtoType<UnsignedIntegerElement>("CueDuration", new byte[] { (byte)0xB2 }, 4);
            CueBlockNumber = new ProtoType<UnsignedIntegerElement>("CueBlockNumber", new byte[] { (byte)0x53, (byte)0x78 }, 4);
            CueCodecState = new ProtoType<UnsignedIntegerElement>("CueCodecState", new byte[] { (byte)0xEA }, 4);
            CueReference = new ProtoType<MasterElement>("CueReference", new byte[] { (byte)0xDB }, 4);
            CueRefTime = new ProtoType<UnsignedIntegerElement>("CueRefTime", new byte[] { (byte)0x96 }, 5);
            CueRefCluster = new ProtoType<UnsignedIntegerElement>("CueRefCluster", new byte[] { (byte)0x97 }, 5);
            CueRefNumber = new ProtoType<UnsignedIntegerElement>("CueRefNumber", new byte[] { (byte)0x53, (byte)0x5F }, 5);
            CueRefCodecState = new ProtoType<UnsignedIntegerElement>("CueRefCodecState", new byte[] { (byte)0xEB }, 5);
            Attachments = new ProtoType<MasterElement>("Attachments", new byte[] { (byte)0x19, (byte)0x41, (byte)0xA4, (byte)0x69 }, 1);
            AttachedFile = new ProtoType<MasterElement>("AttachedFile", new byte[] { (byte)0x61, (byte)0xA7 }, 2);
            FileDescription = new ProtoType<UTF8StringElement>("FileDescription", new byte[] { (byte)0x46, (byte)0x7E }, 3);
            FileName = new ProtoType<UTF8StringElement>("FileName", new byte[] { (byte)0x46, (byte)0x6E }, 3);
            FileMimeType = new ProtoType<StringElement>("FileMimeType", new byte[] { (byte)0x46, (byte)0x60 }, 3);
            FileData = new ProtoType<BinaryElement>("FileData", new byte[] { (byte)0x46, (byte)0x5C }, 3);
            FileUID = new ProtoType<UnsignedIntegerElement>("FileUID", new byte[] { (byte)0x46, (byte)0xAE }, 3);
            FileReferral = new ProtoType<BinaryElement>("FileReferral", new byte[] { (byte)0x46, (byte)0x75 }, 3);
            FileUsedStartTime = new ProtoType<UnsignedIntegerElement>("FileUsedStartTime", new byte[] { (byte)0x46, (byte)0x61 }, 3);
            FileUsedEndTime = new ProtoType<UnsignedIntegerElement>("FileUsedEndTime", new byte[] { (byte)0x46, (byte)0x62 }, 3);
            Chapters = new ProtoType<MasterElement>("Chapters", new byte[] { (byte)0x10, (byte)0x43, (byte)0xA7, (byte)0x70 }, 1);
            EditionEntry = new ProtoType<MasterElement>("EditionEntry", new byte[] { (byte)0x45, (byte)0xB9 }, 2);
            EditionUID = new ProtoType<UnsignedIntegerElement>("EditionUID", new byte[] { (byte)0x45, (byte)0xBC }, 3);
            EditionFlagHidden = new ProtoType<UnsignedIntegerElement>("EditionFlagHidden", new byte[] { (byte)0x45, (byte)0xBD }, 3);
            EditionFlagDefault = new ProtoType<UnsignedIntegerElement>("EditionFlagDefault", new byte[] { (byte)0x45, (byte)0xDB }, 3);
            EditionFlagOrdered = new ProtoType<UnsignedIntegerElement>("EditionFlagOrdered", new byte[] { (byte)0x45, (byte)0xDD }, 3);
            ChapterAtom = new ProtoType<MasterElement>("ChapterAtom", new byte[] { (byte)0xB6 }, 3);
            ChapterUID = new ProtoType<UnsignedIntegerElement>("ChapterUID", new byte[] { (byte)0x73, (byte)0xC4 }, 4);
            ChapterStringUID = new ProtoType<UTF8StringElement>("ChapterStringUID", new byte[] { (byte)0x56, (byte)0x54 }, 4);
            ChapterTimeStart = new ProtoType<UnsignedIntegerElement>("ChapterTimeStart", new byte[] { (byte)0x91 }, 4);
            ChapterTimeEnd = new ProtoType<UnsignedIntegerElement>("ChapterTimeEnd", new byte[] { (byte)0x92 }, 4);
            ChapterFlagHidden = new ProtoType<UnsignedIntegerElement>("ChapterFlagHidden", new byte[] { (byte)0x98 }, 4);
            ChapterFlagEnabled = new ProtoType<UnsignedIntegerElement>("ChapterFlagEnabled", new byte[] { (byte)0x45, (byte)0x98 }, 4);
            ChapterSegmentUID = new ProtoType<BinaryElement>("ChapterSegmentUID", new byte[] { (byte)0x6E, (byte)0x67 }, 4);
            ChapterSegmentEditionUID = new ProtoType<UnsignedIntegerElement>("ChapterSegmentEditionUID", new byte[] { (byte)0x6E, (byte)0xBC }, 4);
            ChapterPhysicalEquiv = new ProtoType<UnsignedIntegerElement>("ChapterPhysicalEquiv", new byte[] { (byte)0x63, (byte)0xC3 }, 4);
            ChapterTrack = new ProtoType<MasterElement>("ChapterTrack", new byte[] { (byte)0x8F }, 4);
            ChapterTrackNumber = new ProtoType<UnsignedIntegerElement>("ChapterTrackNumber", new byte[] { (byte)0x89 }, 5);
            ChapterDisplay = new ProtoType<MasterElement>("ChapterDisplay", new byte[] { (byte)0x80 }, 4);
            ChapString = new ProtoType<UTF8StringElement>("ChapString", new byte[] { (byte)0x85 }, 5);
            ChapLanguage = new ProtoType<StringElement>("ChapLanguage", new byte[] { (byte)0x43, (byte)0x7C }, 5);
            ChapCountry = new ProtoType<StringElement>("ChapCountry", new byte[] { (byte)0x43, (byte)0x7E }, 5);
            ChapProcess = new ProtoType<MasterElement>("ChapProcess", new byte[] { (byte)0x69, (byte)0x44 }, 4);
            ChapProcessCodecID = new ProtoType<UnsignedIntegerElement>("ChapProcessCodecID", new byte[] { (byte)0x69, (byte)0x55 }, 5);
            ChapProcessPrivate = new ProtoType<BinaryElement>("ChapProcessPrivate", new byte[] { (byte)0x45, (byte)0x0D }, 5);
            ChapProcessCommand = new ProtoType<MasterElement>("ChapProcessCommand", new byte[] { (byte)0x69, (byte)0x11 }, 5);
            ChapProcessTime = new ProtoType<UnsignedIntegerElement>("ChapProcessTime", new byte[] { (byte)0x69, (byte)0x22 }, 6);
            ChapProcessData = new ProtoType<BinaryElement>("ChapProcessData", new byte[] { (byte)0x69, (byte)0x33 }, 6);
            Tags = new ProtoType<MasterElement>("Tags", new byte[] { (byte)0x12, (byte)0x54, (byte)0xC3, (byte)0x67 }, 1);
            Tag = new ProtoType<MasterElement>("Tag", new byte[] { (byte)0x73, (byte)0x73 }, 2);
            Targets = new ProtoType<MasterElement>("Targets", new byte[] { (byte)0x63, (byte)0xC0 }, 3);
            TargetTypeValue = new ProtoType<UnsignedIntegerElement>("TargetTypeValue", new byte[] { (byte)0x68, (byte)0xCA }, 4);
            TargetType = new ProtoType<StringElement>("TargetType", new byte[] { (byte)0x63, (byte)0xCA }, 4);
            TagTrackUID = new ProtoType<UnsignedIntegerElement>("TagTrackUID", new byte[] { (byte)0x63, (byte)0xC5 }, 4);
            TagEditionUID = new ProtoType<UnsignedIntegerElement>("TagEditionUID", new byte[] { (byte)0x63, (byte)0xC9 }, 4);
            TagChapterUID = new ProtoType<UnsignedIntegerElement>("TagChapterUID", new byte[] { (byte)0x63, (byte)0xC4 }, 4);
            TagAttachmentUID = new ProtoType<UnsignedIntegerElement>("TagAttachmentUID", new byte[] { (byte)0x63, (byte)0xC6 }, 4);
            SimpleTag = new ProtoType<MasterElement>("SimpleTag", new byte[] { (byte)0x67, (byte)0xC8 }, 3);
            TagName = new ProtoType<UTF8StringElement>("TagName", new byte[] { (byte)0x45, (byte)0xA3 }, 4);
            TagLanguage = new ProtoType<StringElement>("TagLanguage", new byte[] { (byte)0x44, (byte)0x7A }, 4);
            TagDefault = new ProtoType<UnsignedIntegerElement>("TagDefault", new byte[] { (byte)0x44, (byte)0x84 }, 4);
            TagString = new ProtoType<UTF8StringElement>("TagString", new byte[] { (byte)0x44, (byte)0x87 }, 4);
            TagBinary = new ProtoType<BinaryElement>("TagBinary", new byte[] { (byte)0x44, (byte)0x85 }, 4);

        }
    }
}
