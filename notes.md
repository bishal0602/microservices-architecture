## Single Resposibility Principle

_"Gather together those things that change for the same reason, and separate those things that change for different reasons."_

#### kubernetes ingress-nginx
> `kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.8.0/deploy/static/provider/cloud/deploy.yaml`

#### Setting mssql secret in kubernetes
> `kubectl create secret generic mssql --from-literal=SA_PASSWORD="pa55w0rd!"`
