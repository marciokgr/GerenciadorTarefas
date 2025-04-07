FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /
COPY ["src/GerenciadorTarefas.Domain/GerenciadorTarefas.Domain.csproj", "src/GerenciadorTarefas.Domain/"]
COPY ["src/GerenciadorTarefas.Facade/GerenciadorTarefas.Application.csproj", "src/GerenciadorTarefas.Application/"]
COPY ["src/GerenciadorTarefas.Infra.CrossCutting/GerenciadorTarefas.Infra.CrossCutting.csproj", "src/GerenciadorTarefas.Infra.CrossCutting/"]
COPY ["src/GerenciadorTarefas.Infra.Data/GerenciadorTarefas.Infra.Data.csproj", "src/GerenciadorTarefas.Infra.Data/"]
COPY ["src/GerenciadorTarefas.WebApi/GerenciadorTarefas.WebApi.csproj", "src/GerenciadorTarefas.WebApi/"]

RUN dotnet restore "src/GerenciadorTarefas.WebApi/GerenciadorTarefas.WebApi.csproj"
COPY . .
WORKDIR "/src/GerenciadorTarefas.WebApi/"
RUN dotnet build "./GerenciadorTarefas.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./GerenciadorTarefas.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GerenciadorTarefas.WebApi.dll"]
