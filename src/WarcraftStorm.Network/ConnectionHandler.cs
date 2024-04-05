using System.Net;
using System.Net.Sockets;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace WarcraftStorm.Network;

internal class ConnectionHandler(ILogger<ConnectionHandler> logger, IConnectionFactory connectionFactory, IOptions<ConnectionHandlerOptions> options) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        TcpListener server = new TcpListener(IPAddress.Any, options.Value.Port);
        server.Start();
        while(!stoppingToken.IsCancellationRequested)
        {
            TcpClient client = await server.AcceptTcpClientAsync(stoppingToken);
            Connection connection = connectionFactory.CreateConnection(client);
            logger.LogInformation("[{client}] NEW CONNECTION", client.Client.RemoteEndPoint);
            ThreadPool.QueueUserWorkItem(connection.Run);
        }
        server.Stop();
    }
} 
