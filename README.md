# Brandsdal Group CasePortal
If you're reading this you likely got a second job interview at Brandsdal Group coming up.

## Background
This is a simplified version of how a web application is typically structured in Brandsdal Group.
It is meant to give a more relevant case environment than "New Project" would do.

Web application is based on a ASP.NET Core MVC Web App template, with some of our adaptations for structure and setup.

## Getting Started
1. Clone project (avoid using a fork as the fork will be publicly visible)
2. Open solution in an up to date Visual Studio 2022 instance (any edition will do, including Community)
3. Restore NPM packages
4. Make sure project builds and runs (if you have any issues, follow instructions in email)
5. Implement the requirements outlined pr email

## NServiceBus setup
It's required to add a License xml file to the local app data folder in order to send/receive messages on the NServiceBus. 
The license file is included in the solution under the Configuration folder. This needs to copied to a new folder (ParticularSoftware) in you local app data folder.

For Windows cmd.exe:
mkdir %LocalAppData%\ParticularSoftware 2> NUL
copy /Y license.xml %LocalAppData%\ParticularSoftware

For Windows PowerShell:
mkdir -Force $Env:LocalAppData\ParticularSoftware
copy license.xml $Env:LocalAppData\ParticularSoftware

## Delivery of finished case
1. Delete the node_modules folder from your finished project.
2. Zip and return by email or fileshare link (do not PR as it will be publicly visible)
