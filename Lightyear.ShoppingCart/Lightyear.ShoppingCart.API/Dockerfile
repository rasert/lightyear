FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY Lightyear.ShoppingCart.API/Lightyear.ShoppingCart.API.csproj Lightyear.ShoppingCart.API/
RUN dotnet restore Lightyear.ShoppingCart.API/Lightyear.ShoppingCart.API.csproj
COPY . .
WORKDIR /src/Lightyear.ShoppingCart.API
RUN dotnet build Lightyear.ShoppingCart.API.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Lightyear.ShoppingCart.API.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Lightyear.ShoppingCart.API.dll"]
