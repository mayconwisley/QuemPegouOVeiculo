# API de controle de veículos

## Arquitetura

Monólito modular ASP.NET Core 10. Uma única API e um banco PostgreSQL atendem aos módulos `Cadastros` e `Operacoes`:

```text
Api/
├── QuemPegouOVeiculo.Domain/          # Entidades e invariantes; sem EF ou ASP.NET
├── QuemPegouOVeiculo.Application/     # Casos de uso de comandos e consultas; portas de persistência
├── QuemPegouOVeiculo.Infrastructure/  # EF Core, repositórios e migrations
├── QuemPegouOVeiculo.Api/             # Endpoints, DI, erros HTTP e OpenAPI
└── QuemPegouOVeiculo.Tests/           # Testes das regras e casos de uso
```

Cada camada organiza seu código por módulo e feature. `Operacoes` referencia motorista e veículo por ID; o domínio não depende do módulo de cadastro nem de infraestrutura. O fluxo de escrita é endpoint → comando → entidade → repositório de escrita → PostgreSQL. O fluxo de leitura é endpoint → consulta → repositório de leitura → DTO projetado. CQRS aqui separa responsabilidades de leitura e escrita, sem fila, event sourcing ou mediador externo.

O contexto EF mantém os schemas `cadastros` e `operacoes`. Índices únicos protegem CPF, placa e uma única movimentação aberta por veículo. Restrições de banco reforçam quilometragem, valores e ordem de datas. As consultas usam `AsNoTracking`, projeção e paginação no banco; comandos usam entidades rastreadas. A API retorna `ProblemDetails` para validação (400), ausência (404) e conflitos de negócio ou integridade (409).

Não existe projeto de conversão do Firebird. O banco legado está vazio e não há clientes; o schema PostgreSQL começa na migration EF `InitialCreate`. O WinForms usa a API por `HttpClient`, sem credenciais PostgreSQL no executável. A URL da API é configurada em `QuemPegouOVeiculo/App.config` ou pela variável `QUEMPEGOU_API_URL`.

## Tipos e contrato

- `DateOnly` representa datas sem horário, como vencimento da CNH, abastecimento, multa e manutenção.
- `DateTime` em UTC representa horários de saída, chegada e períodos de status. Envie ISO 8601 com `Z`, por exemplo `2026-09-27T14:30:00Z`.
- Quilometragem é `int`, valores monetários e litros são `decimal`.
- CPF é normalizado para 11 dígitos, placa para maiúsculas. Motorista e veículo usam `ativo: bool`.
- Movimentação usa `emAberto` derivado de `chegadaUtc`; chegada e `kmFinal` devem ser informados juntos. O endpoint de conclusão é `POST /api/v1/movimentacoes/{id}/concluir`.

## PostgreSQL por ambiente

A API monta a connection string com `QVeiculoUser` e `QVeiculoPass` do ambiente. No Windows, lê as variáveis do processo e, se ausentes, as variáveis de sistema. As credenciais não ficam nos arquivos JSON nem no executável desktop.

| Ambiente | Host | Porta | Banco | SSL |
| --- | --- | --- | --- | --- |
| Development | `localhost` | `5432` | `qveiculo_dev` | `Disable` |
| Production | `localhost` | `5432` | `qveiculo_prod` | `Prefer` |

Os valores estão em `appsettings.Development.json` e `appsettings.Production.json`. `Postgres__Host`, `Postgres__Port`, `Postgres__Database` e `Postgres__SslMode` podem sobrescrevê-los por ambiente. O servidor PostgreSQL não cria os bancos automaticamente; crie `qveiculo_dev` e `qveiculo_prod` antes de aplicar suas migrations.

Para iniciar em desenvolvimento com o PostgreSQL local:

```powershell
$env:ASPNETCORE_ENVIRONMENT = 'Development'
dotnet ef database update --project Api/QuemPegouOVeiculo.Infrastructure --startup-project Api/QuemPegouOVeiculo.Api --context FleetDbContext
$env:ASPNETCORE_URLS = 'http://localhost:5000'
dotnet run --no-launch-profile --project Api/QuemPegouOVeiculo.Api
```

O Compose é opcional e publica o PostgreSQL em `localhost:5433` para não conflitar com o serviço local. Nesse caso, exporte as credenciais também para o processo do terminal antes de subir o container:

```powershell
$env:QVeiculoUser = [Environment]::GetEnvironmentVariable('QVeiculoUser', 'Machine')
$env:QVeiculoPass = [Environment]::GetEnvironmentVariable('QVeiculoPass', 'Machine')
docker compose -f Api/compose.yaml up -d --wait
$env:Postgres__Port = '5433'
```

Depois execute os comandos de migration e inicialização de Development acima.

A migration deve ser aplicada por comando de implantação, nunca automaticamente a cada inicialização da API. Para produção, defina `ASPNETCORE_ENVIRONMENT=Production`, gere e revise um script SQL idempotente antes de aplicar:

```powershell
dotnet ef migrations script --idempotent --project Api/QuemPegouOVeiculo.Infrastructure --startup-project Api/QuemPegouOVeiculo.Api --context FleetDbContext --output artifacts/migrations.sql
```

## Endpoints

Base `/api/v1`. Os recursos `motoristas`, `veiculos`, `movimentacoes`, `abastecimentos`, `multas`, `manutencoes`, `status-veiculo` e `vencimentos-cnh` oferecem `GET /` (parâmetros `page` e `pageSize`, até 100 itens), `GET /{id}`, `POST /`, `PUT /{id}` e `DELETE /{id}`. `POST` responde 201 com `Location`; `PUT` e `DELETE` respondem 204.

O documento OpenAPI fica em `/openapi/v1.json` apenas no ambiente `Development`. `/health/live` verifica o processo e `/health/ready` verifica o PostgreSQL.

As consultas usadas pelo desktop estão em `/api/v1/consultas/{recurso}`. Elas aceitam `busca`, `veiculoId`, `motoristaId`, `ativo`, `emAberto`, `dataDe`, `dataAte`, `inicioUtc`, `fimUtc`, `campoData`, `page` e `pageSize`, conforme o recurso. Para períodos de horários, `fimUtc` é exclusivo. A última quilometragem está em `/api/v1/consultas/veiculos/{id}/ultima-quilometragem?origem=movimentacao|abastecimento`. Consulte [PARIDADE_DESKTOP.md](PARIDADE_DESKTOP.md) para a matriz de telas e validação.

Exemplo de cadastro de veículo:

```powershell
$body = @{ placa = 'ABC1D23'; modelo = 'Veículo de teste'; chassi = ''; renavam = ''; ativo = $true } | ConvertTo-Json
Invoke-RestMethod -Method Post -Uri 'http://localhost:5000/api/v1/veiculos' -ContentType 'application/json' -Body $body
```

## Testes e próximos passos

```powershell
dotnet test Api/QuemPegouOVeiculo.Tests
dotnet build Api/QuemPegouOVeiculo.Api
```

Antes de publicar a versão web, adicionar autenticação/autorização, HTTPS, CORS restrito, auditoria e testes de integração com PostgreSQL. O WinForms já usa o adaptador HTTP; os relatórios são alimentados pelas consultas filtradas da API. Não exponha SQL arbitrário por endpoint.
