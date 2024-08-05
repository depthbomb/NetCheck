using NetCheck.Forms;
using NetCheck.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace NetCheck;

internal static class Program
{
    [STAThread]
    private static int Main()
    {
        using (new Mutex(true, "NetCheckMutex", out _))
        {
            var services = new ServiceCollection()
                           //
                           // Forms
                           //
                           .AddSingleton<MainForm>()
                           .AddSingleton<ColorDisplayForm>()
                           //
                           // Controls
                           //
                           .AddSingleton<StatusBanner>()
                           //
                           // Services
                           //
                           .AddSingleton<TrayService>()
                           .AddSingleton<NetworkService>()
                           .AddSingleton<PingService>()
                           .AddSingleton<ProbeService>()
                           .BuildServiceProvider();
            
            ApplicationConfiguration.Initialize();

            services.GetRequiredService<TrayService>().TrayIcon.Visible = true;
            
            Application.Run();

            return 0;
        }
    }
}
