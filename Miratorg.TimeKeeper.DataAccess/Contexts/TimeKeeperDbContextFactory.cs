using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Miratorg.TimeKeeper.DataAccess.Contexts;

public interface ITimeKeeperDbContextFactory
{
    Task<TimeKeeperDbContext> Create();
}

public class TimeKeeperDbContextFactory : ITimeKeeperDbContextFactory
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<TimeKeeperDbContextFactory> _logger;

    public TimeKeeperDbContextFactory(IServiceScopeFactory serviceScopeFactory, ILogger<TimeKeeperDbContextFactory> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }

    public Task<TimeKeeperDbContext> Create()
    {
        var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<TimeKeeperDbContext>();
        return Task.FromResult(dbContext);
    }
}
