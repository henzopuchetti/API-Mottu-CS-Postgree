# ---------- STAGE: build ----------
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src

# Copiar csproj e restaurar
COPY ["MottuApi.csproj", "."]
RUN dotnet restore MottuApi.csproj

# Copiar resto do código
COPY . . 

# Publicar em Release
RUN dotnet publish MottuApi.csproj -c Release -o /app/publish

# ---------- STAGE: runtime ----------
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
WORKDIR /app

# Criar usuário sem privilégios
RUN addgroup -S appgroup && adduser -S -G appgroup appuser

# Copiar build da stage anterior
COPY --from=build /app/publish ./

# Rodar como usuário não-root
USER appuser

# Definir URL da API e expor porta
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "MottuApi.dll"]
