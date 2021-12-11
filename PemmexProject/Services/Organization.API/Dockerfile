#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Organization.API/Organization.API.csproj", "Organization.API/"]
COPY ["PemmexCommonLibs/PemmexCommonLibs.csproj", "PemmexCommonLibs/"]
RUN dotnet restore "Organization.API/Organization.API.csproj"
COPY . .
WORKDIR "/src/Organization.API"
RUN dotnet build "Organization.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Organization.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Organization.API.dll"]
