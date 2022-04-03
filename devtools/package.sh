cd ..
cd src/
dotnet tool update -g Amazon.Lambda.Tools
dotnet lambda package SurfReport.zip --framework netcoreapp3.1 -pl .