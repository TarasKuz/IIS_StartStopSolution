using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Web.Administration;
using IISWebApiService.Models;

namespace IISWebApiService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IISController : ControllerBase
    {
        [HttpPost("Start")]
        public CommandResult StartPost(WebSiteNameDto webSiteNameDto)
        {
            return StartStopBase(webSiteNameDto);
        }

        [HttpPost("Stop")]
        public CommandResult StopPost(WebSiteNameDto webSiteNameDto)
        {
            return StartStopBase(webSiteNameDto, false);
        }

        private static CommandResult StartStopBase(WebSiteNameDto request, bool isStart = true)
        {
            var commandResult = new CommandResult
            {
                Status = StatusCodes.Failure.ToString()
            };

            try
            {
                using var server = new ServerManager();
                var site = server.Sites.FirstOrDefault(s => s.Name == request.WebSiteName);

                if (site == null)
                {
                    commandResult.Status = StatusCodes.Failure.ToString();
                    return commandResult;
                }

                var r = isStart ? site.Start() : site.Stop();
                commandResult.Status = StatusCodes.Success.ToString();
            }
            catch
            {
            }

            return commandResult;
        }
    }
}
