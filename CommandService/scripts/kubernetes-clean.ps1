$deployment = "commands-depl"
$clusterIpService = "commands-clusterip-srv"

kubectl delete deployment $deployment
kubectl delete service $clusterIpService