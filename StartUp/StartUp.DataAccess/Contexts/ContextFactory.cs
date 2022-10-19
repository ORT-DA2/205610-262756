using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace StartUp.DataAccess.Contexts;

public enum ContextType { SQLite, SQLServer }

public class ContextFactory : IDesignTimeDbContextFactory<StartUpContext>
{
   
    public StartUpContext CreateDbContext(string[] args)
    {
        return GetNewContext();
    }

    public static StartUpContext GetNewContext(ContextType type = ContextType.SQLServer)
    {
        var builder = new DbContextOptionsBuilder<StartUpContext>();
        DbContextOptions options = null;

        if (type == ContextType.SQLite)
        {
            options = GetSqliteConfig(builder);
        }
        else
        {
            options = GetSqlServerConfig(builder);
        }

        return new StartUpContext(options);
    }

    private static DbContextOptions GetSqliteConfig(DbContextOptionsBuilder builder)
    {
        builder.UseSqlite("Filename=:memory:");

        return builder.Options;
    }

    private static DbContextOptions GetSqlServerConfig(DbContextOptionsBuilder builder)
    {
        //Gets directory from startup project being used, NOT this class's path 
        var directory = Directory.GetCurrentDirectory();

        var configuration = new ConfigurationBuilder()
            .SetBasePath(directory)
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("StartUpDb");
        builder.UseSqlServer(connectionString);
        return builder.Options;
    }
}