# Azure Kubernetes Services (AKS) Edge Essentials - Hands-on

## Overview

This repository complements primary Azure Kubernetes Services Edge Essentials (aka. AKS EE) repository located [HERE](https://github.com/Azure/AKS-Edge) Purpose of this complement is to provide deeper hands-on experience and information to facilitate evaluation and adoption of AKS EE.

- Tracking features and issues with the Azure Kubernetes Service (AKS) Edge Essentials MUST happen in [AKS EE primary repository](https://github.com/Azure/AKS-Edge) and not in this repository.
- This repository is forked from [AKS EE primary repository](https://github.com/Azure/AKS-Edge) and intent is to keep it in sync with this upstream main branch.
- Proposed complements should reside in [hands-on directory](./hands-on) and relates to following areas:

| Name           | Description      |
|----------------|------------------|
| [install](./hands-on/install/install.md) | Procedure and troubleshooting AKS EE installation. |
| [arc](./hands-on/arc/arc.md) | Connection to Azure via Azure ARC and usage of proposed extensions (eg. GitOps) | 
| [interop](./hands-on/interop/readme.md) |  Interoperability between AKS EE K8s/K3s cluster and native Windows applications. | 
| cncf (to come) |  Usage of additional CNCF extension to extend AKS EE K8s/K3s cluster capabilities (eg. Helm, NGINX ...). | 

> [!IMPORTANT]
> 02/03/2023 -  Public preview has been refreshed! Try out our new bits.(Release Candidate for GA) (version 1.0.266.0).

## Important Links

- AKS Edge Essentials Primary GitHub repository: https://github.com/Azure/AKS-Edge
- AKS Edge Essentials Overview: https://aka.ms/aks-edge/overview
- AKS Edge Essentials Quickstart: https://aka.ms/aks-edge/quickstart
- AKS Edge Essentials Release Notes: https://aka.ms/aks-edge/releases
- AKS Esge Essentials PowerShell Reference: https://aka.ms/aks-edge/reference

## Bug Guidance

Bug reports filed on this repository should concern content in [hands-on directory](./hands-on) and follow the default issue template that is shown when opening a new issue. At a bare minimum, issues reported on this repository must:

1. **Be reproducible outside of the current cluster**

    This means that if you file an issue that would require direct access to your cluster and/or Azure resources you will be redirected to open an Azure support ticket. Microsoft employees may not ask for personal / subscription information on Github. 

1. **Contain the following information**
    1. **A good title**: Clear, relevant and descriptive - so that a general idea of the problem can be grasped immediately
    1. **Description**: Before you go into the detail of steps to replicate the issue, you need a brief description. Assume that whomever is reading the report is unfamiliar with the issue/system in question
    1. **Clear, concise steps to replicate the issue outside of your specific cluster**.These should let anyone clearly see what you did to see the problem, and also allow them to recreate it easily themselves. This section should also include results - both expected and the actual - along with relevant URLs.
    1. **Be sure to include any supporting information you might have that could aid the developers**.This includes YAML files/deployments, scripts to reproduce, exact commands used, screenshots, etc.

## Contributing

This project welcomes contributions and suggestions. Most contributions require you to agree to a Contributor License Agreement (CLA) declaring that you have the right to, and actually do, grant us
the rights to use your contribution. For details, visit [Microsoft CLA Opensource](https://cla.opensource.microsoft.com).

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/).
For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or
contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.