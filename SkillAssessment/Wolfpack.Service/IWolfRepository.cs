using System.Collections.Generic;
using Wolfpack.DomainModel;

namespace Wolfpack.DomainServices
{
    /// <summary>
    /// A repository interface that provides functionality for dealing with the wolf domain.
    /// </summary>
    public interface IWolfRepository
    {
        /// <summary>
        /// Gets single <see cref="Wolf"/> domain object with provided identifier.
        /// </summary>
        /// <param name="wolfId">The provided identifier.</param>
        /// <returns>Instance of <see cref="Wolf"/> domain object.</returns>
        Wolf GetWolfById(int wolfId);

        /// <summary>
        /// Gets all <see cref="Wolf"/> domain objects found.
        /// </summary>
        /// <remarks>Will return an empty collection if no records are found.</remarks>
        /// <returns>A collection of <see cref="Wolf"/> domain objects.</returns>
        IEnumerable<Wolf> GetAllWolves();

        /// <summary>
        /// Adds a <see cref="Wolf"/> to the records.
        /// </summary>
        /// <param name="wolf">The domain object to be added.</param>
        /// <remarks>Can be converted into void method.</remarks>
        /// <returns>The <see cref="Wolf"/> domain object after successful creation.
        /// </returns>
        Wolf AddWolf(Wolf wolf);

        /// <summary>
        /// Deletes the <see cref="Wolf"/> with provided identifier <paramref name="wolfId"/>
        /// </summary>
        /// <param name="wolfId">The provided identifier.</param>
        void DeleteWolf(int wolfId);

        /// <summary>
        /// Updates the <see cref="Wolf"/> with provided identifier <paramref name="id"/>,
        /// using the properties of the properties of the domain object
        /// <paramref name="wolf"/>.
        /// </summary>
        /// <param name="id">The provided identifier.</param>
        /// <param name="wolf">A domain object to be used for update.</param>
        void UpdateWolf(int id, Wolf wolf);
    }
}
