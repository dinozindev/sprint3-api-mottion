# Mottu Mottion - API

## Integrantes

- Giovanna Revito Roz - RM558981
- Kaian Gustavo de Oliveira Nascimento - RM558986
- Lucas Kenji Kikuchi - RM554424

## Descrição do Projeto

A Mottion nasce como resposta a um dos maiores gargalos operacionais
enfrentados atualmente pela Mottu: a falta de controle, rastreabilidade e padronização
na gestão de pátios de motos em suas filiais. Com mais de 100 unidades espalhadas
por diferentes regiões e layouts, a tarefa de localizar motos, controlar manutenções e
evitar furtos se tornou complexa, sujeita a falhas manuais e impactos diretos na
produtividade e segurança. 

Nosso projeto tem como missão automatizar e otimizar a operação física de
motos no pátio, garantindo visibilidade em tempo real, ações corretivas proativas e
total rastreabilidade — usando sensores IoT, visão computacional e um sistema web
/ mobile responsivo.

A API construída em .NET é responsável por gerenciar informações sobre os clientes, motos, pátios, vagas, setores, funcionários, gerentes, cargos e movimentações. Além disso, ela será de suma importância para a atualização e localização em tempo real da ocupação de motos em cada um dos setores do pátio, além de fornecer o histórico de movimentações de uma moto dentro da filial.

## Justificativa da Arquitetura

Optamos por utilizar **ASP.NET Core com Minimal APIs** pela simplicidade na definição de rotas e menor boilerplate em comparação com Controllers tradicionais.  

A separação em **camadas (Models, DTOs, Services e Endpoints)** garante melhor manutenção e testabilidade do código.  

A escolha do **Entity Framework Core** com banco Oracle se deu por facilitar o mapeamento objeto-relacional, reduzindo código de SQL manual.  

Além disso, configuramos **OpenAPI/Scalar** para documentação automática e padronizada dos endpoints, o que facilita o consumo da API.
## Instalação

### Instalação e Execução da API (.NET 9)
#### 📋 Pré-requisitos
Antes de instalar, verifique se os seguintes itens estão instalados:

- .NET 9 SDK

- Oracle Database ou acesso a um banco Oracle

- Oracle Entity Framework Core Provider

- Visual Studio 2022+ ou Rider (opcional)

- Git (opcional)

### Clone o repositório e acesse o diretório:

```bash
git clone https://github.com/dinozindev/sprint3-api-mottion.git
cd sprint3-api-mottion
cd Sprint3-API
```

### Instale as dependências:
```bash
dotnet restore
```

### Se deseja utilizar o banco de dados Oracle já desenvolvido (com todos os inserts), insira a linha abaixo em um arquivo .env na raiz do projeto:
```code
ConnectionStrings__OracleConnection=User Id=RM558986;Password=fiap25;Data Source=oracle.fiap.com.br:1521/orcl;
```

### Se deseja utilizar o próprio banco de dados Oracle, substitua o id e senha com suas credenciais:
```code
ConnectionStrings__OracleConnection=User Id=<id>;Password=<senha>;Data Source=oracle.fiap.com.br:1521/orcl;
```

### E execute para criar as tabelas: 
```bash
dotnet ef database update
```

### Inicie a aplicação: 
```bash
dotnet run
```

### Para acessar a documentação da aplicação: 
```bash
http://localhost:5147/scalar
```

### Para fazer um teste rápido através de um script, execute:
```bash
./script-test.sh
```

## Rotas da API

### Parâmetros de Rotas Paginadas (aplicável a todas)

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `pageNumber`      | `int` | **Obrigatório**. O número da página atual |
| `pageSize`      | `int` | **Obrigatório**. A quantidade de registros por página |

### Clientes

- #### Retorna todos os clientes

```http
  GET /clientes?pageNumber=&pageSize=
```

Response Body:

```json
{
  "totalCount": 1,
  "pageNumber": 1,
  "pageSize": 1,
  "totalPages": 1,
  "data": [
    {
      "clienteId": 1,
      "nomeCliente": "string",
      "telefoneCliente": "string",
      "sexoCliente": "string",
      "emailCliente": "string",
      "cpfCliente": "string",
      "motos": [
        {
          "motoId": 1,
          "placaMoto": null,
          "modeloMoto": "string",
          "situacaoMoto": "string",
          "chassiMoto": "string"
        }
      ]
    }
  ],
  "links": [
    {
      "rel": "string",
      "href": "string",
      "method": "string"
    }
  ]
}
```

Códigos de Resposta

| Código HTTP | Significado                     | Quando ocorre                                             |
|-------------|----------------------------------|-----------------------------------------------------------|
| 200 OK      | Requisição bem-sucedida         | Quando há clientes cadastrados                            |
| 204 No Content | Sem conteúdo a retornar      | Quando não há clientes cadastrados                        |
| 500 Internal Server Error | Erro interno     | Quando ocorre uma falha inesperada no servidor            |

- #### Retorna um cliente pelo ID

```http
  GET /clientes/{id}
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Obrigatório**. O ID do cliente que você deseja consultar |

Response Body:

```json
  {
  "data": {
    "clienteId": 1,
    "nomeCliente": "string",
    "telefoneCliente": "string",
    "sexoCliente": "string",
    "emailCliente": "string",
    "cpfCliente": "string",
    "motos": [
      {
        "motoId": 1,
        "placaMoto": null,
        "modeloMoto": "string",
        "situacaoMoto": "string",
        "chassiMoto": "string"
      }
    ]
  },
  "links": [
    {
      "rel": "string",
      "href": "string",
      "method": "string"
    }
  ]
}
```

Códigos de Resposta

| Código HTTP | Significado                     | Quando ocorre                                             |
|-------------|----------------------------------|-----------------------------------------------------------|
| 200 OK      | Requisição bem-sucedida         | Quando o cliente foi encontrado                            |
| 404 Not Found | Recurso não encontrado        | Quando o cliente especificado não existe       |
| 500 Internal Server Error | Erro interno     | Quando ocorre uma falha inesperada no servidor            |

- #### Cria um cliente

```http
  POST /clientes
```

Request Body:

```json
{
  "nomeCliente": "",
  "telefoneCliente": "",
  "sexoCliente": "",
  "emailCliente": "",
  "cpfCliente": ""
}
```

Códigos de Resposta

| Código HTTP       | Significado                     | Quando ocorre                                                  |
|-------------------|----------------------------------|----------------------------------------------------------------|
| 201 Created       | Recurso criado com sucesso      | Quando um cliente é criado com êxito |
| 400 Bad Request   | Requisição malformada           | Quando os dados enviados estão incorretos ou incompletos       |
| 409 Conflict      | Conflito de estado              | Quando há conflito, como dados duplicados (CPF)                     |
| 500 Internal Server Error | Erro interno             | Quando ocorre uma falha inesperada no servidor                 |

- #### Atualiza um cliente

```http
  PUT /clientes/{id}
```

Request Body:

```json
{
  "nomeCliente": "",
  "telefoneCliente": "",
  "sexoCliente": "",
  "emailCliente": "",
  "cpfCliente": ""
}
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Obrigatório**. O ID do cliente que você atualizar |

Códigos de Resposta

| Código HTTP       | Significado                     | Quando ocorre                                                  |
|-------------------|----------------------------------|----------------------------------------------------------------|
| 201 Created       | Recurso criado com sucesso      | Quando um cliente é criado com êxito |
| 400 Bad Request   | Requisição malformada           | Quando os dados enviados estão incorretos ou incompletos       |
| 404 Not Found | Recurso não encontrado        |  Quando nenhum cliente foi encontrado com o ID especificado      |
| 409 Conflict      | Conflito de estado              | Quando há conflito, como dados duplicados (CPF)                     |
| 500 Internal Server Error | Erro interno             | Quando ocorre uma falha inesperada no servidor                 |

- #### Deleta um cliente

```http
  DELETE /clientes/{id}
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Obrigatório**. O ID do cliente que você deseja deletar |

Códigos de Resposta

| Código HTTP       | Significado                     | Quando ocorre                                                  |
|-------------------|----------------------------------|----------------------------------------------------------------|
| 204 No Content    | Sem conteúdo a retornar         | Quando a remoção do cliente é válida, mas não há dados para retornar   |
| 404 Not Found     | Recurso não encontrado          | Quando o cliente especificado não é encontrado                |
| 500 Internal Server Error | Erro interno             | Quando ocorre uma falha inesperada no servidor                 |

### Motos

- #### Retorna todas as motos

```http
  GET /motos?pageNumber=&pageSize=
```

Response Body:

```json
{
  "totalCount": 1,
  "pageNumber": 1,
  "pageSize": 1,
  "totalPages": 1,
  "data": [
    {
      "motoId": 1,
      "placaMoto": null,
      "modeloMoto": "string",
      "situacaoMoto": "string",
      "chassiMoto": "string",
      "cliente": {
        "clienteId": 1,
        "nomeCliente": "string",
        "telefoneCliente": "string",
        "sexoCliente": "string",
        "emailCliente": "string",
        "cpfCliente": "string"
      }
    }
  ],
  "links": [
    {
      "rel": "string",
      "href": "string",
      "method": "string"
    }
  ]
}
```

Códigos de Resposta

| Código HTTP | Significado                     | Quando ocorre                                             |
|-------------|----------------------------------|-----------------------------------------------------------|
| 200 OK      | Requisição bem-sucedida         | Quando há motos cadastradas                            |
| 204 No Content | Sem conteúdo a retornar      | Quando não há motos cadastradas                        |
| 500 Internal Server Error | Erro interno     | Quando ocorre uma falha inesperada no servidor            |

- #### Retorna uma moto pelo ID

```http
  GET /motos/{id}
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Obrigatório**. O ID da moto que você deseja consultar |

```json
  {
  "data": {
    "motoId": 1,
    "placaMoto": null,
    "modeloMoto": "string",
    "situacaoMoto": "string",
    "chassiMoto": "string",
    "cliente": {
      "clienteId": 1,
      "nomeCliente": "string",
      "telefoneCliente": "string",
      "sexoCliente": "string",
      "emailCliente": "string",
      "cpfCliente": "string"
    }
  },
  "links": [
    {
      "rel": "string",
      "href": "string",
      "method": "string"
    }
  ]
}
```
Códigos de Resposta

| Código HTTP | Significado                     | Quando ocorre                                             |
|-------------|----------------------------------|-----------------------------------------------------------|
| 200 OK      | Requisição bem-sucedida         | Quando a moto foi encontrada                            |
| 404 Not Found | Recurso não encontrado        | Quando a moto especificada não existe       |
| 500 Internal Server Error | Erro interno     | Quando ocorre uma falha inesperada no servidor            |

- #### Retorna uma moto pelo ID

```http
  GET /motos/por-chassi/{numero-chassi}
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `numero-chassi`      | `string` | **Obrigatório**. O número de chassi da moto que você deseja consultar |

```json
{
  "data": {
    "motoId": 1,
    "placaMoto": null,
    "modeloMoto": "string",
    "situacaoMoto": "string",
    "chassiMoto": "string",
    "cliente": {
      "clienteId": 1,
      "nomeCliente": "string",
      "telefoneCliente": "string",
      "sexoCliente": "string",
      "emailCliente": "string",
      "cpfCliente": "string"
    }
  },
  "links": [
    {
      "rel": "string",
      "href": "string",
      "method": "string"
    }
  ]
}
```
Códigos de Resposta

| Código HTTP | Significado                     | Quando ocorre                                             |
|-------------|----------------------------------|-----------------------------------------------------------|
| 200 OK      | Requisição bem-sucedida         | Quando a moto foi encontrada                            |
| 404 Not Found | Recurso não encontrado        | Quando a moto especificada não existe       |
| 500 Internal Server Error | Erro interno     | Quando ocorre uma falha inesperada no 


- #### Retorna a última posição de uma moto pelo ID

```http
  GET /motos/{id}/ultima-posicao
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Obrigatório**. O ID da moto que você deseja obter a última posição |

Response Body: 

```json
{
  "data": {
    "vaga": {
      "vagaId": 1,
      "numeroVaga": "string",
      "statusOcupada": 1
    },
    "setor": {
      "setorId": 1,
      "tipoSetor": "string",
      "statusSetor": "string",
      "patioId": 1
    },
    "dtEntrada": "2025-09-10T15:03:13.669Z",
    "dtSaida": null,
    "permanece": true
  },
  "links": [
    {
      "rel": "string",
      "href": "string",
      "method": "string"
    }
  ]
}
```

Códigos de Resposta

| Código HTTP | Significado                     | Quando ocorre                                             |
|-------------|----------------------------------|-----------------------------------------------------------|
| 200 OK      | Requisição bem-sucedida         | Quando a posição anterior é encontrada                             |
| 404 Not Found | Recurso não encontrado        |  Quando nenhuma posição anterior foi encontrada       |
| 500 Internal Server Error | Erro interno     | Quando ocorre uma falha inesperada no servidor            |

- #### Cria uma moto

```http
  POST /motos
```

Request Body:

```json
{
  "placaMoto": null,
  "modeloMoto": "",
  "situacaoMoto": "",
  "chassiMoto": ""
}
```

Códigos de Resposta

| Código HTTP       | Significado                     | Quando ocorre                                                  |
|-------------------|----------------------------------|----------------------------------------------------------------|
| 201 Created       | Recurso criado com sucesso      | Quando uma moto é criada com êxito |
| 400 Bad Request   | Requisição malformada           | Quando os dados enviados estão incorretos ou incompletos       |
| 409 Conflict      | Conflito de estado              | Quando há conflito, como dados duplicados (placa e chassi)                     |
| 500 Internal Server Error | Erro interno             | Quando ocorre uma falha inesperada no servidor                 |

- #### Atualiza uma moto

```http
  PUT /motos/{id}
```

Request Body:

```json
{
  "placaMoto": null,
  "modeloMoto": "",
  "situacaoMoto": "",
  "chassiMoto": ""
}
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Obrigatório**. O ID da moto que você atualizar |

Códigos de Resposta

| Código HTTP       | Significado                     | Quando ocorre                                                  |
|-------------------|----------------------------------|----------------------------------------------------------------|
| 201 Created       | Recurso criado com sucesso      | Quando uma moto é criada com êxito |
| 400 Bad Request   | Requisição malformada           | Quando os dados enviados estão incorretos ou incompletos       |
| 404 Not Found | Recurso não encontrado        |  Quando nenhuma moto foi encontrada com o ID especificado      |
| 409 Conflict      | Conflito de estado              | Quando há conflito, como dados duplicados (placa e chassi)                     |
| 500 Internal Server Error | Erro interno             | Quando ocorre uma falha inesperada no servidor                 |



- #### Remove um cliente da moto

```http
  PUT /motos/{id}/remover-cliente
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Obrigatório**. O ID da moto que você atualizar |
| `clienteId`      | `int` | **Obrigatório**. O ID do cliente que você deseja remover |

Códigos de Resposta

| Código HTTP       | Significado                     | Quando ocorre                                                  |
|-------------------|----------------------------------|----------------------------------------------------------------|
| 204 No Content    | Sem conteúdo a retornar         | Quando a atualização da moto é válida, mas não há dados para retornar   |
| 404 Not Found     | Recurso não encontrado          | Quando a moto não é encontrada                 |
| 500 Internal Server Error | Erro interno             | Quando ocorre uma falha inesperada no servidor                 |


- #### Altera / Adiciona um cliente a moto

```http
  PUT /motos/{id}/alterar-cliente/{clienteId}
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Obrigatório**. O ID da moto que você atualizar |
| `clienteId`      | `int` | **Obrigatório**. O ID do cliente que você deseja atualizar / adicionar |

Códigos de Resposta

| Código HTTP       | Significado                     | Quando ocorre                                                  |
|-------------------|----------------------------------|----------------------------------------------------------------|
| 204 No Content    | Sem conteúdo a retornar         | Quando a atualização da moto é válida, mas não há dados para retornar   |
| 404 Not Found     | Recurso não encontrado          | Quando a moto não é encontrada                 |
| 500 Internal Server Error | Erro interno             | Quando ocorre uma falha inesperada no servidor                 |

- #### Deleta uma moto

```http
  DELETE /motos/{id}
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Obrigatório**. O ID da moto que você deseja deletar |

Códigos de Resposta

| Código HTTP       | Significado                     | Quando ocorre                                                  |
|-------------------|----------------------------------|----------------------------------------------------------------|
| 204 No Content    | Sem conteúdo a retornar         | Quando a remoção da moto é válida, mas não há dados para retornar   |
| 404 Not Found     | Recurso não encontrado          | Quando a moto especificada não é encontrada                |
| 500 Internal Server Error | Erro interno             | Quando ocorre uma falha inesperada no servidor                 |


### Patios

- #### Retorna a lista de pátios com setores e vagas

```http
  GET /patios?pageNumber=&pageSize=
```

Response Body: 

```json
{
  "totalCount": 1,
  "pageNumber": 1,
  "pageSize": 1,
  "totalPages": 1,
  "data": [
    {
      "patioId": 1,
      "localizacaoPatio": "string",
      "nomePatio": "string",
      "descricaoPatio": "string",
      "setores": [
        {
          "setorId": 1,
          "tipoSetor": "string",
          "statusSetor": "string",
          "vagas": [
            {
              "vagaId": 1,
              "numeroVaga": "string",
              "statusOcupada": 1
            }
          ]
        }
      ]
    }
  ],
  "links": [
    {
      "rel": "string",
      "href": "string",
      "method": "string"
    }
  ]
}
```

Códigos de Resposta

| Código HTTP       | Significado                     | Quando ocorre                                                  |
|-------------------|----------------------------------|----------------------------------------------------------------|
| 200 OK            | Requisição bem-sucedida         | Quando os patios são encontrados                     |
| 204 No Content    | Sem conteúdo a retornar         | Quando nenhum pátio existe  |
| 500 Internal Server Error | Erro interno             | Quando ocorre uma falha inesperada no servidor                 |

- #### Retorna um pátio a partir de um ID

```http
  GET /patios/{id}
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Obrigatório**. O ID do pátio que deseja consultar |

Response Body: 

```json
{
  "data": {
    "patioId": 1,
    "localizacaoPatio": "string",
    "nomePatio": "string",
    "descricaoPatio": "string",
    "setores": [
      {
        "setorId": 1,
        "tipoSetor": "string",
        "statusSetor": "string",
        "vagas": [
          {
            "vagaId": 1,
            "numeroVaga": "string",
            "statusOcupada": 1
          }
        ]
      }
    ]
  },
  "links": [
    {
      "rel": "string",
      "href": "string",
      "method": "string"
    }
  ]
}
```

Códigos de Resposta

| Código HTTP       | Significado                     | Quando ocorre                                                  |
|-------------------|----------------------------------|----------------------------------------------------------------|
| 200 OK            | Requisição bem-sucedida         | Quando o pátio é encontrado                     |
| 404 Not Found     | Recurso não encontrado          | Quando o pátio especificado não é encontrado               |
| 500 Internal Server Error | Erro interno             | Quando ocorre uma falha inesperada no servidor                 |

### Cargos

- #### Retorna a lista de cargos

```http
  GET /cargos?pageNumber=&pageSize=
```

Response Body: 

```json
{
  "totalCount": 1,
  "pageNumber": 1,
  "pageSize": 1,
  "totalPages": 1,
  "data": [
    {
      "cargoId": 1,
      "nomeCargo": "string",
      "descricaoCargo": "string"
    }
  ],
  "links": [
    {
      "rel": "string",
      "href": "string",
      "method": "string"
    }
  ]
}
```

Códigos de Resposta

| Código HTTP       | Significado                     | Quando ocorre                                                  |
|-------------------|----------------------------------|----------------------------------------------------------------|
| 200 OK            | Requisição bem-sucedida         | Quando os cargos são encontrados                     |
| 204 No Content    | Sem conteúdo a retornar         | Quando nenhum cargo existe  |
| 500 Internal Server Error | Erro interno             | Quando ocorre uma falha inesperada no servidor                 |

- #### Retorna um cargo a partir de um ID

```http
  GET /cargos/{id}
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Obrigatório**. O ID do cargo que deseja consultar |

Response Body: 

```json
{
  "data": {
    "cargoId": 1,
    "nomeCargo": "string",
    "descricaoCargo": "string"
  },
  "links": [
    {
      "rel": "string",
      "href": "string",
      "method": "string"
    }
  ]
}
```

Códigos de Resposta

| Código HTTP       | Significado                     | Quando ocorre                                                  |
|-------------------|----------------------------------|----------------------------------------------------------------|
| 200 OK            | Requisição bem-sucedida         | Quando o cargo é encontrado                     |
| 404 Not Found     | Recurso não encontrado          | Quando o cargo especificado não é encontrado               |
| 500 Internal Server Error | Erro interno             | Quando ocorre uma falha inesperada no servidor                 |

### Vagas

- #### Retorna a lista de vagas

```http
  GET /vagas?pageNumber=&pageSize=
```

Response Body: 

```json
{
  "totalCount": 1,
  "pageNumber": 1,
  "pageSize": 1,
  "totalPages": 1,
  "data": [
    {
      "vagaId": 1,
      "numeroVaga": "string",
      "statusOcupada": 1,
      "setor": {
        "setorId": 1,
        "tipoSetor": "string",
        "statusSetor": "string",
        "patioId": 1
      }
    }
  ],
  "links": [
    {
      "rel": "string",
      "href": "string",
      "method": "string"
    }
  ]
}
```

Códigos de Resposta

| Código HTTP       | Significado                     | Quando ocorre                                                  |
|-------------------|----------------------------------|----------------------------------------------------------------|
| 200 OK            | Requisição bem-sucedida         | Quando as vagas são encontradas                     |
| 204 No Content    | Sem conteúdo a retornar         | Quando nenhuma vaga existe  |
| 500 Internal Server Error | Erro interno             | Quando ocorre uma falha inesperada no servidor                 |

- #### Retorna uma vaga a partir de um ID

```http
  GET /vagas/{id}
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Obrigatório**. O ID da vaga que deseja consultar |

Response Body: 

```json
{
  "data": {
    "vagaId": 1,
    "numeroVaga": "string",
    "statusOcupada": 1,
    "setor": {
      "setorId": 1,
      "tipoSetor": "string",
      "statusSetor": "string",
      "patioId": 1
    }
  },
  "links": [
    {
      "rel": "string",
      "href": "string",
      "method": "string"
    }
  ]
}
```

Códigos de Resposta

| Código HTTP       | Significado                     | Quando ocorre                                                  |
|-------------------|----------------------------------|----------------------------------------------------------------|
| 200 OK            | Requisição bem-sucedida         | Quando a vaga é encontrada                     |
| 404 Not Found     | Recurso não encontrado          | Quando a vaga especificada não é encontrada               |
| 500 Internal Server Error | Erro interno             | Quando ocorre uma falha inesperada no servidor                 |

### Funcionarios

- #### Retorna a lista de funcionários

```http
  GET /funcionarios?pageNumber=&pageSize=
```

Response Body: 

```json
{
  "totalCount": 1,
  "pageNumber": 1,
  "pageSize": 1,
  "totalPages": 1,
  "data": [
    {
      "funcionarioId": 1,
      "nomeFuncionario": "string",
      "telefoneFuncionario": "string",
      "cargo": {
        "cargoId": 1,
        "nomeCargo": "string",
        "descricaoCargo": "string"
      },
      "patio": {
        "patioId": 1,
        "localizacaoPatio": "string",
        "nomePatio": "string",
        "descricaoPatio": "string"
      }
    }
  ],
  "links": [
    {
      "rel": "string",
      "href": "string",
      "method": "string"
    }
  ]
}
```

Códigos de Resposta

| Código HTTP       | Significado                     | Quando ocorre                                                  |
|-------------------|----------------------------------|----------------------------------------------------------------|
| 200 OK            | Requisição bem-sucedida         | Quando os funcionários são encontrados                     |
| 204 No Content    | Sem conteúdo a retornar         | Quando nenhum funcionário existe  |
| 500 Internal Server Error | Erro interno             | Quando ocorre uma falha inesperada no servidor                 |

- #### Retorna um funcionário a partir de um ID

```http
  GET /funcionarios/{id}
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Obrigatório**. O ID do funcionário que deseja consultar |

Response Body: 

```json
{
  "data": {
    "funcionarioId": 1,
    "nomeFuncionario": "string",
    "telefoneFuncionario": "string",
    "cargo": {
      "cargoId": 1,
      "nomeCargo": "string",
      "descricaoCargo": "string"
    },
    "patio": {
      "patioId": 1,
      "localizacaoPatio": "string",
      "nomePatio": "string",
      "descricaoPatio": "string"
    }
  },
  "links": [
    {
      "rel": "string",
      "href": "string",
      "method": "string"
    }
  ]
}
```

Códigos de Resposta

| Código HTTP       | Significado                     | Quando ocorre                                                  |
|-------------------|----------------------------------|----------------------------------------------------------------|
| 200 OK            | Requisição bem-sucedida         | Quando o funcionário é encontrado                     |
| 404 Not Found     | Recurso não encontrado          | Quando o funcionário especificado não é encontrado               |
| 500 Internal Server Error | Erro interno             | Quando ocorre uma falha inesperada no servidor                 |

### Gerentes

- #### Retorna a lista de gerentes

```http
  GET /gerentes?pageNumber=&pageSize=
```

Response Body: 

```json
{
  "totalCount": 1,
  "pageNumber": 1,
  "pageSize": 1,
  "totalPages": 1,
  "data": [
    {
      "gerenteId": 1,
      "nomeGerente": "string",
      "telefoneGerente": "string",
      "cpfGerente": "string",
      "patio": {
        "patioId": 1,
        "localizacaoPatio": "string",
        "nomePatio": "string",
        "descricaoPatio": "string"
      }
    }
  ],
  "links": [
    {
      "rel": "string",
      "href": "string",
      "method": "string"
    }
  ]
}
```

Códigos de Resposta

| Código HTTP       | Significado                     | Quando ocorre                                                  |
|-------------------|----------------------------------|----------------------------------------------------------------|
| 200 OK            | Requisição bem-sucedida         | Quando os gerentes são encontrados                     |
| 204 No Content    | Sem conteúdo a retornar         | Quando nenhum gerente existe  |
| 500 Internal Server Error | Erro interno             | Quando ocorre uma falha inesperada no servidor                 |

- #### Retorna um gerente a partir de um ID

```http
  GET /gerentes/{id}
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Obrigatório**. O ID do gerente que deseja consultar |

Response Body: 

```json
{
  "data": {
    "gerenteId": 1,
    "nomeGerente": "string",
    "telefoneGerente": "string",
    "cpfGerente": "string",
    "patio": {
      "patioId": 1,
      "localizacaoPatio": "string",
      "nomePatio": "string",
      "descricaoPatio": "string"
    }
  },
  "links": [
    {
      "rel": "string",
      "href": "string",
      "method": "string"
    }
  ]
}
```

Códigos de Resposta

| Código HTTP       | Significado                     | Quando ocorre                                                  |
|-------------------|----------------------------------|----------------------------------------------------------------|
| 200 OK            | Requisição bem-sucedida         | Quando o gerente é encontrado                     |
| 404 Not Found     | Recurso não encontrado          | Quando o gerente especificado não é encontrado               |
| 500 Internal Server Error | Erro interno             | Quando ocorre uma falha inesperada no servidor                 |

### Setores

- #### Retorna a lista de setores

```http
  GET /setores?pageNumber=&pageSize=
```

Response Body: 

```json
{
  "totalCount": 1,
  "pageNumber": 1,
  "pageSize": 1,
  "totalPages": 1,
  "data": [
    {
      "setorId": 1,
      "tipoSetor": "string",
      "statusSetor": "string",
      "patio": {
        "patioId": 1,
        "localizacaoPatio": "string",
        "nomePatio": "string",
        "descricaoPatio": "string"
      },
      "vagas": [
        {
          "vagaId": 1,
          "numeroVaga": "string",
          "statusOcupada": 1
        }
      ]
    }
  ],
  "links": [
    {
      "rel": "string",
      "href": "string",
      "method": "string"
    }
  ]
}
```

Códigos de Resposta

| Código HTTP       | Significado                     | Quando ocorre                                                  |
|-------------------|----------------------------------|----------------------------------------------------------------|
| 200 OK            | Requisição bem-sucedida         | Quando os setores são encontrados                     |
| 204 No Content    | Sem conteúdo a retornar         | Quando nenhum setor existe  |
| 500 Internal Server Error | Erro interno             | Quando ocorre uma falha inesperada no servidor                 |


- #### Retorna um setor a partir de um ID

```http
  GET /setores/{id}
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Obrigatório**. O ID do setor que deseja consultar |

Response Body: 

```json
{
  "data": {
    "setorId": 1,
    "tipoSetor": "string",
    "statusSetor": "string",
    "patio": {
      "patioId": 1,
      "localizacaoPatio": "string",
      "nomePatio": "string",
      "descricaoPatio": "string"
    },
    "vagas": [
      {
        "vagaId": 1,
        "numeroVaga": "string",
        "statusOcupada": 1
      }
    ]
  },
  "links": [
    {
      "rel": "string",
      "href": "string",
      "method": "string"
    }
  ]
}
```

Códigos de Resposta

| Código HTTP       | Significado                     | Quando ocorre                                                  |
|-------------------|----------------------------------|----------------------------------------------------------------|
| 200 OK            | Requisição bem-sucedida         | Quando o setor é encontrado                     |
| 404 Not Found     | Recurso não encontrado          | Quando o setor especificado não é encontrado               |
| 500 Internal Server Error | Erro interno             | Quando ocorre uma falha inesperada no servidor                 |

### Movimentações

- #### Retorna a lista de movimentações

```http
  GET /movimentacoes?pageNumber=&pageSize=
```

Response Body: 

```json
{
  "totalCount": 1,
  "pageNumber": 1,
  "pageSize": 1,
  "totalPages": 1,
  "data": [
    {
      "movimentacaoId": 1,
      "dtEntrada": "2025-09-10T15:03:13.669Z",
      "dtSaida": null,
      "descricaoMovimentacao": null,
      "moto": {
        "motoId": 1,
        "placaMoto": null,
        "modeloMoto": "string",
        "situacaoMoto": "string",
        "chassiMoto": "string",
        "cliente": {
          "clienteId": 1,
          "nomeCliente": "string",
          "telefoneCliente": "string",
          "sexoCliente": "string",
          "emailCliente": "string",
          "cpfCliente": "string"
        }
      },
      "vaga": {
        "vagaId": 1,
        "numeroVaga": "string",
        "statusOcupada": 1,
        "setor": {
          "setorId": 1,
          "tipoSetor": "string",
          "statusSetor": "string",
          "patioId": 1
        }
      }
    }
  ],
  "links": [
    {
      "rel": "string",
      "href": "string",
      "method": "string"
    }
  ]
}
```

Códigos de Resposta

| Código HTTP       | Significado                     | Quando ocorre                                                  |
|-------------------|----------------------------------|----------------------------------------------------------------|
| 200 OK            | Requisição bem-sucedida         | Quando as movimentações são encontradas                     |
| 204 No Content    | Sem conteúdo a retornar         | Quando nenhuma movimentação existe  |
| 500 Internal Server Error | Erro interno             | Quando ocorre uma falha inesperada no servidor                 |

- #### Retorna uma movimentação a partir de um ID

```http
  GET /movimentacoes/{id}
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Obrigatório**. O ID da movimentação que deseja consultar |

Response Body: 

```json
{
  "data": {
    "movimentacaoId": 1,
    "dtEntrada": "2025-09-10T15:03:13.669Z",
    "dtSaida": null,
    "descricaoMovimentacao": null,
    "moto": {
      "motoId": 1,
      "placaMoto": null,
      "modeloMoto": "string",
      "situacaoMoto": "string",
      "chassiMoto": "string",
      "cliente": {
        "clienteId": 1,
        "nomeCliente": "string",
        "telefoneCliente": "string",
        "sexoCliente": "string",
        "emailCliente": "string",
        "cpfCliente": "string"
      }
    },
    "vaga": {
      "vagaId": 1,
      "numeroVaga": "string",
      "statusOcupada": 1,
      "setor": {
        "setorId": 1,
        "tipoSetor": "string",
        "statusSetor": "string",
        "patioId": 1
      }
    }
  },
  "links": [
    {
      "rel": "string",
      "href": "string",
      "method": "string"
    }
  ]
}
```

Códigos de Resposta

| Código HTTP       | Significado                     | Quando ocorre                                                  |
|-------------------|----------------------------------|----------------------------------------------------------------|
| 200 OK            | Requisição bem-sucedida         | Quando a movimentação é encontrada                     |
| 404 Not Found     | Recurso não encontrado          | Quando a movimentação especificada não é encontrada              |
| 500 Internal Server Error | Erro interno             | Quando ocorre uma falha inesperada no servidor                 |

- #### Retorna movimentações de uma moto específica

```http
  GET /movimentacoes/por-moto/{motoId}?pageNumber=&pageSize=
```
| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Obrigatório**. O ID da moto que deseja consultar as movimentações |

Response Body: 

```json
{
  "totalCount": 1,
  "pageNumber": 1,
  "pageSize": 1,
  "totalPages": 1,
  "data": [
    {
      "movimentacaoId": 1,
      "dtEntrada": "2025-09-10T15:03:13.669Z",
      "dtSaida": null,
      "descricaoMovimentacao": null,
      "moto": {
        "motoId": 1,
        "placaMoto": null,
        "modeloMoto": "string",
        "situacaoMoto": "string",
        "chassiMoto": "string",
        "cliente": {
          "clienteId": 1,
          "nomeCliente": "string",
          "telefoneCliente": "string",
          "sexoCliente": "string",
          "emailCliente": "string",
          "cpfCliente": "string"
        }
      },
      "vaga": {
        "vagaId": 1,
        "numeroVaga": "string",
        "statusOcupada": 1,
        "setor": {
          "setorId": 1,
          "tipoSetor": "string",
          "statusSetor": "string",
          "patioId": 1
        }
      }
    }
  ],
  "links": [
    {
      "rel": "string",
      "href": "string",
      "method": "string"
    }
  ]
}
```

Códigos de Resposta

| Código HTTP       | Significado                     | Quando ocorre                                                  |
|-------------------|----------------------------------|----------------------------------------------------------------|
| 200 OK            | Requisição bem-sucedida         | Quando as movimentações da moto são encontradas                     |
| 204 No Content    | Sem conteúdo a retornar         | Quando nenhuma movimentação existe  |
| 404 Not Found     | Recurso não encontrado          | Quando a moto especificada não foi encontrada              |
| 500 Internal Server Error | Erro interno             | Quando ocorre uma falha inesperada no servidor                 |

- #### Retorna a ocupação por setor de um pátio com base nas movimentações

```http
  GET /movimentacoes/ocupacao-por-setor/patio/{id}?pageNumber=&pageSize=
```
| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Obrigatório**. O ID do pátio que deseja consultar a ocupação dos setores |

Response Body: 

```json
{
  "totalCount": 1,
  "pageNumber": 1,
  "pageSize": 1,
  "totalPages": 1,
  "data": [
    {
      "setor": "string",
      "totalVagas": 1,
      "motosPresentes": 1
    }
  ],
  "links": [
    {
      "rel": "string",
      "href": "string",
      "method": "string"
    }
  ]
}
```

Códigos de Resposta

| Código HTTP       | Significado                     | Quando ocorre                                                  |
|-------------------|----------------------------------|----------------------------------------------------------------|
| 200 OK            | Requisição bem-sucedida         | Quando a ocupação dos setores é retornada                     |
| 204 No Content    | Sem conteúdo a retornar         | Quando nenhum setor existe  |
| 404 Not Found     | Recurso não encontrado          | Quando o pátio especificado não foi encontrado              |
| 500 Internal Server Error | Erro interno             | Quando ocorre uma falha inesperada no servidor                 |

- #### Cria uma nova movimentação

```http
  POST /movimentacoes
```

Request Body:

```json
{
  "descricaoMovimentacao": "",
  "motoId": 1,
  "vagaId": 1
}
```

Códigos de Resposta

| Código HTTP       | Significado                     | Quando ocorre                                                  |
|-------------------|----------------------------------|----------------------------------------------------------------|
| 201 Created       | Recurso criado com sucesso      | Quando uma movimentação é criada com êxito |
| 400 Bad Request   | Requisição malformada           | Quando os dados enviados estão incorretos ou incompletos       |
| 409 Conflict      | Conflito de estado              | Quando há conflito, como dados duplicados (vaga e moto já presentes em outra movimentação)                     |
| 500 Internal Server Error | Erro interno             | Quando ocorre uma falha inesperada no servidor                 |

- #### Atualiza a data de saída da movimentação

```http
PUT /movimentacoes/{id}/saida
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Obrigatório**. O ID da movimentação que deseja atualizar a data de saída |

Códigos de Resposta

| Código HTTP       | Significado                     | Quando ocorre                                                  |
|-------------------|----------------------------------|----------------------------------------------------------------|
| 204 No Content    | Sem conteúdo a retornar         | Quando a atualização da data de saída da movimentação é válida, mas não há dados para retornar   |
| 400 Bad Request   | Requisição malformada           | Quando os dados enviados estão incorretos ou incompletos       |
| 404 Not Found     | Recurso não encontrado          | Quando a movimentação especificada não é encontrada                |
| 500 Internal Server Error | Erro interno             | Quando ocorre uma falha inesperada no servidor                 |

- #### Deleta uma movimentação

```http
  DELETE /movimentacoes/{id}
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Obrigatório**. O ID da movimentação que você deseja deletar |

Códigos de Resposta

| Código HTTP       | Significado                     | Quando ocorre                                                  |
|-------------------|----------------------------------|----------------------------------------------------------------|
| 204 No Content    | Sem conteúdo a retornar         | Quando a remoção da movimentação é válida, mas não há dados para retornar   |
| 404 Not Found     | Recurso não encontrado          | Quando a movimentação especificada não é encontrada                |
| 500 Internal Server Error | Erro interno             | Quando ocorre uma falha inesperada no servidor                 |
