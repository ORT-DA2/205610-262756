using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace StartUp.DataAccess;

public enum ContextType { Memory, SQL }

public class ContextFactory : IDesignTimeDbContextFactory<StartUpContext>
{

    public StartUpContext CreateDbContext(string[] args)
    {
        return GetNewContext();
    }

    public static StartUpContext GetNewContext(ContextType type = ContextType.SQL)
    {
        var builder = new DbContextOptionsBuilder<StartUpContext>();
        DbContextOptions options = null;

        if (type == ContextType.Memory)
        {
            options = GetMemoryConfig(builder);
        }
        else
        {
            options = GetSqlConfig(builder);
        }

        return new StartUpContext(options);
    }

    private static DbContextOptions GetMemoryConfig(DbContextOptionsBuilder builder)
    {
        builder.UseInMemoryDatabase("StartUpDb");

        return builder.Options;
    }

    private static DbContextOptions GetSqlConfig(DbContextOptionsBuilder builder)
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