#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Pemmex.Identity/Pemmex.Identity.csproj", "Services/Pemmex.Identity/"]
COPY ["Common/PemmexCommonLibs/PemmexCommonLibs.csproj", "Common/PemmexCommonLibs/"]
RUN dotnet restore "Services/Pemmex.Identity/Pemmex.Identity.csproj" --disable-parallel
COPY . .
WORKDIR "/src/Services/Pemmex.Identity"
RUN dotnet build "Pemmex.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pemmex.Identity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pemmex.Identity.dll"]
