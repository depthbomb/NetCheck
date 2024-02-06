using NetCheck.Shared;
using System.Reflection;
using NetCheck.Worker.Hubs;
using NetCheck.Worker.Services;
using Microsoft.Net.Http.Headers;

namespace NetCheck.Worker;

public static class Program
{
    public static void Main()
    {
        var builder = WebApplication.CreateBuilder(new WebApplicationOptions
        {
            #if DEBUG
            WebRootPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "..", "..", "..", "static")
            #else
            WebRootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "static")
            #endif
        });

        builder.Logging.SetMinimumLevel(LogLevel.Warning);

        builder.Services.AddSignalR();
        builder.Services.AddHttpClient("Probe", client =>
        {
            client.Timeout = TimeSpan.FromSeconds(3);
            client.DefaultRequestHeaders.Add(HeaderNames.UserAgent, "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");
        });
        builder.Services.AddWindowsService();
        builder.Services.AddSingleton<PingService>();
        builder.Services.AddSingleton<NetworkService>();
        builder.Services.AddHostedService<ConnectivityHostedService>();
        builder.Services.AddControllers();

        var app = builder.Build();
        app.UseFileServer();
        app.UseRouting();
        app.MapControllers();
        app.MapFallbackToFile("index.html");
        app.MapHub<ConnectionHub>("/connection");
        app.Run($"http://localhost:{Constants.WorkerPort}");
    }
}
