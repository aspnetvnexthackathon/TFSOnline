using Microsoft.Data.Entity.SqlServer;
using Microsoft.Framework.DependencyInjection;
using System;
using System.Threading.Tasks;
using Microsoft.Framework.OptionsModel;
using Microsoft.AspNet.Identity;
using System.Security.Principal;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.Data.Entity;
using System.Linq;

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
                    if (await db.Database.EnsureCreatedAsync())
                    {
                        //await InsertTestData(serviceProvider);
                    }
                    await CreateAdminUser(serviceProvider);
                }
            }
        }

        private static async Task InsertTestData(IServiceProvider serviceProvider)
        {
            var bugs = GetBugs();
            await AddOrUpdateAsync(serviceProvider, a => a.BugId, bugs);
        }

        private static async Task AddOrUpdateAsync<TEntity>(
            IServiceProvider serviceProvider,
            Func<TEntity, object> propertyToMatch, IEnumerable<TEntity> entities)
            where TEntity : class
        {
            // Query in a separate context so that we can attach existing entities as modified
            List<TEntity> existingData;
            using (var db = serviceProvider.GetService<TFSOnlineContext>())
            {
                existingData = db.Set<TEntity>().ToList();
            }

            using (var db = serviceProvider.GetService<TFSOnlineContext>())
            {
                foreach (var item in entities)
                {
                    db.ChangeTracker.Entry(item).State = existingData.Any(g => propertyToMatch(g).Equals(propertyToMatch(item)))
                        ? EntityState.Modified
                        : EntityState.Added;
                }

                await db.SaveChangesAsync();
            }
        }

        private static Bug[] GetBugs()
        {
            var bugs = new Bug[]
            {
                new Bug() { AssignedTo = "test1", BugId = 1, CreatedBy = "test1", BugTitle = "Bug1", Description = "Description 1", Priority = 0, State = BugState.Active },
                new Bug() { AssignedTo = "test1", BugId = 2, CreatedBy = "test1", BugTitle = "Bug2", Description = "Description 2", Priority = 1, State = BugState.Active },
                new Bug() { AssignedTo = "test1", BugId = 3, CreatedBy = "test1", BugTitle = "Bug3", Description = "Description 3", Priority = 2, State = BugState.Active },
                new Bug() { AssignedTo = "test1", BugId = 4, CreatedBy = "test1", BugTitle = "Bug4", Description = "Description 4", Priority = 0, State = BugState.Resolved },
                new Bug() { AssignedTo = "test1", BugId = 5, CreatedBy = "test1", BugTitle = "Bug5", Description = "Description 5", Priority = 1, State = BugState.Resolved },
                new Bug() { AssignedTo = "test1", BugId = 6, CreatedBy = "test1", BugTitle = "Bug6", Description = "Description 6", Priority = 2, State = BugState.Resolved },
                new Bug() { AssignedTo = "test1", BugId = 7, CreatedBy = "test1", BugTitle = "Bug7", Description = "Description 7", Priority = 0, State = BugState.Closed },
                new Bug() { AssignedTo = "test1", BugId = 8, CreatedBy = "test1", BugTitle = "Bug8", Description = "Description 8", Priority = 1, State = BugState.Closed },
                new Bug() { AssignedTo = "test1", BugId = 9, CreatedBy = "test1", BugTitle = "Bug9", Description = "Description 9", Priority = 2, State = BugState.Closed }
            };

            return bugs;
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