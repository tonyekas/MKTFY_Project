using System;

namespace MKTFY.Models.ViewModels
{
    public class ListingUpdateVM
    {
        //Price = src.Price;
        //TypeOfListing = src.TypeofListing;
        public Guid Id { get; set; }
        public string Location { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
        public string Category { get; set; }
        public string ProductName { get; set; }
        public string Condition { get; set; }
        public string UserListing { get; set; }
    }
}
