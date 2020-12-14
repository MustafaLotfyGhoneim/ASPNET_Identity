using ASPNET_Identity.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System;

[assembly: OwinStartupAttribute(typeof(ASPNET_Identity.Startup))]
namespace ASPNET_Identity
{
    public partial class Startup
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRoles();
            CreateUsers(); 
        }
        public void CreateUsers()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = new ApplicationUser();
            user.Email = "MustafaLotfy@mustafa.com";
            user.UserName = "Mustafa";
            var check = userManager.Create(user, "Mustafa@2020");
            if (check.Succeeded)
            {
                userManager.AddToRole(user.Id, "Admins");
            }
        }
        public void CreateRoles()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            if (!roleManager.RoleExists("Admins"))
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admins";
                roleManager.Create(role);
            }
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Authors";
                roleManager.Create(role);
            }
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Readers";
                roleManager.Create(role);
            }
        }

    }
}
