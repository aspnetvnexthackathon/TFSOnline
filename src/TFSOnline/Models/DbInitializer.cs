using Microsoft.Data.Entity.SqlServer;
using Microsoft.Framework.DependencyInjection;
using System;
using System.Threading.Tasks;
using Microsoft.Framework.OptionsModel;
using Microsoft.AspNet.Identity;
using System.Security.Principal;
using System.Security.Claims;

namespace TFSOnline.Models
{
    /// <summary>
    /// Summary description for DbInitializer
    /// </summary>
    public class DbInitializer
    {
        public static async Task InitializeTFSOnlineDatabaseAsync(IServiceProvider serviceProvider)
        {
            using (var db = serviceProvider.GetService<TFSOnlineContext>())
            {
                var sqlServerDataStore = db.Configuration.DataStore as SqlServerDataStore;
                if (sqlServerDataStore != null)
                {
                    await db.Database.EnsureCreatedAsync();
                    await CreateAdminUser(serviceProvider);
                }
            }
        }

        private static async Task CreateAdminUser(IServiceProvider serviceProvider)
        {
            var options = serviceProvider.GetService<IOptionsAccessor<TFSOnlineContextOptions>>().Options;
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            var user = await userManager.FindByNameAsync(options.DefaultAdminUserName);
            if (user == null)
            {
                user = new ApplicationUser { UserName = options.DefaultAdminUserName };
                await userManager.CreateAsync(user, options.DefaultAdminPassword);
                await userManager.AddClaimAsync(user, new Claim("CanMakeAnnouncement", "Allowed"));
            }
        }
    }
}