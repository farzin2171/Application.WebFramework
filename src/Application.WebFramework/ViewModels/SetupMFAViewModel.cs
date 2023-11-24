using System.ComponentModel.DataAnnotations;

namespace Application.WebFramework.ViewModels
{
    public class SetupMFAViewModel
    {
        public string? Key { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string SecurityCode { get; set; } = string.Empty;

        public Byte[]? QRCodeBytes { get; set; }
    }
}
