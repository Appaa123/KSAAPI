# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["KSAApi.csproj", "./"]
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Runtime with required system libs
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Install SRV resolution dependencies
RUN apt-get update && apt-get install -y \
    openssl \
    libnss3 \
    ca-certificates \
    dnsutils \
    && rm -rf /var/lib/apt/lists/*

COPY --from=build /app/publish .

# For Render or other cloud deployment
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "KSAApi.dll"]
