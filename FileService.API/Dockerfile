﻿FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["FileService.API/FileService.API.csproj", "FileService.API/"]
RUN dotnet restore "FileService.API/FileService.API.csproj"
COPY . .
WORKDIR "/src/FileService.API"
RUN dotnet build "FileService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FileService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FileService.API.dll"]
