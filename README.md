# Desafio Fabrica Pedidos Back - FABRICA

Este repositório contém a implementação do backend para o desafio de fábrica de pedidos. O objetivo é gerenciar e processar pedidos de revendas, integrando com uma API externa simulada (API-Fabrica) e garantindo a robustez e escalabilidade da solução.

## Tecnologias Utilizadas:

*   **Linguagem:** C# (.NET 8)
*   **Framework Web:** ASP.NET Core MVC
*   **ORM:** Entity Framework Core (EF Core)
*   **Banco de Dados:** PostgreSQL (usando Docker para isolamento e portabilidade)
*   **Logging:** Serilog (com integração com o console e, potencialmente, outros sinks como arquivos ou serviços de monitoramento)
*   **Autenticação:** JWT (JSON Web Token) para segurança e autorização.
*   **Mensageria:** RabbitMQ (para comunicação assíncrona e filas de mensagens)
*   **Testes Unitários:** xUnit
*   **Docker:** Contêinerização com Docker para facilitar a execução e reprodução do projeto.

## Boas Práticas Implementadas:

*   **Arquitetura em Camadas:** O código foi estruturado utilizando uma arquitetura em camadas (Presentation, Domain, Application, Infrastructure), promovendo separação de responsabilidades e testabilidade.
*   **Clean Code:**  Seguimos os princípios do Clean Code para garantir a legibilidade, manutenibilidade e organização do código.
*   **Validação:** Validações foram implementadas tanto no nível das entidades (DataAnnotations) quanto no nível da aplicação, garantindo a integridade dos dados.
*   **Tratamento de Exceções:** Tratamento robusto de exceções para garantir que erros sejam registrados e tratados adequadamente.
*   **Logging Detalhado:** O Serilog é utilizado para registrar eventos importantes do sistema, auxiliando na depuração e monitoramento.
*   **Autenticação Segura:** Implementamos a autenticação JWT para proteger as rotas da API e controlar o acesso aos recursos.
*   **Testes Unitários:** Testes unitários foram criados para garantir que cada componente funcione conforme esperado e para prevenir regressões.
*   **Dockerização:** A utilização do Docker garante um ambiente consistente e reproduzível, facilitando a colaboração e implantação.
*   **Async/Await:**  Utilizamos `async` e `await` para operações de I/O (como acesso ao banco de dados) para garantir que o sistema não fique bloqueado.

## Migrations Automáticas:

O EF Core foi configurado para realizar migrações automáticas do banco de dados, facilitando a evolução do esquema de dados ao longo do tempo. Alterações nas entidades são refletidas no banco de dados com um simples comando.

## Seeds Automáticos:

Implementamos seeds automáticos para popular o banco de dados com alguns dados iniciais, incluindo um usuário "admin" para fins de teste e demonstração. Este usuário pode ser usado para acessar funcionalidades administrativas da aplicação durante o desenvolvimento.

## RabbitMQ:

Utilizamos o RabbitMQ para implementar uma fila de mensagens. Isso permite que tarefas demoradas ou que não precisam ser executadas imediatamente (como a integração com a API Fabrica) sejam processadas de forma assíncrona, sem bloquear a resposta à requisição do cliente.

## Docker Compose:

Para facilitar a execução do projeto, fornecemos um arquivo `docker-compose.yaml` que define e configura todos os serviços necessários (banco de dados PostgreSQL, RabbitMQ). Isso permite que você execute todo o ambiente em poucos comandos.

## Como Executar e Testar o Projeto:

1.  **Pré-requisitos:**
    *   Docker instalado e configurado.
    *   Docker Compose instalado.
2.  **Clone o Repositório:** `git clone [URL do repositório]`
3.  **Navegue até a Pasta do Projeto:** `cd Desafio-Fabrica-Pedidos-Back`
4.  **Execute o Docker Compose:** `docker-compose up --build` (O comando `--build` garante que as imagens sejam construídas antes de iniciar os containers)
5.  **Acesse a Aplicação:** A aplicação estará disponível em `https://localhost:8080`.  (Pode ser necessário configurar o certificado SSL se estiver usando HTTPS).

## Como Testar:

*   Os testes unitários podem ser executados através do Visual Studio ou utilizando a linha de comando: `dotnet test`

## Contribuições:

Sinta-se à vontade para contribuir com este projeto, reportando bugs, sugerindo melhorias e enviando pull requests.
