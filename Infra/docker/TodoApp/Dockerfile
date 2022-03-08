ARG  DOTNET_VERSION=5.0
FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS build

COPY TodoApp /app/
RUN dotnet publish /app/TodoApp.csproj -c Release -o /public

FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION}-alpine
WORKDIR /public
COPY --from=build /public .

ARG API_URL_CONNECTION
ENV API_URL_CONNECTION=$API_URL_CONNECTION

ENTRYPOINT ["/usr/bin/dotnet", "/public/TodoApp.dll"]