$ContainerName = "platformservice"
$ImageName = "microservices/platform-service"

docker run --name $ContainerName -p 8080:80 -d $ImageName