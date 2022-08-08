using IdentityModel;
using Mango.Services.Identity.DBContexts;
using Mango.Services.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Mango.Services.Identity.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            if (_roleManager.FindByIdAsync(SD.Admin).GetAwaiter().GetResult() == null)
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Customer)).GetAwaiter().GetResult();
            }
            else
            {
                return;
            }

            //First User
            ApplicationUser adminUser = new()
            {
                UserName = "adminuser",
                Email = "admin1@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "111222333",
                FirstName = "Ben",
                LastName = "Admin"

            };

            _userManager.CreateAsync(adminUser, "Admin123!").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(adminUser, SD.Admin).GetAwaiter().GetResult();

            var temp1 = _userManager.AddClaimsAsync(adminUser, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, adminUser.FirstName + " " + adminUser.LastName),
                new Claim(JwtClaimTypes.GivenName, adminUser.FirstName),
                new Claim(JwtClaimTypes.FamilyName, adminUser.LastName),
                new Claim(JwtClaimTypes.Role, SD.Admin)
            }).Result;

            //Second User
            ApplicationUser customerUser = new()
            {
                UserName = "customeruser",
                Email = "customer1@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "333222111",
                FirstName = "Jimmy",
                LastName = "Customer"
            };

            _userManager.CreateAsync(customerUser, "Customer123!").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(customerUser, SD.Customer).GetAwaiter().GetResult();

            var temp2 = _userManager.AddClaimsAsync(customerUser, new Claim[]
            {
                   new Claim(JwtClaimTypes.Name, customerUser.FirstName+ "" +customerUser.LastName),
                   new Claim(JwtClaimTypes.GivenName, customerUser.FirstName),
                   new Claim(JwtClaimTypes.FamilyName, customerUser.LastName),
                   new Claim(JwtClaimTypes.Role, SD.Admin),
            }).Result;
        }
    }
}
