using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using webapplication.Models;

namespace webapplication.Data
{
    public class SeedDB
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            context.Database.EnsureCreated();
            if (!context.Users.Any())
            {
                User user = new User()
                {
                    Email = "paul@mail.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "paul"
                };
                userManager.CreateAsync(user, "Paul@123");
            }
        }
    }
}