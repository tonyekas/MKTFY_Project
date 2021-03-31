using System.ComponentModel.DataAnnotations;

namespace MKTFY.Models.ViewModels
{
    public class ForgetPasswordVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
