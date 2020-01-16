﻿using System.IO;
using GameFormatReader.Common;

// TODO: Finish this up.

namespace GameFormatReader.GCWii.Discs.Wii
{
    /// <summary>
    /// Represents a Wii <see cref="Disc"/>.
    /// </summary>
    public sealed class DiscWii : Disc
    {
        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="filepath">Path to the Wii <see cref="Disc"/>.</param>
        public DiscWii(string filepath) 
        {
            Initialize(File.OpenRead(filepath));
        }

        public DiscWii(Stream stream)
        {
            Initialize(stream);
        }

        private void Initialize(Stream stream)
        {
            using (var reader = new EndianBinaryReader(stream, Endian.Big))
            {
                Header = new DiscHeaderWii(reader);

                // Skip to the partiton data offset.
                reader.BaseStream.Position = 0x40000;

                // TODO: Read the rest of the disc.
            }
        }


        #endregion

        #region Properties

        /// <summary>
        /// Wii <see cref="DiscHeader"/>.
        /// </summary>
        public override DiscHeader Header
        {
            get;
            protected set;
        }

        #endregion
    }
}
