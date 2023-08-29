FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NWCodeFirstMVC/NWCodeFirstMVC.csproj", "NWCodeFirstMVC/"]
COPY ["NwCodeFirstMVC.Data/NwCodeFirstMVC.Data.csproj", "NwCodeFirstMVC.Data/"]
COPY ["NWCodeFirstMVC.Domain/NWCodeFirstMVC.Domain.csproj", "NWCodeFirstMVC.Domain/"]
COPY ["NWCodeFirstMVC.App/NWCodeFirstMVC.App.csproj", "NWCodeFirstMVC.App/"]
COPY ["NWCodeFirstMVC.Infrastructure/NWCodeFirstMVC.Infrastructure.csproj", "NWCodeFirstMVC.Infrastructure/"]
COPY ["NWCodeFirstMVC.Api/NWCodeFirstMVC.Api.csproj", "NWCodeFirstMVC.Api/"]
RUN dotnet restore "NWCodeFirstMVC.Api/NWCodeFirstMVC.Api.csproj"
COPY . .
WORKDIR "/src/NWCodeFirstMVC.Api"
RUN dotnet build "NWCodeFirstMVC.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN apt-get update -yq \
    && apt-get install curl gnupg -yq \
    && curl -sL https://deb.nodesource.com/setup_14.x | bash \
    && apt-get install nodejs -yq
RUN dotnet publish "NWCodeFirstMVC.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NWCodeFirstMVC.Api.dll"]
