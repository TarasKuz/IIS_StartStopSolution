using System;
using System.Threading.Tasks;
using CommandLine;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace IISStartStopClient
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            ArgOptions argOptions = null;
            // = new ArgOptions{
            //     Action = "stop",
            //     WebSiteName = "dev.domain.com"
            // };

            Parser.Default.ParseArguments<ArgOptions>(args)
                               .WithParsed<ArgOptions>(parsedArgs => argOptions = parsedArgs);

            ValidateArgs(argOptions);

            var actionUrl = "http://localhost:5009/IIS/" + argOptions.Action;

            var client = new RestClient(actionUrl);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("accept", "text/plain");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{\"webSiteName\":\"" + argOptions.WebSiteName + "\"}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);

            dynamic jObj = JObject.Parse(response.Content);
            if (jObj.status != "Success")
            {
                throw new Exception("jObj.status != \"Success\"\n\n" + response.ErrorMessage, response.ErrorException);
            }
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