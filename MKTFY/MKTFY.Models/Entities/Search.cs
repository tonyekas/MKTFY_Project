using MKTFY.Models.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace MKTFY.Models.Entities
{
    public class Search : BaseEntity<Guid>
    {
        private string user;
        private string category;
        private string item;

        public Search()
             : base() { }

        //public query; //Item() : base() { }
        //public Search(User user, string category, string item)
        //{
        //    UserId = src.Id; //UserId;
        //    category = CategoryId;
        //    item = ItemName;
        //}
        public Search(SearchAddVM src)
              : base()
        {
            ItemName = src.ItemName;
            UserId = src.UserId;
            CategoryId = src.CategoryId;
        }

        public Search(Guid id, string category, string item)
        {
            this.Id = id;
            this.category = category;
            this.item = item;
        }
        [Required]
        public string ItemName { get; set; }
        public string CategoryId { get; set; } // For users to search category section
        [Required]
        public string UserId { get; set; }
        public User User { get; set; } // One to many relationship as One User can have many Searches under his profile
    }
}
