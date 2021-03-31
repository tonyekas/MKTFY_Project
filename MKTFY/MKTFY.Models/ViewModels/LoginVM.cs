using System.ComponentModel.DataAnnotations;

namespace MKTFY.Models.ViewModels
{
    public class LoginVM
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string ClientId { get; set; }
    }
}
