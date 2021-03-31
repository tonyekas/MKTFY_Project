using MKTFY.Models.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace MKTFY.Models.Entities
{
    public class Listing : BaseEntity<Guid>
    {
        public Listing()
            : base() { }

        //public Listing(Listing src)
        //    : base()
        //{
        //}
        public Listing(Listing src)
            : base()
        {
            Condition = src.Condition;
            Description = src.Description;
            Location = src.Location;
            Price = src.Price;
            ProductName = src.ProductName;
            CategoryId = src.CategoryId;
            UserId = src.UserId;
            Id = src.Id;
        }

        public Listing(ListingAddVM src)
              : base()
        {
            Condition = src.Condition;
            Description = src.Description;
            Location = src.Location;
            Price = src.Price;
            ProductName = src.ProductName;
            CategoryId = src.CategoryId;
            UserId = src.UserId;
        }

        public Listing(ListingUpdateVM src)
        {
            Id = src.Id;
            Location = src.Location;
            Description = src.Description;
            Price = src.Price;
            ProductName = src.ProductName;
            Condition = src.Condition;
        }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string Condition { get; set; }

        // The below properties define the ONE to MANY relationship: for Users and Category

        public Guid CategoryId { get; set; } // This may be needful in this context
        public Category Category { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
