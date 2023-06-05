Get-ChildItem ../K8S | ForEach-Object {
    kubectl apply -f $_.FullName
}