using Microsoft.AspNetCore.SignalR.Client;

namespace NetCheck.Managers;

public static class BackendManager
{
    public static void Initialize()
    {
        var port = PortManager.Port;
        var hub  = new HubConnectionBuilder().WithUrl($"http://localhost:{port}/connection").Build();

        hub.On<bool>("Online", OnOnlineStatus);
        hub.StartAsync().Wait();
    }

    private static void OnOnlineStatus(bool isOnline) => TrayManager.SetIcon(isOnline ? IconName.Online : IconName.Offline);
}
