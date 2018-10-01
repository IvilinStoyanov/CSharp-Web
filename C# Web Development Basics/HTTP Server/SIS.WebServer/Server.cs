using System;
using SIS.WebServer.Routing;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace SIS.WebServer
{
    public class Server
    {
        public Server(int port, ServerRoutingTable serverRoutingTable)
        {
            this.port = port;
            this.listener = new TcpListener(IPAddress.Parse(LocalhostIpAddress), this.port);
            this.serverRoutingTable = serverRoutingTable;
        }

        private const string LocalhostIpAddress = "127.0.0.1";

        private readonly int port;

        private readonly TcpListener listener;

        private readonly ServerRoutingTable serverRoutingTable;

        private bool isRunning;

        public void Run()
        {
            this.listener.Start();
            this.isRunning = true;

           Console.WriteLine($"Server started at http://{LocalhostIpAddress}:{this.port}");

            var task = Task.Run(this.ListenLoop);
            task.Wait();
        }

        public async Task ListenLoop()
        {
            while (this.isRunning)
            {
                var client = await this.listener.AcceptSocketAsync();
                var connectionHandler = new ConnectionHandler(client, this.serverRoutingTable);
                var responseTask = connectionHandler.ProccesRequestAsync();
                responseTask.Wait();           
            }
        }
    }
}
