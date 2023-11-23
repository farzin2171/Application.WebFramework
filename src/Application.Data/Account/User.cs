using Microsoft.AspNetCore.Identity;

namespace Application.Data.Account
{
    public class User : IdentityUser
    {
        public string Name { get; set; } = string.Empty;
        public string Family { get; set; } = string.Empty;
        public string ProfileImage { get; set; } = string.Empty;
    }
}
