FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AestheticsLife.FileUploader.Service/AestheticsLife.FileUploader.Service.csproj", "AestheticsLife.FileUploader.Service/"]
COPY ["AestheticsLife.File.Services.Abstractions/AestheticsLife.File.Services.Abstractions.csproj", "AestheticsLife.File.Services.Abstractions/"]
COPY ["AestheticsLife.File.Services/AestheticsLife.File.Services.csproj", "AestheticsLife.File.Services/"]
RUN dotnet restore "AestheticsLife.FileUploader.Service/AestheticsLife.FileUploader.Service.csproj"
COPY . .
WORKDIR "/src/AestheticsLife.FileUploader.Service"
RUN dotnet build "AestheticsLife.FileUploader.Service.csproj" -c Release -o /app/build
RUN dotnet pack "AestheticsLife.FileUploader.Service.csproj" --no-build

FROM build AS publish
RUN dotnet publish "AestheticsLife.FileUploader.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AestheticsLife.FileUploader.Service.dll"]
