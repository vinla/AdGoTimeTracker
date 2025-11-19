# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY AdGoTimeTracker.Api/*.csproj AdGoTimeTracker.Api/
COPY AdGoTimeTracker.Core/*.csproj AdGoTimeTracker.Core/
COPY AdGoTimeTracker.MemoryStore/*.csproj AdGoTimeTracker.MemoryStore/
COPY AdGoTimeTracker.SqlEntityStore/*.csproj AdGoTimeTracker.SqlEntityStore/
RUN dotnet restore "./AdGoTimeTracker.Api/AdGoTimeTracker.Api.csproj"
RUN dotnet restore "./AdGoTimeTracker.Core/AdGoTimeTracker.Core.csproj"
RUN dotnet restore "./AdGoTimeTracker.MemoryStore/AdGoTimeTracker.MemoryStore.csproj"
RUN dotnet restore "./AdGoTimeTracker.SqlEntityStore/AdGoTimeTracker.SqlEntityStore.csproj"
COPY . .
WORKDIR "/src/AdGoTimeTracker.Api"
RUN dotnet build "./AdGoTimeTracker.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AdGoTimeTracker.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AdGoTimeTracker.Api.dll"]