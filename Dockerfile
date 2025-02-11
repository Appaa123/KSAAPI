# Use official .NET SDK image for build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy all files and build the application
COPY . ./
RUN dotnet publish -c Release -o /out

# Use a lightweight runtime for execution
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /out ./

# Expose port 8080
EXPOSE 8080
CMD ["dotnet", "KSAAPI.dll"]
