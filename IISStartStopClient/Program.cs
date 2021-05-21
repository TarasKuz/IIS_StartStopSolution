using System;
using System.Threading.Tasks;
using Grpc.Net.Client;

namespace IISStartStopClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new IISServices.StartStopService.StartStopServiceClient(channel);
            var reply = await client.StopAsync(new IISServices.WebSiteNameDto
            {
                WebSiteName = "dev.domain.com"
            });
            Console.WriteLine(reply.Status);
            Console.WriteLine("Press any key to exit...");
        }
    }
}
