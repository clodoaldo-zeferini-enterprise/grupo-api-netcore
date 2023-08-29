#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Service.Grupo.API__2/Service.Grupo.API__2.csproj", "Service.Grupo.API__2/"]
RUN dotnet restore "Service.Grupo.API__2/Service.Grupo.API__2.csproj"
COPY . .
WORKDIR "/src/Service.Grupo.API__2"
RUN dotnet build "Service.Grupo.API__2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Service.Grupo.API__2.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Service.Grupo.API__2.dll"]