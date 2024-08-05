namespace NetCheck.Events;

public class ProbingUrlEventArgs : EventArgs
{
    public string Url { get; set; }

    public ProbingUrlEventArgs(string url)
    {
        Url = url;
    }
}
