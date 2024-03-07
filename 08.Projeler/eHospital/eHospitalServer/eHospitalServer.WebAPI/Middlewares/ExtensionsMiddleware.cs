using eHospitalServer.Entities.Enum;
using eHospitalServer.Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace eHospitalServer.WebAPI.Middlewares;

public class ExtensionsMiddleware
{
    public static void CreateFirstUser(WebApplication app)
    {
        using (var scoped = app.Services.CreateScope())
        {
            var userManager = scoped.ServiceProvider.GetRequiredService<UserManager<User>>();

            if (!userManager.Users.Any(p => p.UserName == "admin"))
            {
                User user = new()
                {
                    UserName = "admin",
                    Email = "admin@admin.com",
                    FirstName = "Ramazan",
                    LastName = "Kurt",
                    IdentityNumber = "11111111111",
                    FullAddress = "İstanbul",
                    DateOfBirth = DateOnly.Parse("15.08.1998"),
                    EmailConfirmed = true,
                    IsActive = true,
                    IsDeleted = false,
                    BloodType = "A rh-",
                    UserType = UserType.Admin
                };

                userManager.CreateAsync(user, "1").Wait();
            }
        }
    }
}