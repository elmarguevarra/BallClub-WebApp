#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["BallClub/BallClub.csproj", "BallClub/"]
COPY ["BallClub.Repositories/BallClub.Repositories.csproj", "BallClub.Repositories/"]
COPY ["BallClub.Domain/BallClub.Domain.csproj", "BallClub.Domain/"]
COPY ["BallClub.Repository.MySQL/BallClub.Repository.Dapper.csproj", "BallClub.Repository.MySQL/"]
RUN dotnet restore "BallClub/BallClub.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "BallClub.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BallClub.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BallClub.dll"]