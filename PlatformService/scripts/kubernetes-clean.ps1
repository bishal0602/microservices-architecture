$deployment = "platforms-depl"
$nodePortService = "platformnpservice-srv"
$clusterIpService = "platforms-clusterip-srv"

kubectl delete deployment $deployment
kubectl delete service $nodePortService
kubectl delete service $clusterIpService