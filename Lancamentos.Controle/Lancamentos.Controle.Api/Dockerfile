FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["Lancamentos.Controle/Lancamentos.Controle.Api/Lancamentos.Controle.Api.csproj", "Lancamentos.Controle/Lancamentos.Controle.Api/"]
COPY ["Lancamentos.Controle/Lancamentos.Controle.Application/Lancamentos.Controle.Application.csproj", "Lancamentos.Controle/Lancamentos.Controle.Application/"]
COPY ["Lancamentos.Controle/Lancamentos.Controle.Infra.CrossCutting.IoC/Lancamentos.Controle.Infra.CrossCutting.IoC.csproj", "Lancamentos.Controle/Lancamentos.Controle.Infra.CrossCutting.IoC/"]
COPY ["Lancamentos.Controle/Lancamentos.Controle.Infra.Data/Lancamentos.Controle.Infra.Data.csproj", "Lancamentos.Controle/Lancamentos.Controle.Infra.Data/"]
RUN dotnet restore "./Lancamentos.Controle/Lancamentos.Controle.Api/./Lancamentos.Controle.Api.csproj"
COPY . .

WORKDIR "/src/Lancamentos.Controle/Lancamentos.Controle.Api"
RUN dotnet build "./Lancamentos.Controle.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Lancamentos.Controle.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lancamentos.Controle.Api.dll"]