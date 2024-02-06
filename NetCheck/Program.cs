namespace NetCheck;

internal static class Program
{
    [STAThread]
    private static int Main()
    {
        if (!ServiceManager.ServiceExists())
        {
            MessageBox.Show("The NetCheck Worker Service could not be found on this system. Please re-install NetCheck.", "Missing Service", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return 1;
        }

        if (!ServiceManager.IsServiceRunning())
        {
            MessageBox.Show("The NetCheck Worker Service is not running. Start the service manually or re-install NetCheck.", "Service Not Running", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
            return 1;
        }

        return InitializeClient();
    }

    private static int InitializeClient()
    {
        using (new Mutex(true, "NetCheckMutex", out _))
        {
            ApplicationConfiguration.Initialize();

            try
            {
                if (ServiceManager.IsServiceRespondingAsync().Result)
                {
                    WebViewManager.Initialize();
                    TrayManager.Initialize();
                    IpcManager.Initialize();
                    MainFormManager.Initialize();
                    Application.Run();
                }
                else
                {
                    MessageBox.Show("Failed to ping worker after multiple attempts.", "Initialization Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return 1;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());

                return 1;
            }
            
            return 0;
        }
    }
}
