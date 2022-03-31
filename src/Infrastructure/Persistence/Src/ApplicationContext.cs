using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Objects.Dto;
using Objects.Settings;
using Persistence.Configurations;

namespace Persistence
{
    public class ApplicationContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=database.db", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Filename=database.db");
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TaskDbConfiguration());
            modelBuilder.ApplyConfiguration(new SettingsDbConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<TaskDto> Tasks { get; set; }

        public DbSet<BaseSettings> Settings { get; set; }
    }
}
