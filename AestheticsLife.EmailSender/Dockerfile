FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

ENV AzureWebJobsStorage=$AzureWebJobsStorage
ENV BlobContainerName=$BlobContainerName
ENV BlobOutputContainerName=$BlobOutputContainerName
ENV QueueName=$QueueName

WORKDIR /src
COPY ["AestheticsLife.EmailSender/AestheticsLife.EmailSender.csproj", "AestheticsLife.EmailSender/"]
RUN dotnet restore "AestheticsLife.EmailSender/AestheticsLife.EmailSender.csproj"

COPY . .
WORKDIR "/src/AestheticsLife.EmailSender"
RUN dotnet build "AestheticsLife.EmailSender.csproj" -c Release -o /app/build
RUN dotnet pack "AestheticsLife.EmailSender.csproj" --no-build

FROM build AS publish
RUN dotnet publish "AestheticsLife.EmailSender.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AestheticsLife.EmailSender.dll"]