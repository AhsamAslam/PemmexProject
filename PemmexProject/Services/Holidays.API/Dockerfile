#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Holidays.API/Holidays.API.csproj", "Services/Holidays.API/"]
COPY ["Common/PemmexCommonLibs/PemmexCommonLibs.csproj", "Common/PemmexCommonLibs/"]
RUN dotnet restore "Services/Holidays.API/Holidays.API.csproj" --disable-parallel
COPY . .
WORKDIR "/src/Services/Holidays.API"
RUN dotnet build "Holidays.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Holidays.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Holidays.API.dll"]
