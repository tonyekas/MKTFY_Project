using MKTFY.Models.Entities;
using System;
using System.Collections.Generic;

namespace MKTFY.Models.ViewModels
{
    public class UserVM
    {
        public UserVM()
            : base()
        {
        }


        public UserVM(User src)
        {
            Id = src.Id;
            Email = src.Email;
            FirstName = src.FirstName;
            LastName = src.LastName;
            Address = src.Address;
            PhoneNumber = src.PhoneNumber;
        }

        ////Below is added to hold as collection of what the User would be purchasing
        //public UserVM()
        //{
        //    ItemsInCarts = new List<string>();

        //}

        public String Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public object Address { get; set; }
        public string PhoneNumber { get; set; }

        //public UserVM(User result)
        //{
        //    Id = result.Id;
        //    Email = result.Email;
        //    FirstName = result.FirstName;
        //    LastName = result.LastName;
        //    PhoneNumber = result.PhoneNumber;
        //}
    }
}
