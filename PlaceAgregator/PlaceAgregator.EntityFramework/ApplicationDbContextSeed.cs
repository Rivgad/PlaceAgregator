using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PlaceAgregator.Shared.Extensions;
using PlaceAgregator.Shared.Models;
using PlaceAgregator.Shared.Models.Enums;

namespace PlaceAgregator.EntityFramework
{

    public static class ApplicationDbContextSeed
    {
        public static async Task SeedAsync(
            ApplicationDbContext appDbContext,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            bool recreate
            )
        {
            if (appDbContext.Database.IsNpgsql())
            {
                appDbContext.Database.Migrate();
            }

            await SeedRolesAsync(roleManager);
            await SeedUsersAsync(userManager, recreate: recreate);
        }
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            var rolesNames = Enum.GetValues(typeof(Role))
                .Cast<Role>()
                .Select(roleEnum => roleEnum.GetDescriptionAttribute().ToLower())
                .ToList();

            foreach (string role in rolesNames)
            {
                if (!roleManager.Roles.Any(r => r.Name == role))
                {
                    await roleManager.CreateAsync(new IdentityRole() { Name = role.ToLower(), NormalizedName = role.ToUpper() });
                }
                else
                {
                    var roleToUpdate = await roleManager.FindByNameAsync(role);
                    roleToUpdate.NormalizedName = role.ToUpper();
                    roleToUpdate.Name = role.ToLower();

                    await roleManager.UpdateAsync(roleToUpdate);
                }
            }
        }
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager, bool recreate)
        {
            string defaultPassword = "P@ssword1";

            string defaultUserEmail = "user@test.com";
            string defaultUserName = "User";

            string defaultAdminEmail = "admin@test.com";
            string defaultAdminUserName = "Admin";

            string defaultManagerEmail = "manager@test.com";
            string defaultManagerUserName = "Manager";

            Dictionary<Role, AppUser> defaultUsersDict = new Dictionary<Role, AppUser>();
            defaultUsersDict.Add(Role.User,
                new AppUser
                {
                    UserName = defaultUserName,
                    Email = defaultUserEmail,
                    EmailConfirmed = true
                });
            defaultUsersDict.Add(Role.Manager,
                new AppUser
                {
                    UserName = defaultManagerUserName,
                    Email = defaultManagerEmail,
                    EmailConfirmed = true
                });
            defaultUsersDict.Add(
                Role.Admin,
                new AppUser
                {
                    UserName = defaultAdminUserName,
                    Email = defaultAdminEmail,
                    EmailConfirmed = true
                });

            foreach (var roleUserPair in defaultUsersDict)
            {
                var existedUser = await userManager.FindByNameAsync(roleUserPair.Value.UserName);
                if (existedUser != null)
                {
                    if (recreate)
                    {
                        await userManager.DeleteAsync(existedUser);
                        await userManager.CreateAsync(roleUserPair.Value, defaultPassword);
                        await userManager.AddToRoleAsync(roleUserPair.Value, roleUserPair.Key.GetDescriptionAttribute());
                    }
                }
                else
                {
                    await userManager.CreateAsync(roleUserPair.Value, defaultPassword);
                    await userManager.AddToRoleAsync(roleUserPair.Value, roleUserPair.Key.GetDescriptionAttribute());
                }
            }
        }


    }
}
