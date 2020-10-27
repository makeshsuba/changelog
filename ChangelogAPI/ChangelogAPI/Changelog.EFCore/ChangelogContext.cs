using Changelog.EFCore.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Changelog.EFCore
{
    public class ChangelogContext : DbContext
    {
        public ChangelogContext(DbContextOptions<ChangelogContext> options)
           : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map table names
            modelBuilder.HasDefaultSchema("dbo");

            modelBuilder.Entity<LogTypesModel>().HasData(
                new LogTypesModel { LogTypeId = 1, LogType = "Bug", IsVisible = true },
                new LogTypesModel { LogTypeId = 2, LogType = "Update", IsVisible = true },
                new LogTypesModel { LogTypeId = 3, LogType = "Feature", IsVisible = true }
                );
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<UserRegistrationModel> User { get; set; }
        public DbSet<LogsModel> Logs { get; set; }
        public DbSet<LogTypesModel> LogTypes { get; set; }

    }
    public class DesingTimeDBContextFactory : IDesignTimeDbContextFactory<ChangelogContext>
    {
        public ChangelogContext CreateDbContext(string[] args)
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + "/../ChangelogAPI/appsettings.json").Build();
            var builder = new DbContextOptionsBuilder<ChangelogContext>();
            var connectionString = config.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);
            return new ChangelogContext(builder.Options);
        }
    }
}
