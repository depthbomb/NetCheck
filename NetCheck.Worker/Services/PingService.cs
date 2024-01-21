namespace NetCheck.Worker.Services;

public class PingService : IDisposable
{
    private const    int           MaxPastPings  = 100;
    private readonly IList<double> _averagePings = [];

    public void Dispose()
    {
        _averagePings.Clear();
    }

    public IDisposable CreateMeasurer()
    {
        return new PingMeasurer(this);
    }

    public double GetAveragePing()
    {
        if (_averagePings.Count == 0)
        {
            return 0;
        }

        return _averagePings.Average();
    }

    private void AddAveragePing(double ping)
    {
        _averagePings.Insert(0, ping);
        if (_averagePings.Count > MaxPastPings)
        {
            _averagePings.RemoveAt(MaxPastPings - 1);
        }
    }

    private class PingMeasurer : IDisposable
    {
        private readonly PingService _ping;
        private readonly DateTime    _now;
        
        public PingMeasurer(PingService ping)
        {
            _ping = ping;
            _now  = DateTime.UtcNow;
        }
        
        public void Dispose()
        {
            _ping.AddAveragePing((DateTime.UtcNow - _now).TotalMilliseconds);
        }
    }
}
