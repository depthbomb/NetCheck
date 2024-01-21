namespace NetCheck.Managers;

public enum IconName
{
    Initial,
    Online,
    Offline
}

public static class TrayManager
{
    private static NotifyIcon? _trayIcon;

    private static readonly Icon InitialIcon = Resources.Icons.Icon;
    private static readonly Icon OnlineIcon  = Resources.Icons.IconOnline;
    private static readonly Icon OfflineIcon = Resources.Icons.IconOffline;

    public static NotifyIcon TrayIcon
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

                SetIcon(IconName.Initial);
            }

            return _trayIcon;
        }
    }

    public static void Initialize()
    {
        TrayIcon.Visible = true;
    }

    public static void SetIcon(IconName icon)
    {
        if (_trayIcon == null)
        {
            return;
        }
        
        _trayIcon.Icon = icon switch
        {
            IconName.Initial => InitialIcon,
            IconName.Online  => OnlineIcon,
            IconName.Offline => OfflineIcon,
            _                => throw new ArgumentOutOfRangeException(nameof(icon), icon, null)
        };
    }

    private static ContextMenuStrip CreateContextMenu()
    {
        var menu = new ContextMenuStrip();

        menu.Items.Add("Exit", null, OnExitClick);

        return menu;
    }
    
    private static void TrayIconOnDoubleClick(object? sender, EventArgs e) => MainFormManager.ToggleVisibility();

    private static void OnExitClick(object? sender, EventArgs e)
    {
        if (_trayIcon != null)
        {
            _trayIcon.Visible = false;
            _trayIcon.Dispose();
        }

        MainFormManager.Form.ShouldClose = true;
        MainFormManager.Form.Close();
        
        Application.Exit();
    }
}
