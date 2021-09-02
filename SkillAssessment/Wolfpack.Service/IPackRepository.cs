using System.Collections.Generic;
using Wolfpack.DomainModel;

namespace Wolfpack.DomainServices
{
    /// <summary>
    /// A repository interface that provides functionality for dealing with the pack domain.
    /// </summary>
    public interface IPackRepository
    {
        /// <summary>
        /// Gets single <see cref="Pack"/> domain object with provided identifier.
        /// </summary>
        /// <param name="packId">The provided identifier.</param>
        /// <returns>Instance of <see cref="Pack"/> domain object.</returns>
        Pack GetPackById(int packId);

        /// <summary>
        /// Gets all <see cref="Pack"/> domain objects found.
        /// </summary>
        /// <remarks>Will return an empty collection if no records are found.</remarks>
        /// <returns>A collection of <see cref="Pack"/> domain objects.</returns>
        IEnumerable<Pack> GetAllPacks();

        /// <summary>
        /// Adds a <see cref="Pack"/> to the records.
        /// </summary>
        /// <param name="pack">The domain object to be added.</param>
        /// <returns>The <see cref="Pack"/> domain object after successful creation.
        /// </returns>
        void CreatePack(Pack pack);

        /// <summary>
        /// Deletes the <see cref="Pack"/> with provided identifier <paramref name="packId"/>
        /// </summary>
        /// <param name="packId">The provided identifier.</param>
        void DeletePack(int packId);

        /// <summary>
        /// Updates the <see cref="Pack"/> with provided identifier <paramref name="id"/>,
        /// using the properties of the properties of the domain object
        /// <paramref name="pack"/>.
        /// </summary>
        /// <param name="id">The provided identifier.</param>
        /// <param name="pack">A domain object to be used for update.</param>
        void UpdatePack(int id, Pack pack);

        /// <summary>
        /// Adds a <see cref="Wolf"/> to a <see cref="Pack"/> using the provided
        /// identifiers.
        /// </summary>
        /// <param name="packId">The provided <see cref="Pack"/> identifier.</param>
        /// <param name="wolfId">The provided <see cref="Wolf"/> identifier.</param>
        void AddWolfToPack(int packId, int wolfId);

        /// <summary>
        /// Removes a <see cref="Wolf"/> from a <see cref="Pack"/> using the provided
        /// identifiers.
        /// </summary>
        /// <param name="packId">The provided <see cref="Pack"/> identifier.</param>
        /// <param name="wolfId">The provided <see cref="Wolf"/> identifier.</param>
        void RemoveWolfFromPack(int packId, int wolfId);
    }
}
