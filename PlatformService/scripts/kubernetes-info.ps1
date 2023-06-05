
$array = @("pods", "services", "deployments")

$array.ForEach( {  
    Write-Output "`n=== $_ ===`n"
    kubectl get $_
} )
# Write-Host "=== Services ===`n$servicesTable`n"
# kubectl get pods

# kubectl get deployments
# kubectl get services