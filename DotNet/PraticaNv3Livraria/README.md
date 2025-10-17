# 📚 Prática de CRUD em Memória (Seção 3 - Rocketseat)

Este projeto é uma **API RESTful** simples desenvolvida em **ASP.NET Core** que implementa as quatro operações básicas de CRUD (Create, Read, Update, Delete) para o gerenciamento de uma Livraria.

O principal objetivo desta prática, alinhada à **Seção 3 do curso de .NET da Rocketseat**, é demonstrar o conceito de **persitência em memória** (simulação de um banco de dados) utilizando o padrão **Singleton** e a **Injeção de Dependência**.

### ⚙️ Tecnologias Utilizadas

* **.NET 8** (ou versão compatível)
* **ASP.NET Core Web API**
* **Swagger/OpenAPI** para documentação e testes
* **Guid (UUID)** como identificador único
* **Serviço Singleton** para persistência em memória

---

## 🎯 Funcionalidades da API

A aplicação gerencia o modelo `Book` com as seguintes funcionalidades e validações:

### Endpoints (CRUD)

| Verbo HTTP | Endpoint | Descrição | Status Codes |
| :--- | :--- | :--- | :--- |
| **GET** | `/api/Books` | Lista todos os livros em memória (inclui dados iniciais). | `200 OK` |
| **GET** | `/api/Books/{id}` | Busca um livro específico pelo seu `Guid` (UUID). | `200 OK`, `404 Not Found` |
| **POST** | `/api/Books` | Adiciona um novo livro. O `Id` é gerado automaticamente. | `201 Created`, `400 Bad Request` |
| **PUT** | `/api/Books/{id}` | Atualiza todos os dados de um livro existente. | `204 No Content`, `400 Bad Request`, `404 Not Found` |
| **DELETE** | `/api/Books/{id}` | Remove um livro do armazenamento em memória. | `204 No Content`, `404 Not Found` |

### Validações Implementadas

O endpoint `POST` inclui as seguintes verificações:

* **Campos Obrigatórios:** Validação automática via `required` (retorna `400`).
* **Tamanho das Strings:** Título e Autor devem ter entre 3 e 100 caracteres. Gênero entre 2 e 50.
* **Preço Mínimo:** O `Price` deve ser no mínimo R$20,00.
* **Duplicidade:** Verifica se já existe um livro com o mesmo **Título** e **Autor** (retorna `400`).

### ⚠️ Persistência em Memória

É fundamental lembrar que todos os dados (livros) são armazenados em uma lista na memória do servidor. Isso significa que **todos os itens serão perdidos ao parar ou reiniciar a aplicação.**

---

## 🚀 Como Rodar o Projeto

Siga os passos abaixo para iniciar a API no seu ambiente local.

### 1. Pré-requisitos

Certifique-se de ter o **SDK do .NET 8** (ou superior) instalado em sua máquina.

### 2. Navegar e Construir

Abra o terminal na pasta raiz do projeto (`PraticaNv3Livraria`):

```bash
# Restaura as dependências do projeto
dotnet restore

# Constrói o projeto
dotnet build
