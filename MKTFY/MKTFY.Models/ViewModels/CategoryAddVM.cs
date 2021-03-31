using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MKTFY.Models.ViewModels
{
    public class CategoryAddVM
    {
        [Required]
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string Deals { get; set; }
    }
}
