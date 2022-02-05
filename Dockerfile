ARG  DOTNET_VERSION=5.0
FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS build

COPY TodoApi /app/
RUN dotnet publish /app/TodoApi.csproj -c Release -o /public

FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION}-alpine
WORKDIR /public
COPY --from=build /public .

ARG CONNECTION_STRING
ENV CONNECTION_STRING=$CONNECTION_STRING

ENTRYPOINT ["/usr/bin/dotnet", "/public/TodoApi.dll"]