using NetCheck.Forms;

namespace NetCheck.Services;

public enum TrayState
{
    Initial,
    Online,
    Offline
}

public class TrayService
{
    public NotifyIcon TrayIcon
    {
        get
        {
            if (_trayIcon == null)
            {
                _trayIcon                  =  new NotifyIcon();
                _trayIcon.Text             =  "NetCheck";
                _trayIcon.Visible          =  false;
                _trayIcon.ContextMenuStrip =  CreateContextMenu();
                _trayIcon.DoubleClick      += TrayIconOnDoubleClick;

                SetState(TrayState.Initial);
            }

            return _trayIcon;
        }
    }
    
    private NotifyIcon? _trayIcon;

    private readonly MainForm         _mainForm;
    private readonly ColorDisplayForm _colorDisplayForm;
    private readonly Icon             _initialIcon = Resources.Icons.Icon;
    private readonly Icon             _onlineIcon  = Resources.Icons.IconOnline;
    private readonly Icon             _offlineIcon = Resources.Icons.IconOffline;

    public TrayService(MainForm mainForm, ColorDisplayForm colorDisplayForm, ProbeService probe)
    {
        _mainForm         = mainForm;
        _colorDisplayForm = colorDisplayForm;

        probe.ProbeResults += ProbeOnProbeResults;
    }

    private void SetState(TrayState state)
    {
        TrayIcon.Text = state switch
        {
            TrayState.Initial => "NetCheck",
            TrayState.Online  => "NetCheck: Online",
            TrayState.Offline => "NetCheck: Offline",
            _                 => throw new ArgumentOutOfRangeException(nameof(state), state, null)
        };
        
        TrayIcon.Icon = state switch
        {
            TrayState.Initial => _initialIcon,
            TrayState.Online  => _onlineIcon,
            TrayState.Offline => _offlineIcon,
            _                 => throw new ArgumentOutOfRangeException(nameof(state), state, null)
        };
    }

    private ContextMenuStrip CreateContextMenu()
    {
        var menu = new ContextMenuStrip();

        menu.Items.Add("Toggle status display window", null, (_, _) => _colorDisplayForm.ToggleVisibility());
        menu.Items.Add(new ToolStripSeparator());
        menu.Items.Add("Exit", null, OnExitClick);

        return menu;
    }
    
    #region Event Handlers
    private void TrayIconOnDoubleClick(object? sender, EventArgs e) => _mainForm.ToggleVisibility();

    private void OnExitClick(object? sender, EventArgs e)
    {
        TrayIcon.Visible = false;
        TrayIcon.Dispose();

        _colorDisplayForm.ShouldClose = true;
        _colorDisplayForm.Close();
        
        _mainForm.ShouldClose = true;
        _mainForm.Close();

        Application.Exit();
    }

    private void ProbeOnProbeResults(object? sender, ProbeResultEventArgs e)
        => SetState(e.Online ? TrayState.Online : TrayState.Offline);
    #endregion
}
