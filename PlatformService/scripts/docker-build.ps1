$ImageName = "microservices/platform-service"

Set-Location ../
docker build -t $ImageName .
Set-Location ./scripts/

# .NET 7 feature
# dotnet publish --os linux --arch x64 -p:PublishProfile=DefaultContainer -p:ContainerImageName=platform-service