#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
# copy csproj and restore as distinct layers
COPY *.sln .
COPY BallClub/*.csproj ./BallClub/
COPY BallClub.Domain/*.csproj ./BallClub/
COPY BallClub.Repositories/*.csproj ./BallClub/
COPY BallClub.Repository.DTO/*.csproj ./BallClub/
COPY BallClub.Repository.MySQL/*.csproj ./BallClub/
RUN dotnet restore

# copy everything else and build app
COPY BallClub/. ./BallClub/
COPY BallClub.Domain/. ./BallClub/
COPY BallClub.Repositories/. ./BallClub/
COPY BallClub.Repository.DTO/. ./BallClub/
COPY BallClub.Repository.MySQL/. ./BallClub/
WORKDIR /source/BallClub
RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /app ./
EXPOSE 80
ENTRYPOINT ["dotnet", "BallClub.dll"]