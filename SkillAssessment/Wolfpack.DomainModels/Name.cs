using System;
using System.ComponentModel.DataAnnotations;

namespace Wolfpack.DomainModel
{
    /// <summary>
    /// Class representing a generic name convention.
    /// </summary>
    public class Name
    {
        public string Value { get; set; }

        /// <summary>
        /// Creates a <see cref="Name"/> object.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>A <see cref="Name"/> object.</returns>
        /// <exception cref="ArgumentNullException">It is thrown if the <paramref name="name"/> is null or contains
        /// only white spaces.</exception>
        public static Name CreateFrom(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ValidationException("Name may not be empty.");
            }

            return new Name()
            {
                Value = name.Trim()
            };
        }
    }
}
