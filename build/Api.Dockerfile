FROM microsoft/dotnet:2.1-sdk AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY /src/IdentityServerApp.Api/*.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY /src/IdentityServerApp.Api/ ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM microsoft/dotnet:2.1-aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "IdentityServerApp.Api.dll"]