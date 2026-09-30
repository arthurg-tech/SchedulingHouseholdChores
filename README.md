# 🏡 Gestão de Tarefas Domésticas (Scheduling Household Chores)

Uma aplicação Full-Stack desenvolvida para facilitar o gerenciamento, o rastreamento e a organização de tarefas domésticas recorrentes. O projeto demonstra a integração completa de uma arquitetura moderna utilizando o ecossistema .NET, com foco em uma experiência de uso simples e eficiente.

## 🚀 Tecnologias Utilizadas

**Back-End:**
* **ASP.NET Core Web API:** Estruturação dos endpoints RESTful.
* **Entity Framework Core:** ORM utilizado para manipulação e persistência de dados.
* **SQLite:** Banco de dados leve e portátil, ideal para o escopo do projeto.

**Front-End:**
* **Blazor WebAssembly (WASM):** Interface de usuário construída com C# rodando nativamente no navegador.
* **MudBlazor:** Biblioteca de componentes baseada no Material Design, garantindo uma interface rica, limpa e responsiva.

**Testes e Qualidade:**
* **xUnit & EF Core InMemory:** Validação da lógica de negócios e endpoints isoladamente.

## ⚙️ Funcionalidades Atuais
- [x] Criação de novas tarefas com definição de título, descrição detalhada e frequência de repetição (em dias).
- [x] Listagem dinâmica de todas as tarefas cadastradas através de uma interface visual em *Cards*.
- [x] Integração em tempo real entre o cliente Blazor e a API REST.
- [x] Validação de formulários para garantir a consistência das informações.

## 🛣️ Roadmap e Passos Futuros

O projeto está em constante evolução para se tornar um assistente pessoal cada vez mais útil no dia a dia. As próximas grandes atualizações focarão em conveniência e personalização:

* **Contas de Usuário e Painel Pessoal:** Criação de um sistema de cadastro e login para que cada pessoa tenha o seu próprio ambiente privado. Dessa forma, a aplicação poderá ser usada por diferentes pessoas, onde cada usuário gerencia a sua própria lista de tarefas de forma isolada, sem misturar com as tarefas dos outros.
* **Lembretes Automáticos por E-mail:** Para que o usuário não precise lembrar de abrir o aplicativo todos os dias, o sistema passará a ser proativo. Uma rotina em segundo plano será implementada para enviar lembretes direto para o e-mail do usuário sempre que uma tarefa atingir a sua data limite (por exemplo, avisar que está na hora de comprar areia para os gatos ou lavar os filtros do ar-condicionado).

## 💻 Como Executar o Projeto Localmente

1. Clone este repositório:
   ```bash
   git clone https://github.com/seu-usuario/SchedulingHouseholdChores.git
