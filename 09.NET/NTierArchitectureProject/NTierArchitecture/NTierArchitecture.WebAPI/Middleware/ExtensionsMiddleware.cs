using Microsoft.AspNetCore.Identity;
using NTierArchitecture.Entities.Models;

namespace NTierArchitecture.WebAPI.Middleware
{
    public static class ExtensionsMiddleware
    {
        public static void CreateFirstUser(WebApplication app)
        {
            using (var scoped = app.Services.CreateScope())
            {
                var userManager = scoped.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                if (!userManager.Users.Any())
                {
                    userManager.CreateAsync(new()
                    {
                        Email = "ramazankurt670@gmail.com",
                        UserName = "RamazanKurt",
                        FirstName = "Ramazan",
                        LastName = "Kurt",
                        EmailConfirmed = true
                    }, "Password12*").Wait();
                }
            }
        }
    }
}
