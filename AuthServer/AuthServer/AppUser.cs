using Microsoft.AspNetCore.Identity;

namespace AuthServer
{
    class AppUser : IdentityUser
    {
        public IEnumerable<IdentityRole>? Roles { get; set; }
    }
}
