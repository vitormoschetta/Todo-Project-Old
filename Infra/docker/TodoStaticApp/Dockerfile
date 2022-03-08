ARG  DOTNET_VERSION=5.0
FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS build
COPY ./TodoStaticApp /app
RUN dotnet publish /app/TodoApp.csproj -c Release -o /public

FROM nginx:alpine
WORKDIR /usr/local/webapp/nginx/html
COPY --from=build /public/wwwroot .
COPY /Infra/docker/TodoStaticApp/nginx.conf /etc/nginx/nginx.conf