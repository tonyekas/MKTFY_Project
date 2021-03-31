using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MKTFY.Models.Entities
{
    public class Category : BaseEntity<Guid>
    {
        // creating the Category to inherit from the base class with a function
        public Category()
            : base() { }

        public Category(string category) : base()
        {
            CategoryName = category;
        }
        public string CategoryName { get; set; }

        public ICollection<Listing> Listings { get; set; }
    }
}
