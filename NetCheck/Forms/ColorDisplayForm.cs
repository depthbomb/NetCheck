using System.ComponentModel;

namespace NetCheck.Forms;

public partial class ColorDisplayForm : Form
{
    public bool ShouldClose = false;
    
    public ColorDisplayForm(ProbeService probe)
    {
        Icon = Resources.Icons.Icon;
        
        InitializeComponent();
        this.RespectDarkMode();
        
        Resize        += OnResize;
        HandleCreated += OnHandleCreated;
        Closing       += OnClosing;
        
        probe.ProbeResults += ProbeOnProbeResults;
        
        OnResize(null, EventArgs.Empty);
    }

    private void OnClosing(object? sender, CancelEventArgs e)
    {
        if (!ShouldClose)
        {
            e.Cancel = true;
            this.ToggleVisibility();
        }
    }

    private void OnHandleCreated(object? sender, EventArgs e)
    {
        if (Utils.IsSystemUsingDarkMode())
        {
            NativeMethods.SetPreferredAppMode(2);
            NativeMethods.UseImmersiveDarkMode(Handle, true);
            NativeMethods.FlushMenuThemes();
        }
    }

    private void OnResize(object? sender, EventArgs e)
    {
        c_StatusLabel.Font = new Font(
            c_StatusLabel.Font.FontFamily,
            (float)(0.08 * Width + 0.08 * Height),
            c_StatusLabel.Font.Style
        );
    }

    private void ProbeOnProbeResults(object? sender, ProbeResultEventArgs e)
    {
        BackColor          = e.Online ? GlobalShared.OnlineColor : GlobalShared.OfflineColor;
        c_StatusLabel.Text = e.Online ? "Online" : "Offline";
    }
}
