using Microsoft.AspNet.Identity.EntityFramework;
using ProjectManager.Domain.Seed;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ProjectManager.Domain
{
    public class AppContext : IdentityDbContext<User>
    {
        public AppContext() : base("ProjectManager")
        {
            Database.SetInitializer(new AppContextSeedInitializer());
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Assignment> Assignments { get; set; }

        public static AppContext Create()
        {
            return new AppContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}
