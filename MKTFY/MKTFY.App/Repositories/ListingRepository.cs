using Microsoft.EntityFrameworkCore;
using MKTFY.App.Repositories.Interfaces;
using MKTFY.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKTFY.App.Repositories
{
    public class ListingRepository : BaseRepository<Listing, Guid, ApplicationDbContext>, IListingRepository
    {

        public ListingRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<Listing> UpdateListing(Listing list) //Added "List"
        {
            //var updateList = await _entityDbset.FirstOrDefaultAsync(uplist => uplist.Id == list.Id);
            //_context.Listings.UpdateRange(list);
            //await _context.SaveChangesAsync();
            //return list;

            var updateList = await _entityDbset.FirstOrDefaultAsync(uplist => uplist.Id == list.Id); // Ok, worked well
            updateList.ProductName = list.ProductName;
            updateList.Location = list.Location;
            updateList.Description = list.Description;
            updateList.Price = list.Price;
            updateList.Category = list.Category;
            updateList.Condition = list.Condition;

            _entityDbset.Update(updateList);
            await _context.SaveChangesAsync();

            return updateList;
            //..................................................//
            //return new ListingManageVM { };
            //var updateList = await _entityDbset.FindAsync(list.Id).ConfigureAwait(false);
            //....................................................//
            //IEnumerable<Listing> updatelist = await _context.Listings.Where(uplist => uplist.Id == list.Id).ToListAsync();
            //List<Listing> result = new List<Listing>();
            //foreach (var item in updatelist)
            //{
            //    Listing items = new Listing(item);
            //    //result.Update(items);
            //    _entityDbset.Update(items);
            //}
            //await _context.SaveChangesAsync();
            //return result;
        }

        public async Task<Search> Search(Search src)
        {
            Guid id = src.Id;
            string category = src.CategoryId;
            string item = src.ItemName;

            var query = new Search(id, category, item);
            var output = await _context.Searches.AddAsync(query);
            await _context.SaveChangesAsync();

            return output.Entity;
        }
        public async Task<IEnumerable<Listing>> SearchList(string price, string productname)
        {
            IQueryable<Listing> search = _context.Listings;

            if (!string.IsNullOrEmpty(productname))
            {
                search = search.Where(s => s.Description.Contains(price)
                    || s.ProductName.Contains(productname));
            }
            await _context.SaveChangesAsync();

            return await search.ToListAsync();
        }

        // UserListing items
        public async Task<List<Listing>> UserListing(string userid)
        {
            List<Listing> result = await _context.Listings.Where(list => list.UserId == userid).ToListAsync();
            //List<Listing> result = new List<Listing>();
            //foreach (var item in userListItem)
            //{
            //    Listing items = new Listing(item);
            //    result.Add(items);
            //}
            return result;
        }
        public async Task<List<Listing>> GetListByCat(Guid catId)
        {
            var itemList = await _context.Listings.Where(k => k.CategoryId == catId).ToListAsync();
            var itemcat = await _context.Categories.Where(k => k.Id == catId).ToListAsync();
            var cat = itemcat[0];
            var categoryList = cat.Listings.ToList();

            return categoryList;
        }
        /*
        public async Task<List<Listing>> UserSearchItems(Guid id, string userid, string item)
        {
            var itemSearch = new Search(id, userid, item);

            var items = await _context.Searches.AddAsync(itemSearch); // add the above search item
            await _context.SaveChangesAsync();

            if (item != null)
            {
                
            }
            return ;  //_entityDbset.ToListAsync;

        }*/
    }
}
