# Credito API

Serviço em .NET 8 para integração e consulta de créditos constituídos, com API, worker de consumo Kafka e persistência em PostgreSQL. Inclui health checks, validação com FluentValidation e migrations EF Core.

## Tecnologias
- .NET 8 (API + Worker)
- ASP.NET Core, EF Core 8, Npgsql
- Kafka (Confluent)
- Docker / Docker Compose
- xUnit

## Endpoints principais
- `POST /api/creditos/integrar-credito-constituido` — publica cada crédito no tópico Kafka.
- `GET /api/creditos/{numeroNfse}` — lista créditos por NFS-e.
- `GET /api/creditos/credito/{numeroCredito}` — obtém crédito específico.
- Health checks: `/self` e `/ready`.

## Estrutura
- `src/CreditoApi` — API.
- `src/CreditoApi.Worker` — worker que consome Kafka e grava no banco.
- `src/CreditoApi.Application` / `Domain` / `Infrastructure` / `SharedKernel` — camadas de aplicação, domínio, infra e base.
- `tests/CreditoApi.UnitTests` — testes unitários.
- `docker-compose.yml` — Kafka, ZooKeeper, Postgres, API, Worker.

## Pré-requisitos
- Docker e Docker Compose (para rodar tudo via contêiner).
- Ou .NET 8 SDK + Postgres/Kafka locais se quiser executar sem Docker.

## Executando com Docker
1. Build/subida dos serviços:
   ```sh
   docker compose up -d --build
   ```
2. API: `http://localhost:8080` (Swagger habilitado em Development).
3. Kafka: `localhost:9094` (listener host). Broker interno: `kafka:9092`.
4. Postgres: `localhost:5432` (db `credito_api`, user/password `postgres`).
5. Migrations e seed são aplicados automaticamente na API em `ASPNETCORE_ENVIRONMENT=Development`.

## Executando localmente (sem Docker)
1. Ajuste connection strings/Kafka em `src/CreditoApi/appsettings.Development.json` e `src/CreditoApi.Worker/appsettings.Development.json`.
2. Rode migrations:
   ```sh
   dotnet ef database update --project src/CreditoApi.Infrastructure --startup-project src/CreditoApi
   ```
3. Suba a API:
   ```sh
   dotnet run --project src/CreditoApi
   ```
4. Suba o worker:
   ```sh
   dotnet run --project src/CreditoApi.Worker
   ```

## Testes
```sh
dotnet test
```

## Observações
- O seeder roda apenas em `Development` e popula os 3 créditos do enunciado.
- O ID dos créditos é `BIGINT` identity conforme modelagem solicitada.
