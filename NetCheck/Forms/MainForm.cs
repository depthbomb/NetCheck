using NetCheck.Controls;
using System.ComponentModel;

namespace NetCheck.Forms;

public partial class MainForm : Form
{
    public bool ShouldClose = false;

    private readonly ProbeService   _probe;
    private readonly NetworkService _network;

    public MainForm(StatusBanner statusBanner, ProbeService probe, NetworkService network)
    {
        Visible = false;
        Icon    = Resources.Icons.Icon;

        _probe              =  probe;
        _network            =  network;
        _probe.ProbeResults += ProbeOnProbeResults;

        InitializeComponent();
        this.RespectDarkMode();
        SubscribeToHelpEvents();
        
        c_MainTableLayout.Controls.Add(statusBanner, 0, 0);

        c_ProbeLogListView.Columns.Add("URL", -2);
        c_ProbeLogListView.Columns.Add("Latency");
    }

    private void SubscribeToHelpEvents()
    {
        c_LastPingValueLabel.HelpRequested    += (_, _) => this.ShowHelpMessageBox("Last Ping", "This displays the time it took between the last request and its response.\nWhile the URLs that are probed are typically low-latency with a very high uptime, there are various factors that could result in the reported ping being inaccurate. Unless it is very high, take this value with a grain of salt.");
        c_AveragePingValueLabel.HelpRequested += (_, _) => this.ShowHelpMessageBox("Average Ping", "This displays the average of the last 50 pings.\nThis value will not be accurate if the application has just been started because it has not recorded enough pings.");
        c_NetworkTypeValueLabel.HelpRequested += (_, _) => this.ShowHelpMessageBox("Network Interface", "This displays whether you are on an Ethernet or Wi-Fi connection.");
        c_ProbeLogListView.HelpRequested      += (_, _) => this.ShowHelpMessageBox("Log", "This displays a log the last 50 probe attempts including the URL that was probed and the response time.");
    }
    
    #region Event Handlers
    private void OnClosing(object? sender, CancelEventArgs e)
    {
        if (!ShouldClose)
        {
            Hide();
            e.Cancel = true;
        }
    }

    private void ProbeOnProbeResults(object? sender, ProbeResultEventArgs e)
    {
        var lastPing = Math.Round(e.LastLatency);
        var avgPing  = Math.Round(e.AverageLatency);

        var lastPingLabelForeColor = lastPing >= 175 ? GlobalShared.OfflineColor : lastPing >= 100 ? GlobalShared.DegradedColor : Color.Black;
        var avgPingLabelForeColor  = avgPing  >= 175 ? GlobalShared.OfflineColor : avgPing  >= 100 ? GlobalShared.DegradedColor : Color.Black;
        
        var lastPingForeColor = lastPing >= 100 ? Color.White : Color.Black;
        var lastPingBackColor = lastPing >= 175 ? GlobalShared.OfflineColor : lastPing >= 100 ? GlobalShared.DegradedColor : Color.White;

        c_LastPingValueLabel.Text         = $"{lastPing}ms";
        c_AveragePingValueLabel.Text      = $"{avgPing}ms";
        c_NetworkTypeValueLabel.Text      = _network.UsingEthernet() ? "Ethernet" : "Wi-Fi";
        c_LastPingValueLabel.ForeColor    = lastPingLabelForeColor;
        c_AveragePingValueLabel.ForeColor = avgPingLabelForeColor;

        var uri         = new Uri(e.Url);
        var logListItem = new ListViewItem($"{uri.Scheme}://{uri.Host}")
        {
            UseItemStyleForSubItems = false
        };
        
        logListItem.SubItems.Add(c_LastPingValueLabel.Text);
        logListItem.SubItems[1].ForeColor = lastPingForeColor;
        logListItem.SubItems[1].BackColor = lastPingBackColor;

        c_ProbeLogListView.Items.Add(logListItem);

        if (c_ProbeLogListView.Items.Count > 50)
        {
            c_ProbeLogListView.Items.RemoveAt(0);
        }

        c_ProbeLogListView.Columns[0].Width = -2;
        c_ProbeLogListView.Columns[1].Width = -1;
        c_ProbeLogListView.Items[^1].EnsureVisible();
    }
    #endregion
}
