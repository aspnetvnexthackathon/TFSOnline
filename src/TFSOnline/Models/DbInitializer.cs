using Microsoft.Data.Entity.SqlServer;
using Microsoft.Framework.DependencyInjection;
using System;
using System.Threading.Tasks;
using TFSOnline.Models;

namespace TFSOnline
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
                }
            }
        }
    }
}