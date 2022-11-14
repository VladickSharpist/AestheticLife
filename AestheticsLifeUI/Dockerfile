FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AestheticsLifeUI/AestheticsLifeUI.csproj", "AestheticsLifeUI/"]
RUN dotnet restore "AestheticsLifeUI/AestheticsLifeUI.csproj"
COPY . .
WORKDIR "/src/AestheticsLifeUI"
RUN dotnet build "AestheticsLifeUI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AestheticsLifeUI.csproj" -c Release -o /app/publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY AestheticsLifeUI/nginx.conf /etc/nginx/nginx.conf
