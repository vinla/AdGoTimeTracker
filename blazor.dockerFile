FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY AdGoTimeTracker/*.csproj AdGoTimeTracker/
COPY AdGoTimeTracker.Client/*.csproj AdGoTimeTracker.Client/
COPY AdGoTimeTracker.Core/*.csproj AdGoTimeTracker.Core/

RUN dotnet restore "./AdGoTimeTracker/AdGoTimeTracker.csproj"
RUN dotnet restore "./AdGoTimeTracker.Core/AdGoTimeTracker.Core.csproj"
RUN dotnet restore "./AdGoTimeTracker.Client/AdGoTimeTracker.Client.csproj"
COPY . .
RUN dotnet build "./AdGoTimeTracker/AdGoTimeTracker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./AdGoTimeTracker/AdGoTimeTracker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AdGoTimeTracker.dll"]