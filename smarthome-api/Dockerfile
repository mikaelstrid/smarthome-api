FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["smarthome-api/smarthome-api.csproj", "smarthome-api/"]
RUN dotnet restore "smarthome-api/smarthome-api.csproj"
COPY . .
WORKDIR "/src/smarthome-api"
RUN dotnet build "smarthome-api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "smarthome-api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "smarthome-api.dll"]