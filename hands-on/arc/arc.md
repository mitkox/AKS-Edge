# Arc-connected AKS Edge Essentials - AKS hybrid 

## Lab Overview
This article describes how to connect your AKS Edge Essentials cluster to Azure Arc so that you can monitor the health of your cluster on the Azure portal.
## Lab Prerequisites
* Download the Azure/AKS-Edge GitHub repo [link](https://github.com/Azure/AKS-Edge/tree/main), if you have not done earlier.Navigate to the Code tab and click the Download Zip button to download the repository as a .zip file. Extract the GitHub .zip file to a working folder.

Connect your cluster to Arc: 
* Use Azure CLI with the commands from the AKSEdgeDeploy module [link](https://github.com/Azure/AKS-Edge/tree/main/tools/modules/AksEdgeDeploy/README.md). This approach is described in this article.

## 1. Configure your Azure environment

#### STEP 1: Create and verify a resource group for AKS Edge Essentials Azure resources
![image](https://user-images.githubusercontent.com/10614734/207283102-540bbafd-33f9-48d9-b148-c5b2d7e9c32c.png)

#### STEP 2: Specify the required names for the resource group and service principal in the aide-userconfig.json file along with your subscription/tenant information.
![image](https://user-images.githubusercontent.com/10614734/207279084-ef8dcc2e-1f5f-42ba-b33a-9f33b0c04299.png)

#### STEP 3: Run or double-click the AksEdgePrompt.cmd file
![image](https://user-images.githubusercontent.com/10614734/207279237-fde5a58a-e5b7-4a1b-b50d-e5b598bdd984.png)

#### STEP 4: Run the AksEdgeAzureSetup.ps1 script in the tools\scripts\AksEdgeAzureSetup folder

```bash
# prompts for interactive login for serviceprincipal creation with Contributor role at resource group level
..\tools\scripts\AksEdgeAzureSetup\AksEdgeAzureSetup.ps1 .\aide-userconfig.json -spContributorRole
```
![image](https://user-images.githubusercontent.com/10614734/207279963-c89b5546-a2cb-493d-9ea6-0ab2928773cc.png)

### ![image](https://user-images.githubusercontent.com/10614734/207280036-954068da-ddde-43b9-a484-75f9e2135e20.png)

![image](https://user-images.githubusercontent.com/10614734/207280272-a404bcd0-badd-4c8e-8c97-ef885bed52d5.png)
![image](https://user-images.githubusercontent.com/10614734/207280300-98311514-2204-4dbe-8e72-018d7452e324.png)

## 2. Connect your cluster to Arc
#### STEP 1: Load the JSON configuration into the AksEdgeShell using Read-AideUserConfig and verify that the values are updated using Get-AideUserConfig
```bash
Read-AideUserConfig
Get-AideUserConfig
```

#### STEP 1: Load the JSON configuration into the AksEdgeShell using Read-AideUserConfig and verify that the values are updated using Get-AideUserConfig

```bash
Read-AideUserConfig
Get-AideUserConfig
```
#### STEP 2: Run Initialize-AideArc
```bash
Initialize-AideArc
```
![image](https://user-images.githubusercontent.com/10614734/207280979-dca50aaf-e3b0-4393-ac97-ac7ce83827b9.png)

#### STEP 3: Run Connect-AideArc
```bash
# Connect Arc-enabled server and Arc-enabled kubernetes
Connect-AideArc
```
![image](https://user-images.githubusercontent.com/10614734/207281095-afc7289f-0ac6-424b-a9f0-87e01159509a.png)


## 3. View AKS Edge resources in Azure
![image](https://user-images.githubusercontent.com/10614734/207281237-b224d065-7f9b-4bfe-b05b-3a1fc4febc11.png)

#### Go to your ../tools/servicetoken.txt file, copy the full string, and paste it into the Azure portal. You can also run Get-AideArcKubernetesServiceToken to retrieve your service token.
![image](https://user-images.githubusercontent.com/10614734/207283577-2419f14d-48f9-41fd-8f8d-13dc3c857a5a.png)


