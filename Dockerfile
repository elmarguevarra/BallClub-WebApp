FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY *.sln ./
COPY BallClub/*.csproj ./BallClub/
COPY BallClub.Domain/*.csproj ./BallClub.Domain/
COPY BallClub.Repositories/*.csproj ./BallClub.Repositories/
COPY BallClub.Repository.DTO/*.csproj ./BallClub.Repository.DTO/
COPY BallClub.Repository.MySQL/*.csproj ./BallClub.Repository.MySQL/
RUN dotnet restore 


COPY . .
WORKDIR /src/BallClub
RUN dotnet build -c Release -o /app

WORKDIR /src/BallClub.Domain
RUN dotnet build -c Release -o /app

WORKDIR /src/BallClub.Repositories
RUN dotnet build -c Release -o /app

WORKDIR /src/BallClub.Repository.DTO
RUN dotnet build -c Release -o /app

WORKDIR /src/BallClub.Repository.MySQL
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BallClub.dll"]