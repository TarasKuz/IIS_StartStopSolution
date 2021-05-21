using System.Threading.Tasks;
using Grpc.Core;

namespace IISServices
{
    public class IISStartStopService : StartStopService.StartStopServiceBase
    {
        public override Task<CommandResult> Start(WebSiteNameDto request, ServerCallContext context)
        {
            return Task.FromResult(new CommandResult{
                Status = StatusCode.Success.ToString()
            });
        }

        public override Task<CommandResult> Stop(WebSiteNameDto request, ServerCallContext context)
        {
            return Task.FromResult(new CommandResult{
                Status = StatusCode.Success.ToString()
            });
        }
    }
}
