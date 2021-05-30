# IIS_StartStopSln

If you develop CI/CD that should deploy an app to IIS with GitHub Actions/Workflow or AzureDevOps and you don't want to provide Admin User Privileges for GitHub Runners or Azure Agents here is the solution!

IISWebApiService - allows to start/stop a specific app pool. Run it as a Windonws Service with Admin User privileges on a server.

IISWebApiClient - allows a GitHub Runner or Azure Agent to start/stop a specific app pool Without Admin User Privilege.

## Usage of client
> IISWebApiClient.exe -a stop -n dev.domain.com
>
> IISWebApiClient.exe -a start -n dev.domain.com

## Start as a service
> sc create _serviceName binPath=_absolutePathToExeFile start=delayed-auto