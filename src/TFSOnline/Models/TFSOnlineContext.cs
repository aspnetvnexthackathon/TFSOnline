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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Bug>().Key(a => a.BugId);
        }
    }

    public class TFSOnlineContextOptions : DbContextOptions
    {

    }
}