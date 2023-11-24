using System.ComponentModel.DataAnnotations;

namespace Application.WebFramework.ViewModels
{
    public class UserProfileViewModel
    {
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Department { get; set; } = string.Empty;

        [Required]
        public string Position { get; set; } = string.Empty;
    }
}
