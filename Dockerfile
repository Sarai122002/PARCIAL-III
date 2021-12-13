FROM mcr.microsoft.com/dotnet/aspnet:5.0

COPY bin/Release/net5.0/publish/ ParcialComputo3/
WORKDIR /ParcialComputo3
CMD ASPNETCORE_URLS=http://*:$PORT dotnet ParcialComputo3.dll