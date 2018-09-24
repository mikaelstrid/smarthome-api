FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
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