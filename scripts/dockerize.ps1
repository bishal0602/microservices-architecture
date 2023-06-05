function docker_build {
    param(
        [string]$Path,
        [string]$ImageName
    )

    docker build -t $ImageName $Path
}

docker_build -Path "../CommandService" -ImageName "microservices/command-service"
docker_build -Path "../PlatformService" -ImageName "microservices/platform-service"
