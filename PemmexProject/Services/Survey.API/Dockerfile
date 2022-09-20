#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Survey.API/Survey.API.csproj", "Services/Survey.API/"]
COPY ["Common/PemmexCommonLibs/PemmexCommonLibs.csproj", "Common/PemmexCommonLibs/"]
COPY ["Common/EventBus.Messages/EventBus.Messages.csproj", "Common/EventBus.Messages/"]
RUN dotnet restore "Services/Survey.API/Survey.API.csproj" --disable-parallel
COPY . .
WORKDIR "/src/Services/Survey.API"
RUN dotnet build "Survey.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Survey.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Survey.API.dll"]
