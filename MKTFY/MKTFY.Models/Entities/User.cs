using Microsoft.AspNetCore.Identity;
using MKTFY.Models.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace MKTFY.Models.Entities
{
    public class User : IdentityUser

    {

        public User()
            : base() { }

        public User(UserVM user) : base()
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            PhoneNumber = user.PhoneNumber;
            // Address = user.Address; 

        }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        //public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
