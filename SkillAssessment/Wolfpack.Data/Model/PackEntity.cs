using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Wolfpack.DomainModel;

namespace Wolfpack.Data.Model
{
    /// <summary>
    /// Represents a pack that is stored in the database.
    /// </summary>
    public class PackEntity
    {
        public PackEntity()
        {
            Wolfs = new List<WolfEntity>();
        }

        /// <summary>
        /// Gets or sets the identifier of the pack.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 0)]
        public int PackId { get; set; }

        /// <summary>
        /// Gets or sets the name of the pack.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the wolf in the pack.
        /// </summary>
        public ICollection<WolfEntity> Wolfs { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PackEntity"/> based on the provided domain object.
        /// </summary>
        /// <param name="pack">
        /// The pack domain object for which a corresponding entity is to be created.
        /// </param>
        /// <returns>
        /// A new instance of the <see cref="PackEntity"/> class that represents the <paramref name="pack"/>
        /// </returns>
        public static PackEntity Initialize(Pack pack)
        {
            return new PackEntity()
            {
                Name = pack.Name.Value
            };
        }

        /// <summary>
        /// Converts a <see cref="PackEntity"/> object to a <see cref="Pack"/> domain object.
        /// </summary>
        /// <returns>A <see cref="Pack"/> domain object.</returns>
        public Pack Convert()
        {
            return new Pack()
            {
                PackId = this.PackId,
                Name = DomainModel.Name.CreateFrom(this.Name),
                Wolves = this.Wolfs.ToList().ConvertAll(wolf => wolf.Convert())
            };
        }

        /// <summary>
        /// Updates the <see cref="PackEntity"/> properties to match a <see cref="Pack"/> domain
        /// object.
        /// </summary>
        /// <param name="pack">A <see cref="Pack"/> domain object.</param>
        public void UpdateEntity(Pack pack)
        {
            this.Name = pack.Name.Value;
        }
    }
}