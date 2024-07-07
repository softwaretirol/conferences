# Profiling a .NET6 app running in a linux container with dotnet-trace, dotnet-dump, dotnet-counters, dotnet-gcdump and Visual Studio

This repository contains a demo application. It is a .NET 6 API with 3 endpoints:

- /blocking-threads endpoint.
- /high-cpu endpoint.
- /memory-leak endpoint.

Each endpoint contains a different performance issue.

This repository is used to demonstrate how to profile an app using  the .NET CLI diagnostic tools (dotnet-dump, dotnet-trace, dotnet-counters, dotnet-gcdump) and Visual Studio.   

More info about it on my blog post:
- https://www.mytechramblings.com/posts/profiling-a-net-app-with-dotnet-cli-diagnostic-tools/

