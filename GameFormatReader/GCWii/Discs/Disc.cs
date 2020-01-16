using System;
using System.IO;

namespace GameFormatReader.GCWii.Discs
{
    /// <summary>
    /// Represents a GameCube or Wii disc.
    /// </summary>
    public abstract class Disc
    {
        #region Properties

        /// <summary>
        /// The <see cref="DiscHeader"/> for this disc.
        /// </summary>
        public abstract DiscHeader Header
        {
            get;
            protected set;
        }

        #endregion
    }
}
