namespace NetCheck.Events;

public class ProbeResultEventArgs : EventArgs
{
    public string Url            { get; set; }
    public bool   Online         { get; set; }
    public double LastLatency    { get; set; }
    public double AverageLatency { get; set; }

    public ProbeResultEventArgs(string url, bool online, double lastLatency, double averageLatency)
    {
        Url            = url;
        Online         = online;
        LastLatency    = lastLatency;
        AverageLatency = averageLatency;
    }
}
