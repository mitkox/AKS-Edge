# Step 1: Build and push service container to Azure Container Registry

## Progress

- [x] [Prerequisite - AKS Edge Essentials Installation](../../install/install.md)
- [x] [Step 1 - Build and push service container to Azure Container Registry](./01_build_and_push.md)
- [ ] [Step 2 - Deploy services onto AKS Edge Cluster](./02_deploy.md)
- [ ] [Step 3 - Build and run Windows console application](./03_win_app.md)

---  

## Build the vote service container

1. In Visual Studio Code, select **File** > **Open Folder** to open the **app/vote-service** project folder. 

2. Open the VS Code integrated terminal by selecting **View** > **Terminal**.

3. Build the container using docker build command. 

    ```
    docker build -t  <ACR login server>/<name-of-container>:<tag-name> .
    ```

    Docker building should take ~1-2 minutes. If everything was successful, you the docker should be ready to push to your container registry. Run `docker image list` to check, for example, you will see:

    ```
    REPOSITORY                           TAG       IMAGE ID       CREATED          SIZE
    aksedgeneo.azurecr.io/vote-service   latest    0928eb2bf3ca   10 seconds ago   181MB
    ```

---

## Login to container registry

Sign in to Docker by entering the following command in the terminal. Sign in with the username, password, and login server from your Azure container registry. You can retrieve these values from the **Access keys** section of your registry in the Azure portal.

```
docker login -u <ACR username> -p <ACR password> <ACR login server>
```

> 💡You may receive a security warning recommending the use of --password-stdin. While that best practice is recommended for production scenarios, it's outside the scope of this tutorial. For more information, see the docker login reference.

---

## Push to container registry

In the previous section, you created and build the service container image. Now you need to push it to your Azure container registry.

Push the container image to the Azure container registry

```
docker push <ACR login server>/<name-of-container>:<tag-name>
```

At the end, you will be able to see a digest for your container image.

```
PS ...\AKS-Edge\hands-on\interop\app\vote-service> docker push aksedgeneo.azurecr.io/vote-service
Using default tag: latest
The push refers to repository [aksedgeneo.azurecr.io/vote-service]
1b3cf1b5286e: Pushed
4c3fa7688367: Pushed
7eda0a9772ff: Pushed
7873ffdc5975: Pushed
6c3b0ca38fdf: Pushed
2510f5d20f3d: Pushed
34d5ebaa5410: Pushed
latest: digest: sha256:e40f839eb7594810876742d0559d070867fad8e0e61d94020cd5eaefa41a0d53 size: 1784
```