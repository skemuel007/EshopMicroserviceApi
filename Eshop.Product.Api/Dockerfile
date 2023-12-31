FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Eshop.Product.Api/Eshop.Product.Api.csproj", "Eshop.Product.Api/"]
COPY ["Eshop.Infrastructure/Eshop.Infrastructure.csproj", "Eshop.Infrastructure/"]
COPY ["Eshop.Product.DataProvider/Eshop.Product.DataProvider.csproj", "Eshop.Product.DataProvider/"]
RUN dotnet restore "Eshop.Product.Api/Eshop.Product.Api.csproj"
COPY . .
WORKDIR "/src/Eshop.Product.Api"
RUN dotnet build "Eshop.Product.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Eshop.Product.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Eshop.Product.Api.dll"]
