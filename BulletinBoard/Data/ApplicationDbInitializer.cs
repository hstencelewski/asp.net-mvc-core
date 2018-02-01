using System.Linq;
using Project.Helpers;
using Project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Project.Data
{
    public class ApplicationDbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationDbInitializer(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public void Seed()
        {
            _context.Database.Migrate();
            
            if (!_context.Roles.Any())
            {
                var roleNames = new[]
                {
                    RoleHelper.Administrator,
                    RoleHelper.Moderator,
                    RoleHelper.User
                };

                foreach (var roleName in roleNames)
                {
                    var role = new IdentityRole(roleName) { NormalizedName = RoleHelper.Normalize(roleName) };
                    _context.Roles.Add(role);
                }
            }
            
            if (!_context.ApplicationUsers.Any())
            {
                const string userName = "admin@admin.com";
                const string userPass = "admin";

                var user = new ApplicationUser { UserName = userName, Email = userName };
                _userManager.CreateAsync(user, userPass).Wait();
                _userManager.AddToRoleAsync(user, RoleHelper.Administrator).Wait();
            }

            _context.SaveChanges();
        }
    }
}
