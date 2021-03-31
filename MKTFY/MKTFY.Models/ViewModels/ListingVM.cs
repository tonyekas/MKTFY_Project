using MKTFY.Models.Entities;
using System;

namespace MKTFY.Models.ViewModels
{
    public class ListingVM
    {
        public ListingVM(Listing src)
        {
            Id = src.Id;
            Condition = src.Condition;
            Description = src.Description;
            ProductName = src.ProductName;
            Price = src.Price;
            Location = src.Location;
            CategoryId = src.CategoryId;

        } //Price = src.Price;
          //TypeOfListing = src.TypeofListing;
        public Guid Id { get; set; }
        public string Condition { get; set; }

        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Location { get; set; }
        public Guid CategoryId { get; set; } // Adding a foreign Key may not be needed here
        public string Category { get; set; } //Category = src.Category;

        public string ProductName { get; set; }
    }

}
