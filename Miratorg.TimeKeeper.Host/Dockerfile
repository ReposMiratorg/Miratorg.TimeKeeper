FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

RUN apt-get update && apt-get install -y \
    libldap-2.5
#    && rm -rf /var/lib/apt/lists/*

USER app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Miratorg.TimeKeeper.Host/Miratorg.TimeKeeper.Host.csproj", "Miratorg.TimeKeeper.Host/"]
COPY ["Miratorg.TimeKeeper.BusinessLogic/Miratorg.TimeKeeper.BusinessLogic.csproj", "Miratorg.TimeKeeper.BusinessLogic/"]
COPY ["Miratorg.TimeKeeper.DataAccess/Miratorg.TimeKeeper.DataAccess.csproj", "Miratorg.TimeKeeper.DataAccess/"]
RUN dotnet restore "./Miratorg.TimeKeeper.Host/Miratorg.TimeKeeper.Host.csproj"
COPY . .
WORKDIR "/src/Miratorg.TimeKeeper.Host"
RUN dotnet build "./Miratorg.TimeKeeper.Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Miratorg.TimeKeeper.Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Miratorg.TimeKeeper.Host.dll"]