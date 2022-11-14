FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 81

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["AestheticsLife.Gateway.Service/AestheticsLife.Gateway.Service.csproj", "AestheticsLife.Gateway.Service/"]
RUN dotnet restore "AestheticsLife.Gateway.Service/AestheticsLife.Gateway.Service.csproj"
COPY . .
WORKDIR "/src/AestheticsLife.Gateway.Service"
RUN dotnet build "AestheticsLife.Gateway.Service.csproj" -c Release -o /app/build
RUN dotnet pack "AestheticsLife.Gateway.Service.csproj" --no-build

FROM build AS publish
RUN dotnet publish "AestheticsLife.Gateway.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AestheticsLife.Gateway.Service.dll"]