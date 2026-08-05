using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.Identity;
using ECommerce.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Seeding
{
    public class IdentityDataSeeder : IDataSeeder
    {
        private readonly StoreIdentityDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ILogger<IdentityDataSeeder> logger;

        public IdentityDataSeeder(StoreIdentityDbContext dbContext,
            UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager,ILogger<IdentityDataSeeder> logger)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.logger = logger;
        }
        public async Task SeedAsync(CancellationToken ct = default)
        {
            try
            {
                var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync(ct);

                if (pendingMigrations.Any())
                    await dbContext.Database.MigrateAsync(ct);

                if (!await roleManager.Roles.AnyAsync(ct))
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                    await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }
                if (!await userManager.Users.AnyAsync(ct))
                {
                    var admin = new ApplicationUser()
                    {
                        DisplayName = "Mohamed Elgazar",
                        Email = "mohady@gmail.com",
                        UserName = "Mohamed",
                        PhoneNumber = "01002420630",
                    };
                    var result = await userManager.CreateAsync(admin, "P@ssW0rd");

                    if (result.Succeeded)
                        await userManager.AddToRoleAsync(admin, "Admin");
                    else
                        logger.LogWarning("The User did not Create");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Can not Seed The Data");
                throw;
            }
        }
    }
}
