FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY Lightyear.Catalog.API/Lightyear.Catalog.API.csproj Lightyear.Catalog.API/
RUN dotnet restore Lightyear.Catalog.API/Lightyear.Catalog.API.csproj
COPY . .
WORKDIR /src/Lightyear.Catalog.API
RUN dotnet build Lightyear.Catalog.API.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Lightyear.Catalog.API.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Lightyear.Catalog.API.dll"]
