

### Criar Cluster
```
kind create cluster --name todo-project
```

### Criar POD e Service
```
kind create cluster --name todo-project

ubectl apply -f app.yaml
ubectl apply -f app-service.yaml
```

### Excluir 
```
kubectl delete -f app-service.yaml 
kubectl delete -f app.yaml 
```

### Mapear porta de um Service do Cluster para o HOST
```
kubectl port-forward service/app-service 6002:6002
```
Acessar: <http://localhost:6002/>



### Aplicacao UI para gerenciar Kubernetes:

<https://k8slens.dev/>

