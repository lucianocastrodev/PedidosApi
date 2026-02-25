# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5220

# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# ⚡ Copia só o csproj primeiro para aproveitar cache
COPY PedidosApi/PedidosApi.csproj ./PedidosApi/
RUN dotnet restore "./PedidosApi/PedidosApi.csproj"

# Copia todo o restante do código
COPY . .

WORKDIR "/src/PedidosApi"
RUN dotnet build "./PedidosApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Etapa de publish
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PedidosApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Etapa final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PedidosApi.dll"]