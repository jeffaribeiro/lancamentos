FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 6000
EXPOSE 6001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["Lancamentos.Consolidado/Lancamentos.Consolidado.Api/Lancamentos.Consolidado.Api.csproj", "Lancamentos.Consolidado/Lancamentos.Consolidado.Api/"]
RUN dotnet restore "./Lancamentos.Consolidado/Lancamentos.Consolidado.Api/./Lancamentos.Consolidado.Api.csproj"
COPY . .

WORKDIR "/src/Lancamentos.Consolidado/Lancamentos.Consolidado.Api"
RUN dotnet build "./Lancamentos.Consolidado.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Lancamentos.Consolidado.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lancamentos.Consolidado.Api.dll"]