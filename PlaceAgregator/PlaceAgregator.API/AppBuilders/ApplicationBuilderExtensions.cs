using Microsoft.AspNetCore.Identity;
using PlaceAgregator.EntityFramework;
using PlaceAgregator.Shared.Models;

namespace PlaceAgregator.API.AppBuilders
{

    public static class ApplicationBuilderExtensions
    {
        public static async Task<IApplicationBuilder> SeedDatabase(this WebApplication app, bool recreate)
        {
            app.Logger.LogInformation("App created...");

            app.Logger.LogInformation("Seeding Database...");

            using (var scope = app.Services.CreateScope())
            {
                var scopedProvider = scope.ServiceProvider;
                try
                {
                    var userManager = scopedProvider.GetRequiredService<UserManager<AppUser>>();
                    var roleManager = scopedProvider.GetRequiredService<RoleManager<IdentityRole>>();
                    var appDbContext = scopedProvider.GetRequiredService<ApplicationDbContext>();

                    await ApplicationDbContextSeed.SeedAsync(
                        appDbContext: appDbContext,
                        userManager: userManager,
                        roleManager: roleManager,
                        recreate: recreate);

                    app.Logger.LogInformation($"Database seeded");
                }
                catch (Exception ex)
                {
                    app.Logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }


            return app;
        }
    }
}
