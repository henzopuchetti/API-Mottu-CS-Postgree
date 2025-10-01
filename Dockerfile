# ---------- STAGE: build ----------
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src

# Copiar csproj e restaurar dependências
COPY ["MottuApi.csproj", "."]
RUN dotnet restore "MottuApi.csproj"

# Copiar todo o código
COPY . .

# Publicar em Release
RUN dotnet publish "MottuApi.csproj" -c Release -o /app/publish

# ---------- STAGE: runtime ----------
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
WORKDIR /app

# Criar usuário sem privilégios (não-root)
RUN addgroup -S appgroup && adduser -S -G appgroup appuser

# Copiar arquivos publicados
COPY --from=build /app/publish ./

# Usar usuário não-root
USER appuser

# Definir porta de execução
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "MottuApi.dll"]
