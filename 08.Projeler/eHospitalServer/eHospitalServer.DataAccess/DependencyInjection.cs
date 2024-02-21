using eHospitalServer.DataAccess.Context;
using eHospitalServer.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;

namespace eHospitalServer.DataAccess;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccess(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>((options) =>
        {
            options
                .UseNpgsql(configuration.GetConnectionString("PostgreSQL"))
                .UseSnakeCaseNamingConvention();
        });

        return services;
    }
    public static IServiceCollection AddIdentityAccess(
        this IServiceCollection services)
    {
        services.AddIdentity<User, IdentityRole<Guid>>(x =>
        {       
        }).AddEntityFrameworkStores<ApplicationDbContext>();
        return services;
    }
}