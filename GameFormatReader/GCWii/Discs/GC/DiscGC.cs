﻿using System.IO;
using GameFormatReader.Common;

namespace GameFormatReader.GCWii.Discs.GC
{
    /// <summary>
    /// Represents a GameCube <see cref="Disc"/>.
    /// </summary>
    public sealed class DiscGC : Disc
    {
        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="filepath">Path to the GameCube <see cref="Disc"/>.</param>
        public DiscGC(string filepath) : base()
        {
            Initialize(File.OpenRead(filepath));
        }

        public DiscGC(Stream fileStream)
        {
            Initialize(fileStream);
        }

        private void Initialize(Stream stream)
        {
            using (var reader = new EndianBinaryReader(stream, Endian.Big))
            {
                Header = new DiscHeaderGC(reader);
                Apploader = new Apploader(reader);
                FileSystemTable = new FST(reader);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// GameCube <see cref="DiscHeader"/>.
        /// </summary>
        public override DiscHeader Header
        {
            get;
            protected set;
        }

        /// <summary>
        /// The Apploader on this GameCube <see cref="Disc"/>.
        /// </summary>
        public Apploader Apploader
        {
            get;
            private set;
        }

        /// <summary>
        /// File-system table of this disc.
        /// </summary>
        public FST FileSystemTable
        {
            get;
            private set;
        }

        #endregion
    }
}
