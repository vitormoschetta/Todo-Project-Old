ARG  DOTNET_VERSION=5.0
FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS build

COPY TodoApp /app/
RUN dotnet publish /app/TodoApp.csproj -c Release -o /public

FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION}-alpine
WORKDIR /public
COPY --from=build /public .

ARG SERVER_URL
ENV SERVER_URL=$SERVER_URL

ENTRYPOINT ["/usr/bin/dotnet", "/public/TodoApp.dll"]