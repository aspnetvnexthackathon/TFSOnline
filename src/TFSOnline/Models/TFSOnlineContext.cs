using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Framework.OptionsModel;
using Microsoft.AspNet.Identity.Entity;

namespace TFSOnline.Models
{
    public class ApplicationUser : User { }

    public class TFSOnlineContext : IdentitySqlContext<ApplicationUser>
    {
        public TFSOnlineContext(IServiceProvider serviceProvider, IOptionsAccessor<TFSOnlineContextOptions> optionsAccessor)
                    : base(serviceProvider, optionsAccessor.Options)
        {

        }

        public DbSet<Bug> Bugs { get; set; }

        public DbSet<UserSavedQuery> UserSavedQueries { get; set; }

        public DbSet<SavedQuery> SavedQueries { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Bug>().Key(a => a.BugId);
            builder.Entity<SavedQuery>().Key(a => a.RecordId);
            builder.Entity<UserSavedQuery>().Key("UserId", "QueryId");
        }
    }

    public class TFSOnlineContextOptions : DbContextOptions
    {
        public string DefaultAdminUserName { get; set; }

        public string DefaultAdminPassword { get; set; }
    }
}