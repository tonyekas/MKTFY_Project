using MKTFY.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MKTFY.Models.ViewModels
{
    public class SearchAddVM
    {
        public SearchAddVM() : base() { }
        public SearchAddVM(SearchVM src)
              : base()
        {
            ItemName = src.ItemName;
            UserId = src.UserId;
            CategoryId = src.CategoryId;
        }
        public string ItemName { get; set; }
        public string UserId { get; set; }
        public string CategoryId { get; set; }

    }
}
