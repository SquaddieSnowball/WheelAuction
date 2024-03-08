FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/WheelAuction.Domain/WheelAuction.Domain.csproj", "src/WheelAuction.Domain/"]
COPY ["src/WheelAuction.Application/WheelAuction.Application.csproj", "src/WheelAuction.Application/"]
COPY ["src/WheelAuction.Infrastructure/WheelAuction.Infrastructure.csproj", "src/WheelAuction.Infrastructure/"]
COPY ["src/WheelAuction.WebUI.Client/WheelAuction.WebUI.Client.csproj", "src/WheelAuction.WebUI.Client/"]
COPY ["src/WheelAuction.WebUI.Server/WheelAuction.WebUI.Server.csproj", "src/WheelAuction.WebUI.Server/"]
RUN dotnet restore "./src/WheelAuction.WebUI.Server/WheelAuction.WebUI.Server.csproj"
COPY . .
WORKDIR "/src/src/WheelAuction.WebUI.Server"
RUN dotnet build "./WheelAuction.WebUI.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WheelAuction.WebUI.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WheelAuction.WebUI.Server.dll"]