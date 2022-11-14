FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 81

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AestheticLife.Auth.Services.Abstractions/AestheticLife.Auth.Services.Abstractions.csproj", "AestheticLife.Auth.Services.Abstractions/"]
COPY ["Microservices.Shared/Microservices.Shared.csproj", "Microservices.Shared/"]
COPY ["RabbitMq/RabbitMq.csproj", "RabbitMq/"]
COPY ["AestheticsLife.User.Service/AestheticsLife.User.Service.csproj", "AestheticsLife.User.Service/"]
COPY ["Aesthetic.CQRS/Aesthetic.CQRS.csproj", "Aesthetic.CQRS/Aesthetic.CQRS/"]
COPY ["AestheticsLife.DataAccess.User/AestheticsLife.DataAccess.User.csproj", "AestheticsLife.DataAccess.User/"]
RUN dotnet restore "AestheticsLife.User.Service/AestheticsLife.User.Service.csproj"
COPY . .
WORKDIR "/src/AestheticsLife.User.Service"
RUN dotnet build "AestheticsLife.User.Service.csproj" -c Release -o /app/build
RUN dotnet pack "AestheticsLife.User.Service.csproj" --no-build

FROM build AS publish
RUN dotnet publish "AestheticsLife.User.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AestheticsLife.User.Service.dll"]