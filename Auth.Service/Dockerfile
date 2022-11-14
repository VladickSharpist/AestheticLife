FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 81

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AestheticLife.Auth.Services.Abstractions/AestheticLife.Auth.Services.Abstractions.csproj", "AestheticLife.Auth.Services.Abstractions/"]
COPY ["Microservices.Shared/Microservices.Shared.csproj", "Microservices.Shared/"]
COPY ["Auth.Service/Auth.Service.csproj", "Auth.Service/"]
COPY ["Aesthetic.CQRS/Aesthetic.CQRS.csproj", "Aesthetic.CQRS/Aesthetic.CQRS/"]
COPY ["DataAccess.Auth/DataAccess.Auth.csproj", "DataAccess.Auth/"]
RUN dotnet restore "Auth.Service/Auth.Service.csproj"
COPY . .
WORKDIR "/src/Auth.Service"
RUN dotnet build "Auth.Service.csproj" -c Release -o /app/build
RUN dotnet pack "Auth.Service.csproj" --no-build

FROM build AS publish
RUN dotnet publish "Auth.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Auth.Service.dll"]