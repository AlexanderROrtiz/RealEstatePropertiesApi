# Build stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["src/RealEstate.Properties.Api/RealEstate.Properties.Api.csproj", "src/RealEstate.Properties.Api/"]
COPY ["src/RealEstate.Properties.Application/RealEstate.Properties.Application.csproj", "src/RealEstate.Properties.Application/"]
COPY ["src/RealEstate.Properties.Domain/RealEstate.Properties.Domain.csproj", "src/RealEstate.Properties.Domain/"]
COPY ["src/RealEstate.Properties.Infrastructure/RealEstate.Properties.Infrastructure.csproj", "src/RealEstate.Properties.Infrastructure/"]

RUN dotnet restore "src/RealEstate.Properties.Api/RealEstate.Properties.Api.csproj"

COPY . .
WORKDIR "/src/src/RealEstate.Properties.Api"
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "RealEstate.Properties.Api.dll"]