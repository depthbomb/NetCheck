using System.Diagnostics;

namespace NetCheck.Worker.Services;

public class ClientHeartbeatHostedService : IHostedService, IDisposable
{
    private const string ClientProcessName = "netcheck";
    
    private readonly ILogger<ClientHeartbeatHostedService> _logger;
    private readonly IHostApplicationLifetime              _lifetime;
    private readonly Timer                                 _timer;

    public ClientHeartbeatHostedService(ILogger<ClientHeartbeatHostedService> logger, IHostApplicationLifetime lifetime)
    {
        _logger   = logger;
        _lifetime = lifetime;
        _timer    = new Timer(CheckForRunningClient, null, Timeout.Infinite, Timeout.Infinite);
    }

    #region Implementation of IHostedService
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer.Change(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }
    #endregion

    #region IDisposable
    public void Dispose()
    {
        _timer.Dispose();
    }
    #endregion

    private void CheckForRunningClient(object? _)
    {
        var processes = Process.GetProcessesByName(ClientProcessName);
        if (processes.Length == 0)
        {
            _logger.LogInformation("Client process not found, stopping application");
            
            _lifetime.StopApplication();
        }
    }
}
