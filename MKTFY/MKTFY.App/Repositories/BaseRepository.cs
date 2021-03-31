using Microsoft.EntityFrameworkCore;
using MKTFY.App.Exceptions;
using MKTFY.App.Repositories.Interfaces;
using MKTFY.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKTFY.App.Repositories
{
    public class BaseRepository<TEntity, TPrimaryKey, TDbContext> : IBaseRepository<TEntity, TPrimaryKey>
        where TEntity : BaseEntity<TPrimaryKey>
        where TDbContext : DbContext
    {
        protected readonly TDbContext _context;
        protected DbSet<TEntity> _entityDbset;

        public BaseRepository(TDbContext context)
        {
            _context = context;
            _entityDbset = context.Set<TEntity>();
        }
        public async Task<TEntity> Create(TEntity src)
        {
            //// Create the new entity
            //var entity = new TEntity(src);

            // Add and save the entity
            _entityDbset.Add(src);
            await _context.SaveChangesAsync();

            return src;
        }

        public async Task<TEntity> Get(TPrimaryKey id)
        {
            // Get the entity
            //var result = await from listing in _context.Listings where listing.Id == id select listing;
            //var result = await _entityDbset.FirstAsync(i => i.Id == id);

            var result = await _entityDbset.FindAsync(id).ConfigureAwait(false);

            if (result == null)
            {
                throw new NotFoundException("Item ID " + id + " is not found");
            }

            return result;
        }

        public async Task<List<TEntity>> GetAll()
        {
            // Get the entities
            var results = await _entityDbset.ToListAsync();

            // //Below codes are to be removed for easier clean up
            //var models = new List<ListingVM>();
            //foreach (var entity in results)
            //    models.Add(new ListingVM(entity));

            return results;
        }

        public async Task Delete(TPrimaryKey id)
        {
            //var result = await _entityDbset.FirstAsync(i => i.Id == id);
            var result = await _entityDbset.FindAsync(id).ConfigureAwait(false);
            if (result == null)
            {
                throw new DllNotFoundException("Item with " + id + " not found");
            }
            _entityDbset.Remove(result);
            await _context.SaveChangesAsync();
        }
    }
}
