# Build BattleshipsBE
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS dotnetbuild
WORKDIR /src

ARG src="Szerver/Osztott szavazasi rendszer/Osztott szavazasi rendszer/Osztott szavazasi rendszer.csproj"
COPY ${src} .
RUN dotnet restore

ARG src2="Szerver/Osztott szavazasi rendszer/Osztott szavazasi rendszer"
COPY ${src2} .
RUN dotnet build -c Release

# RUN dotnet test ...

RUN dotnet publish -c Release -o /dist


# build Vue app:
FROM node:alpine as buildvue

WORKDIR /src

COPY Kliens/package.json .
RUN npm install

# webpack build
COPY Kliens .
RUN npm run build


# Copy results from both places into production container:
FROM mcr.microsoft.com/dotnet/sdk:6.0

WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT Production
ENV ASPNETCORE_URLS http://+:5000
EXPOSE 5000

# copy .net content
COPY --from=dotnetbuild /dist .
# copy vue content into .net's static files folder:
COPY --from=buildvue /src/dist /app/wwwroot

CMD ASPNETCORE_URLS=http://*:$PORT dotnet "Osztott szavazasi rendszer.dll"