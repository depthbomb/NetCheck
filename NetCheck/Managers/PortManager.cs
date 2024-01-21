using System.Net.Sockets;

namespace NetCheck.Managers;

public static class PortManager
{
    private static int _port;

    public static int Port
    {
        get
        {
            if (_port == 0)
            {
                _port = GetRandomUnusedPort();
            }

            return _port;
        }
    }

    private static int GetRandomUnusedPort()
    {
        var tcpListener = new TcpListener(IPAddress.Loopback, 0);

        tcpListener.Start();

        var port = ((IPEndPoint)tcpListener.LocalEndpoint).Port;

        tcpListener.Stop();

        return port;
    }
}
