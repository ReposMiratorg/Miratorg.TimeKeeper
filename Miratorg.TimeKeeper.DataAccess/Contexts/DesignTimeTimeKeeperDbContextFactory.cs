using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Miratorg.TimeKeeper.DataAccess.Contexts;

public class DesignTimeTimeKeeperDbContextFactory : IDesignTimeDbContextFactory<TimeKeeperDbContext>
{
    public TimeKeeperDbContext CreateDbContext(string[] args)
    {
        string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        Console.WriteLine($"User: environment: '{environment}'");

        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment}.json", optional: true)
            //.AddEnvironmentVariables()
            .Build();

        var conectionString = config.GetConnectionString("DefaultConnection");
        Console.WriteLine($"Connection String: '{conectionString}'");

        var optionsBuilder = new DbContextOptionsBuilder<TimeKeeperDbContext>();
        optionsBuilder.UseSqlServer(conectionString);

        return new TimeKeeperDbContext(optionsBuilder.Options);
    }
}
