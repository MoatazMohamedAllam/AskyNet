using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.Identity
{
    public class IdentitySeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    FirstName = "Moataz",
                    LastName = "Allam",
                    Email = "moataz@domain.com",
                };
               
                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}
