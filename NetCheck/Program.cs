using NetCheck.Managers;

namespace NetCheck;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        using (new Mutex(true, "NetCheckMutex", out bool createdNew))
        {
            try
            {
                if (!createdNew)
                {
                    Console.WriteLine("Instance is already running");
                    return;
                }
                
                ApplicationConfiguration.Initialize();
                WorkerManager.Initialize();
                if (WorkerManager.IsWorkerRespondingAsync().Result)
                {
                    WebViewManager.Initialize();
                    TrayManager.Initialize();
                    BackendManager.Initialize();
                    MainFormManager.Initialize();
                    Application.Run();
                }
                else
                {
                    MessageBox.Show("Failed to ping worker after multiple attempts.", "Initialization Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
    }
}
