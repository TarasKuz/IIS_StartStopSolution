# IIS_StartStopSln

If you develop CI/CD that should deploy an app to IIS with GitHub Actions/Workflow or AzureDevOps and you don't want to provide an Admin User privileges for GitHub Runners or Azure Agents here is the solution!

IISStartStopService - allows to start/stop a specific web site. Run it as a Windonws Service with Admin User privileges on a server.

IISStartStopClient - allows a GitHub Runner or Azure Agent to start/stop a specific web site Without Admin User Privilege.

Those are grpc server & client.

## Usage of client
> IISStartStopClient.exe -a stop -n dev.domain.com
>
> IISStartStopClient.exe -a start -n dev.domain.com

## Start as a service
> sc create _serviceName binPath=_absolutePathToExeFile start=delayed-auto

You're welcome :).
