namespace NetCheck.Services;

public class ProbeService
{
    public event EventHandler<ProbingUrlEventArgs>?  Probing;
    public event EventHandler<ProbeResultEventArgs>? ProbeResults;
    public event EventHandler?                       OfflineThresholdReached;
    
    private int _offlineResponses;
    
    private readonly PingService             _ping;
    private readonly Random                  _rng;
    private readonly CancellationTokenSource _cts;
    private readonly HttpClient              _http;
    private readonly IReadOnlyList<string>   _probeDomains;
    private readonly List<string>            _lastProbedDomains;

    public ProbeService(PingService ping)
    {
        _ping         = ping;
        _rng          = new Random();
        _cts          = new CancellationTokenSource();
        _http         = new HttpClient();
        _http.Timeout = TimeSpan.FromSeconds(5);
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
            "googleusercontent.com"
        ];
        _lastProbedDomains = [];

        Task.Run(RunProbeAsync, _cts.Token);
    }
    
    private async Task RunProbeAsync()
    {
        await Task.Delay(500, _cts.Token);
        
        while (!_cts.IsCancellationRequested)
        {
            var probeUrl = GetRandomProbeUrl();

            Probing?.Invoke(this, new ProbingUrlEventArgs(probeUrl));

            var online = false;
            using (_ping.CreateMeasurer())
            {
                try
                {
                    await _http.GetAsync(probeUrl, _cts.Token);

                    online = true;

                    _offlineResponses = 0;
                }
                catch
                {
                    _offlineResponses++;
                }
            }

            ProbeResults?.Invoke(this, new ProbeResultEventArgs(
                probeUrl,
                online,
                _ping.GetLastPing(),
                _ping.GetAveragePing()));

            if (_offlineResponses == 3)
            {
                OfflineThresholdReached?.Invoke(this, EventArgs.Empty);
            }

            await Task.Delay(500, _cts.Token);
        }
    }

    private string GetRandomProbeUrl()
    {
        string url;
        do
        {
            url = _probeDomains.OrderBy(_ => _rng.Next()).First();
        } while (_lastProbedDomains.Contains(url));

        _lastProbedDomains.Add(url);
        if (_lastProbedDomains.Count > 10)
        {
            _lastProbedDomains.RemoveAt(0);
        }

        return $"https://{url}/generate_204";
    }
}
