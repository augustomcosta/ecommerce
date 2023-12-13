using ecommerce_api.Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace ecommerce_api.Data.Identity;

public class AppIdentityDbContextSeed
{
    public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
    {
        if (!userManager.Users.Any())
        {
            var user = new AppUser
            {
                DisplayName = "Bob",
                Email = "bob@test.com",
                UserName = "bob@test.com",
                Address = new Address
                {
                    FirstName = "Bob",
                    LastName = "Bobbity",
                    Street = "10 The Street",
                    State = "New York",
                    City = "NY",
                    ZipCode = "90210"
                }
            };
            await userManager.CreateAsync(user, "Pa$$w0rd");
        }
    }    
}