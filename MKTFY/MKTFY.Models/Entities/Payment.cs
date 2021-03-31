using System;
using System.Collections.Generic;
using System.Text;

namespace MKTFY.Models.Entities
{
    public class Payment : BaseEntity<Guid>

    {
        //public string CardNumberId { get; set; }
        //public int Month { get; set; }
        //public int Year { get; set; }
        //public string CVC { get; set; }
        public decimal Value { get; set; }

        // Adding below to show One to MANY relationship
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
