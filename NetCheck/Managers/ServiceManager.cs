using NetCheck.Shared;
using System.ServiceProcess;

namespace NetCheck.Managers;

public static class ServiceManager
{
    private const int    MaxPingAttempts    = 30;
    private const string ServiceName        = "NetCheck.Worker";
    private const string ServiceBinaryName  = "netcheck_worker.exe";
    private const string ServiceDescription = "NetCheck Worker Service";

    public static async Task<bool> IsServiceRespondingAsync()
    {
        using (var http = new HttpClient())
        {
            http.Timeout = TimeSpan.FromSeconds(1);
            for (int attempts = 0; attempts < MaxPingAttempts; attempts++)
            {
                Console.WriteLine("Pinging worker...");

                try
                {
                    var res = await http.GetAsync($"http://localhost:{Constants.WorkerPort}/ping");
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
            }

            Console.WriteLine($"Worker failed to respond after {MaxPingAttempts} attempts");

            return false;
        }
    }
    
    public static bool ServiceExists()
    {
        var services = ServiceController.GetServices();

        return services.Any(x => x.ServiceName == Constants.WorkerServiceName);
    }
    
    public static bool IsServiceRunning()
    {
        try
        {
            using (var svcCtl = new ServiceController(Constants.WorkerServiceName))
            {
                return svcCtl.Status == ServiceControllerStatus.Running;
            }
        }
        catch
        {
            return false;
        }
    }

    public static void CreateService()
    {
        #if DEBUG
        var serviceBinaryPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, ServiceBinaryName);
        #else
        var serviceBinaryPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath)!, ServiceBinaryName);
        #endif

        RunScCommand($"create \"{ServiceName}\" binPath= \"{serviceBinaryPath}\" start= auto DisplayName= \"{ServiceDescription}\"");
    }

    public static void StartService()
    {
        RunScCommand($"start {ServiceName}");
    }

    public static void StopService()
    {
        RunScCommand($"stop {ServiceName}");
    }
    
    public static void DeleteService()
    {
        RunScCommand($"delete {ServiceName}");
    }

    private static void RunScCommand(string command, bool waitForExit = true)
    {
        var psi = new ProcessStartInfo("sc.exe", command)
        {
            UseShellExecute = false,
            #if DEBUG
            CreateNoWindow = false,
            #else
            CreateNoWindow = false,
            #endif
            Verb = "runas"
        };
        
        using (var proc = new Process())
        {
            proc.StartInfo = psi;
            proc.Start();
            
            if (waitForExit)
            {
                proc.WaitForExit();
            }
        }
    }
}
