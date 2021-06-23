using System.Linq;
using IISWebApiService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Web.Administration;

namespace IISWebApiService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IISController : ControllerBase
    {
        [HttpPost("Start")]
        public CommandResult StartPost(WebSiteNameRequest webSiteNameRequest)
        {
            return StartStopBase(webSiteNameRequest);
        }

        [HttpPost("Stop")]
        public CommandResult StopPost(WebSiteNameRequest webSiteNameRequest)
        {
            return StartStopBase(webSiteNameRequest, false);
        }

        private static CommandResult StartStopBase(WebSiteNameRequest request, bool isStartAction = true)
        {
            var commandResult = new CommandResult
            {
                Status = StatusCodes.Failure.ToString()
            };

            try
            {
                using var server = new ServerManager();
                commandResult.Status = ProcessSite(server, request, isStartAction) && ProcessPool(server, request, isStartAction)
                    ? StatusCodes.Success.ToString()
                    : StatusCodes.Failure.ToString();
            }
            catch
            {
            }

            return commandResult;
        }

        private static bool ProcessPool(ServerManager server, WebSiteNameRequest request, bool isStartAction)
        {
            var appPool = server.ApplicationPools.FirstOrDefault(s => s.Name == request.WebSiteName);

            if (appPool == null)
            {
                return false;
            }

            var result = isStartAction ? appPool.Start() : appPool.Stop();
            return true;
        }

        private static bool ProcessSite(ServerManager server, WebSiteNameRequest request, bool isStartAction)
        {
            var site = server.Sites.FirstOrDefault(s => s.Name == request.WebSiteName);

            if (site == null)
            {
                return false;
            }

            var r = isStartAction ? site.Start() : site.Stop();
            return true;
        }
    }
}