using Application.Data.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Application.WebFramework.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<User> signInManager;
        public LogoutModel(SignInManager<User> signInManager)
        {
            this.signInManager = signInManager; 
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await signInManager.SignOutAsync();
            return RedirectToPage("/Index");
        }
    }
}
