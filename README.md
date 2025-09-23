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
http://localhost:5147/swagger
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
  "totalCount": 32,
  "pageNumber": 1,
  "pageSize": 1,
  "totalPages": 32,
  "data": [
    {
      "clienteId": 1,
      "nomeCliente": "Carlos Silva",
      "telefoneCliente": "11912345678",
      "sexoCliente": "M",
      "emailCliente": "carlos@email.com",
      "cpfCliente": "12345678900",
      "motos": [
        {
          "motoId": 1,
          "placaMoto": "ABC1234",
          "modeloMoto": "Mottu Pop",
          "situacaoMoto": "Em Trânsito",
          "chassiMoto": "CHS12345678901234"
        }
      ]
    }
  ],
  "links": [
    {
      "rel": "self",
      "href": "/clientes?pageNumber=1&pageSize=1",
      "method": "GET"
    },
    {
      "rel": "create",
      "href": "/clientes",
      "method": "POST"
    },
    {
      "rel": "next",
      "href": "/clientes?pageNumber=2&pageSize=1",
      "method": "GET"
    },
    {
      "rel": "prev",
      "href": "",
      "method": "GET"
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
    "nomeCliente": "Carlos Silva",
    "telefoneCliente": "11912345678",
    "sexoCliente": "M",
    "emailCliente": "carlos@email.com",
    "cpfCliente": "12345678900",
    "motos": [
      {
        "motoId": 1,
        "placaMoto": "ABC1234",
        "modeloMoto": "Mottu Pop",
        "situacaoMoto": "Em Trânsito",
        "chassiMoto": "CHS12345678901234"
      }
    ]
  },
  "links": [
    {
      "rel": "self",
      "href": "/clientes/1",
      "method": "GET"
    },
    {
      "rel": "update",
      "href": "/clientes/1",
      "method": "PUT"
    },
    {
      "rel": "delete",
      "href": "/clientes/1",
      "method": "DELETE"
    },
    {
      "rel": "list",
      "href": "/clientes",
      "method": "GET"
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

Exemplo:
```json
{
  "nomeCliente": "Carlos dos Santos",
  "telefoneCliente": "11999999999",
  "sexoCliente": "M",
  "emailCliente": "c.santos@email.com",
  "cpfCliente": "12345678909"
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

Exemplo:
```
{
  "nomeCliente": "Jonas dos Santos Jr",
  "telefoneCliente": "11999999998",
  "sexoCliente": "M",
  "emailCliente": "jonas.santosjr@email.com",
  "cpfCliente": "12345678906"
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
  "totalCount": 33,
  "pageNumber": 1,
  "pageSize": 1,
  "totalPages": 33,
  "data": [
    {
      "motoId": 1,
      "placaMoto": "ABC1234",
      "modeloMoto": "Mottu Pop",
      "situacaoMoto": "Em Trânsito",
      "chassiMoto": "CHS12345678901234",
      "cliente": {
        "clienteId": 1,
        "nomeCliente": "Carlos Silva",
        "telefoneCliente": "11912345678",
        "sexoCliente": "M",
        "emailCliente": "carlos@email.com",
        "cpfCliente": "12345678900"
      }
    }
  ],
  "links": [
    {
      "rel": "self",
      "href": "/motos?pageNumber=1&pageSize=1",
      "method": "GET"
    },
    {
      "rel": "create",
      "href": "/motos",
      "method": "POST"
    },
    {
      "rel": "next",
      "href": "/motos?pageNumber=2&pageSize=1",
      "method": "GET"
    },
    {
      "rel": "prev",
      "href": "",
      "method": "GET"
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
    "placaMoto": "ABC1234",
    "modeloMoto": "Mottu Pop",
    "situacaoMoto": "Em Trânsito",
    "chassiMoto": "CHS12345678901234",
    "cliente": {
      "clienteId": 1,
      "nomeCliente": "Carlos Silva",
      "telefoneCliente": "11912345678",
      "sexoCliente": "M",
      "emailCliente": "carlos@email.com",
      "cpfCliente": "12345678900"
    }
  },
  "links": [
    {
      "rel": "self",
      "href": "/motos/1",
      "method": "GET"
    },
    {
      "rel": "update",
      "href": "/motos/1",
      "method": "PUT"
    },
    {
      "rel": "delete",
      "href": "/motos/1",
      "method": "DELETE"
    },
    {
      "rel": "list",
      "href": "/motos",
      "method": "GET"
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
    "placaMoto": "ABC1234",
    "modeloMoto": "Mottu Pop",
    "situacaoMoto": "Em Trânsito",
    "chassiMoto": "CHS12345678901234",
    "cliente": {
      "clienteId": 1,
      "nomeCliente": "Carlos Silva",
      "telefoneCliente": "11912345678",
      "sexoCliente": "M",
      "emailCliente": "carlos@email.com",
      "cpfCliente": "12345678900"
    }
  },
  "links": [
    {
      "rel": "self",
      "href": "/motos/1",
      "method": "GET"
    },
    {
      "rel": "update",
      "href": "/motos/1",
      "method": "PUT"
    },
    {
      "rel": "delete",
      "href": "/motos/1",
      "method": "DELETE"
    },
    {
      "rel": "list",
      "href": "/motos",
      "method": "GET"
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
      "numeroVaga": "A1-V1",
      "statusOcupada": 0
    },
    "setor": {
      "setorId": 1,
      "tipoSetor": "Pendência",
      "statusSetor": "Parcial",
      "patioId": 1
    },
    "dtEntrada": "2025-01-02T00:00:00",
    "dtSaida": "2025-01-03T00:00:00",
    "permanece": false
  },
  "links": [
    {
      "rel": "self",
      "href": "/motos/1",
      "method": "GET"
    },
    {
      "rel": "update",
      "href": "/motos/1",
      "method": "PUT"
    },
    {
      "rel": "delete",
      "href": "/motos/1",
      "method": "DELETE"
    },
    {
      "rel": "list",
      "href": "/motos",
      "method": "GET"
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

Exemplo:

```json
{
  "placaMoto": "ABC1235",
  "modeloMoto": "Mottu Sport",
  "situacaoMoto": "Ativa",
  "chassiMoto": "9C2JC5020NR123456"
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

Exemplo: 

```json
{
  "placaMoto": "ABC1236",
  "modeloMoto": "Mottu Pop",
  "situacaoMoto": "Ativa",
  "chassiMoto": "9C2JC5020NR123457"
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
  "totalCount": 8,
  "pageNumber": 1,
  "pageSize": 1,
  "totalPages": 8,
  "data": [
    {
      "patioId": 1,
      "localizacaoPatio": "Zona Norte",
      "nomePatio": "Pátio Norte",
      "descricaoPatio": "Área ampla e coberta",
      "setores": [
        {
          "setorId": 1,
          "tipoSetor": "Pendência",
          "statusSetor": "Parcial",
          "vagas": [
            {
              "vagaId": 3,
              "numeroVaga": "A1-V3",
              "statusOcupada": 1
            },
            {
              "vagaId": 2,
              "numeroVaga": "A1-V2",
              "statusOcupada": 0
            },
            {
              "vagaId": 1,
              "numeroVaga": "A1-V1",
              "statusOcupada": 0
            },
            {
              "vagaId": 4,
              "numeroVaga": "A1-V4",
              "statusOcupada": 1
            }
          ]
        },
        {
          "setorId": 2,
          "tipoSetor": "Reparos Simples",
          "statusSetor": "Parcial",
          "vagas": [
            {
              "vagaId": 7,
              "numeroVaga": "A2-V3",
              "statusOcupada": 1
            },
            {
              "vagaId": 6,
              "numeroVaga": "A2-V2",
              "statusOcupada": 0
            },
            {
              "vagaId": 5,
              "numeroVaga": "A2-V1",
              "statusOcupada": 0
            },
            {
              "vagaId": 8,
              "numeroVaga": "A2-V4",
              "statusOcupada": 1
            }
          ]
        },
        {
          "setorId": 3,
          "tipoSetor": "Danos Estruturais Graves",
          "statusSetor": "Parcial",
          "vagas": [
            {
              "vagaId": 9,
              "numeroVaga": "A3-V1",
              "statusOcupada": 0
            },
            {
              "vagaId": 10,
              "numeroVaga": "A3-V2",
              "statusOcupada": 0
            },
            {
              "vagaId": 11,
              "numeroVaga": "A3-V3",
              "statusOcupada": 1
            },
            {
              "vagaId": 12,
              "numeroVaga": "A3-V4",
              "statusOcupada": 1
            }
          ]
        },
        {
          "setorId": 4,
          "tipoSetor": "Motor Defeituoso",
          "statusSetor": "Parcial",
          "vagas": [
            {
              "vagaId": 13,
              "numeroVaga": "A4-V1",
              "statusOcupada": 0
            },
            {
              "vagaId": 14,
              "numeroVaga": "A4-V2",
              "statusOcupada": 0
            },
            {
              "vagaId": 15,
              "numeroVaga": "A4-V3",
              "statusOcupada": 1
            },
            {
              "vagaId": 16,
              "numeroVaga": "A4-V4",
              "statusOcupada": 1
            }
          ]
        },
        {
          "setorId": 5,
          "tipoSetor": "Agendada Para Manutenção",
          "statusSetor": "Parcial",
          "vagas": [
            {
              "vagaId": 17,
              "numeroVaga": "A5-V1",
              "statusOcupada": 0
            },
            {
              "vagaId": 20,
              "numeroVaga": "A5-V4",
              "statusOcupada": 1
            },
            {
              "vagaId": 19,
              "numeroVaga": "A5-V3",
              "statusOcupada": 1
            },
            {
              "vagaId": 18,
              "numeroVaga": "A5-V2",
              "statusOcupada": 0
            }
          ]
        },
        {
          "setorId": 6,
          "tipoSetor": "Pronta para Aluguel",
          "statusSetor": "Parcial",
          "vagas": [
            {
              "vagaId": 22,
              "numeroVaga": "A6-V2",
              "statusOcupada": 0
            },
            {
              "vagaId": 21,
              "numeroVaga": "A6-V1",
              "statusOcupada": 0
            },
            {
              "vagaId": 23,
              "numeroVaga": "A6-V3",
              "statusOcupada": 1
            },
            {
              "vagaId": 24,
              "numeroVaga": "A6-V4",
              "statusOcupada": 1
            }
          ]
        },
        {
          "setorId": 7,
          "tipoSetor": "Sem Placa",
          "statusSetor": "Parcial",
          "vagas": [
            {
              "vagaId": 26,
              "numeroVaga": "A7-V2",
              "statusOcupada": 0
            },
            {
              "vagaId": 25,
              "numeroVaga": "A7-V1",
              "statusOcupada": 0
            },
            {
              "vagaId": 27,
              "numeroVaga": "A7-V3",
              "statusOcupada": 1
            },
            {
              "vagaId": 28,
              "numeroVaga": "A7-V4",
              "statusOcupada": 1
            }
          ]
        },
        {
          "setorId": 8,
          "tipoSetor": "Minha Mottu",
          "statusSetor": "Parcial",
          "vagas": [
            {
              "vagaId": 29,
              "numeroVaga": "A8-V1",
              "statusOcupada": 0
            },
            {
              "vagaId": 32,
              "numeroVaga": "A8-V4",
              "statusOcupada": 1
            },
            {
              "vagaId": 31,
              "numeroVaga": "A8-V3",
              "statusOcupada": 1
            },
            {
              "vagaId": 30,
              "numeroVaga": "A8-V2",
              "statusOcupada": 0
            },
            {
              "vagaId": 33,
              "numeroVaga": "A8-V5",
              "statusOcupada": 1
            }
          ]
        }
      ]
    }
  ],
  "links": [
    {
      "rel": "self",
      "href": "/patios?pageNumber=1&pageSize=1",
      "method": "GET"
    },
    {
      "rel": "next",
      "href": "/patios?pageNumber=2&pageSize=1",
      "method": "GET"
    },
    {
      "rel": "prev",
      "href": "",
      "method": "GET"
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
    "localizacaoPatio": "Zona Norte",
    "nomePatio": "Pátio Norte",
    "descricaoPatio": "Área ampla e coberta",
    "setores": [
      {
        "setorId": 1,
        "tipoSetor": "Pendência",
        "statusSetor": "Parcial",
        "vagas": [
          {
            "vagaId": 3,
            "numeroVaga": "A1-V3",
            "statusOcupada": 1
          },
          {
            "vagaId": 2,
            "numeroVaga": "A1-V2",
            "statusOcupada": 0
          },
          {
            "vagaId": 1,
            "numeroVaga": "A1-V1",
            "statusOcupada": 0
          },
          {
            "vagaId": 4,
            "numeroVaga": "A1-V4",
            "statusOcupada": 1
          }
        ]
      },
      {
        "setorId": 2,
        "tipoSetor": "Reparos Simples",
        "statusSetor": "Parcial",
        "vagas": [
          {
            "vagaId": 7,
            "numeroVaga": "A2-V3",
            "statusOcupada": 1
          },
          {
            "vagaId": 6,
            "numeroVaga": "A2-V2",
            "statusOcupada": 0
          },
          {
            "vagaId": 5,
            "numeroVaga": "A2-V1",
            "statusOcupada": 0
          },
          {
            "vagaId": 8,
            "numeroVaga": "A2-V4",
            "statusOcupada": 1
          }
        ]
      },
      {
        "setorId": 3,
        "tipoSetor": "Danos Estruturais Graves",
        "statusSetor": "Parcial",
        "vagas": [
          {
            "vagaId": 9,
            "numeroVaga": "A3-V1",
            "statusOcupada": 0
          },
          {
            "vagaId": 10,
            "numeroVaga": "A3-V2",
            "statusOcupada": 0
          },
          {
            "vagaId": 11,
            "numeroVaga": "A3-V3",
            "statusOcupada": 1
          },
          {
            "vagaId": 12,
            "numeroVaga": "A3-V4",
            "statusOcupada": 1
          }
        ]
      },
      {
        "setorId": 4,
        "tipoSetor": "Motor Defeituoso",
        "statusSetor": "Parcial",
        "vagas": [
          {
            "vagaId": 13,
            "numeroVaga": "A4-V1",
            "statusOcupada": 0
          },
          {
            "vagaId": 14,
            "numeroVaga": "A4-V2",
            "statusOcupada": 0
          },
          {
            "vagaId": 15,
            "numeroVaga": "A4-V3",
            "statusOcupada": 1
          },
          {
            "vagaId": 16,
            "numeroVaga": "A4-V4",
            "statusOcupada": 1
          }
        ]
      },
      {
        "setorId": 5,
        "tipoSetor": "Agendada Para Manutenção",
        "statusSetor": "Parcial",
        "vagas": [
          {
            "vagaId": 17,
            "numeroVaga": "A5-V1",
            "statusOcupada": 0
          },
          {
            "vagaId": 20,
            "numeroVaga": "A5-V4",
            "statusOcupada": 1
          },
          {
            "vagaId": 19,
            "numeroVaga": "A5-V3",
            "statusOcupada": 1
          },
          {
            "vagaId": 18,
            "numeroVaga": "A5-V2",
            "statusOcupada": 0
          }
        ]
      },
      {
        "setorId": 6,
        "tipoSetor": "Pronta para Aluguel",
        "statusSetor": "Parcial",
        "vagas": [
          {
            "vagaId": 22,
            "numeroVaga": "A6-V2",
            "statusOcupada": 0
          },
          {
            "vagaId": 21,
            "numeroVaga": "A6-V1",
            "statusOcupada": 0
          },
          {
            "vagaId": 23,
            "numeroVaga": "A6-V3",
            "statusOcupada": 1
          },
          {
            "vagaId": 24,
            "numeroVaga": "A6-V4",
            "statusOcupada": 1
          }
        ]
      },
      {
        "setorId": 7,
        "tipoSetor": "Sem Placa",
        "statusSetor": "Parcial",
        "vagas": [
          {
            "vagaId": 26,
            "numeroVaga": "A7-V2",
            "statusOcupada": 0
          },
          {
            "vagaId": 25,
            "numeroVaga": "A7-V1",
            "statusOcupada": 0
          },
          {
            "vagaId": 27,
            "numeroVaga": "A7-V3",
            "statusOcupada": 1
          },
          {
            "vagaId": 28,
            "numeroVaga": "A7-V4",
            "statusOcupada": 1
          }
        ]
      },
      {
        "setorId": 8,
        "tipoSetor": "Minha Mottu",
        "statusSetor": "Parcial",
        "vagas": [
          {
            "vagaId": 29,
            "numeroVaga": "A8-V1",
            "statusOcupada": 0
          },
          {
            "vagaId": 32,
            "numeroVaga": "A8-V4",
            "statusOcupada": 1
          },
          {
            "vagaId": 31,
            "numeroVaga": "A8-V3",
            "statusOcupada": 1
          },
          {
            "vagaId": 30,
            "numeroVaga": "A8-V2",
            "statusOcupada": 0
          },
          {
            "vagaId": 33,
            "numeroVaga": "A8-V5",
            "statusOcupada": 1
          }
        ]
      }
    ]
  },
  "links": [
    {
      "rel": "self",
      "href": "/patios/1",
      "method": "GET"
    },
    {
      "rel": "list",
      "href": "/patios",
      "method": "GET"
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
  "totalCount": 8,
  "pageNumber": 1,
  "pageSize": 1,
  "totalPages": 8,
  "data": [
    {
      "cargoId": 1,
      "nomeCargo": "Auxiliar",
      "descricaoCargo": "Responsável por auxiliar nas tarefas gerais da empresa"
    }
  ],
  "links": [
    {
      "rel": "self",
      "href": "/cargos?pageNumber=1&pageSize=1",
      "method": "GET"
    },
    {
      "rel": "next",
      "href": "/cargos?pageNumber=2&pageSize=1",
      "method": "GET"
    },
    {
      "rel": "prev",
      "href": "",
      "method": "GET"
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
    "nomeCargo": "Auxiliar",
    "descricaoCargo": "Responsável por auxiliar nas tarefas gerais da empresa"
  },
  "links": [
    {
      "rel": "self",
      "href": "cargos/1",
      "method": "GET"
    },
    {
      "rel": "list",
      "href": "/cargos",
      "method": "GET"
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
  "totalCount": 201,
  "pageNumber": 1,
  "pageSize": 1,
  "totalPages": 201,
  "data": [
    {
      "vagaId": 1,
      "numeroVaga": "A1-V1",
      "statusOcupada": 0,
      "setor": {
        "setorId": 1,
        "tipoSetor": "Pendência",
        "statusSetor": "Parcial",
        "patioId": 1
      }
    }
  ],
  "links": [
    {
      "rel": "self",
      "href": "/vagas?pageNumber=1&pageSize=1",
      "method": "GET"
    },
    {
      "rel": "next",
      "href": "/vagas?pageNumber=2&pageSize=1",
      "method": "GET"
    },
    {
      "rel": "prev",
      "href": "",
      "method": "GET"
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
    "numeroVaga": "A1-V1",
    "statusOcupada": 0,
    "setor": {
      "setorId": 1,
      "tipoSetor": "Pendência",
      "statusSetor": "Parcial",
      "patioId": 1
    }
  },
  "links": [
    {
      "rel": "self",
      "href": "/vagas/1",
      "method": "GET"
    },
    {
      "rel": "list",
      "href": "/vagas",
      "method": "GET"
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
  "totalCount": 8,
  "pageNumber": 1,
  "pageSize": 1,
  "totalPages": 8,
  "data": [
    {
      "funcionarioId": 1,
      "nomeFuncionario": "Ricardo Ramos",
      "telefoneFuncionario": "11911112222",
      "cargo": {
        "cargoId": 1,
        "nomeCargo": "Auxiliar",
        "descricaoCargo": "Responsável por auxiliar nas tarefas gerais da empresa"
      },
      "patio": {
        "patioId": 1,
        "localizacaoPatio": "Zona Norte",
        "nomePatio": "Pátio Norte",
        "descricaoPatio": "Área ampla e coberta"
      }
    }
  ],
  "links": [
    {
      "rel": "self",
      "href": "/funcionarios?pageNumber=1&pageSize=1",
      "method": "GET"
    },
    {
      "rel": "next",
      "href": "/funcionarios?pageNumber=2&pageSize=1",
      "method": "GET"
    },
    {
      "rel": "prev",
      "href": "",
      "method": "GET"
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
    "nomeFuncionario": "Ricardo Ramos",
    "telefoneFuncionario": "11911112222",
    "cargo": {
      "cargoId": 1,
      "nomeCargo": "Auxiliar",
      "descricaoCargo": "Responsável por auxiliar nas tarefas gerais da empresa"
    },
    "patio": {
      "patioId": 1,
      "localizacaoPatio": "Zona Norte",
      "nomePatio": "Pátio Norte",
      "descricaoPatio": "Área ampla e coberta"
    }
  },
  "links": [
    {
      "rel": "self",
      "href": "funcionarios/1",
      "method": "GET"
    },
    {
      "rel": "list",
      "href": "/funcionarios",
      "method": "GET"
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
  "totalCount": 8,
  "pageNumber": 1,
  "pageSize": 1,
  "totalPages": 8,
  "data": [
    {
      "gerenteId": 1,
      "nomeGerente": "Rodrigo Neves",
      "telefoneGerente": "11900001111",
      "cpfGerente": "99999999900",
      "patio": {
        "patioId": 1,
        "localizacaoPatio": "Zona Norte",
        "nomePatio": "Pátio Norte",
        "descricaoPatio": "Área ampla e coberta"
      }
    }
  ],
  "links": [
    {
      "rel": "self",
      "href": "/gerentes?pageNumber=1&pageSize=1",
      "method": "GET"
    },
    {
      "rel": "next",
      "href": "/gerentes?pageNumber=2&pageSize=1",
      "method": "GET"
    },
    {
      "rel": "prev",
      "href": "",
      "method": "GET"
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
    "nomeGerente": "Rodrigo Neves",
    "telefoneGerente": "11900001111",
    "cpfGerente": "99999999900",
    "patio": {
      "patioId": 1,
      "localizacaoPatio": "Zona Norte",
      "nomePatio": "Pátio Norte",
      "descricaoPatio": "Área ampla e coberta"
    }
  },
  "links": [
    {
      "rel": "self",
      "href": "/gerentes/1",
      "method": "GET"
    },
    {
      "rel": "list",
      "href": "/gerentes",
      "method": "GET"
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
  "totalCount": 64,
  "pageNumber": 1,
  "pageSize": 1,
  "totalPages": 64,
  "data": [
    {
      "setorId": 1,
      "tipoSetor": "Pendência",
      "statusSetor": "Parcial",
      "patio": {
        "patioId": 1,
        "localizacaoPatio": "Zona Norte",
        "nomePatio": "Pátio Norte",
        "descricaoPatio": "Área ampla e coberta"
      },
      "vagas": [
        {
          "vagaId": 1,
          "numeroVaga": "A1-V1",
          "statusOcupada": 0
        },
        {
          "vagaId": 4,
          "numeroVaga": "A1-V4",
          "statusOcupada": 1
        },
        {
          "vagaId": 3,
          "numeroVaga": "A1-V3",
          "statusOcupada": 1
        },
        {
          "vagaId": 2,
          "numeroVaga": "A1-V2",
          "statusOcupada": 0
        }
      ]
    }
  ],
  "links": [
    {
      "rel": "self",
      "href": "/setores?pageNumber=1&pageSize=1",
      "method": "GET"
    },
    {
      "rel": "next",
      "href": "/setores?pageNumber=2&pageSize=1",
      "method": "GET"
    },
    {
      "rel": "prev",
      "href": "",
      "method": "GET"
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
    "tipoSetor": "Pendência",
    "statusSetor": "Parcial",
    "patio": {
      "patioId": 1,
      "localizacaoPatio": "Zona Norte",
      "nomePatio": "Pátio Norte",
      "descricaoPatio": "Área ampla e coberta"
    },
    "vagas": [
      {
        "vagaId": 1,
        "numeroVaga": "A1-V1",
        "statusOcupada": 0
      },
      {
        "vagaId": 4,
        "numeroVaga": "A1-V4",
        "statusOcupada": 1
      },
      {
        "vagaId": 3,
        "numeroVaga": "A1-V3",
        "statusOcupada": 1
      },
      {
        "vagaId": 2,
        "numeroVaga": "A1-V2",
        "statusOcupada": 0
      }
    ]
  },
  "links": [
    {
      "rel": "self",
      "href": "setores/1",
      "method": "GET"
    },
    {
      "rel": "list",
      "href": "/setores",
      "method": "GET"
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
  "totalCount": 33,
  "pageNumber": 1,
  "pageSize": 1,
  "totalPages": 33,
  "data": [
    {
      "movimentacaoId": 1,
      "dtEntrada": "2025-01-02T00:00:00",
      "dtSaida": "2025-01-03T00:00:00",
      "descricaoMovimentacao": "Aguardando liberação",
      "moto": {
        "motoId": 1,
        "placaMoto": "ABC1234",
        "modeloMoto": "Mottu Pop",
        "situacaoMoto": "Em Trânsito",
        "chassiMoto": "CHS12345678901234",
        "cliente": {
          "clienteId": 1,
          "nomeCliente": "Carlos Silva",
          "telefoneCliente": "11912345678",
          "sexoCliente": "M",
          "emailCliente": "carlos@email.com",
          "cpfCliente": "12345678900"
        }
      },
      "vaga": {
        "vagaId": 1,
        "numeroVaga": "A1-V1",
        "statusOcupada": 0,
        "setor": {
          "setorId": 1,
          "tipoSetor": "Pendência",
          "statusSetor": "Parcial",
          "patioId": 1
        }
      }
    }
  ],
  "links": [
    {
      "rel": "self",
      "href": "/movimentacoes?pageNumber=1&pageSize=1",
      "method": "GET"
    },
    {
      "rel": "create",
      "href": "/movimentacoes",
      "method": "POST"
    },
    {
      "rel": "next",
      "href": "/movimentacoes?pageNumber=2&pageSize=1",
      "method": "GET"
    },
    {
      "rel": "prev",
      "href": "",
      "method": "GET"
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
    "dtEntrada": "2025-01-02T00:00:00",
    "dtSaida": "2025-01-03T00:00:00",
    "descricaoMovimentacao": "Aguardando liberação",
    "moto": {
      "motoId": 1,
      "placaMoto": "ABC1234",
      "modeloMoto": "Mottu Pop",
      "situacaoMoto": "Em Trânsito",
      "chassiMoto": "CHS12345678901234",
      "cliente": {
        "clienteId": 1,
        "nomeCliente": "Carlos Silva",
        "telefoneCliente": "11912345678",
        "sexoCliente": "M",
        "emailCliente": "carlos@email.com",
        "cpfCliente": "12345678900"
      }
    },
    "vaga": {
      "vagaId": 1,
      "numeroVaga": "A1-V1",
      "statusOcupada": 0,
      "setor": {
        "setorId": 1,
        "tipoSetor": "Pendência",
        "statusSetor": "Parcial",
        "patioId": 1
      }
    }
  },
  "links": [
    {
      "rel": "self",
      "href": "movimentacoes/1",
      "method": "GET"
    },
    {
      "rel": "update",
      "href": "movimentacoes/1",
      "method": "PUT"
    },
    {
      "rel": "delete",
      "href": "movimentacoes/1",
      "method": "DELETE"
    },
    {
      "rel": "list",
      "href": "/movimentacoes",
      "method": "GET"
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
  "totalCount": 33,
  "pageNumber": 1,
  "pageSize": 2,
  "totalPages": 17,
  "data": [
    {
      "movimentacaoId": 1,
      "dtEntrada": "2025-01-02T00:00:00",
      "dtSaida": "2025-01-03T00:00:00",
      "descricaoMovimentacao": "Aguardando liberação",
      "moto": {
        "motoId": 1,
        "placaMoto": "ABC1234",
        "modeloMoto": "Mottu Pop",
        "situacaoMoto": "Em Trânsito",
        "chassiMoto": "CHS12345678901234",
        "cliente": {
          "clienteId": 1,
          "nomeCliente": "Carlos Silva",
          "telefoneCliente": "11912345678",
          "sexoCliente": "M",
          "emailCliente": "carlos@email.com",
          "cpfCliente": "12345678900"
        }
      },
      "vaga": {
        "vagaId": 1,
        "numeroVaga": "A1-V1",
        "statusOcupada": 0,
        "setor": {
          "setorId": 1,
          "tipoSetor": "Pendência",
          "statusSetor": "Parcial",
          "patioId": 1
        }
      }
    }
  ],
  "links": [
    {
      "rel": "self",
      "href": "/movimentacoes/por-moto/1?pageNumber=1&pageSize=2",
      "method": "GET"
    },
    {
      "rel": "create",
      "href": "/movimentacoes",
      "method": "POST"
    },
    {
      "rel": "next",
      "href": "/movimentacoes/por-moto/1?pageNumber=2&pageSize=2",
      "method": "GET"
    },
    {
      "rel": "prev",
      "href": "",
      "method": "GET"
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
  "totalCount": 8,
  "pageNumber": 1,
  "pageSize": 1,
  "totalPages": 8,
  "data": [
    {
      "setor": "Pendência",
      "totalVagas": 4,
      "motosPresentes": 2
    },
    {
      "setor": "Reparos Simples",
      "totalVagas": 4,
      "motosPresentes": 2
    },
    {
      "setor": "Danos Estruturais Graves",
      "totalVagas": 4,
      "motosPresentes": 2
    },
    {
      "setor": "Motor Defeituoso",
      "totalVagas": 4,
      "motosPresentes": 2
    },
    {
      "setor": "Agendada Para Manutenção",
      "totalVagas": 4,
      "motosPresentes": 2
    },
    {
      "setor": "Pronta para Aluguel",
      "totalVagas": 4,
      "motosPresentes": 2
    },
    {
      "setor": "Sem Placa",
      "totalVagas": 4,
      "motosPresentes": 2
    },
    {
      "setor": "Minha Mottu",
      "totalVagas": 5,
      "motosPresentes": 3
    }
  ],
  "links": [
    {
      "rel": "self",
      "href": "/movimentacoes/ocupacao-por-setor/patio/1?pageNumber=1&pageSize=1",
      "method": "GET"
    },
    {
      "rel": "create",
      "href": "/movimentacoes",
      "method": "POST"
    },
    {
      "rel": "next",
      "href": "/movimentacoes/ocupacao-por-setor/patio/1?pageNumber=2&pageSize=1",
      "method": "GET"
    },
    {
      "rel": "prev",
      "href": "",
      "method": "GET"
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
  "descricaoMovimentacao": "Movimentação na Vaga 1 pela Moto 1",
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
