FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SignalR.Service/SignalR.Service.csproj", "SignalR.Service/"]
RUN dotnet restore "SignalR.Service/SignalR.Service.csproj"
COPY . .
WORKDIR "/src/SignalR.Service"
RUN dotnet build "SignalR.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SignalR.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SignalR.Service.dll"]
