using Microsoft.AspNetCore.Mvc;
using NetCheck.Worker.Services;

namespace NetCheck.Worker.Controllers;

public class NetworkController : Controller
{
    private readonly NetworkService _network;

    public NetworkController(NetworkService network)
    {
        _network = network;
    }
    
    [HttpGet("/network_type")]
    public string GetNetworkType()
    {
        return _network.UsingEthernet() ? "ethernet" : "wifi";
    }
}
