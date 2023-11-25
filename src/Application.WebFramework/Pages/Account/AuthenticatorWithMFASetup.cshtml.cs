using Application.Data.Account;
using Application.Services.QRCode;
using Application.WebFramework.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Application.WebFramework.Pages.Account
{
    public class AuthenticatorWithMFASetupModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly IQRCodeService _qrCodeService;

        [BindProperty]
        public SetupMFAViewModel viewModel { get; set; }

        [BindProperty]
        public bool Succeeded { get; set; }

        public AuthenticatorWithMFASetupModel(UserManager<User> userManager , IQRCodeService qrCodeService)
        {
            _userManager = userManager;
            _qrCodeService = qrCodeService;
            viewModel = new SetupMFAViewModel();
            Succeeded = false;
        }


        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                await _userManager.ResetAuthenticatorKeyAsync(user);
                var key = await _userManager.GetAuthenticatorKeyAsync(user);
                viewModel.Key = key ?? String.Empty;
                viewModel.QRCodeBytes = _qrCodeService.GenerateQRCodeBytes(
                   "my web app",
                   this.viewModel.Key,
                   user.Email ?? string.Empty);
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var user = await _userManager.GetUserAsync(base.User);
            if (user != null && await _userManager.VerifyTwoFactorTokenAsync(
                    user,
                    _userManager.Options.Tokens.AuthenticatorTokenProvider,
                    viewModel.SecurityCode))
            {
                await _userManager.SetTwoFactorEnabledAsync(user, true);
                this.Succeeded = true;
            }
            else
            {
                ModelState.AddModelError("AuthenticatorSetup", "Something went wrong with the authenticator setup.");
            }

            return Page();
        }
    }
}
