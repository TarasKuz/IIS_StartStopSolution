using System;
using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Net.Client;
using CommandLine;

namespace IISStartStopClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            ArgOptions argOptions = null;
            Parser.Default.ParseArguments<ArgOptions>(args)
                               .WithParsed<ArgOptions>(parsedArgs => argOptions = parsedArgs);

            ValidateArgs(argOptions);

            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            using var channel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions { HttpHandler = httpHandler });
            var client = new IISServices.StartStopService.StartStopServiceClient(channel);

            var request = new IISServices.WebSiteNameDto
            {
                WebSiteName = argOptions.WebSiteName
            };

            IISServices.CommandResult reply = null;
            if (argOptions.Action == "start")
            {
                reply = await client.StartAsync(request);
            }
            else if (argOptions.Action == "stop")
            {
                reply = await client.StopAsync(request);
            }

            Console.WriteLine(reply.Status);
        }

        private static void ValidateArgs(ArgOptions argOptions)
        {
            if (string.IsNullOrEmpty(argOptions.WebSiteName))
            {
                throw new ArgumentException("'name' arg is null or empty.");
            }

            if (argOptions.Action != "start" && argOptions.Action != "stop")
            {
                throw new ArgumentException("Supported actions are: start or stop");
            }
        }
    }
}