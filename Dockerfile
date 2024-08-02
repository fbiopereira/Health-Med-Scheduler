#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["HealthMedScheduler.API/HealthMedScheduler.Api.csproj", "HealthMedScheduler.API/"]
COPY ["HealthMedScheduler.Application/HealthMedScheduler.Application.csproj", "HealthMedScheduler.Application/"]
COPY ["HealthMedScheduler.Domain/HealthMedScheduler.Domain.csproj", "HealthMedScheduler.Domain/"]
COPY ["HealthMedScheduler.Infrastructure/HealthMedScheduler.Infrastructure.csproj", "HealthMedScheduler.Infrastructure/"]
RUN dotnet restore "HealthMedScheduler.API/HealthMedScheduler.Api.csproj"

COPY . . 
WORKDIR "/src/HealthMedScheduler.API"
RUN dotnet build "./HealthMedScheduler.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./HealthMedScheduler.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 4040
ENV ASPNETCORE_URLS=http://*:4040
ENTRYPOINT ["dotnet", "HealthMedScheduler.Api.dll"]
