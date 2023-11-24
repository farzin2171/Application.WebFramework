using Application.Data.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Application.WebFramework.Pages.Account
{
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<User> _userManager;

        public ConfirmEmailModel(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public string Message { get; set; } = string.Empty;
        public async Task<IActionResult> OnGetAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    Message = "Email address is successfully confimred, you can now try to login.";
                    return Page();
                }
            }

            this.Message = "Failed to validate email.";
            return Page();
        }
    }
}
