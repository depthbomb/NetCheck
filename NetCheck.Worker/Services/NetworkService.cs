using System.Net.NetworkInformation;

namespace NetCheck.Worker.Services;

public class NetworkService
{
    public bool UsingEthernet()
    {
        var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
        foreach (var networkInterface in networkInterfaces)
        {
            if (networkInterface.OperationalStatus == OperationalStatus.Up)
            {
                if (networkInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
                {
                    return false;
                }

                return true;
            }
        }
        
        return false;
    }
}
