using System.Collections.Generic;

namespace Wolfpack.DomainModel
{
    /// <summary>
    /// Class representing the pack domain object.
    /// </summary>
    public class Pack
    {
        public int PackId { get; set; }

        public Name Name { get; set; }

        public List<Wolf> Wolves { get; set; }
    }
}
