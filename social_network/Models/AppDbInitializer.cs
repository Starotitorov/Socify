using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace social_network.Models
{
    public class AppDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var role1 = new IdentityRole { Name = "Admin" };
            var role2 = new IdentityRole { Name = "User" };

            roleManager.Create(role1);
            roleManager.Create(role2);

            var admin = new ApplicationUser
            {
                UserName = "starotitorov1997@gmail.com",
                FirstName = "Artem",
                LastName = "Starotitorov",
                BirthDate = new DateTime(1997, 4, 2),
                Email = "starotitorov1997@gmail.com",
                Gender = "Male",
                HomeTown = "Minsk"
            };
            
            string password = "Artem_1997";

            var result = userManager.Create(admin, password);

            if (result.Succeeded)
            {
                userManager.AddToRole(admin.Id, role1.Name);
            }

            for (int i = 0; i < 20; ++i)
            {
                var user = new ApplicationUser
                {
                    UserName = $"user{i + 1}@example.com",
                    FirstName = $"User{i + 1}",
                    LastName = $"Example{ i + 1}",
                    BirthDate = new DateTime(1996, 10, 12),
                    Email = $"user{i + 1}@example.com",
                    Gender = "Male",
                    HomeTown = "Minsk"
                };

                password = "password";

                result = userManager.Create(user, password);

                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, role2.Name);
                }
            }

            context.SaveChanges();
        }
    }
}