# Use the .NET SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["OcelotApiGateway.csproj", "./"]
RUN dotnet restore "./OcelotApiGateway.csproj"

# Copy the entire source code and build the application
COPY . .
RUN dotnet publish "OcelotApiGateway.csproj" -c Release -o /app/publish

# Use the .NET runtime image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Create a non-root user and switch to it
RUN adduser --disabled-password --gecos '' appuser
USER appuser

# Expose the port the app runs on
EXPOSE 5000

# Define the entry point for the application
ENTRYPOINT ["dotnet", "OcelotApiGateway.dll"]