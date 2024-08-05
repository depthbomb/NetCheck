namespace NetCheck.Services;

public class PingService
{
    private double _lastPing = double.MaxValue;

    private const int MaxPastPings = 50;
    
    private readonly IList<double> _averagePings = [];

    public IDisposable CreateMeasurer() => new PingMeasurer(this);

    public double GetLastPing() => _lastPing;

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
            _ping._lastPing = (DateTime.UtcNow - _now).TotalMilliseconds;
            _ping.AddAveragePing((DateTime.UtcNow - _now).TotalMilliseconds);
        }
    }
}
