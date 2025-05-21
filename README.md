# 💰 Sistema Bancário API

Este projeto é uma API RESTful desenvolvida em .NET para gerenciamento de um sistema bancário. A aplicação permite o controle de clientes, contas bancárias e transações como depósitos, saques e transferências.

---

## 📌 Funcionalidades

- Cadastro e gerenciamento de **clientes**
- Abertura e encerramento de **contas bancárias**
- Realização de **depósitos**, **saques** e **transferências**
- Validações e regras de negócio implementadas
- Organização seguindo boas práticas (camadas separadas: Controllers, Services, Repositories, Models)

---

## 🛠️ Tecnologias Utilizadas

- [.NET 6 ou superior](https://dotnet.microsoft.com/)
- ASP.NET Core Web API
- Entity Framework Core (EF Core)
- SQL Server (ou outro banco relacional)
- Swagger (para documentação e testes)

---
📂 Estrutura do Projeto
SistemaBancario/
├── Controllers/        # Endpoints da API
├── Models/             # Entidades e DTOs
├── Services/           # Lógica de negócio
├── Repositories/       # Acesso a dados
├── Data/               # Contexto do banco de dados
├── Program.cs          # Ponto de entrada da aplicação
├── appsettings.json    # Configurações da aplicação

---

📈 Futuras Melhorias

Implementar autenticação com JWT

Adicionar testes unitários

Consumir API externa para o cep

Deploy em nuvem (Azure ou Render)

