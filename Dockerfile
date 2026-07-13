FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS build

WORKDIR /app

COPY . .

RUN dotnet publish "api/api.csproj" -o api/published -p:UseAppHost=false
 


FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine

WORKDIR /app

COPY --from=build /app/api/published/ ./

ENTRYPOINT [ "dotnet", "api.dll" ]