# Step 3: Build and run Windows console application

## Progress

- [x] [Prerequisite - AKS Edge Essentials Installation](../../install/install.md)
- [x] [Step 1 - Build and push service container to Azure Container Registry](./01_build_and_push.md)
- [X] [Step 2 - Deploy services onto AKS Edge Cluster](./02_deploy.md)
- [X] [Step 3 - Build and run Windows console application](./03_win_app.md)

---

## Build and run the console application

1. Open Visual Studio Code, select **File** > **Open Folder** to open the pre-configured **app/vote-win-app** project folder. 

2. Open the integrated terminal by selecting **View** > **Terminal**.

3. Make sure you are in vote-win-app folder, run `dotnet build` to build the project. The build process pull all NuGet packages required to generate protobuf and gRPC client code to link with applicaiton. 

4. The application binary can be found in `bin\debug\net6.0\vote-win-app.exe`. Navigate to this folder and run with help parameters.

    ```
    .\vote-win-app.exe --help
    ```

    The available parameters are listed:

    | Parameter | Option | Description |
    | --------- | ------ | ----------- |
    | Help | -? -h --help | Show help information.
    | Server hostname | -s --server | Vote service external IP | 
    | Server port | -p --port | Vote service host port | 

5. Run the windows console app with the appropriate parameters. For example
   
    ```
    .\vote-win-app.exe -s 192.168.0.90 -p 49998
    ```

6.  Once the application is running, you will see a prompt menu, select option (1) to (4) and ENTER to vote!. 

    ```
    Tickets: Cat: 0 | Dog: 0


    ---- Vote Your Favor ----
    (1) Cat
    (2) Dog
    (3) Reset
    (4) Exit
    -------------------------
    Please enter:
    ```

7. More playaround: 
   
   1. Try to delete vote-service pod, observe if any interrupt in vote-win-app.
   2. Try to delte redis Pod and observe logs in vote service, and see if any data is lost.
   3. Try to add security to existing design.
