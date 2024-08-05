using System.Net.NetworkInformation;

namespace NetCheck.Services;

public class NetworkService
{
    public bool UsingEthernet()
    {
        foreach (var networkInterface in NetworkInterface.GetAllNetworkInterfaces())
        {
            if (networkInterface.OperationalStatus == OperationalStatus.Up)
            {
                return networkInterface.NetworkInterfaceType != NetworkInterfaceType.Wireless80211;
            }
        }
        
        return false;
    }
}
