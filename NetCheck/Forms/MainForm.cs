using Microsoft.Win32;
using System.ComponentModel;
using Microsoft.Web.WebView2.Core;

namespace NetCheck.Forms;

public partial class MainForm : Form
{
    public bool ShouldClose = false;

    public MainForm()
    {
        Visible = false;
        Icon    = Resources.Icons.Icon;

        InitializeComponent();

        HandleCreated += OnHandleCreated;

        #pragma warning disable CS4014
        InitializeWebViewAsync();
        #pragma warning restore CS4014
    }

    private async Task InitializeWebViewAsync()
    {
        try
        {
            await _webView2.EnsureCoreWebView2Async(
                await CoreWebView2Environment.CreateAsync()
            );
            
            #if RELEASE
            _webView2.CoreWebView2.Settings.AreDevToolsEnabled            = false;
            _webView2.CoreWebView2.Settings.IsStatusBarEnabled            = false;
            _webView2.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;
            #endif
            
            _webView2.CoreWebView2.Navigate($"http://localhost:{PortManager.Port}");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Failed to initialize CoreWebView2: {ex.Message}", "WebView Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Application.Exit();
        }
    }

    private static bool IsSystemUsingDarkMode()
    {
        try
        {
            var res = (int)(Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", -1) ?? 0);

            return res == 0;
        }
        catch
        {
            return false;
        }
    }
    
    #region Event Handlers
    private void OnHandleCreated(object? sender, EventArgs e)
    {
        if (IsSystemUsingDarkMode())
        {
            NativeMethods.SetPreferredAppMode(2);
            NativeMethods.UseImmersiveDarkMode(Handle, true);
            NativeMethods.FlushMenuThemes();
        }
    }

    private void OnClosing(object? sender, CancelEventArgs e)
    {
        if (!ShouldClose)
        {
            Hide();
            e.Cancel = true;
        }
        else
        {
            HandleCreated -= OnHandleCreated;
        }
    }
    #endregion
}
