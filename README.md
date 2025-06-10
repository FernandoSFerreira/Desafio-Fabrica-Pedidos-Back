# Desafio Fabrica Pedidos Back - FABRICA

Este reposit�rio cont�m a implementa��o do backend para o desafio de f�brica de pedidos. O objetivo � gerenciar e processar pedidos de revendas, integrando com uma API externa simulada (API-Fabrica) e garantindo a robustez e escalabilidade da solu��o.

## Tecnologias Utilizadas:

*   **Linguagem:** C# (.NET 8)
*   **Framework Web:** ASP.NET Core MVC
*   **ORM:** Entity Framework Core (EF Core)
*   **Banco de Dados:** PostgreSQL (usando Docker para isolamento e portabilidade)
*   **Logging:** Serilog (com integra��o com o console e, potencialmente, outros sinks como arquivos ou servi�os de monitoramento)
*   **Autentica��o:** JWT (JSON Web Token) para seguran�a e autoriza��o.
*   **Mensageria:** RabbitMQ (para comunica��o ass�ncrona e filas de mensagens)
*   **Testes Unit�rios:** xUnit
*   **Docker:** Cont�ineriza��o com Docker para facilitar a execu��o e reprodu��o do projeto.

## Boas Pr�ticas Implementadas:

*   **Arquitetura em Camadas:** O c�digo foi estruturado utilizando uma arquitetura em camadas (Presentation, Domain, Application, Infrastructure), promovendo separa��o de responsabilidades e testabilidade.
*   **Clean Code:**  Seguimos os princ�pios do Clean Code para garantir a legibilidade, manutenibilidade e organiza��o do c�digo.
*   **Valida��o:** Valida��es foram implementadas tanto no n�vel das entidades (DataAnnotations) quanto no n�vel da aplica��o, garantindo a integridade dos dados.
*   **Tratamento de Exce��es:** Tratamento robusto de exce��es para garantir que erros sejam registrados e tratados adequadamente.
*   **Logging Detalhado:** O Serilog � utilizado para registrar eventos importantes do sistema, auxiliando na depura��o e monitoramento.
*   **Autentica��o Segura:** Implementamos a autentica��o JWT para proteger as rotas da API e controlar o acesso aos recursos.
*   **Testes Unit�rios:** Testes unit�rios foram criados para garantir que cada componente funcione conforme esperado e para prevenir regress�es.
*   **Dockeriza��o:** A utiliza��o do Docker garante um ambiente consistente e reproduz�vel, facilitando a colabora��o e implanta��o.
*   **Async/Await:**  Utilizamos `async` e `await` para opera��es de I/O (como acesso ao banco de dados) para garantir que o sistema n�o fique bloqueado.

## Migrations Autom�ticas:

O EF Core foi configurado para realizar migra��es autom�ticas do banco de dados, facilitando a evolu��o do esquema de dados ao longo do tempo. Altera��es nas entidades s�o refletidas no banco de dados com um simples comando.

## Seeds Autom�ticos:

Implementamos seeds autom�ticos para popular o banco de dados com alguns dados iniciais, incluindo um usu�rio "admin" para fins de teste e demonstra��o. Este usu�rio pode ser usado para acessar funcionalidades administrativas da aplica��o durante o desenvolvimento.

## RabbitMQ:

Utilizamos o RabbitMQ para implementar uma fila de mensagens. Isso permite que tarefas demoradas ou que n�o precisam ser executadas imediatamente (como a integra��o com a API Fabrica) sejam processadas de forma ass�ncrona, sem bloquear a resposta � requisi��o do cliente.

## Docker Compose:

Para facilitar a execu��o do projeto, fornecemos um arquivo `docker-compose.yaml` que define e configura todos os servi�os necess�rios (banco de dados PostgreSQL, RabbitMQ). Isso permite que voc� execute todo o ambiente em poucos comandos.

## Como Executar e Testar o Projeto:

1.  **Pr�-requisitos:**
    *   Docker instalado e configurado.
    *   Docker Compose instalado.
2.  **Clone o Reposit�rio:** `git clone [URL do reposit�rio]`
3.  **Navegue at� a Pasta do Projeto:** `cd Desafio-Fabrica-Pedidos-Back`
4.  **Execute o Docker Compose:** `docker-compose up --build` (O comando `--build` garante que as imagens sejam constru�das antes de iniciar os containers)
5.  **Acesse a Aplica��o:** A aplica��o estar� dispon�vel em `https://localhost:8080`.  (Pode ser necess�rio configurar o certificado SSL se estiver usando HTTPS).

## Como Testar:

*   Os testes unit�rios podem ser executados atrav�s do Visual Studio ou utilizando a linha de comando: `dotnet test`

## Contribui��es:

Sinta-se � vontade para contribuir com este projeto, reportando bugs, sugerindo melhorias e enviando pull requests.
