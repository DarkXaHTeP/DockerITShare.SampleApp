# build frontend
FROM node:10.15.3-stretch AS frontend-env

WORKDIR /app

COPY . ./
RUN npm install
RUN npm run build:prod

# build backend
FROM mcr.microsoft.com/dotnet/core/sdk:2.2.105-alpine3.9 AS build-env
WORKDIR /app

COPY . ./
COPY --from=frontend-env /app/wwwroot ./wwwroot

RUN dotnet publish SampleApp.Web.csproj -c Release -r linux-musl-x64 -o out

# build runtime image
FROM mcr.microsoft.com/dotnet/core/runtime-deps:2.2.3-alpine3.9
WORKDIR /app
COPY --from=build-env /app/out ./
ENV ASPNETCORE_URLS http://0.0.0.0:5000
EXPOSE 5000
ENTRYPOINT ["./SampleApp.Web"]