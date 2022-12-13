# Step 2: Deploy services onto AKS Edge Cluster

## Progress

- [x] [Prerequisite - AKS Edge Essentials Installation](../../install/install.md)
- [x] [Step 1 - Build and push service container to Azure Container Registry](./01_build_and_push.md)
- [X] [Step 2 - Deploy services onto AKS Edge Cluster](./02_deploy.md)
- [ ] [Step 3 - Build and run Windows console application](./03_win_app.md)

---

## Deploy Redis

1. First create a namespace for your demo artifacts.
   
    ```
    kubectl create namespace interop
    ```

2. Navigate to [charts](../charts/) folder and run:
   
    ```
    kubectl apply -f .\redis
    ```

    This will create a [Kubernetes Statefulset](https://kubernetes.io/docs/concepts/workloads/controllers/statefulset/) to take care of a single Redis Pod with [presistent volume](https://kubernetes.io/docs/concepts/storage/persistent-volumes/) and expose a [headless service](https://kubernetes.io/docs/concepts/services-networking/service/#headless-services) to allow other Pods on the cluster to access Redis through a DNS resolvable name `<service-name>.<namespace>.svc.cluster.local`.
    
    By default, redis Pod is listening on port 6379. You can change this by modifying [redis-cm.yaml](../charts/redis/redis-cm.yaml) and [redis-svc.yaml](../charts/redis/redis-svc.yaml) file. 

    > 💡The redis Statefulset mount [hostPath](https://kubernetes.io/docs/concepts/storage/volumes/#hostpath) type of presistent volume on `/home/aksedge-user/redis-data` folder of your Linux VM node. This kind of volume present many security risks, can be only used for demo purpose. 

## Deploy Vote service

1. By default, access to pull or push content from an Azure container registry is only available to authenticated users. And to allow the Kuberentes cluster to pull from ACR, we must provide a Service Principal and configure a pull secret, check this [article](https://learn.microsoft.com/en-us/azure/container-registry/container-registry-auth-kubernetes) for more details. In this hands-on lab, we will just enable anonymous access for simplicity. Open a Cloud shell or a Terminal with Az CLI installed and logined.

    ```
    az acr update --name <your-registry-name-no-need-acr.io> --anonymous-pull-enabled
    ```

2. Navigate to [vote-service](../charts/vote-service/) folder and open the [vote-service-deploy.yaml](../charts/vote-service/vote-service-deploy.yaml) file to edit:

   1. In line 20, replace the `<ACR login server>/<name-of-container>:<tag-name>` to the container name you've pushed in Step 1 of this lab.

   2. In line 22 and line 29, replace the `<service-port-expose-on-container>` to a port you want your service runs inside cluster network. Please note the `containerPort` in line 22 must be a integar, and `value` in line 29 must be string.
   
   3. The service is generic and we use `Cat` and `Dog` as candidate for voting. If you have other preference you can change it. 

3. Open the [vote-service-svc.yaml](../charts/vote-service/vote-service-svc.yaml) file to edit

   1. In line 9, modify the `<host-port>` to a port that eventually you want to run on your host. 

   2. In line 10, modify the `<service-port-expose-on-container>` to the same container port we specified in above step. 

4. On your Terminal, run below command in the [Charts](../charts/) folder to deploy resources.

    ```
    kubectl apply -f .\vote-service
    ```

    This will create a [Kubernetes Deployment](https://kubernetes.io/docs/concepts/workloads/controllers/deployment/) to take care of a vote service Pod and expose a [Loadbalancer service](https://kubernetes.io/docs/concepts/services-networking/service/#loadbalancer) to get an External IP from [kube-vip-cloud-provider](https://github.com/kube-vip/kube-vip-cloud-provider) running on AKS Edge control plane. 
    
    Check the connection to Redis is successful by running below command, replacing the pod name with your own:

    ```
    kubectl logs <vote-service-pod-name> -n interop
    ```

    If there is no problem, you will be able to see similar logs like below:

    ```
    gRPC Server started on port 49999
    Connected to Redis server
    ...
    ```

5. Now your service should be accessible from your Windows host, run below command to get the External IP. 
   
    ```
    kubectl get svc -n interop
    ```

    For example, the snapshot shows this service is exposed on 192.168.0.90:49998. 

    ```
    NAME               TYPE           CLUSTER-IP       EXTERNAL-IP    PORT(S)           AGE
    redis-service      ClusterIP      None             <none>         6379/TCP          114m
    vote-service-svc   LoadBalancer   10.110.206.156   192.168.0.90   49998:30020/TCP   15s
    ```
