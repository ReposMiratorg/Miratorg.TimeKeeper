using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Miratorg.TimeKeeper.DataAccess.Contexts;

public interface IDashboardMTSDbContextFactory
{
    Task<TemplateDbContext> Create();
}

public class DashboardMTSDbContextFactory : IDashboardMTSDbContextFactory
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<DashboardMTSDbContextFactory> _logger;

    public DashboardMTSDbContextFactory(IServiceScopeFactory serviceScopeFactory, ILogger<DashboardMTSDbContextFactory> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }

    public Task<TemplateDbContext> Create()
    {
        var dbContext = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<TemplateDbContext>();

        return Task.FromResult(dbContext);
    }
}
