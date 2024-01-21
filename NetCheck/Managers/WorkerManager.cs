using System.Reflection;
using System.Diagnostics;

namespace NetCheck.Managers;

public static class WorkerManager
{
    private const int    MaxPingAttempts = 20;
    private const string AssemblyName    = "netcheck_worker.exe";

    public static void Initialize()
    {
        if (!IsWorkerRunning())
        {
            StartWorker();
        }
    }

    public static async Task<bool> IsWorkerRespondingAsync()
    {
        using (var http = new HttpClient())
        {
            for (int attempts = 0; attempts < MaxPingAttempts; attempts++)
            {
                if (!IsWorkerRunning())
                {
                    Console.WriteLine("Worker is not started?");

                    StartWorker();
                }
                
                Console.WriteLine("Pinging worker...");

                try
                {
                    var res = await http.GetAsync($"http://localhost:{PortManager.Port}/ping");
                    if (res.StatusCode == HttpStatusCode.NoContent)
                    {
                        Console.WriteLine("Worker is responding");
                        
                        return true;
                    }
                }
                catch
                {
                    // ignored
                }

                Console.WriteLine("Waiting to ping worker again...");

                await Task.Delay(1_000);
            }

            Console.WriteLine($"Worker failed to respond after {MaxPingAttempts} attempts");

            return false;
        }
    }

    private static bool IsWorkerRunning()
    {
        var proc = Process.GetProcesses().FirstOrDefault(p => p.ProcessName == AssemblyName.Replace(".exe", ""));
        return proc != null;
    }

    private static void StartWorker()
    {
        Console.WriteLine("Starting worker...");

        var appDirectory = Path.GetDirectoryName(Application.ExecutablePath)!;
        #if DEBUG
        var workerPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "..", "..", "..", "..", "NetCheck.Worker", "bin", "Debug", "net8.0", AssemblyName);
        #else
        var workerPath = Path.Combine(appDirectory, AssemblyName);
        #endif

        var psi = new ProcessStartInfo
        {
            FileName = workerPath,
            #if DEBUG
            CreateNoWindow = false,
            #else
            CreateNoWindow = true,
            #endif
            UseShellExecute = false,
            ArgumentList =
            {
                PortManager.Port.ToString(),
                appDirectory
            }
        };

        try
        {
            Process.Start(psi);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Could not start worker service: {ex.Message}", "Startup Error");
            Application.Exit();
        }
    }
}
