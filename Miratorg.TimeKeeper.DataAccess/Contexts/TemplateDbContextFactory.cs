using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Miratorg.TimeKeeper.DataAccess.Contexts;

public interface ITemplateDbContextFactory
{
    Task<TemplateDbContext> Create();
}

public class TemplateDbContextFactory : ITemplateDbContextFactory
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<TemplateDbContextFactory> _logger;

    public TemplateDbContextFactory(IServiceScopeFactory serviceScopeFactory, ILogger<TemplateDbContextFactory> logger)
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
