# Paridade entre API e desktop

## Implementação

O WinForms utiliza a API HTTP para os oito recursos: motoristas, veículos, movimentações, abastecimentos, multas, manutenções, status de veículo e vencimentos de CNH. As operações de inclusão, alteração e exclusão passam pelo cliente em `Desktop.Client/Infrastructure/Api`. A conclusão de movimentação usa `POST /api/v1/movimentacoes/{id}/concluir`. O projeto `BancoDados` e o acesso Firebird foram retirados do código versionado e não fazem parte da solução.

Os arquivos `.FDB` legados foram retirados do código versionado; o histórico permanece recuperável pelo Git. Nenhuma conversão ou importação foi implementada.

As consultas de grade, seletores e relatórios usam `GET /api/v1/consultas/{recurso}` com filtros opcionais. O cliente percorre as páginas de 100 registros e monta os `DataTable` exigidos pelos formulários e pelos sete arquivos RDLC existentes. A consulta de último KM usa `GET /api/v1/consultas/veiculos/{id}/ultima-quilometragem?origem=movimentacao|abastecimento`.

| Recurso | Busca da grade | Filtros de relatório/seletores | Gravação |
| --- | --- | --- | --- |
| Motoristas | Nome, CNH, CPF, RG | Ativos/inativos, seletor de ativos | CRUD |
| Veículos | Placa, modelo, Renavam | Ativos/inativos, seletor de ativos, último KM | CRUD |
| Movimentações | Modelo, motorista, descrição | Veículo, motorista, período de saída ou chegada, abertas | CRUD e conclusão |
| Abastecimentos | Modelo, motorista, descrição | Veículo, motorista, período, último KM | CRUD |
| Multas | Modelo, motorista, descrição | Veículo, motorista, período | CRUD |
| Manutenções | Modelo, descrição | Veículo, período | CRUD |
| Status de veículo | Modelo, descrição | Veículo, período de início ou fim, sem fim | CRUD |
| Vencimentos de CNH | Nome do motorista | Nome na resposta | CRUD |

O adaptador preserva os nomes e tipos de colunas usados pelas telas e pelos RDLC. Horários recebidos em UTC são convertidos para o horário local do Windows; horários locais enviados pelo desktop são convertidos para UTC. Datas sem horário são enviadas como `yyyy-MM-dd`. Placas com hífen são normalizadas na API; CPF inválido é recusado. A API mantém as invariantes de KM e de movimentação aberta.

## Configuração

Defina a URL base da API em `QuemPegouOVeiculo/App.config` (`ApiBaseUrl`) ou na variável de ambiente `QUEMPEGOU_API_URL`. O valor padrão é `http://localhost:5000`. O desktop não recebe credenciais PostgreSQL. A API monta a conexão com `QVeiculoUser` e `QVeiculoPass` e com o perfil PostgreSQL do ambiente.

Para depurar, escolha `Desktop + API (Development)` nos perfis da solução do Visual Studio. Ao abrir somente o executável WinForms, mantenha a API iniciada separadamente em `http://localhost:5000`; `/health/ready` deve responder 200. O cliente informa o endereço configurado quando a conexão HTTP falha.

## Validação realizada

- A solução completa compila com MSBuild do Visual Studio 18, incluindo WinForms .NET Framework 4.8 e API .NET 10.
- A API inicia e publica as nove rotas de consulta; paginação inválida retorna 400.
- Um servidor HTTP local simulado confirmou a leitura de motorista, a leitura de movimentação com cálculo de KM total e o envio de cadastro de motorista pela biblioteca .NET Framework.
- Os testes unitários de domínio e aplicação da API passam.
- No PostgreSQL local, `qveiculo_dev` recebeu a migration inicial; `/health/ready` retornou 200 e o cadastro, consulta e exclusão de um veículo passaram pela API. O registro de teste foi removido.
- Após a reorganização do desktop, as oito fachadas de consulta retornaram `DataTable` pela API real; os 24 recursos de formulários/controles, sete RDLC e dez fontes de dados foram conferidos no assembly compilado.
- A navegação MDI foi verificada com abertura, reativação da mesma janela e abertura de outra tela (contagens de filhos `1, 1, 2`).

## Homologação pendente

A integração básica da API com PostgreSQL foi validada em Development. A operação real do WinForms, os oito CRUDs completos, todos os filtros de relatório, seletores, último KM, conclusão de movimentação, conversão de fuso e erros 400/404/409 ainda precisam de homologação integrada. Os relatórios RDLC também precisam de verificação visual com dados reais.

O adaptador preserva as chamadas síncronas das telas legadas. Em uma etapa de modernização do WinForms, converter os eventos e serviços para `async/await` para evitar bloqueio da interface em redes lentas. Essa mudança não altera o contrato da API.
