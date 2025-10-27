# Etapa 1: Build da aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia tudo
COPY . .

# Restaura dependências e compila o projeto API
RUN dotnet restore GamificationEventBackend.sln
RUN dotnet publish GamificationEvent.API/GamificationEvent.API.csproj -c Release -o /app/publish

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Expõe a porta usada pela API
EXPOSE 8080

# Define o comando de execução
ENTRYPOINT ["dotnet", "GamificationEvent.API.dll"]
