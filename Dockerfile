# Use the official .NET SDK image for building the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory inside the container
WORKDIR /app

# Copy the solution file and restore dependencies
COPY MTFG_bot.sln ./
COPY MTFG_bot/*.csproj ./MTFG_bot/
RUN dotnet restore

# Copy the rest of the project files
COPY . ./

# Build the project in Release mode and publish the output
RUN dotnet publish ./MTFG_bot/MTFG_bot.csproj -c Release -o /out

# Use a smaller runtime image for the final deployment
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS runtime

# Set the working directory for the runtime
WORKDIR /app

# Copy the built output from the build stage
COPY --from=build /out .

# Set the environment variable (if needed)
# ENV MY_ENV_VAR=your_value

# Entry point to run the console app
ENTRYPOINT ["dotnet", "MTFG_bot.dll"]
