# Lançamentos

## Desenho da solução

A solução desenvolvida contempla os seguintes componentes:

### Serviço para Controle de Lançamentos
- API NET 8: possui um endpoint para gravar no banco de dados SQL Server e disparar um evento de atualização do Serviço de Consolidado Diário via fila
- Banco de dados SQL Server: armazena todos os lançamentos diários
- RabbitMQ Producer: consolida os saldos diários e envia uma mensagem para a fila solicitando a atualização do cache Redis (evento disparado via MediatR)

### Serviço de Consolidado Diário
- API NET 8: possui um endpoint para ler o saldo de um dia específico
- RabbitMQ Consumer: lê as mensagens da fila no RabbitMQ (via BackgroundService)
- Cache Redis: armazena todos os saldos diários


![Desenho da solução](SolutionItems/design/desenho-solucao.svg)

## Estrutura projeto (Visual Studio)

![Estrutura Projeto VS](SolutionItems/prints/estrutura.png)


## Instruções para execução da aplicação

Pré-requisitos:
- Docker e Docker Compose instalados

Basta executar os arquivos .bat abaixo para testar todos os cenários da solução:
- [Iniciar aplicação](SolutionItems/env-start.bat)
- [Parar aplicação](SolutionItems/env-stop.bat)
- [Parar Serviço para Controle de Lançamentos](SolutionItems/servico-controle-stop.bat)
- [Reiniciar Serviço para Controle de Lançamentos](SolutionItems/servico-controle-restart.bat)

URLs:
- API Controle de Lançamwntos: 127.0.0.1:5001/swagger/index.html ou localhost:5001/swagger/index.html
- API Consolidado Diário: 127.0.0.1:6001/swagger/index.html ou localhost:6001/swagger/index.html
- RabbitMQ Admin: 127.0.0.1:15672 ou localhost:15672 (user: guest | pass: guest)
- SQL Server: localhost:1433 (user:sa | pass: 123Aa321)
- Redis: localhost:6379

Teste de carga:
- Executar a class libray Lancamentos.Consolidado.Tests.LoadTests
 
![Teste de carga](SolutionItems/prints/teste-carga.png)
