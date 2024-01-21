using NetCheck.Worker.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace NetCheck.Worker.Services;

public class ConnectivityHostedService : IHostedService
{
    private int _offlineResponses = 0;
    
    private readonly IHubContext<ConnectionHub> _hub;
    private readonly PingService                _ping;
    private readonly Random                     _rng;
    private readonly CancellationTokenSource    _cts;
    private readonly HttpClient                 _http;
    private readonly IReadOnlyList<string>      _probeDomains;

    public ConnectivityHostedService(IHubContext<ConnectionHub> hub, PingService ping, IHttpClientFactory httpClientFactory)
    {
        _hub  = hub;
        _ping = ping;
        _rng  = new Random();
        _cts  = new CancellationTokenSource();
        _http = httpClientFactory.CreateClient("Probe");
        _probeDomains =
        [
            "clients1.google.com",
            "clients2.google.com",
            "clients3.google.com",
            "clients4.google.com",
            "clients5.google.com",
            "clients6.google.com",
            "maps.google.com",
            "mt0.google.com",
            "mt1.google.com",
            "mt2.google.com",
            "mt3.google.com",
            "i.ytimg.com",
            "i9.ytimg.com",
            "yt3.ggpht.com",
            "yt4.ggpht.com",
            "youtube.com",
            "studio.youtube.com",
            "gstatic.com",
            "ssl.gstatic.com",
            "maps.gstatic.com",
            "fonts.gstatic.com",
            "ogs.google.com",
            "mail.google.com",
            "apis.google.com",
            "fonts.google.com",
            "googleapis.com",
            "fonts.googleapis.com",
            "googleusercontent.com",
            "lh0.googleusercontent.com",
            "lh1.googleusercontent.com",
            "lh2.googleusercontent.com",
            "lh3.googleusercontent.com",
            "lh4.googleusercontent.com",
            "lh5.googleusercontent.com",
            "lh6.googleusercontent.com"
        ];
    }

    #region Implementation of IHostedService
    public Task StartAsync(CancellationToken cancellationToken)
    {
        Task.Run(RunProbeAsync, cancellationToken);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _cts.Cancel();

        return Task.CompletedTask;
    }
    #endregion

    private async Task RunProbeAsync()
    {
        while (!_cts.IsCancellationRequested)
        {
            var probeUrl = GetRandomProbeUrl();

            await _hub.Clients.All.SendAsync("Checking", probeUrl, _cts.Token);

            using (_ping.CreateMeasurer())
            {
                try
                {
                    await _http.GetAsync(probeUrl, _cts.Token);
                    await _hub.Clients.All.SendAsync("Online", true, _cts.Token);

                    _offlineResponses = 0;
                }
                catch
                {
                    await _hub.Clients.All.SendAsync("Online", false, _cts.Token);

                    _offlineResponses++;
                }
            }

            await _hub.Clients.All.SendAsync("Latency", _ping.GetAveragePing(), _cts.Token);

            if (_offlineResponses == 3)
            {
                await _hub.Clients.All.SendAsync("NotifyOffline", _cts.Token);
            }

            await Task.Delay(500, _cts.Token);
        }
    }

    private string GetRandomProbeUrl()
    {
        var url = _probeDomains.OrderBy(_ => _rng.Next()).First();

        return $"https://{url}/generate_204";
    }
}
