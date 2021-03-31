using System;
using System.ComponentModel.DataAnnotations;

namespace MKTFY.Models.ViewModels
{
    public class ListingAddVM
    {
        [Required]
        public string ProductName { get; set; }

        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        // A user may be allowed to list a product without an address
        public string Condition { get; set; }
        // The Category may not be critically essential as some products may not be categorized by a User
        public string Location { get; set; }
        public Guid CategoryId { get; set; }
        public string UserId { get; set; }
    }
}
