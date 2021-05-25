using System;
using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Net.Client;
using CommandLine;
using RestSharp;

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


            var actionUrl = "http://localhost:5001/IIS/" + argOptions.Action;

            var client = new RestClient(actionUrl);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("accept", "text/plain");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{\"webSiteName\":\"" + argOptions.WebSiteName + "\"}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);

            //IISServices.CommandResult reply = null;
            //Console.WriteLine(reply.Status);
            Console.WriteLine(response);
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