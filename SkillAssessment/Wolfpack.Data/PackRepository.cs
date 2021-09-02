using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Wolfpack.Data.Model;
using Wolfpack.DomainModel;
using Wolfpack.DomainServices;

namespace Wolfpack.Data
{
    /// <summary>
    /// A repository that provides functionality for dealing with the wolf domain.
    /// </summary>
    public class PackRepository : IPackRepository
    {
        private readonly DataContext _dbContext;

        /// <summary>
        /// Initializes <see cref="PackRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public PackRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc cref="IPackRepository"/>
        /// <exception cref="KeyNotFoundException">Thrown when the no record with the provided
        /// identifier is found.</exception>
        public Pack GetPackById(int packId)
        {
            var entity = _dbContext.Packs.Find(packId);
            return entity == null
                ? throw new KeyNotFoundException($"Pack with id: {packId} does not exist")
                : entity.Convert();
        }

        /// <inheritdoc cref="IPackRepository"/>
        public IEnumerable<Pack> GetAllPacks()
        {
            return _dbContext.Packs.ToList().ConvertAll(pack => pack.Convert());
        }

        /// <inheritdoc cref="IPackRepository"/>
        public void CreatePack(Pack pack)
        {
            _dbContext.Add(PackEntity.Initialize(pack));
            _dbContext.SaveChanges();
        }

        /// <inheritdoc cref="IPackRepository"/>
        /// <exception cref="KeyNotFoundException">Thrown when the no record with the provided
        /// identifier is found.</exception>
        public void DeletePack(int packId)
        {
            var pack = FindPackEntity(packId);
            _dbContext.Packs.Remove(pack);
            _dbContext.SaveChanges();
        }

        /// <inheritdoc cref="IPackRepository"/>
        /// <exception cref="KeyNotFoundException">Thrown when the no record with the provided
        /// identifier is found.</exception>
        public void UpdatePack(int id, Pack pack)
        {
            var packEntity = FindPackEntity(id);
            packEntity.UpdateEntity(pack);
            _dbContext.Entry(packEntity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        /// <inheritdoc cref="IPackRepository"/>
        public void AddWolfToPack(int packId, int wolfId)
        {
            var packEntity = FindPackEntity(packId);
            var wolfEntity = FindWolfEntity(wolfId);
            if (packEntity.Wolfs.Any(x => x.Equals(wolfEntity))) return;
            packEntity.Wolfs.Add(wolfEntity);
            _dbContext.Entry(packEntity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        /// <inheritdoc cref="IPackRepository"/>
        public void RemoveWolfFromPack(int packId, int wolfId)
        {
            var packEntity = FindPackEntity(packId);
            var wolfEntity = FindWolfEntity(wolfId);
            if (!packEntity.Wolfs.Any(x => x.Equals(wolfEntity))) return;
            packEntity.Wolfs.Remove(wolfEntity);
            _dbContext.Entry(packEntity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Gets the <see cref="PackEntity"/> with the given identifier.
        /// </summary>
        /// <param name="id">Identifier of the entity to find.</param>
        /// <returns>A <see cref="PackEntity"/> database object.</returns>
        private PackEntity FindPackEntity(int id)
        {
            return _dbContext.Packs.Find(id)
                   ?? throw new KeyNotFoundException($"Pack with id: {id} does not exist");
        }

        /// <summary>
        /// Gets the <see cref="WolfEntity"/> with the given identifier.
        /// </summary>
        /// <param name="id">Identifier of the entity to find.</param>
        /// <returns>A <see cref="WolfEntity"/> database object.</returns>
        private WolfEntity FindWolfEntity(int id)
        {
            return _dbContext.Wolfs.Find(id)
                   ?? throw new KeyNotFoundException($"Wolf with id: {id} does not exist");
        }
    }
}
