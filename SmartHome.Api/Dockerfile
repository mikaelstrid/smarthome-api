#FROM microsoft/dotnet:2.1-sdk AS build
#WORKDIR /app

# copy csproj and restore as distinct layers
#COPY "SmartHome.Api.csproj" .
#RUN dotnet restore

# copy everything else and build app
#COPY . ./
#RUN dotnet publish -c Release -o out

#FROM microsoft/dotnet:2.1-aspnetcore-runtime AS runtime
#WORKDIR /app
#COPY --from=build /app/out ./
#ENTRYPOINT ["dotnet", "MikaelStrid.SmartHome.Api.dll"]


FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80
#EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["SmartHome.Api/SmartHome.Api.csproj", "SmartHome.Api/"]
RUN dotnet restore "SmartHome.Api/SmartHome.Api.csproj"
COPY . .
WORKDIR "/src/SmartHome.Api"
RUN dotnet build "SmartHome.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "SmartHome.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MikaelStrid.SmartHome.Api.dll"]