using BlogProject_Devskill.Data;
using BlogProject_Devskill.Membership.Contexts;
using BlogProject_Devskill.Membership.Entities;
using BlogProject_Devskill.Membership.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject_Devskill.Membership.Data
{
    public class AccountSeed : DataSeed
    {
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationRoleManager _roleManager;

        private readonly ApplicationUser _superAdminUser, _adminUser, _customerUser;
        private readonly ApplicationRole _superAdminRole, _adminRole, _customerRole;

        public AccountSeed(ApplicationUserManager userManager, ApplicationRoleManager roleManager, ApplicationDbContext context)
            : base(context)
        {
            _userManager = userManager;
            _roleManager = roleManager;

            _adminUser = new ApplicationUser("admin", "Talat Morshed", "8801817316436", "admin@gmail.com");

            _adminRole = new ApplicationRole("Administrator");
        }

        private async Task<bool> CheckAndCreateRoleAsync(ApplicationRole role)
        {
            if ((await _roleManager.FindByNameAsync(role.Name)) == null)
            {
                var result = await _roleManager.CreateAsync(role);
                return result.Succeeded;
            }
            return true;
        }

        private async Task SeedUserAsync()
        {
            IdentityResult result = null;

            if ((await _userManager.FindByNameAsync(_adminUser.UserName.ToUpper())) == null)
            {
                result = await _userManager.CreateAsync(_adminUser, "Tan#123");
                if (result.Succeeded)
                {
                    if (await CheckAndCreateRoleAsync(_adminRole))
                    {
                        await _userManager.AddToRoleAsync(_adminUser, _adminRole.Name);
                    }
                }
            }
        }

        public override async Task SeedAsync()
        {
            await SeedUserAsync();
        }

    }
}
