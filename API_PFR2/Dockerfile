# Base runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

# Phase build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copier le csproj et restaurer les dépendances
COPY ["API_PFR2/API_PFR2.csproj", "API_PFR2/"]
RUN dotnet restore "API_PFR2/API_PFR2.csproj"

# Copier tout le reste du projet
COPY API_PFR2/ API_PFR2/
WORKDIR "/src/API_PFR2"
RUN dotnet build "API_PFR2.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Phase publish
FROM build AS publish
RUN dotnet publish "API_PFR2.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Phase finale
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
USER app 
ENTRYPOINT ["dotnet", "API_PFR2.dll"]