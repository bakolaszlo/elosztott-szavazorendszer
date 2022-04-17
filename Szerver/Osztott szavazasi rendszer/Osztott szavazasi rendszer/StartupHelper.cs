using Microsoft.EntityFrameworkCore;
using Npgsql;
using Osztott_szavazasi_rendszer.Data;

namespace Osztott_szavazasi_rendszer
{
    public class StartupHelper
    {
        public static string GetConnectionString(ConfigurationManager configuration)
        {
            var originalConnectionString = Environment.GetEnvironmentVariable("DATABASE_URL");
            if (originalConnectionString != null)
            {
                try
                {
                    return GetHerokuConnectionString(originalConnectionString);
                }
                catch (Exception exc)
                {
                    throw new Exception($"Cannot parse connection string from {originalConnectionString}.", exc);
                }
            }

            return configuration["ConnectionStrings:DefaultConnection"];
        }

        public static string GetHerokuConnectionString(string connectionString)
        {
            var databaseUri = new Uri(connectionString);
            var userInfo = databaseUri.UserInfo.Split(':');
            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = databaseUri.Host,
                Port = databaseUri.Port,
                Username = userInfo[0],
                Password = userInfo[1],
                Database = databaseUri.LocalPath.TrimStart('/'),
                SslMode = SslMode.Require,
                TrustServerCertificate = true
            };
            return builder.ToString();
        }

        public static void ApplyMigrations()
        {
            using var context = new AppDbContext();
            Console.WriteLine("Start migrating.");
            context.Database.Migrate();
            Console.WriteLine("DB migrated.");
        }
    }

    
}
