using NetCheck.Forms;

namespace NetCheck.Controls;

public partial class StatusBanner : UserControl
{
    private readonly ColorDisplayForm _colorDisplayForm;

    public StatusBanner(ColorDisplayForm colorDisplayForm, ProbeService probe)
    {
        _colorDisplayForm = colorDisplayForm;
        
        InitializeComponent();

        HelpRequested             += OnHelpRequested;
        DoubleClick               += OnDoubleClick;
        c_StatusLabel.DoubleClick += OnDoubleClick;
        probe.ProbeResults        += ProbeOnProbeResults;
    }

    private void OnDoubleClick(object? sender, EventArgs e) => _colorDisplayForm.ToggleVisibility();

    private void OnHelpRequested(object? sender, HelpEventArgs e)
        => MessageBox.Show(this, "This banner shows a simple visual representation of your connection status.\nDouble-click it to show a larger version.", "Status Banner");

    private void ProbeOnProbeResults(object? sender, ProbeResultEventArgs e)
    {
        var online = e.Online;

        BackColor          = online ? GlobalShared.OnlineColor : GlobalShared.OfflineColor;
        c_StatusLabel.Text = online ? "Online" : "Offline";
    }
}
