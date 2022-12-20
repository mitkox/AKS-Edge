# AKS Edge Essentials - Azure Arc connectivity and extensions

## Lab Overview
This article describes how to connect your AKS Edge Essentials (aka. AKS EE) cluster to Azure.
Connection to Azure is performed using Azure Arc, to manage, monitor, secure and extend it.

In summary, the lab includes the following exercises:
* Review the hands-on Lab Prerequisites
* Configure Azure environment to connect AKS EE cluster to Azure Arc  
* Connect AKS EE cluster to Azure Arc
* Explore AKS EE cluster in Azure portal
* Deploy application with Azure Arc and GitOps

## Lab Prerequisites

To exercise this sample, you'll need the following:

* A single deployment of AKS EE cluster up and running on a Windows machine (possibly a VM) If you haven't done this step, please complete [AKS Edge Essentials Installation](../install/install.md) lab first.
* A download (or clone) of [Azure/AKS-Edge GitHub repository](https://github.com/Azure/AKS-Edge/tree/main) on the Windows machine.
* An Azure subscription with either the Owner role or a combination of Contributor and User Access Administrator roles.
* A github account with access permissions allowing to create a fork from an external repository and to commit in this fork.

* 💡 (Optional) If Windows machine (AKS EE cluster is installed on) is not authorized to sign in the Azure subscription, you will need either:
* ----> Another Azure subscription to create Azure Arc ressources. [Azure credits for Visual Studio subscribers](https://azure.microsoft.com/en-us/pricing/member-offers/credit-for-visual-studio-subscribers/) is opening subscription which are not restricting Windows machine authorized to sign. 
* ----> Another Windows machine with AKS EE installed (no need to have a cluster deployed on this one, just need to access AKS EE scripts) to create a service principal in the Azure Subscription.

## 1. Configure your Azure environment

Azure environment requires configuration to successfully connect your AKS EE cluster.
Follow steps below, explained in [AKS EE documentation instructions](https://learn.microsoft.com/en-us/azure/aks/hybrid/aks-edge-howto-connect-to-arc#1-configure-your-azure-environment) to prepare this configuration.
💡 These steps (and particularly script in step 1.4) must be executed from a Windows machine authorized to sign in the Azure Subscription.

#### Step 1.1 : Create and verify a resource group for AKS Edge Essentials Azure resources
![image](https://user-images.githubusercontent.com/10614734/207283890-8d3d00b3-068d-4464-bf86-59b7f7dcaf74.png)

#### Step 1.2 : Specify the required names for the resource group and service principal in the aide-userconfig.json file along with your subscription/tenant information. You can use an existing service principal or if you add a new name, the system creates one for you in the next step.Please keep the ServicePrincipalId and Password empty and this will be created and updated in the following steps.
![image](https://user-images.githubusercontent.com/10614734/207516671-1976ef55-8d55-4df7-8748-21ff87545545.png)

💡 Auth (Leave this blank, as it will be automatically filled in the next step)

#### Step 1.3 : Run or double-click the AksEdgePrompt.cmd file (tools folder)
![image](https://user-images.githubusercontent.com/10614734/207279237-fde5a58a-e5b7-4a1b-b50d-e5b598bdd984.png)

#### Step 1.4 : Run AksEdgeAzureSetup.ps1 (tools\scripts\AksEdgeAzureSetup folder)
```bash
# prompts for interactive login for service principal creation with Contributor role at resource group level
..\tools\scripts\AksEdgeAzureSetup\AksEdgeAzureSetup.ps1 .\aide-userconfig.json -spContributorRole
```

![image](https://user-images.githubusercontent.com/10614734/207279963-c89b5546-a2cb-493d-9ea6-0ab2928773cc.png)
![image](https://user-images.githubusercontent.com/10614734/207280272-a404bcd0-badd-4c8e-8c97-ef885bed52d5.png)
![image](https://user-images.githubusercontent.com/10614734/207280300-98311514-2204-4dbe-8e72-018d7452e324.png)

Once you have completed these instructions, you should have a 'ready to connect' configuration file (.\tools\aide-userconfig.json) with service principal credentials filled in.

## 2. Connect your cluster to Arc

Follow steps below, explained in [AKS EE documentation instructions](https://learn.microsoft.com/en-us/azure/aks/hybrid/aks-edge-howto-connect-to-arc#2-connect-your-cluster-to-arc) to connect AKS EE cluster to Arc.

💡 AKS EE installation includes both [Azure Connected Machine agent](https://learn.microsoft.com/en-us/azure/azure-arc/servers/agent-overview) and [Azure Arc-enabled Kubernetes agent](https://learn.microsoft.com/en-us/azure/azure-arc/kubernetes/conceptual-agent-overview).
[Azure Connected Machine agent](https://learn.microsoft.com/en-us/azure/azure-arc/servers/agent-overview) enables connectivity between Windows host and Azure ARC.
[Azure Arc-enabled Kubernetes agent](https://learn.microsoft.com/en-us/azure/azure-arc/kubernetes/conceptual-agent-overview) enables connectivity between AKS EE cluster and Azure ARC.
Thus, connection to ARC (Step 2.3) may be requested:
- both to Windows host and AKS EE cluster (Connect-AideArc)
- only to Windows host (Connect-AideArcServer)
- only AKS EE cluster (Connect-AideArcKubernetes)

#### Step 2.1 : Load JSON configuration into the AksEdgeShell using Read-AideUserConfig and verify that the values are updated using Get-AideUserConfig
```bash
Read-AideUserConfig
Get-AideUserConfig
```
#### Step 2.2 : Run Initialize-AideArc
```bash
Initialize-AideArc
```
![image](https://user-images.githubusercontent.com/10614734/207280979-dca50aaf-e3b0-4393-ac97-ac7ce83827b9.png)

#### Step 2.3 : Run Connect-AideArc
```bash
# Connect Arc-enabled server and Arc-enabled Kubernetes
Connect-AideArc
```
![image](https://user-images.githubusercontent.com/10614734/208622508-3e416d8d-71c4-4c8c-bc3e-7ee68f99aa43.png)

* 💡This step can take up to 10 minutes and PowerShell may be stuck on "Establishing Azure Connected Kubernetes for your cluster name". The PowerShell will output True and return to the prompt when the process is completed. A bearer token will be saved in servicetoken.txt in the tools folder.

## 3. Explore AKS EE cluster in Azure portal

Follow steps below, explained in [AKS EE documentation instructions](https://learn.microsoft.com/en-us/azure/aks/hybrid/aks-edge-howto-connect-to-arc#3-view-aks-edge-resources-in-azure) to explore AKS EE cluster in Azure portal.

#### Step 3.1 :

Navigate to Azure Arc to explore details on your AKS EE cluster.

![image](https://user-images.githubusercontent.com/10614734/207281237-b224d065-7f9b-4bfe-b05b-3a1fc4febc11.png)

Open ./tools/servicetoken.txt file, copy the bearer token string, and paste it into the Azure portal.
You can also run Get-AideArcKubernetesServiceToken to retrieve your service token.

![image](https://user-images.githubusercontent.com/10614734/207283577-2419f14d-48f9-41fd-8f8d-13dc3c857a5a.png)

Navigate in your AKS EE cluster to explore, namespaces, workloads (pods), services ...

## 4. Deploy application with Azure Arc and GitOps

Follow steps below, explained in [AKS EE documentation instructions](https://learn.microsoft.com/en-us/azure/aks/hybrid/aks-edge-howto-use-gitops) to deploy application with Azure Arc and GitOps
