using Microsoft.EntityFrameworkCore;
using Osztott_szavazasi_rendszer.Models;

namespace Osztott_szavazasi_rendszer.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
        public DbSet<Form> Forms { get; set; }
        public DbSet<SubmittedForm> SubmittedForms { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                var originalConnectionString = Environment.GetEnvironmentVariable("DATABASE_URL");
                if (originalConnectionString != null)
                {
                    try
                    {
                        connectionString = StartupHelper.GetHerokuConnectionString(originalConnectionString);
                    }
                    catch (Exception exc)
                    {
                        // throw new Exception($"Cannot parse connection string from {originalConnectionString}.", exc);
                    }
                }
                optionsBuilder.UseNpgsql(connectionString);
            }
        }

      

    }
}
