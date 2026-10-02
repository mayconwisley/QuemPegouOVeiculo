# API de controle de veículos

## Arquitetura

Monólito modular ASP.NET Core 10. Uma única API e um banco PostgreSQL atendem aos módulos `Registrations`, `Operations`, `Access` e `Dashboard`:

```text
Api/
├── FleetManagement.Domain/          # Entidades e invariantes; sem EF ou ASP.NET
├── FleetManagement.Application/     # Casos de uso de comandos e consultas; portas de persistência
├── FleetManagement.Infrastructure/  # EF Core, repositórios e migrations
├── FleetManagement.Api/             # Endpoints, DI, erros HTTP e OpenAPI
└── FleetManagement.Tests/           # Testes das regras e casos de uso
```

Cada camada organiza seu código por módulo e feature. `Operations` referencia motorista e veículo por ID; o domínio não depende do módulo de cadastro nem de infraestrutura. O fluxo de escrita é endpoint → comando → entidade → repositório de escrita → PostgreSQL. O fluxo de leitura é endpoint → consulta → repositório de leitura → DTO projetado. CQRS aqui separa responsabilidades de leitura e escrita, sem fila, event sourcing ou mediador externo.

O contexto EF usa os schemas `registrations`, `operations` e `security`. Índices únicos protegem CPF, placa, nome de usuário e uma única movimentação aberta por veículo. Restrições de banco reforçam quilometragem, valores e ordem de datas. As consultas usam `AsNoTracking`, projeção e paginação no banco; comandos usam entidades rastreadas.

Os casos de uso retornam `Result` ou `Result<T>` para falhas esperadas. A camada de aplicação converte violações do domínio, ausência de registros e conflitos de persistência em erros explícitos; exceções inesperadas continuam sendo tratadas pelo middleware global. Os endpoints mapeiam esses erros para `ProblemDetails` com `code` (`validation`, `not_found`, `conflict`) e status 400, 404 ou 409. As respostas de sucesso mantêm os contratos 200, 201 e 204 usados pelo desktop.

Não existe projeto de conversão do Firebird. O WPF usa a API por `HttpClient`, sem credenciais PostgreSQL no executável. A URL da API é definida por `FLEET_MANAGEMENT_API_URL`, com `http://localhost:5000` como padrão local. O WinForms legado continua aceitando `FleetManagement/App.config` e a variável antiga `QUEMPEGOU_API_URL`.

A migration histórica `InitialCreate` permanece intacta. `StandardizeEnglishSchema` renomeia schemas, tabelas, colunas, índices e restrições do PostgreSQL, preservando os registros caso o banco local já tenha sido criado. Em um banco novo, aplique as duas migrations na ordem padrão do EF Core.

`AddAccessAudit` cria usuários e auditoria no schema `security`; `AddSecurityVersion` revoga tokens antigos após mudanças de acesso. `AddOperationsPlanning` cria planos preventivos e checklists e adiciona a previsão de retorno às movimentações. As alterações de dados e os registros de auditoria são confirmados na mesma transação. CPF, RG, hash de senha e versão de segurança não são copiados para o JSON de auditoria.

A organização do WPF e dos projetos legados está descrita no [README principal](../README.md).

## Executar com o desktop

No Visual Studio, selecione o perfil da solução `WPF + API (Development)` e inicie a depuração. Ele inicia a API e o WPF. Ambos usam `http://localhost:5000` por padrão. Mantenha o serviço PostgreSQL local em execução e aplique as migrations de Development antes de abrir os cadastros.

Para executar o `.exe` WPF isoladamente, inicie a API em outro terminal e mantenha o processo aberto:

```powershell
$env:ASPNETCORE_ENVIRONMENT = 'Development'
$env:ASPNETCORE_URLS = 'http://localhost:5000'
dotnet run --no-launch-profile --project Api/FleetManagement.Api
```

Confirme `http://localhost:5000/health/ready` antes de usar o desktop. Crie o primeiro administrador conforme a seção de acesso abaixo. Se a API estiver em outro endereço, defina `FLEET_MANAGEMENT_API_URL` no processo do WPF.

## Tipos e contrato

- `DateOnly` representa datas sem horário, como vencimento da CNH, abastecimento, multa e manutenção.
- `DateTime` em UTC representa horários de saída, chegada e períodos de status. Envie ISO 8601 com `Z`, por exemplo `2026-09-27T14:30:00Z`.
- Quilometragem é `int`, valores monetários e litros são `decimal`.
- CPF é normalizado para 11 dígitos, placa para maiúsculas. Motorista e veículo usam `active: bool`.
- Movimentação aberta tem `arrivalUtc` e `finalMileage` nulos; chegada e quilometragem final devem ser informadas juntas. O endpoint de conclusão é `POST /api/v1/movements/{id}/complete`.

## PostgreSQL por ambiente

A API monta a connection string com `FleetUser` e `FleetPass` do ambiente. No Windows, lê as variáveis do processo e, se ausentes, as variáveis de sistema. As credenciais não ficam nos arquivos JSON nem no executável desktop.

| Ambiente | Host | Porta | Banco | SSL |
| --- | --- | --- | --- | --- |
| Development | `localhost` | `5432` | `qveiculo_dev` | `Disable` |
| Production | `localhost` | `5432` | `qveiculo_prod` | `Prefer` |

Os valores estão em `appsettings.Development.json` e `appsettings.Production.json`. `Postgres__Host`, `Postgres__Port`, `Postgres__Database` e `Postgres__SslMode` podem sobrescrevê-los por ambiente. Os nomes dos bancos `qveiculo_dev` e `qveiculo_prod` foram mantidos para respeitar os ambientes existentes; schemas e objetos internos seguem a nova nomenclatura. O servidor PostgreSQL não cria os bancos automaticamente.

No PostgreSQL local do Windows, crie o banco de desenvolvimento uma vez com `createdb` (substitua o caminho do executável se necessário):

```powershell
$env:PGPASSWORD = [Environment]::GetEnvironmentVariable('FleetPass', 'Machine')
$postgresUser = [Environment]::GetEnvironmentVariable('FleetUser', 'Machine')
& 'C:\Programas\PostgreSQL\18\bin\createdb.exe' -h localhost -p 5432 -U $postgresUser qveiculo_dev
Remove-Item Env:PGPASSWORD
```

Use um banco separado para Production. Crie `qveiculo_prod` apenas ao preparar esse ambiente e aplique nele somente o script de migration revisado.

Para iniciar em desenvolvimento com o PostgreSQL local:

```powershell
$env:ASPNETCORE_ENVIRONMENT = 'Development'
dotnet ef database update --project Api/FleetManagement.Infrastructure --startup-project Api/FleetManagement.Api --context FleetDbContext
$env:ASPNETCORE_URLS = 'http://localhost:5000'
dotnet run --no-launch-profile --project Api/FleetManagement.Api
```

O Compose é opcional e publica o PostgreSQL em `localhost:5433` para não conflitar com o serviço local. Nesse caso, exporte as credenciais também para o processo do terminal antes de subir o container:

```powershell
$env:FleetUser = [Environment]::GetEnvironmentVariable('FleetUser', 'Machine')
$env:FleetPass = [Environment]::GetEnvironmentVariable('FleetPass', 'Machine')
docker compose -f Api/compose.yaml up -d --wait
$env:Postgres__Port = '5433'
```

Depois execute os comandos de migration e inicialização de Development acima.

A migration deve ser aplicada por comando de implantação, nunca automaticamente a cada inicialização da API. Para produção, defina `ASPNETCORE_ENVIRONMENT=Production`, gere e revise um script SQL idempotente antes de aplicar:

```powershell
dotnet ef migrations script --idempotent --project Api/FleetManagement.Infrastructure --startup-project Api/FleetManagement.Api --context FleetDbContext --output artifacts/migrations.sql
```

## Usuários e segurança

A API exige autenticação em `/api/v1`, exceto `POST /api/v1/auth/login`. O login tem limite de cinco tentativas por minuto por endereço IP. Os perfis são `Administrator` (todas as operações, usuários e auditoria), `Operator` (leitura e gravação dos recursos de frota) e `Viewer` (leitura, painel e relatórios). A API valida a situação e o perfil do usuário no banco em cada requisição; alterações de acesso revogam tokens antigos. O token expira em oito horas e o desktop o mantém somente em memória.

Depois das migrations, crie **uma vez** o administrador inicial em um banco sem usuários. No terminal PowerShell ou no Console do Gerenciador de Pacotes do Visual Studio, a partir da raiz da solução, execute **uma única linha**:

```powershell
& .\scripts\Bootstrap-Admin.ps1
```

O script solicita a senha de forma protegida, cria `admin` em `Development` e limpa as variáveis temporárias mesmo se houver erro. Para outro usuário ou ambiente, use `-Username` e `-Environment`. No `Read-Host`, o texto antes de `-AsSecureString` é apenas o **rótulo do prompt**; digite a senha quando o prompt aparecer, sem incluí-la no comando.

Em `Production`, configure `FLEET_JWT_KEY` com pelo menos 32 bytes UTF-8, gere-a aleatoriamente e mantenha o valor estável entre reinicializações. No Windows, a API lê essa variável do processo ou, se ausente, das variáveis de sistema. Em `Development`, a ausência dessa variável gera uma chave temporária por processo, invalidando sessões após reiniciar a API. Ao disponibilizar a API fora do computador local, publique-a somente por HTTPS e configure a URL HTTPS no desktop. A futura interface web deverá receber CORS restrito à sua origem conhecida.

`GET /api/v1/dashboard` retorna contadores e itens de atenção; `GET /api/v1/audit?page=1&pageSize=50` é paginado e exclusivo do administrador. `GET /api/v1/users`, `POST /api/v1/users`, `PUT /api/v1/users/{id}` e `PUT /api/v1/users/{id}/password` administram contas sem expor hashes. A aplicação impede a remoção do próprio acesso administrativo e a desativação do último administrador.

## Endpoints

Base `/api/v1`. Os recursos `drivers`, `vehicles`, `movements`, `refuelings`, `fines`, `maintenance`, `vehicle-statuses` e `license-expirations` oferecem `GET /` (parâmetros `page` e `pageSize`, até 100 itens), `GET /{id}`, `POST /`, `PUT /{id}` e `DELETE /{id}`. `POST` responde 201 com `Location`; `PUT` e `DELETE` respondem 204.

O documento OpenAPI fica em `/openapi/v1.json` apenas no ambiente `Development`. `/health/live` verifica o processo e `/health/ready` verifica o PostgreSQL.

As consultas usadas pelo desktop estão em `/api/v1/queries/{resource}`. Elas aceitam `search`, `vehicleId`, `driverId`, `active`, `isOpen`, `fromDate`, `toDate`, `startUtc`, `endUtc`, `dateField`, `page` e `pageSize`, conforme o recurso. Para períodos de horários, `endUtc` é exclusivo. A última quilometragem está em `/api/v1/queries/vehicles/{id}/latest-mileage?source=movement|refueling`. A validação anterior dos sete RDLC refere-se ao WinForms legado; o WPF usa impressão própria.

## Planejamento operacional

`PUT /api/v1/movements/{id}/expected-return` recebe `expectedReturnUtc` em UTC ou `null` para remover a previsão de uma movimentação aberta. `GET /api/v1/movements/{id}/checklists` lista as vistorias e `PUT /api/v1/movements/{id}/checklists/departure|arrival` grava uma vistoria por etapa. O corpo contém `tiresOk`, `lightsOk`, `fluidsOk`, `bodyOk`, `notes` e `checkedAtUtc`. A vistoria de chegada exige movimentação concluída. A exclusão de movimentação remove as vistorias na mesma transação e ambas as alterações ficam na auditoria.

`/api/v1/maintenance-plans` oferece listagem paginada, consulta por ID, criação e atualização. O plano exige ao menos um intervalo positivo (`intervalDays` com `nextDueDate` e/ou `intervalMileage` com `nextDueMileage`). `POST /api/v1/maintenance-plans/{id}/complete` recebe data, quilometragem, valor e observações; cria um registro de manutenção ligado ao plano e avança os próximos vencimentos em uma transação. A quilometragem informada não pode ser menor que a já registrada para o veículo. Atualizações concorrentes do plano retornam conflito. O veículo de um plano não pode ser alterado após a criação. Registros de manutenção vinculados a um plano não podem ser excluídos nem ter veículo ou data alterados; valor e descrição podem ser corrigidos.

O painel calcula, sem persistir alertas duplicados, retornos vencidos, checklists ausentes e planos com data em até 30 dias ou quilometragem atingida. A quilometragem atual considera o maior valor registrado em movimentações e abastecimentos. Movimentações abertas sem previsão continuam destacadas após 24 horas.

## Reservas e exportação

`GET /api/v1/reservations` lista reservas com paginação e filtros opcionais `vehicleId`, `status`, `fromUtc` e `toUtc`. O período consultado inclui reservas que o atravessam. `GET /api/v1/reservations/{id}` consulta uma reserva. `POST /api/v1/reservations` e `PUT /api/v1/reservations/{id}` recebem `vehicleId`, `driverId`, `startUtc`, `endUtc` e `purpose`. Os horários são UTC e o fim é exclusivo. O veículo não pode ser trocado após a criação; somente reservas confirmadas podem ser alteradas.

`POST /api/v1/reservations/{id}/cancel` cancela uma reserva confirmada. `POST /api/v1/reservations/{id}/start` recebe `initialMileage` e `description`, cria a movimentação vinculada e usa o fim da reserva como retorno previsto. A saída pode começar no máximo 15 minutos antes do horário reservado e deve ocorrer antes do fim. A chegada é registrada no endpoint de movimentação existente e conclui a reserva vinculada. Uma movimentação vinculada não pode ser excluída, e sua previsão é controlada pela reserva.

Reservas ativas do mesmo veículo usam intervalos `[início, fim)`: horários adjacentes são aceitos, sobreposições são recusadas. A aplicação verifica também movimentações abertas e serializa gravações por veículo. O PostgreSQL impõe a exclusão de intervalos sobrepostos com `btree_gist`; o usuário que aplica a migration precisa ter permissão para criar essa extensão. A migration `AddVehicleReservations` deve ser revisada e aplicada em cada ambiente antes de usar os endpoints. Não há migração de Firebird.

`GET /api/v1/exports/{resource}.csv` exporta `drivers`, `vehicles`, `movements`, `refuelings`, `fines`, `maintenance`, `vehicle-statuses`, `license-expirations`, `reservations` ou `maintenance-plans`. Os filtros são os mesmos das consultas paginadas, conforme o recurso. O CSV usa UTF-8 com BOM, `;` como separador, campos entre aspas e proteção contra fórmulas de planilha. A resposta é gerada em fluxo e limitada a 10.000 linhas; acima disso retorna HTTP 413 antes de iniciar o arquivo. Os perfis de consulta podem exportar. Datas com horário são identificadas como UTC no cabeçalho.

Exemplo de cadastro de veículo autenticado:

```powershell
$body = @{ plate = 'ABC1D23'; model = 'Veículo de teste'; chassis = ''; renavam = ''; active = $true } | ConvertTo-Json
$credentials = Get-Credential -UserName 'admin' -Message 'Credenciais da API'
$loginBody = @{ username = $credentials.UserName; password = $credentials.GetNetworkCredential().Password } | ConvertTo-Json
$login = Invoke-RestMethod -Method Post -Uri 'http://localhost:5000/api/v1/auth/login' -ContentType 'application/json' -Body $loginBody
Invoke-RestMethod -Method Post -Uri 'http://localhost:5000/api/v1/vehicles' -Headers @{ Authorization = "Bearer $($login.token)" } -ContentType 'application/json' -Body $body
```

## Testes e próximos passos

```powershell
dotnet test Api/FleetManagement.Tests
dotnet build Api/FleetManagement.Api
```

### Preparação dos próximos módulos

- **Publicação online:** falta escolher a origem web para configurar CORS, implantar HTTPS, definir backup/restauração do PostgreSQL e ampliar os testes de integração automatizados. Não exponha SQL arbitrário por endpoint.
