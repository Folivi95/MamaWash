FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["MamaWash/MamaWash.csproj", "MamaWash/"]
RUN dotnet restore "MamaWash/MamaWash.csproj"
COPY . .
WORKDIR "/src/MamaWash"
RUN dotnet build "MamaWash.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "MamaWash.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MamaWash.dll"]