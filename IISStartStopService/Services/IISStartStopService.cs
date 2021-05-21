using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Web.Administration;

namespace IISServices
{
    public class IISStartStopService : StartStopService.StartStopServiceBase
    {
        public override async Task<CommandResult> Start(WebSiteNameDto request, ServerCallContext context)
        {
            return StartStopBase(request);
        }

        public override async Task<CommandResult> Stop(WebSiteNameDto request, ServerCallContext context)
        {
            return StartStopBase(request, false);
        }

        private static CommandResult StartStopBase(WebSiteNameDto request, bool isStart = true)
        {
            var commandResult = new CommandResult
            {
                Status = StatusCode.Failure.ToString()
            };

            try
            {
                using var server = new ServerManager();
                var site = server.Sites.FirstOrDefault(s => s.Name == request.WebSiteName);

                if (site == null)
                {
                    commandResult.Status = StatusCode.Failure.ToString();
                    return commandResult;
                }

                var r = isStart ? site.Start() : site.Stop();
                commandResult.Status = StatusCode.Success.ToString();
            }
            catch
            {
            }

            return commandResult;
        }
    }
}
