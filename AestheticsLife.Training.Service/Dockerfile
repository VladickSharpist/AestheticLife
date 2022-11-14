FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 81

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AestheticsLife.Training.Service/AestheticsLife.Training.Service.csproj", "AestheticsLife.Training.Service/"]
COPY ["Microservices.Shared/Microservices.Shared.csproj", "Microservices.Shared/"]
COPY ["Aesthetic.CQRS/Aesthetic.CQRS.csproj", "Aesthetic.CQRS/Aesthetic.CQRS/"]
COPY ["AestheticsLife.DataAccess.Training/AestheticsLife.DataAccess.Training.csproj", "AestheticsLife.DataAccess.Training/"]
RUN dotnet restore "AestheticsLife.Training.Service/AestheticsLife.Training.Service.csproj"
COPY . .
WORKDIR "/src/AestheticsLife.Training.Service"
RUN dotnet build "AestheticsLife.Training.Service.csproj" -c Release -o /app/build
RUN dotnet pack "AestheticsLife.Training.Service.csproj" --no-build

FROM build AS publish
RUN dotnet publish "AestheticsLife.Training.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AestheticsLife.Training.Service.dll"]