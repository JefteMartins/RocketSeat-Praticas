# Petfolio

API para gerenciamento de um portfólio de animais de estimação. Este projeto foi desenvolvido como uma prática de introdução à arquitetura em camadas com .NET 8.

## Arquitetura

O projeto segue uma arquitetura em camadas para separar as responsabilidades, facilitando a manutenção e a escalabilidade.

- **`Petfolio.API`**: Camada de Apresentação. Responsável por expor os endpoints da API, receber as requisições HTTP e retornar as respostas. Utiliza o ASP.NET Core.
- **`Petfolio.Application`**: Camada de Aplicação. Contém a lógica de negócio e os casos de uso da aplicação (Use Cases).
- **`Petfolio.Communication`**: Camada de Comunicação. Define os contratos de dados (DTOs - Data Transfer Objects) utilizados para a comunicação entre as camadas, como as requisições (Requests) e respostas (Responses).

## Tecnologias Utilizadas

- **.NET 8**: Framework de desenvolvimento.
- **ASP.NET Core**: Para a construção da API.
- **Swagger (OpenAPI)**: Para documentação e teste interativo dos endpoints.

## Endpoints da API

A seguir estão os endpoints disponíveis na API `Petfolio`.

| Verbo  | Endpoint        | Descrição                      | Corpo da Requisição (Request) | Resposta de Sucesso (Response)      |
| :----- | :-------------- | :----------------------------- | :---------------------------- | :---------------------------------- |
| `POST` | `/api/Pet`      | Registra um novo pet.          | `RequesPetJson`               | `201 Created` com `ResponseRegisteredPetJson` |
| `PUT`  | `/api/Pet/{id}` | Atualiza os dados de um pet.   | `RequesPetJson`               | `204 NoContent`                     |
| `GET`  | `/api/Pet`      | Retorna a lista de todos os pets. | N/A                           | `200 OK` com `ResponseAllPetsJson`  |
| `GET`  | `/api/Pet/{id}` | Retorna um pet específico pelo ID. | N/A                           | `200 OK` com `ResponsePetJson`      |
| `DELETE`| `/api/Pet/{id}` | Deleta um pet pelo ID.         | N/A                           | `204 NoContent`                     |

## Como Executar o Projeto

1.  Abra um terminal na pasta raiz do projeto da API: `Petfolio.API`.
2.  Execute o comando a seguir para iniciar a aplicação:
    ```bash
    dotnet run
    ```
3.  A API estará em execução. Para acessar a documentação interativa do Swagger e testar os endpoints, abra o navegador no seguinte endereço: `http://localhost:5049/swagger`.

---

## Entendendo o Fluxo do Projeto

Os fluxogramas abaixo ilustram o fluxo das principais funcionalidades da API.

### 1. Fluxo de Registro de um Novo Pet (`POST /api/Pet`)

Este fluxo descreve como um novo pet é registrado no sistema. O cliente envia os dados do pet, a API os processa através do caso de uso correspondente e retorna uma confirmação.

```mermaid
sequenceDiagram
    participant Client as Cliente (ex: Navegador)
    participant API as Petfolio.API (Controller)
    participant App as Petfolio.Application (UseCase)

    Client->>+API: POST /api/Pet com dados (RequesPetJson)
    API->>+App: Chama RegisterPetUseCase.Execute(request)
    App-->>-API: Retorna o pet registrado (ResponseRegisteredPetJson)
    API-->>-Client: Resposta 201 Created com os dados do pet
end
```

### 2. Fluxo de Busca por Todos os Pets (`GET /api/Pet`)

Este fluxo mostra como o cliente solicita a lista de todos os pets cadastrados.

```mermaid
sequenceDiagram
    participant Client as Cliente (ex: Navegador)
    participant API as Petfolio.API (Controller)
    participant App as Petfolio.Application (UseCase)

    Client->>+API: GET /api/Pet
    API->>+App: Chama GetAllPetsUseCase.Execute()
    App-->>-API: Retorna a lista de pets (ResponseAllPetsJson)
    API-->>-Client: Resposta 200 OK com a lista
end
```

### 3. Fluxo de Busca de Pet por ID (`GET /api/Pet/{id}`)

Este fluxo detalha a busca por um pet específico utilizando seu ID.

```mermaid
sequenceDiagram
    participant Client as Cliente (ex: Navegador)
    participant API as Petfolio.API (Controller)
    participant App as Petfolio.Application (UseCase)

    Client->>+API: GET /api/Pet/1
    API->>+App: Chama GetPetByIdUseCase.Execute(1)
    App-->>-API: Retorna os detalhes do pet (ResponsePetJson)
    API-->>-Client: Resposta 200 OK com os dados do pet
end
```
