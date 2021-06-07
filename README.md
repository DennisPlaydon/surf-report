# Surf Report

## About
This app is a lambda that queries a MetService surf report app for beaches I enjoy attending and sends me a push notification once per day about upcoming good surf.

## Architecture
This is a lambda hosted in AWS.
It uses EventBridge to trigger the lambda once per day at 10am

## Deploying
### Packaging lambda
Run `dotnet lambda package SurfReport.zip --framework netcoreapp3.1 -pl .` to create the zip file with all the assets. This can then be uploaded in the AWS console or use a pipeline to deploy it.