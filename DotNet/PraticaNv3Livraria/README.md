📚 Prática de CRUD em Memória (Seção 3 - Rocketseat)
Este projeto é uma API RESTful simples desenvolvida em ASP.NET Core que implementa as quatro operações básicas de CRUD (Create, Read, Update, Delete) para o gerenciamento de uma Livraria.

O principal objetivo desta prática, alinhada à Seção 3 do curso de .NET da Rocketseat, é demonstrar o conceito de persitência em memória (simulação de um banco de dados) utilizando o padrão Singleton e a Injeção de Dependência.

⚙️ Tecnologias Utilizadas
.NET 8 (ou versão compatível)

ASP.NET Core Web API

Swagger/OpenAPI para documentação e testes

Guid (UUID) como identificador único

Serviço Singleton para persistência em memória

🎯 Funcionalidades da API
A aplicação gerencia o modelo Book com as seguintes funcionalidades:

Verbo HTTP	Endpoint	Descrição
GET	/api/Books	Lista todos os livros em memória.
GET	/api/Books/{id}	Busca um livro específico pelo seu Guid (UUID).
POST	/api/Books	Adiciona um novo livro.
PUT	/api/Books/{id}	Atualiza todos os dados de um livro existente.
DELETE	/api/Books/{id}	Remove um livro do armazenamento em memória.

Exportar para as Planilhas
⚠️ Persistência em Memória
É importante notar que todos os dados (livros) são armazenados em uma lista na memória do servidor. Isso significa que todos os itens serão perdidos ao parar ou reiniciar a aplicação.

🚀 Como Rodar o Projeto
Siga os passos abaixo para iniciar a API no seu ambiente local.

1. Pré-requisitos
Certifique-se de ter o SDK do .NET 8 (ou superior) instalado em sua máquina.

2. Navegar e Construir
Abra o terminal na pasta raiz do projeto (PraticaNv3Livraria):

Bash

# Restaura as dependências do projeto
dotnet restore

# Constrói o projeto
dotnet build
3. Iniciar a Aplicação
Inicie a aplicação utilizando o comando dotnet run:

Bash

dotnet run
O terminal indicará a porta em que a aplicação está rodando (geralmente http://localhost:5xxx ou https://localhost:7xxx).

💻 Como Testar com Swagger
O Swagger é a ferramenta mais rápida para testar todos os endpoints da sua API.

1. Acessar o Swagger UI
Após iniciar a aplicação (dotnet run), abra o seu navegador e acesse a URL da documentação do Swagger:

http://localhost:<porta>/swagger
(Substitua <porta> pelo valor indicado no seu terminal, como 7000 ou 5001)

2. Testando o GET (Listar Livros)
Ao carregar a página, você verá o endpoint GET /api/Books.

Clique em GET /api/Books.

Clique no botão Try it out.

Clique em Execute.

Resultado Esperado: O retorno será um 200 OK e o corpo da resposta trará os dois livros de exemplo que foram configurados para iniciar junto com o serviço (por exemplo, "O Código Limpo" e "1984").

3. Testando o POST (Adicionar Livro)
Clique em POST /api/Books.

Clique no botão Try it out.

Substitua o corpo de exemplo (Example Value) por um novo livro (lembre-se de que o Id não é necessário, pois ele é gerado automaticamente):

JSON

{
  "title": "A Arte da Guerra",
  "author": "Sun Tzu",
  "genre": "Estratégia",
  "price": 35.00,
  "stock": 10
}
Clique em Execute.

Resultado Esperado: O retorno será um 201 Created e o corpo da resposta trará o livro que você acabou de adicionar, incluindo seu novo GUID gerado.

4. Testando o GET por ID (Busca)
Copie o GUID gerado na resposta do POST e cole-o no campo do endpoint GET /api/Books/{id} para testar a busca individual.