$ImageName = "microservices/command-service"

Set-Location ../
docker build -t $ImageName .
Set-Location ./scripts/