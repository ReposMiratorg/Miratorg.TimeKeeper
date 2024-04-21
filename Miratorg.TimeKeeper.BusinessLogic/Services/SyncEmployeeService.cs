using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Miratorg.TimeKeeper.BusinessLogic.Services;

public class SyncEmployeeService : IHostedService
{
    private readonly ILogger<SyncEmployeeService> _logger;
    private bool isWork { get; set; }
    private readonly TimeSpan pause = TimeSpan.FromSeconds(5);

    public SyncEmployeeService(ILogger<SyncEmployeeService> logger)
    {
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        if (isWork == true)
        {
            throw new Exception($"Service '{nameof(SyncEmployeeService)}' already started");
        }

        isWork = true;

        _ = Task.Run(async () =>
        {
            while (isWork == true)
            {
                try
                {
                    // there work going on here ...
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error ''");
                }

                await Task.Delay(pause);
            }
        });
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        isWork = false;
    }
}