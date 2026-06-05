# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY Directory.Build.props .
COPY Portfolio.sln .

COPY src/Portfolio.Domain/Portfolio.Domain.csproj Portfolio.Domain/
COPY src/Portfolio.Application/Portfolio.Application.csproj Portfolio.Application/
COPY src/Portfolio.Infrastructure/Portfolio.Infrastructure.csproj Portfolio.Infrastructure/
COPY src/Portfolio.WebApi/Portfolio.WebApi.csproj Portfolio.WebApi/

RUN dotnet restore Portfolio.WebApi/Portfolio.WebApi.csproj

COPY src/ .

RUN dotnet publish Portfolio.WebApi/Portfolio.WebApi.csproj -c Release -o /app --no-restore

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

RUN groupadd -r appuser && useradd -r -g appuser appuser

COPY --from=build /app .

RUN mkdir -p /app/logs && chown -R appuser:appuser /app

USER appuser

EXPOSE 8080

ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:8080

HEALTHCHECK --interval=30s --timeout=3s --retries=3 \
  CMD curl -f http://localhost:8080/api/health || exit 1

ENTRYPOINT ["dotnet", "Portfolio.WebApi.dll"]
