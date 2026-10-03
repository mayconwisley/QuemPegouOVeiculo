# Gestão de Frota

Aplicação desktop WPF em .NET 10 com API ASP.NET Core 10 e PostgreSQL. O desktop acessa os dados somente pela API; usuário, senha do PostgreSQL e connection string permanecem no servidor.

## Projetos

```text
FleetManagement.Wpf/       Aplicação desktop atual (.NET 10)
  Features/                 Telas por área de negócio
  Features/Shared/          Formulário, grade e seleção reutilizáveis
  Themes/                   Paletas e estilos WPF compartilhados
  Infrastructure/Api/       Cliente HTTP, autenticação e erros da API
Api/                       Domínio, casos de uso, persistência e endpoints
FleetManagement/           WinForms .NET Framework 4.8, mantido temporariamente
Desktop.Client/             Cliente HTTP usado pelo WinForms
Desktop.Models/             Modelos usados pelo WinForms e seus RDLC
```

As regras de negócio, a autorização e a persistência ficam na API. O WPF mantém somente estado de tela, formatação local, seleção de registros e transporte HTTP. O token JWT fica na memória do processo. Quando a API revoga a sessão, o aplicativo solicita novo login.

## Executar

1. Configure o PostgreSQL, aplique as migrations e crie o primeiro administrador conforme [a documentação da API](Api/README.md).
2. No Visual Studio, abra `FleetManagement.sln` e escolha **WPF + API (Development)** no perfil de inicialização da solução.
3. Inicie a depuração e entre com uma conta criada na API.

Para iniciar a API e o WPF em terminais separados:

```powershell
$env:ASPNETCORE_ENVIRONMENT = 'Development'
$env:ASPNETCORE_URLS = 'http://localhost:5000'
dotnet run --no-launch-profile --project Api/FleetManagement.Api
```

```powershell
dotnet run --project FleetManagement.Wpf
```

O WPF usa `http://localhost:5000` por padrão. Defina `FLEET_MANAGEMENT_API_URL` no processo para outra URL, por exemplo `https://api.exemplo.com`. Antes de entrar, a API deve responder em `/health/ready`. Para publicar o desktop:

```powershell
dotnet publish FleetManagement.Wpf/FleetManagement.Wpf.csproj -c Release -r win-x64 --self-contained false
```

O computador cliente precisa do runtime **.NET Desktop 10** correspondente. Nenhum pacote ReportViewer ou conexão direta ao PostgreSQL é necessário no WPF.

## Aparência

No topo da janela principal, **Aparência** oferece **Automático**, **Claro** e **Escuro**. O modo Automático é o padrão e acompanha a preferência de aplicativos do Windows, inclusive quando ela muda com o sistema aberto. A escolha manual fica salva em `%LOCALAPPDATA%\FleetManagement\settings.json` para o usuário do Windows. As paletas em `FleetManagement.Wpf/Themes/` são aplicadas a todas as telas e diálogos sem reiniciar o aplicativo.

## Funcionalidades

| Área | Fluxos no WPF |
| --- | --- |
| Acesso | Login, perfis Administrator/Operator/Viewer, renovação da sessão, gestão de usuários e auditoria |
| Cadastros | Motoristas, veículos e vencimentos de CNH; inclusão, consulta, alteração e exclusão |
| Operação | Movimentações, chegada, previsão de retorno, checklists, abastecimentos, multas, manutenções e situações de veículos |
| Planejamento | Reservas (criar, editar, iniciar, cancelar) e planos preventivos (criar, editar, registrar execução) |
| Painel | Indicadores e pendências com navegação para o recurso correspondente |
| Relatórios | Sete modelos operacionais herdados do WinForms, mais três consultas; visualização paginada, impressão e exportação CSV |

Os sete modelos de relatório do WinForms (motoristas, veículos, situações, movimentações, manutenções, abastecimentos e multas) foram recriados na tela WPF com seus títulos, colunas e totais aplicáveis. A impressão usa o sistema operacional; o CSV continua sendo gerado pela API, com os mesmos filtros e limite de 10.000 linhas. Os cadastros e a seleção de veículo ou motorista ficam dentro da janela principal. A seleção usa busca paginada e preserva os dados já digitados no formulário.

Datas digitadas no WPF usam `dd/MM/aaaa`; horários usam `dd/MM/aaaa HH:mm` e são enviados à API em UTC. Horários recebidos da API são exibidos no fuso local do Windows.

## Validação

```powershell
dotnet build FleetManagement.Wpf/FleetManagement.Wpf.csproj
dotnet test FleetManagement.Wpf.Tests/FleetManagement.Wpf.Tests.csproj
dotnet test Api/FleetManagement.Tests/FleetManagement.Tests.csproj
```

O WinForms permanece no perfil **WinForms legado + API (Development)** enquanto a equivalência operacional é conferida com usuários reais. Seu projeto e os RDLC não são dependências do WPF e podem ser removidos depois da homologação. Não há projeto de migração de Firebird.
