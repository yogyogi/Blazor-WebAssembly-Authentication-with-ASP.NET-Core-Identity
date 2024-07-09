using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthServer;

public class SeedData
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        using var context = new AppDbContext(serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>());
        context.Database.EnsureCreated();
        if (context.Users.Any())
        {
            return;
        }

        string[] roles = [ "Administrator", "Manager" ];
        using var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        using var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

        var user = new AppUser
        {
            Email = "yogi@yogihosting.com",
            NormalizedEmail = "YOGI@YOGIHOSTING.COM",
            UserName = "yogi@yogihosting.com",
            NormalizedUserName = "YOGI@YOGIHOSTING.COM",
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString("D")
        };

        //var password = new PasswordHasher<AppUser>();
        //var hashed = password.HashPassword(user, "Passw0rd!");
        //user.PasswordHash = hashed;

        await userManager.CreateAsync(user, "Passw0rd!");
        await userManager.AddToRolesAsync(user, roles);

        //await userManager.AddToRolesAsync(user, roles);
        //var userStore = new UserStore<AppUser>(context);
        //var result = userStore.CreateAsync(user);

        await context.SaveChangesAsync();
    }
}
