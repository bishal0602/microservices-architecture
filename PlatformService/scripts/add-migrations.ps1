param (
    [Parameter(Mandatory=$true, HelpMessage="The name of the migration")]
    [Alias("n")]
    [string]$Name
)

dotnet ef migrations add $Name -p ..\PlaformService.Infrastructure\ -s ..\PlatformService.API\