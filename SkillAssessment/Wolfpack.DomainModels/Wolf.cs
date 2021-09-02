using System;

namespace Wolfpack.DomainModel
{
    /// <summary>
    /// Class representing the wolf domain object.
    /// </summary>
    public class Wolf
    {
        public int WolfId { get; set; }

        public Name Name { get; set; }

        public Gender Gender { get; set; }
        public Location Location { get; set; }

        public DateTime BirthDate { get; set; }
    }
}
