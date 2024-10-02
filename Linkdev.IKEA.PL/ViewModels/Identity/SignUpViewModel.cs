using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Linkdev.IKEA.PL.ViewModels.Identity
{
    public class SignUpViewModel
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;

        [EmailAddress]
        public string Email { get; set; } = null!;        
        
        [Display(Name = "Username")]
        [MinLength(10)]
        public string UserName { get; set; } = null!;

        public string Password { get; set; } = null!;

        [Display(Name = "Confirm Password")]
        [Compare(nameof(Password), ErrorMessage = "The Confirmed Password must match the provided password")]
        public string ConfirmPassword { get; set; } = null!;

        [Required(ErrorMessage = "You Must Agree on the terms & conditions")]
        public bool IsAgreed { get; set; }
    }
}
