ARG  DOTNET_VERSION=5.0
FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS build

COPY TodoApi /build/
RUN dotnet publish /build/TodoApi.csproj -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION} AS runtime
WORKDIR /app
COPY --from=build /app .

ENTRYPOINT ["dotnet", "/app/TodoApi.dll"]