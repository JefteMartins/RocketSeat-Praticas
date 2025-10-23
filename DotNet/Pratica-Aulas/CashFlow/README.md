# CashFlow API

API para gerenciamento de fluxo de caixa (CashFlow), permitindo o registro de despesas.

## Estrutura do Projeto

O projeto segue uma arquitetura de Clean Architecture, dividida nas seguintes camadas:

- **CashFlow.API:** Camada de apresentação, responsável por expor os endpoints da API.
- **CashFlow.Application:** Camada de aplicação, contendo a lógica de negócio e os casos de uso.
- **CashFlow.Domain:** Camada de domínio, com as entidades principais e as interfaces dos repositórios.
- **CashFlow.Communication:** Camada de comunicação, definindo os DTOs (Data Transfer Objects) para requisições e respostas.
- **CashFlow.Infrastructure:** Camada de infraestrutura, responsável pela implementação do acesso a dados e outras dependências externas.
- **CashFlow.Exception:** Camada de exceções, contendo as exceções customizadas da aplicação.

## Funcionalidades

### Registro de Despesas

A API permite o registro de novas despesas através do endpoint `POST /api/expenses`.

**Request:**

```json
{
  "title": "Compra de material de escritório",
  "description": "Canetas e post-its",
  "amount": 50.75,
  "date": "2024-05-20T14:00:00",
  "paymentType": 1
}
```

**Response:**

- **201 Created:** Em caso de sucesso, com o corpo da resposta vazio.
- **400 Bad Request:** Em caso de dados inválidos na requisição.
- **500 Internal Server Error:** Em caso de erro inesperado no servidor.

## Entendendo o fluxo do projeto

### Fluxo de Registro de Despesa

O fluxograma abaixo descreve o processo de registro de uma nova despesa no sistema.

```mermaid
graph TD
    A[Usuário envia POST /api/expenses] --> B{ExpensesController};
    B --> C{IRegisterExpenseUseCase};
    C --> D[RegisterExpenseUseCase];
    D --> E{Validação};
    E -- Válido --> F[Mapeia para Entidade Expense];
    E -- Inválido --> G[Lança ErrorOnValidationException];
    F --> H{IExpensesRepository.Add};
    H --> I{IUnitOfWork.Commit};
    I --> J[Salva no Banco de Dados];
    J --> K[Retorna 201 Created];
    G --> L{ExceptionFilter};
    L --> M[Retorna 400 Bad Request com erros];

    subgraph "API Layer"
        B
        L
        M
        K
    end

    subgraph "Application Layer"
        C
        D
        E
        G
    end

    subgraph "Domain Layer"
        F
    end

    subgraph "Infrastructure Layer"
        H
        I
        J
    end
```

### Fluxo de Tratamento de Exceções

O sistema possui um filtro de exceções para padronizar as respostas de erro.

```mermaid
graph TD
    A[Ocorre uma exceção] --> B{ExceptionFilter};
    B -- É CashFlowException? --> C{Sim};
    B -- É CashFlowException? --> D{Não};
    C -- É ErrorOnValidationException? --> E{Sim};
    C -- É ErrorOnValidationException? --> F{Não - Outra Exceção Customizada};
    E --> G[Monta ResponseErrorJson com lista de erros];
    G --> H[Retorna 400 Bad Request];
    D --> I[Monta ResponseErrorJson com erro genérico];
    I --> J[Retorna 500 Internal Server Error];
```

### Fluxo de Internacionalização (i18n)

O `CultureMiddleware` permite que as mensagens de erro sejam traduzidas com base no header `Accept-Language` da requisição.

```mermaid
graph TD
    A[Requisição chega na API] --> B{CultureMiddleware};
    B --> C{Verifica header 'Accept-Language'};
    C -- Header presente e suportado --> D[Define a Cultura da Thread];
    C -- Header ausente ou não suportado --> E[Usa cultura padrão];
    D --> F[Próximo middleware];
    E --> F;
```

## Estrutura de Testes

A pasta `tests` contém os projetos de teste da solução, garantindo a qualidade e o correto funcionamento da API.

- **CommonTestUtilities:** Projeto que fornece utilitários para os testes, como a geração de dados falsos (`Bogus`) para as requisições. Isso facilita a criação de cenários de teste consistentes e variados.

- **Validator.Tests:** Projeto de testes unitários para as validações da aplicação. Utiliza o framework `xUnit` para a execução dos testes e as bibliotecas `FluentAssertions` e `Shouldly` para as asserções, tornando os testes mais legíveis e expressivos.