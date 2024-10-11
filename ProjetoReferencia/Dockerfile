# Usando a imagem base do .NET
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

# Usando a imagem do SDK para construir o projeto
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copie os arquivos do projeto
COPY ["ProjetoReferencia/ProjetoReferencia.csproj", "ProjetoReferencia/"]
COPY ["ProjetoReferencia.Application/ProjetoReferencia.Application.csproj", "ProjetoReferencia.Application/"]
COPY ["ProjetoReferencia.Domain/ProjetoReferencia.Domain.csproj", "ProjetoReferencia.Domain/"]
COPY ["ProjetoReferencia.Infra/ProjetoReferencia.Infra.csproj", "ProjetoReferencia.Infra/"]

# Restaure as dependências
RUN dotnet restore "ProjetoReferencia/ProjetoReferencia.csproj"

# Copie o restante dos arquivos do projeto e compile
COPY . .
WORKDIR "/src/ProjetoReferencia"
RUN dotnet build "ProjetoReferencia.csproj" -c Release -o /app/build

# Fase de publicação
FROM build AS publish
RUN dotnet publish "ProjetoReferencia.csproj" -c Release -o /app/publish

# Imagem final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Comando padrão para iniciar a aplicação
ENTRYPOINT ["dotnet", "ProjetoReferencia.dll"]
