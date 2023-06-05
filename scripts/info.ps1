$array = "pods", "services", "deployments"

foreach ($item in $array) {
    Write-Output "`n=== $item ===`n"
    kubectl get $item
}
