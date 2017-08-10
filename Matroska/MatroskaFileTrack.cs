using MkvTests.Ebml;
using MkvTests.Ebml.DocTypes;
using MkvTests.Ebml.Elements;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MkvTests.Matroska
{
    public class MatroskaFileTrack
    {
        public int TrackNumber { get; set; }
        public ulong TrackUID { get; set; }
        public bool FlagEnabled { get; set; }
        public bool FlagDefault { get; set; }
        public bool FlagForced { get; set; }
        public bool FlagLacing { get; set; }
        public int MinCache { get; set; }
        public int MaxBlockAdditionalId { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
        public string CodecId { get; set; }
        public MemoryStream CodecPrivate { get; set; }
        public ulong DefaultDuration { get; set; }
        public bool CodecDecodeAll { get; set; }
        public int SeekPreroll { get; set; }

        private TrackType TrackType { get; set; }
        private MatroskaVideoTrack video;
        private MatroskaAudioTrack audio;
        private List<ulong> joinUIDs;
        private List<ulong> overlayUIDs;

        public MatroskaFileTrack()
        {
            TrackNumber = 1;
            TrackUID = 1337;
            FlagDefault = true;
            FlagEnabled = true;
            FlagForced = true;
            FlagLacing = true;
            MinCache = 0;
            MaxBlockAdditionalId = 0;
            Name = "unnamed";
            Language = "eng";
            CodecDecodeAll = true;
            SeekPreroll = 0;
            video = null;
            audio = null;
            joinUIDs = new List<ulong>();
            overlayUIDs = new List<ulong>();
        }

        public new string ToString()
        {
            var sb = new StringBuilder();

            sb.Append($"\t\tTrackNo: {TrackNumber}\n");
            sb.Append($"\t\tTrackUID: {TrackUID}\n");
            sb.Append($"\t\tTrackType: {TrackType.ToString()}\n");
            sb.Append($"\t\tDefaultDuration: {DefaultDuration}\n");
            sb.Append($"\t\tName: {Name}\n");
            sb.Append($"\t\tLanguage: {Language}\n");
            sb.Append($"\t\tCodecID: {CodecId}\n");
            if(CodecPrivate != null)
            {
                sb.Append($"\t\tCodecPrivate: {CodecPrivate.Length - CodecPrivate.Position} byte(s)\n");
            }

            if(TrackType == TrackType.Video)
            {
                sb.Append($"\t\tPixelWidth: {video.PixelWidth}\n");
                sb.Append($"\t\tPixelHeight: {video.PixelHeight}\n");
                sb.Append($"\t\tDisplayWidth: {video.DisplayWidth}\n");
                sb.Append($"\t\tDisplayHeight: {video.DisplayHeight}\n");
            }

            if(TrackType == TrackType.Audio)
            {
                sb.Append($"\t\tSamplingFrequency: {audio.SamplingFrequency}\n");
                if(audio.OutputSamplingFrequency != 0)
                {
                    sb.Append($"\t\tOutputSamplingFrequency: {audio.OutputSamplingFrequency}\n");
                }
                sb.Append($"\t\tChannels: {audio.Channels}\n");
                if(audio.BitDepth != 0)
                {
                    sb.Append($"\t\tBitDepth: {audio.BitDepth}\n");
                }
            }

            return sb.ToString();
        }

        public static MatroskaFileTrack FromElement(Element level2, FileStream source, EbmlReader reader)
        {
            Element level3 = ((MasterElement)level2).ReadNextChild(reader);
            Element level4 = null;

            MatroskaFileTrack track = new MatroskaFileTrack();
            Utility.LogDebug("Reading track from doc!");
            while(level3 != null)
            {
                if (level3.IsType(MatroskaDocTypes.TrackNumber.Type))
                {
                    level3.ReadData(source);
                    track.TrackNumber = ((int)((UnsignedIntegerElement)level3).Value);
                }
                else if (level3.IsType(MatroskaDocTypes.TrackUID.Type))
                {
                    level3.ReadData(source);
                    track.TrackUID = ((UnsignedIntegerElement)level3).Value;
                }
                else if (level3.IsType(MatroskaDocTypes.TrackType.Type))
                {
                    level3.ReadData(source);
                    track.TrackType = (TrackType)((UnsignedIntegerElement)level3).Value;
                }
                else if (level3.IsType(MatroskaDocTypes.DefaultDuration.Type))
                {
                    level3.ReadData(source);
                    track.DefaultDuration = ((UnsignedIntegerElement)level3).Value;
                }
                else if (level3.IsType(MatroskaDocTypes.Name.Type))
                {
                    level3.ReadData(source);
                    track.Name = ((StringElement)level3).Value;
                }
                else if (level3.IsType(MatroskaDocTypes.Language.Type))
                {
                    level3.ReadData(source);
                    track.Language = ((StringElement)level3).Value;
                }
                else if (level3.IsType(MatroskaDocTypes.CodecID.Type))
                {
                    level3.ReadData(source);
                    track.CodecId = ((StringElement)level3).Value;
                }
                else if (level3.IsType(MatroskaDocTypes.CodecPrivate.Type))
                {
                    level3.ReadData(source);
                    track.CodecPrivate = ((BinaryElement)level3).Data;
                }
                else if (level3.IsType(MatroskaDocTypes.Video.Type))
                {
                    level4 = ((MasterElement)level3).ReadNextChild(reader);
                    track.video = new MatroskaVideoTrack();
                    while(level4 != null)
                    {
                        if (level4.IsType(MatroskaDocTypes.PixelWidth.Type))
                        {
                            level4.ReadData(source);
                            track.video.PixelWidth = (short)((UnsignedIntegerElement)level4).Value;
                        }
                        else if (level4.IsType(MatroskaDocTypes.PixelHeight.Type))
                        {
                            level4.ReadData(source);
                            track.video.PixelHeight = (short)((UnsignedIntegerElement)level4).Value;
                        }
                        else if (level4.IsType(MatroskaDocTypes.DisplayWidth.Type))
                        {
                            level4.ReadData(source);
                            track.video.DisplayWidth = (short)((UnsignedIntegerElement)level4).Value;
                        }
                        else if (level4.IsType(MatroskaDocTypes.DisplayHeight.Type))
                        {
                            level4.ReadData(source);
                            track.video.DisplayHeight = (short)((UnsignedIntegerElement)level4).Value;
                        }

                        level4.SkipData(source);
                        level4 = ((MasterElement)level3).ReadNextChild(reader);
                    }
                }
                else if (level3.IsType(MatroskaDocTypes.Audio.Type))
                {
                    level4 = ((MasterElement)level3).ReadNextChild(reader);
                    track.audio = new MatroskaAudioTrack();
                    while (level4 != null)
                    {
                        if (level4.IsType(MatroskaDocTypes.SamplingFrequency.Type))
                        {
                            level4.ReadData(source);
                            track.audio.SamplingFrequency = (float)((FloatElement)level4).Value;
                        }
                        else if (level4.IsType(MatroskaDocTypes.OutputSamplingFrequency.Type))
                        {
                            level4.ReadData(source);
                            track.audio.OutputSamplingFrequency = (float)((FloatElement)level4).Value;
                        }
                        else if (level4.IsType(MatroskaDocTypes.Channels.Type))
                        {
                            level4.ReadData(source);
                            track.audio.Channels = (short)((UnsignedIntegerElement)level4).Value;
                        }
                        else if (level4.IsType(MatroskaDocTypes.BitDepth.Type))
                        {
                            level4.ReadData(source);
                            track.audio.BitDepth = (byte)((UnsignedIntegerElement)level4).Value;
                        }

                        level4.SkipData(source);
                        level4 = ((MasterElement)level3).ReadNextChild(reader);
                    }
                }

                level3.SkipData(source);
                level3 = ((MasterElement)level2).ReadNextChild(reader);
            }
            Utility.LogDebug("Read track from doc!");
            return track;
        }

        public Element ToElement()
        {
            MasterElement trackEntryElement = MatroskaDocTypes.TrackEntry.GetInstance();

            UnsignedIntegerElement trackNumberElement = MatroskaDocTypes.TrackNumber.GetInstance();
            trackNumberElement.Value = (ulong)TrackNumber;

            UnsignedIntegerElement trackUIDElement = MatroskaDocTypes.TrackType.GetInstance();
            trackUIDElement.Value = TrackUID;

            UnsignedIntegerElement trackTypeElement = MatroskaDocTypes.TrackType.GetInstance();
            trackTypeElement.Value = (ulong)TrackType;
            Utility.LogTrace($"Track type set to {TrackType.ToString()}");

            UnsignedIntegerElement trackFlagEnabledElement = MatroskaDocTypes.FlagEnabled.GetInstance();
            trackFlagEnabledElement.Value = (ulong)(FlagEnabled ? 1 : 0);

            UnsignedIntegerElement trackFlagDefaultElement = MatroskaDocTypes.FlagDefault.GetInstance();
            trackFlagDefaultElement.Value = (ulong)(FlagDefault ? 1 : 0);

            UnsignedIntegerElement trackFlagForcedElement = MatroskaDocTypes.FlagForced.GetInstance();
            trackFlagForcedElement.Value = (ulong)(FlagForced ? 1 : 0);

            UnsignedIntegerElement trackFlagLacingElement = MatroskaDocTypes.FlagLacing.GetInstance();
            trackFlagLacingElement.Value = (ulong)(FlagLacing ? 1 : 0);

            UnsignedIntegerElement trackMinCacheElement = MatroskaDocTypes.MinCache.GetInstance();
            trackMinCacheElement.Value = (ulong)MinCache;

            UnsignedIntegerElement trackMaxBlockAddIdElement = MatroskaDocTypes.MaxBlockAdditionID.GetInstance();
            trackMaxBlockAddIdElement.Value = (ulong)MaxBlockAdditionalId;

            StringElement trackNameElement = MatroskaDocTypes.Name.GetInstance();
            trackNameElement.Value = Name;

            StringElement trackLangElement = MatroskaDocTypes.Language.GetInstance();
            trackLangElement.Value = Language;

            StringElement trackCodecIdElement = MatroskaDocTypes.CodecID.GetInstance();
            trackCodecIdElement.Value = CodecId;

            trackEntryElement.AddChildElement(trackNumberElement);
            trackEntryElement.AddChildElement(trackUIDElement);
            trackEntryElement.AddChildElement(trackTypeElement);

            trackEntryElement.AddChildElement(trackFlagEnabledElement);
            trackEntryElement.AddChildElement(trackFlagDefaultElement);
            trackEntryElement.AddChildElement(trackFlagForcedElement);
            trackEntryElement.AddChildElement(trackFlagLacingElement);
            trackEntryElement.AddChildElement(trackMinCacheElement);
            trackEntryElement.AddChildElement(trackMaxBlockAddIdElement);

            trackEntryElement.AddChildElement(trackNameElement);
            trackEntryElement.AddChildElement(trackLangElement);
            trackEntryElement.AddChildElement(trackCodecIdElement);

            if (CodecPrivate != null && CodecPrivate.Length != CodecPrivate.Position)
            {
                BinaryElement trackCodecPrivateElement = MatroskaDocTypes.CodecPrivate.GetInstance();
                trackCodecPrivateElement.Data = CodecPrivate;
                trackEntryElement.AddChildElement(trackCodecPrivateElement);
            }

            UnsignedIntegerElement trackDefaultDurationElement = MatroskaDocTypes.DefaultDuration.GetInstance();
            trackDefaultDurationElement.Value = DefaultDuration;

            UnsignedIntegerElement trackCodecDecodeAllElement = MatroskaDocTypes.CodecDecodeAll.GetInstance();
            trackCodecDecodeAllElement.Value = (ulong)(CodecDecodeAll ? 0 : 1);

            trackEntryElement.AddChildElement(trackDefaultDurationElement);
            trackEntryElement.AddChildElement(trackCodecDecodeAllElement);

            if (overlayUIDs.Count != 0)
            {
                foreach(ulong overlay in overlayUIDs)
                {
                    UnsignedIntegerElement trackOverlayElement = MatroskaDocTypes.TrackOverlay.GetInstance();
                    trackOverlayElement.Value = overlay;
                    trackEntryElement.AddChildElement(trackOverlayElement);
                }
            }

            // Now we add the audio/video dependant sub-elements
            if (this.TrackType == TrackType.Video)
            {
                MasterElement trackVideoElement = MatroskaDocTypes.Video.GetInstance();

                UnsignedIntegerElement trackVideoPixelWidthElement = MatroskaDocTypes.PixelWidth.GetInstance();
                trackVideoPixelWidthElement.Value = (ulong)this.video.PixelWidth;

                UnsignedIntegerElement trackVideoPixelHeightElement = MatroskaDocTypes.PixelHeight.GetInstance();
                trackVideoPixelHeightElement.Value = (ulong)this.video.PixelHeight;

                UnsignedIntegerElement trackVideoDisplayWidthElement = MatroskaDocTypes.DisplayWidth.GetInstance();
                trackVideoDisplayWidthElement.Value = (ulong)this.video.DisplayWidth;

                UnsignedIntegerElement trackVideoDisplayHeightElement = MatroskaDocTypes.DisplayHeight.GetInstance();
                trackVideoDisplayHeightElement.Value = (ulong)this.video.DisplayHeight;

                trackVideoElement.AddChildElement(trackVideoPixelWidthElement);
                trackVideoElement.AddChildElement(trackVideoPixelHeightElement);
                trackVideoElement.AddChildElement(trackVideoDisplayWidthElement);
                trackVideoElement.AddChildElement(trackVideoDisplayHeightElement);

                trackEntryElement.AddChildElement(trackVideoElement);
            }
            else if (this.TrackType == TrackType.Audio)
            {
                MasterElement trackAudioElement = MatroskaDocTypes.Audio.GetInstance();

                UnsignedIntegerElement trackAudioChannelsElement = MatroskaDocTypes.Channels.GetInstance();
                trackAudioChannelsElement.Value = (ulong)this.audio.Channels;

                UnsignedIntegerElement trackAudioBitDepthElement = MatroskaDocTypes.BitDepth.GetInstance();
                trackAudioBitDepthElement.Value = this.audio.BitDepth;

                FloatElement trackAudioSamplingRateElement = MatroskaDocTypes.SamplingFrequency.GetInstance();
                trackAudioSamplingRateElement.Value = this.audio.SamplingFrequency;

                FloatElement trackAudioOutputSamplingFrequencyElement = MatroskaDocTypes.OutputSamplingFrequency.GetInstance();
                trackAudioOutputSamplingFrequencyElement.Value = this.audio.OutputSamplingFrequency;

                trackAudioElement.AddChildElement(trackAudioChannelsElement);
                trackAudioElement.AddChildElement(trackAudioBitDepthElement);
                trackAudioElement.AddChildElement(trackAudioSamplingRateElement);
                trackAudioElement.AddChildElement(trackAudioOutputSamplingFrequencyElement);

                trackEntryElement.AddChildElement(trackAudioElement);
            }
            if (joinUIDs != null)
            {
                MasterElement trackOpElement = MatroskaDocTypes.TrackOperation.GetInstance();
                MasterElement trackJoinElement = MatroskaDocTypes.TrackJoinBlocks.GetInstance();
                foreach(ulong uid in joinUIDs)
                {
                    UnsignedIntegerElement joinUidElement = MatroskaDocTypes.TrackJoinUID.GetInstance();
                    joinUidElement.Value = uid;
                    trackJoinElement.AddChildElement(joinUidElement);
                }
                trackOpElement.AddChildElement(trackJoinElement);
                trackEntryElement.AddChildElement(trackOpElement);
            }
            return trackEntryElement;
        }
    }
}
