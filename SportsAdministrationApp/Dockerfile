#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["SportsAdministrationApp/SportsAdministrationApp.csproj", "SportsAdministrationApp/"]
RUN dotnet restore "SportsAdministrationApp/SportsAdministrationApp.csproj"
COPY . .
WORKDIR "/src/SportsAdministrationApp"
RUN dotnet build "SportsAdministrationApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SportsAdministrationApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SportsAdministrationApp.dll"]