FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /app

COPY . .

RUN dotnet publish "api/api.csproj" -o api/published -p:UseAppHost=false
 


FROM mcr.microsoft.com/dotnet/aspnet:10.0

WORKDIR /app

COPY --from=build api/published/ ./

ENTRYPOINT [ "dotnet", "api.dll" ]