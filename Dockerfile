# Use official .NET SDK for building the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the project file and restore dependencies
COPY ["KSAApi.csproj", "./"]
RUN dotnet restore

# Copy everything else and build the project
COPY . ./
RUN dotnet publish -c Release -o /app/out

# Use a lightweight runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy the build output
COPY --from=build /app/out .

# Expose port 8080 for Render
EXPOSE 8080
CMD ["dotnet", "KSAApi.dll"]
