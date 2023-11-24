using System.ComponentModel.DataAnnotations;

namespace Application.WebFramework.ViewModels
{
    public class AuthenticatorMFAViewModel
    {
        [Required]
        [Display(Name = "Code")]
        public string SecurityCode { get; set; } = string.Empty;

        public bool RememberMe { get; set; }
    }
}
