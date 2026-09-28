# Paridade entre API e desktop

## Implementação

O WinForms utiliza a API HTTP para os oito recursos: motoristas, veículos, movimentações, abastecimentos, multas, manutenções, status de veículo e vencimentos de CNH. As operações de inclusão, alteração e exclusão passam pelo cliente em `Negocio/ApiGateway`. A conclusão de movimentação usa `POST /api/v1/movimentacoes/{id}/concluir`. O projeto `BancoDados` foi removido da solução e a biblioteca `Negocio` não o referencia mais.

Os arquivos antigos de acesso Firebird e os arquivos `.FDB` continuam no repositório como histórico, fora da compilação e do fluxo operacional. Nenhuma conversão ou importação desses arquivos foi implementada.

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

## Validação realizada

- A solução completa compila com MSBuild do Visual Studio 18, incluindo WinForms .NET Framework 4.8 e API .NET 10.
- A API inicia e publica as nove rotas de consulta; paginação inválida retorna 400.
- Um servidor HTTP local simulado confirmou a leitura de motorista, a leitura de movimentação com cálculo de KM total e o envio de cadastro de motorista pela biblioteca .NET Framework.
- Os testes unitários de domínio e aplicação da API passam.

## Homologação pendente

Não havia servidor PostgreSQL nem daemon Docker disponível durante esta implementação. Por isso, consultas EF, constraints, relatórios RDLC e operação real do WinForms contra o PostgreSQL ainda precisam de teste integrado. Para a homologação, aplicar a migration em banco vazio, executar os oito CRUDs, todos os filtros de relatório, seletores, último KM, conclusão de movimentação, conversão de fuso e erros 400/404/409.

O adaptador preserva as chamadas síncronas das telas legadas. Em uma etapa de modernização do WinForms, converter os eventos e serviços para `async/await` para evitar bloqueio da interface em redes lentas. Essa mudança não altera o contrato da API.
