# ========================
# Etapa de runtime
# ========================
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

# ========================
# Etapa de build
# ========================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copia o csproj
COPY PedidosApi.csproj ./

# Restaura dependências
RUN dotnet restore PedidosApi.csproj

# Copia todo o restante do código
COPY . .

# Build
RUN dotnet build PedidosApi.csproj -c $BUILD_CONFIGURATION -o /app/build

# ========================
# Etapa de publish
# ========================
FROM build AS publish
RUN dotnet publish PedidosApi.csproj -c Release -o /app/publish /p:UseAppHost=false

# ========================
# Etapa final
# ========================
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Render usa a porta 8080 por padrão
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "PedidosApi.dll"]