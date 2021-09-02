using System;
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
    public class WolfRepository : IWolfRepository
    {
        // The database context.
        private readonly DataContext _dbContext;

        /// <summary>
        /// Initializes <see cref="WolfRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public WolfRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc cref="IWolfRepository"/>
        /// <exception cref="KeyNotFoundException">Thrown when the no record with the provided
        /// identifier is found.</exception>
        public Wolf GetWolfById(int wolfId)
        {
            var entity = _dbContext.Wolfs.Find(wolfId);
            return entity == null
                ? throw new KeyNotFoundException($"Wolf with id: {wolfId} does not exist")
                : entity.Convert();
        }

        /// <inheritdoc cref="IWolfRepository"/>
        public IEnumerable<Wolf> GetAllWolves()
        {
            return _dbContext.Wolfs.ToList().ConvertAll(wolf => wolf.Convert());
        }

        /// <inheritdoc cref="IWolfRepository"/>
        public Wolf AddWolf(Wolf wolf)
        {
            CheckGenderEnum(wolf);
            var entity =_dbContext.Add(WolfEntity.Initialize(wolf));
            _dbContext.SaveChanges();
            return entity.Entity.Convert();
        }

        /// <inheritdoc cref="IWolfRepository"/>
        /// <exception cref="KeyNotFoundException">Thrown when the no record with the provided
        /// identifier is found.</exception>
        public void DeleteWolf(int wolfId)
        {
            var wolf = FindWolfEntity(wolfId);
            _dbContext.Wolfs.Remove(wolf);
            _dbContext.SaveChanges();
        }

        /// <inheritdoc cref="IWolfRepository"/>
        /// <exception cref="KeyNotFoundException">Thrown when the no record with the provided
        /// identifier is found.</exception>
        public void UpdateWolf(int id, Wolf wolf)
        {
            CheckGenderEnum(wolf);
            var wolfEntity = FindWolfEntity(id);
            wolfEntity.UpdateEntity(wolf);
            _dbContext.Entry(wolfEntity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Checks if the <see cref="Wolf"/> domain object's property <see cref="Wolf.Gender"/> is in the
        /// range of the enumeration <see cref="Gender"/>.
        /// </summary>
        /// <param name="wolf">The domain object to be checked.</param>
        private void CheckGenderEnum(Wolf wolf)
        {
            if (!Enum.IsDefined(typeof(Gender), wolf.Gender))
                throw new ArgumentOutOfRangeException(nameof(wolf), wolf.Gender,
                    "Gender value is out of the enumeration range");
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
