#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AloDoutor.API/AloDoutor.Api.csproj", "AloDoutor.API/"]
COPY ["AloDoutor.Application/AloDoutor.Application.csproj", "AloDoutor.Application/"]
COPY ["AloDoutor.Domain/AloDoutor.Domain.csproj", "AloDoutor.Domain/"]
COPY ["AloDoutor.Infrastructure/AloDoutor.Infrastructure.csproj", "AloDoutor.Infrastructure/"]
RUN dotnet restore "AloDoutor.API/AloDoutor.Api.csproj"

COPY . . 
WORKDIR "/src/AloDoutor.API"
RUN dotnet build "./AloDoutor.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AloDoutor.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 4040
ENV ASPNETCORE_URLS=http://*:4040
ENTRYPOINT ["dotnet", "AloDoutor.Api.dll"]