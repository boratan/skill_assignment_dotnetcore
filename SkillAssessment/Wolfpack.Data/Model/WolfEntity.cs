using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Wolfpack.DomainModel;

namespace Wolfpack.Data.Model
{
    /// <summary>
    /// Represents a wolf that is stored in the database.
    /// </summary>
    public class WolfEntity
    {
        /// <summary>
        /// Gets or sets the identifier of the wolf.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 0)]
        public int WolfId { get; set; }

        /// <summary>
        /// Gets or sets the name of the wolf.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the gender of the wolf.
        /// </summary>
        [Required]
        public Gender Gender { get; set; }

        /// <summary>
        /// Gets or sets the birth date of the wolf.
        /// </summary>
        [Required]
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Gets or sets the latitude of the wolf's location.
        /// </summary>
        [Required]
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude of the wolf's location.
        /// </summary>
        [Required]
        public double Longitude { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WolfEntity"/> based on the provided domain object.
        /// </summary>
        /// <param name="wolf">
        /// The wolf domain object for which a corresponding entity is to be created.
        /// </param>
        /// <returns>
        /// A new instance of the <see cref="WolfEntity"/> class that represents the <paramref name="wolf"/>
        /// </returns>
        public static WolfEntity Initialize(Wolf wolf)
        {
            return new WolfEntity()
            {
                Name = wolf.Name.Value,
                Gender = wolf.Gender,
                BirthDate = wolf.BirthDate,
                Latitude = wolf.Location.Latitude,
                Longitude = wolf.Location.Longitude
            };
        }

        /// <summary>
        /// Converts a <see cref="WolfEntity"/> object to a <see cref="Wolf"/> domain object.
        /// </summary>
        /// <returns>A <see cref="Wolf"/> domain object.</returns>
        public Wolf Convert()
        {
            return new Wolf()
            {
                WolfId = this.WolfId,
                Name = DomainModel.Name.CreateFrom(this.Name),
                Gender = this.Gender,
                BirthDate = this.BirthDate,
                Location = new Location() { Latitude = this.Latitude, Longitude = this.Longitude }
            };
        }

        /// <summary>
        /// Updates the <see cref="WolfEntity"/> properties to match a <see cref="Wolf"/> domain
        /// object.
        /// </summary>
        /// <param name="wolf">A <see cref="Wolf"/> domain object.</param>
        public void UpdateEntity(Wolf wolf)
        {
            this.Name = wolf.Name.Value;
            this.BirthDate = wolf.BirthDate;
            this.Gender = wolf.Gender;
            this.Latitude = wolf.Location.Latitude;
            this.Longitude = wolf.Location.Longitude;
        }
    }
}