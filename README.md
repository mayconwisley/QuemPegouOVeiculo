# Quem pegou o veículo?

Aplicação WinForms .NET Framework 4.8 com API ASP.NET Core 10 e PostgreSQL. O desktop acessa os dados somente pela API.

O WinForms permanece em .NET Framework 4.8 por depender do controle ReportViewer/RDLC legado. A API e o domínio usam .NET 10; mudar o alvo do desktop exige validar ou substituir a implementação dos relatórios.

## Estrutura

```text
FleetManagement/       WinForms: formulários, controles, utilitários visuais e RDLC
  Shell/                janela principal
  Features/Registrations/  telas de motorista, veículo e vencimento de CNH
  Features/Operations/     telas de movimentação, planejamento, abastecimento, multa, manutenção e status
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

Os formulários, designers e arquivos `.resx` ficam juntos em cada feature. O namespace dos formulários é `FleetManagement`, preservando o vínculo do designer WinForms. Os sete RDLC e os arquivos `.datasource` apontam para o assembly `FleetManagement.Desktop.Models`.

A janela principal ativa uma tela MDI já aberta sem criar outra instância. Uma nova solicitação de relatório substitui o visualizador anterior para aplicar os filtros atuais. Cliques nos cabeçalhos das grades são ignorados pelos manipuladores de seleção.

## Execução local

1. Inicie o PostgreSQL e aplique as migrations de Development conforme [Api/README.md](Api/README.md).
2. Crie o primeiro administrador com o comando de bootstrap descrito no README da API. Não há usuário nem senha padrão.
3. No Visual Studio, abra `FleetManagement.sln`, selecione `Desktop + API (Development)` e execute. Entre com a conta criada.
4. Verifique `http://localhost:5000/health/ready` se algum cadastro não conseguir consultar a API.

Ao executar somente o `.exe` do WinForms, inicie a API separadamente na URL configurada em `FleetManagement/App.config` ou `FLEET_MANAGEMENT_API_URL`.

## Desempenho das telas

Os cadastros carregam grades e listas de seleção de forma assíncrona. A busca espera 300 ms após a última tecla, cancela a consulta anterior e aplica somente a resposta mais recente. Isso mantém a janela responsiva durante a inicialização da API e do PostgreSQL. Consultas de relatório e comandos de gravação ainda passam pelas fachadas síncronas legadas.

Para medir o tempo de abertura e o tempo até a grade receber dados, compile em Debug, inicie a API, defina `FLEET_MEASURE_USERNAME` e `FLEET_MEASURE_PASSWORD` no processo e execute:

```powershell
powershell.exe -NoProfile -Sta -ExecutionPolicy Bypass -File scripts/Measure-DesktopOpen.ps1 -WaitForData
```

O script abre as telas fora da área visível e fecha cada uma após a medição. Execute uma vez após iniciar a API para medir o carregamento frio e uma segunda vez para medir o carregamento aquecido. Os resultados dependem da máquina, rede, tamanho do banco e configuração de Debug/Release.

## Acesso e painel

O desktop solicita login antes da janela principal. O administrador gerencia usuários e consulta a auditoria em **Administração**; o operador usa os cadastros e operações; o perfil de consulta vê o painel, o planejamento e os relatórios sem gravar alterações. A API valida o perfil em cada requisição, inclusive após desativação ou troca de perfil. O token fica apenas na memória do processo; ao expirar, o desktop solicita novo login.

O painel mostra veículos e motoristas ativos, movimentações abertas, status de veículo sem fim, CNHs próximas do vencimento, retornos atrasados, checklists pendentes e planos preventivos a vencer. Movimentações sem previsão de retorno aparecem após 24 horas em aberto.

Em **Planejamento → Previsões e checklists**, o operador registra uma previsão opcional de retorno e preenche a vistoria de saída ou chegada. A vistoria de chegada só pode ser gravada após concluir a movimentação. Em **Planejamento → Manutenção preventiva**, define periodicidade por data, quilometragem ou ambas; a conclusão registra a manutenção e avança o próximo vencimento. Essas operações são auditadas pela API.

Em **Planejamento → Reservas**, o operador agenda veículo, motorista, início, fim e finalidade. A tela permite editar ou cancelar reservas confirmadas e iniciar a saída no horário permitido. A chegada registrada na tela existente conclui também a reserva. O banco impede reservas sobrepostas do mesmo veículo; o fim de um intervalo pode coincidir com o início do próximo. O perfil de consulta pode visualizar, sem alterar.

Em **Relatório → Exportar CSV**, escolha o conjunto de dados e, quando aplicável, busca, veículo, período e status. O arquivo é salvo em UTF-8 com separador `;`, limitado a 10.000 linhas por solicitação. O desktop baixa para um arquivo temporário e substitui o destino somente após receber todo o conteúdo.

## Regras para evolução

- Novas telas pertencem à feature de negócio correspondente, mantendo `.cs`, `.Designer.cs` e `.resx` no mesmo diretório.
- O cliente HTTP e a serialização ficam em `Desktop.Client/Infrastructure/Api`; eventos e controles WinForms ficam no projeto de apresentação.
- Modelos de edição e de relatório ficam em `Desktop.Models`. Não coloque SQL, `HttpClient` ou regras de domínio nesses modelos.
- Preserve os nomes de datasets e colunas consumidos pelos RDLC ao alterar o adaptador `LegacyTableMapper`.
- As fachadas síncronas `Query`, `Insert`, `Update` e `Delete` preservam relatórios e fluxos legados. Em novas telas, use os métodos assíncronos com cancelamento do cliente e `await` nos eventos para manter a interface responsiva.
- O banco de dados ativo é PostgreSQL, acessado exclusivamente pela API. Artefatos `.FDB` legados ainda existem no repositório, mas não participam da solução nem do fluxo de persistência.

Em Development, a integração desktop/API foi validada para os oito cadastros (inclusão, consulta, alteração e exclusão), conclusão de movimentação, filtros, seletores, última quilometragem e respostas HTTP 400/404/409. Os sete relatórios RDLC foram renderizados com dados e inspecionados visualmente. A validação usou registros temporários, removidos ao final.

Após compilar a solução, valide os recursos do designer e os modelos dos relatórios:

```powershell
powershell.exe -NoProfile -ExecutionPolicy Bypass -File scripts/Validate-DesktopAssets.ps1
```
