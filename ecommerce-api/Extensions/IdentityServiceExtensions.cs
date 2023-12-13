using ecommerce_api.Data.Identity;
using ecommerce_api.Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ecommerce_api.Extensions;

public static class IdentityServiceExtensions
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<AppIdentityDbContext>(options =>
        {
            options.UseNpgsql("IdentityConnection");
        });

        services.AddIdentityCore<AppUser>(opt =>
        {
            // app identity options here
        })
            .AddEntityFrameworkStores<AppIdentityDbContext>()
            .AddSignInManager<SignInManager<AppUser>>();

        services.AddAuthentication();
        services.AddAuthorization();
        
        return services;
    }
}