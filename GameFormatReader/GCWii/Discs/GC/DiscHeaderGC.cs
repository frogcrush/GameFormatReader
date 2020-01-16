using System.Text;
using GameFormatReader.Common;

namespace GameFormatReader.GCWii.Discs.GC
{
    /// <summary>
    /// Represents a <see cref="DiscHeader"/> for the GameCube.
    /// Mostly the same as the Wii, except for some minor differences.
    /// </summary>
    public sealed class DiscHeaderGC : DiscHeader
    {
        #region Constructor

        internal DiscHeaderGC(EndianBinaryReader reader)
        {
            ReadHeader(reader);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Offset of the debug monitor (dh.bin).
        /// </summary>
        public uint DebugMonitorOffset
        {
            get;
            private set;
        }

        /// <summary>
        /// Load address for the debug monitor.
        /// </summary>
        public uint DebugMonitorLoadAddress
        {
            get;
            private set;
        }

        /// <summary>
        /// Offset to the main DOL executable.
        /// </summary>
        public uint MainDolOffset
        {
            get;
            private set;
        }

        /// <summary>
        /// Offset to the FST.
        /// </summary>
        public uint FSTOffset
        {
            get;
            private set;
        }

        /// <summary>
        /// Size of the FST.
        /// </summary>
        public uint FSTSize
        {
            get;
            private set;
        }

        /// <summary>
        /// Maximum size of the FST.
        /// Usually the same size as FSTSize.
        /// </summary>
        public uint MaxFSTSize
        {
            get;
            private set;
        }

        #endregion

        #region Private Methods

        // Reads GameCube specific header info.
        protected override void ReadHeader(EndianBinaryReader reader)
        {
            //This is the ID6.
            var chars = reader.ReadChars(6);
            ID6 = new string(chars);
            
            Type = DetermineDiscType(chars[0]);
            GameCode = new string(chars[1], chars[2]);
            RegionCode = DetermineRegion(chars[3]);
            MakerCode = new string(chars[4], chars[5]);
            
            DiscNumber = reader.ReadByte();
            AudioStreaming = reader.ReadBoolean();
            StreamingBufferSize = reader.ReadByte();

            // Skip unused bytes
            reader.BaseStream.Position += 12;

            MagicWord = reader.ReadInt32();

            // Skip to game title. Read until 0x00 (null char) is hit.
            reader.BaseStream.Position = 0x20;

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            GameTitle = Encoding.GetEncoding("shift_jis").GetString(reader.ReadBytesUntil(0x00));

            DebugMonitorOffset = reader.ReadUInt32();
            DebugMonitorLoadAddress = reader.ReadUInt32();

            // Skip unused bytes
            reader.BaseStream.Position = 0x420;

            MainDolOffset = reader.ReadUInt32();
            FSTOffset = reader.ReadUInt32();
            FSTSize = reader.ReadUInt32();
            MaxFSTSize = reader.ReadUInt32();
        }

        #endregion
    }
}
