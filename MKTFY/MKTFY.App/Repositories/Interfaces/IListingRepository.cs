using MKTFY.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKTFY.App.Repositories.Interfaces
{
    public interface IListingRepository : IBaseRepository<Listing, Guid>
    {
        Task<Listing> UpdateListing(Listing list);
        //Task<List<Listing>> UpdateListing(Listing list);
        Task<IEnumerable<Listing>> SearchList(string price, string productname); // Can be used for general search
        Task<Search> Search(Search src); // Searching for specific searches within User Categories
        Task<List<Listing>> UserListing(string UserId);
    }
}
