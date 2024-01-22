using System.ComponentModel;
using Microsoft.Web.WebView2.Core;

namespace NetCheck.Managers;

public static class WebViewManager
{
    private const string EvergreenUrl = "https://go.microsoft.com/fwlink/p/?LinkId=2124703";

    public static void Initialize()
    {
        if (!IsRuntimeInstalled())
        {
            var res = MessageBox.Show("NetCheck requires the WebView2 runtime to be installed. Would you like to download and install it now?", "WebView2 Runtime Required", MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {
                InstallRuntimeAsync().Wait();
            }
            else
            {
                Application.Exit();
            }
        }
    }
    
    private static async Task InstallRuntimeAsync()
    {
        using (var http = new HttpClient())
        {
            var downloadPath = Path.GetTempFileName();
            var res          = await http.GetAsync(EvergreenUrl);
            if (res.IsSuccessStatusCode)
            {
                try
                {
                    var bytes = await res.Content.ReadAsByteArrayAsync();
                    await using (var fs = new FileStream(downloadPath, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        await fs.WriteAsync(bytes);
                    }

                    MessageBox.Show("The WebView2 runtime has been downloaded and will now be installed in the background.\nNetCheck will launch after the WebView2 runtime has been installed.", "WebView2 Runtime Installation");

                    var proc = Process.Start(new ProcessStartInfo
                    {
                        FileName = downloadPath,
                        Arguments = "/silent /install"
                    });
                    await proc!.WaitForExitAsync();
                }
                catch (HttpRequestException ex)
                {
                    MessageBox.Show($"The WebView2 runtime could not be downloaded:\n{ex.Message}", "Runtime Installation Error");
                }
                catch (Win32Exception ex)
                {
                    MessageBox.Show($"The WebView2 runtime installer could not be started:\n{ex.Message}", "Runtime Installation Error");
                }
            }
            else
            {
                MessageBox.Show($"The WebView2 runtime could not be downloaded:\n{res.ReasonPhrase}", "Runtime Installation Error");
            }
        }
    }

    private static bool IsRuntimeInstalled()
    {
        try
        {
            CoreWebView2Environment.GetAvailableBrowserVersionString();

            return true;
        }
        catch
        {
            return false;
        }
    }
}
