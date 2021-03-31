using Microsoft.EntityFrameworkCore;
using MKTFY.App.Repositories.Interfaces;
using MKTFY.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.App.Repositories
{
    public class CategoryRepository : BaseRepository<Category, Guid, ApplicationDbContext>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {

        }
        public async Task<Category> UpdateCat(Category cat)
        {
            var updateCat = await _entityDbset.FirstOrDefaultAsync(src => src.Id == cat.Id);
            //var updateCat = await _entityDbset.FindAsync(cat.Id).ConfigureAwait(false);
            updateCat.CategoryName = cat.CategoryName;

            _entityDbset.Update(updateCat);
            await _context.SaveChangesAsync();

            //return new ListingManageVM { };
            return updateCat;
        }

        public async Task<Category> AddCat(string cat)
        {
            var updateCat = new Category(cat); // await _entityDbset.FindAsync("cat");
            //var updateCat = await _entityDbset.FindAsync(cat.Id).ConfigureAwait(false);

            //updateCat.CategoryName = new Category();


            _entityDbset.Add(updateCat);
            await _context.SaveChangesAsync();

            //return new ListingManageVM { };
            return updateCat;
        }
        public async Task<Category> FindCatByName(string cat)
        {
            //Category dbEntry = new Category();
            var checkCat = await _context.Categories.FirstOrDefaultAsync(cName => cName.CategoryName == cat);
            //var checkCat = new Category();


            return checkCat;
        }

    }
}
