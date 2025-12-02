# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY CreditoApi.sln ./
COPY src/CreditoApi/CreditoApi.csproj src/CreditoApi/
COPY src/CreditoApi.Application/CreditoApi.Application.csproj src/CreditoApi.Application/
COPY src/CreditoApi.Domain/CreditoApi.Domain.csproj src/CreditoApi.Domain/
COPY src/CreditoApi.Infrastructure/CreditoApi.Infrastructure.csproj src/CreditoApi.Infrastructure/
COPY src/CreditoApi.SharedKernel/CreditoApi.SharedKernel.csproj src/CreditoApi.SharedKernel/
COPY src/CreditoApi.Worker/CreditoApi.Worker.csproj src/CreditoApi.Worker/
COPY tests/CreditoApi.UnitTests/CreditoApi.UnitTests.csproj tests/CreditoApi.UnitTests/

RUN dotnet restore CreditoApi.sln

COPY . .
RUN dotnet publish src/CreditoApi/CreditoApi.csproj -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CreditoApi.dll"]
