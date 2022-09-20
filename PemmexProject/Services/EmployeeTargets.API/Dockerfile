#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/EmployeeTargets.API/EmployeeTargets.API.csproj", "Services/EmployeeTargets.API/"]
COPY ["Common/PemmexCommonLibs/PemmexCommonLibs.csproj", "Common/PemmexCommonLibs/"]
COPY ["Common/EventBus.Messages/EventBus.Messages.csproj", "Common/EventBus.Messages/"]
RUN dotnet restore "Services/EmployeeTargets.API/EmployeeTargets.API.csproj" --disable-parallel
COPY . .
WORKDIR "/src/Services/EmployeeTargets.API"
RUN dotnet build "EmployeeTargets.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EmployeeTargets.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EmployeeTargets.API.dll"]
