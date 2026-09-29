# Quem pegou o veículo?

Aplicação WinForms .NET Framework 4.8 com API ASP.NET Core 10 e PostgreSQL. O desktop acessa os dados somente pela API.

O WinForms permanece em .NET Framework 4.8 por depender do controle ReportViewer/RDLC legado. A API e o domínio usam .NET 10; mudar o alvo do desktop exige validar ou substituir a implementação dos relatórios.

## Estrutura

```text
QuemPegouOVeiculo/       WinForms: formulários, controles, utilitários visuais e RDLC
  Shell/                janela principal
  Features/Cadastros/   telas de motorista, veículo e vencimento de CNH
  Features/Operacoes/   telas de movimentação, abastecimento, multa, manutenção e status
  Reports/Forms/        seleção e visualização de relatórios
  Reports/Templates/    definições RDLC copiadas para a saída
  Shared/Controls/      controles reutilizados pelos formulários
  Shared/Presentation/  estado de botões, limpeza e formatação de campos
Desktop.Client/         consultas, comandos, validação local e adaptador HTTP
  Features/             fachadas de leitura e escrita por recurso
  Infrastructure/Api/   transporte HTTP, montagem do JSON e adaptação para DataTable
  Validation/           validações locais usadas antes do envio
Desktop.Models/         modelos editáveis e modelos de design dos RDLC
Api/                    domínio, casos de uso, persistência PostgreSQL e endpoints
```

As dependências seguem `WinForms → Desktop.Client → Desktop.Models`. O WinForms também referencia `Desktop.Models` para edição e para o designer dos relatórios. `Desktop.Client` não referencia `System.Windows.Forms` nem PostgreSQL. As entidades e invariantes ficam na API; os modelos do desktop são dados de tela, não entidades de domínio.

Os formulários, designers e arquivos `.resx` ficam juntos em cada feature. Seus namespaces de formulário foram mantidos para preservar o designer do WinForms e os nomes dos recursos incorporados. Os sete RDLC e os arquivos `.datasource` apontam para o assembly `QuemPegouOVeiculo.Desktop.Models`.

A janela principal ativa uma tela MDI já aberta sem criar outra instância. Uma nova solicitação de relatório substitui o visualizador anterior para aplicar os filtros atuais. Cliques nos cabeçalhos das grades são ignorados pelos manipuladores de seleção.

## Execução local

1. Inicie o PostgreSQL e crie/aplique o banco de Development conforme [Api/README.md](Api/README.md).
2. No Visual Studio, abra `slnQuemPegouOVeiculo.sln`, selecione `Desktop + API (Development)` e execute.
3. Verifique `http://localhost:5000/health/ready` se algum cadastro não conseguir consultar a API.

Ao executar somente o `.exe` do WinForms, inicie a API separadamente na URL configurada em `QuemPegouOVeiculo/App.config` ou `QUEMPEGOU_API_URL`.

## Desempenho das telas

Os cadastros carregam grades e listas de seleção de forma assíncrona. A busca espera 300 ms após a última tecla, cancela a consulta anterior e aplica somente a resposta mais recente. Isso mantém a janela responsiva durante a inicialização da API e do PostgreSQL. Consultas de relatório e comandos de gravação ainda passam pelas fachadas síncronas legadas.

Para medir o tempo de abertura e o tempo até a grade receber dados, compile em Debug, inicie a API e execute:

```powershell
powershell.exe -NoProfile -Sta -ExecutionPolicy Bypass -File scripts/Measure-DesktopOpen.ps1 -WaitForData
```

O script abre as telas fora da área visível e fecha cada uma após a medição. Execute uma vez após iniciar a API para medir o carregamento frio e uma segunda vez para medir o carregamento aquecido. Os resultados dependem da máquina, rede, tamanho do banco e configuração de Debug/Release.

## Regras para evolução

- Novas telas pertencem à feature de negócio correspondente, mantendo `.cs`, `.Designer.cs` e `.resx` no mesmo diretório.
- O cliente HTTP e a serialização ficam em `Desktop.Client/Infrastructure/Api`; eventos e controles WinForms ficam no projeto de apresentação.
- Modelos de edição e de relatório ficam em `Desktop.Models`. Não coloque SQL, `HttpClient` ou regras de domínio nesses modelos.
- Preserve os nomes de datasets e colunas consumidos pelos RDLC ao alterar o adaptador `LegacyTableMapper`.
- As fachadas síncronas `Query`, `Insert`, `Update` e `Delete` preservam relatórios e fluxos legados. Em novas telas, use os métodos assíncronos com cancelamento do cliente e `await` nos eventos para manter a interface responsiva.
- O projeto Firebird e os arquivos `.FDB` foram retirados do código versionado. O banco de dados ativo é PostgreSQL, acessado exclusivamente pela API.

Para a matriz funcional desktop/API e as verificações pendentes, consulte [Api/PARIDADE_DESKTOP.md](Api/PARIDADE_DESKTOP.md).

Após compilar a solução, valide os recursos do designer e os modelos dos relatórios:

```powershell
powershell.exe -NoProfile -ExecutionPolicy Bypass -File scripts/Validate-DesktopAssets.ps1
```
