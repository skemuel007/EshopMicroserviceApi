FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Eshop.ApiGateway/Eshop.ApiGateway.csproj", "Eshop.ApiGateway/"]
COPY ["Eshop.Infrastructure/Eshop.Infrastructure.csproj", "Eshop.Infrastructure/"]
RUN dotnet restore "Eshop.ApiGateway/Eshop.ApiGateway.csproj"
COPY . .
WORKDIR "/src/Eshop.ApiGateway"
RUN dotnet build "Eshop.ApiGateway.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Eshop.ApiGateway.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Eshop.ApiGateway.dll"]
