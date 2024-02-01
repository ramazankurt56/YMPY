using ITDeskServer.Context;
using ITDeskServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ITDeskServer.Middleware
{
    public static class ExtensionsMiddleware
    {
        public static void AutoMigration( WebApplication app)
        {
            using (var scoped = app.Services.CreateScope())
            {
                var context = scoped.ServiceProvider.GetRequiredService<AppDbContext>();
                context.Database.Migrate();
            }
        }
        public static void CreateFirstUser(WebApplication app)
        {
            using (var scoped = app.Services.CreateScope())
            {
                var userManager = scoped.ServiceProvider.GetRequiredService<UserManager<User>>();
                if (!userManager.Users.Any())
                {
                    userManager.CreateAsync(new()
                    {
                        Email = "test@test.com",
                        UserName = "test",
                        FirstName = "ramazan",
                        LastName = "kurt",
                        EmailConfirmed=true
                    }, "Password12*").Wait();
                }
            }
        }
    }
}
