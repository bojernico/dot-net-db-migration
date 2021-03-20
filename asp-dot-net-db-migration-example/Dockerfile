FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS build
WORKDIR /src
COPY published/School.Core.dll ./
ENTRYPOINT ["dotnet", "School.Core.dll"]