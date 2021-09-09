#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["AirboxSystems.API/AirboxSystems.API.csproj", "AirboxSystems.API/"]
RUN dotnet restore "AirboxSystems.API/AirboxSystems.API.csproj"
COPY . .
WORKDIR "/src/AirboxSystems.API"
RUN dotnet build "AirboxSystems.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AirboxSystems.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AirboxSystems.API.dll"]
