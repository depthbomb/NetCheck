using NetCheck.Shared;
using Microsoft.AspNetCore.SignalR.Client;

namespace NetCheck.Managers;

public static class IpcManager
{
    public static void Initialize()
    {
        var hub = new HubConnectionBuilder().WithUrl($"http://localhost:{Constants.WorkerPort}/connection").Build();

        hub.On<bool>("Online", OnOnlineStatus);
        hub.StartAsync().Wait();
    }

    private static void OnOnlineStatus(bool isOnline) => TrayManager.SetIcon(isOnline ? IconName.Online : IconName.Offline);
}
