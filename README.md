# 🛒 Ecommerce Clean Architecture - Desafio de Arquiteturas de Software

## 📚 Sobre o Projeto

Este projeto foi desenvolvido como parte do **Desafio de Arquiteturas de Software** do curso [Arquiteturas de Software Modernas](https://www.torneseumprogramador.com.br/cursos/arquiteturas_software) ministrado pelo **Prof. Danilo Aparecido** na plataforma [Torne-se um Programador](https://www.torneseumprogramador.com.br/).

### 🎯 Objetivo

Implementar um sistema de e-commerce utilizando **Clean Architecture** com .NET 8, Entity Framework Core, SQLite, AutoMapper, FluentValidation e Swagger.

## 🏗️ Arquitetura

O projeto segue os princípios da **Clean Architecture** com separação clara de responsabilidades:

```
┌─────────────────────────────────────┐
│           Ecommerce.WebApi          │ ← Controllers, Program.cs, Home/Health
├─────────────────────────────────────┤
│        Ecommerce.Application        │ ← UseCases, DTOs, Validators
├─────────────────────────────────────┤
│          Ecommerce.Domain           │ ← Entities, InterfacesRepository
├─────────────────────────────────────┤
│      Ecommerce.Infrastructure       │ ← Repositories, DbContext, DI
└─────────────────────────────────────┘
```

### 📁 Estrutura do Projeto

```
Ecommerce/
├── Ecommerce.WebApi/                # Camada de Apresentação (API)
│   ├── Controllers/                  # Controllers da API (Users, Products, Orders, OrderProducts, Home, Health)
│   ├── Profiles/                     # AutoMapper Profiles
│   ├── Program.cs                    # Configuração da aplicação
│   └── appsettings.json              # Configurações
├── Ecommerce.Application/           # Camada de Aplicação
│   ├── DTOs/                         # Data Transfer Objects
│   ├── UseCases/                     # Casos de uso (Application Services)
│   └── Validators/                   # Validadores FluentValidation
├── Ecommerce.Domain/                # Camada de Domínio
│   ├── Entities/                     # Entidades de domínio
│   └── InterfacesRepository/         # Interfaces dos repositórios
├── Ecommerce.Infrastructure/        # Camada de Infraestrutura
│   ├── Repositories/                 # Implementação dos repositórios
│   ├── EcommerceDbContext.cs         # DbContext
│   └── DependencyInjection.cs        # Configuração de DI
├── Ecommerce.Tests/                 # Testes unitários
└── README.md                        # Esta documentação
```

## 🚀 Tecnologias Utilizadas

- **.NET 8** - Framework de desenvolvimento
- **Entity Framework Core** - ORM para acesso a dados
- **SQLite** - Banco de dados local
- **Swagger/OpenAPI** - Documentação da API
- **AutoMapper** - Mapeamento de objetos
- **FluentValidation** - Validação de dados
- **Clean Architecture** - Organização do projeto

## 📋 Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Git](https://git-scm.com/)

## ⚡ Como Executar

```bash
# Clone o repositório
$ git clone <url-do-repositorio>
$ cd desafio-arquiteturas-clean-architecture

# Restaure os pacotes
$ dotnet restore

# Build da aplicação
$ dotnet build

# Execute as migrations (opcional, EF cria o banco automaticamente)
$ cd Ecommerce.WebApi
$ dotnet ef database update
$ cd ..

# Execute a aplicação
$ dotnet run --project Ecommerce.WebApi
```

## 🌐 Acessando a API

Após executar o projeto, a API estará disponível em:

- **API Base**: http://localhost:5000
- **Swagger UI**: http://localhost:5000/swagger
- **Health Check**: http://localhost:5000/api/health

## 📖 Endpoints da API

### 👤 Usuários (User)
| Método | Endpoint         | Descrição           |
|--------|------------------|---------------------|
| POST   | `/api/users`     | Criar usuário       |
| GET    | `/api/users/{id}`| Buscar usuário por ID |

### 📦 Produtos (Product)
| Método | Endpoint           | Descrição           |
|--------|--------------------|---------------------|
| POST   | `/api/products`    | Criar produto       |
| GET    | `/api/products`    | Listar produtos     |
| GET    | `/api/products/{id}`| Buscar produto por ID |

### 🛒 Pedidos (Order)
| Método | Endpoint           | Descrição           |
|--------|--------------------|---------------------|
| POST   | `/api/orders`      | Criar pedido        |
| GET    | `/api/orders`      | Listar pedidos      |
| GET    | `/api/orders/{id}` | Buscar pedido por ID|

### ➕ Produtos do Pedido (OrderProduct)
| Método | Endpoint                          | Descrição                      |
|--------|-----------------------------------|--------------------------------|
| POST   | `/api/orderproducts/{orderId}/add-product` | Adicionar produto ao pedido |

### 🏠 Home
| Método | Endpoint | Descrição                |
|--------|----------|--------------------------|
| GET    | `/`      | Página inicial da API    |

### ❤️ Health
| Método | Endpoint      | Descrição                        |
|--------|---------------|----------------------------------|
| GET    | `/api/health` | Verifica status da API e do banco|

## 🧪 Exemplos de Uso

### Criar Usuário
```bash
curl -X POST "http://localhost:5000/api/users" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "João Silva",
    "email": "joao@email.com",
    "password": "123456"
  }'
```

### Criar Produto
```bash
curl -X POST "http://localhost:5000/api/products" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Notebook Dell",
    "description": "Notebook i7 16GB",
    "price": 4999.99,
    "stock": 10
  }'
```

### Criar Pedido
```bash
curl -X POST "http://localhost:5000/api/orders" \
  -H "Content-Type: application/json" \
  -d '{
    "userId": "guid-do-usuario",
    "products": [
      {
        "productId": "guid-do-produto",
        "quantity": 2
      }
    ]
  }'
```

## 🛡️ Tratamento de Erros

A API retorna mensagens de erro padronizadas para validação e exceções de negócio.

### Exemplo de erro de validação
```json
{
  "errors": {
    "Email": ["Email inválido"]
  }
}
```

## 🔧 Configuração do Banco de Dados

A connection string está definida no arquivo `Ecommerce.WebApi/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=ecommerce.db"
  }
}
```

## 📝 Migrations

Para criar uma nova migration:

```bash
cd Ecommerce.WebApi
 dotnet ef migrations add NomeDaMigration
```

Para aplicar migrations:

```bash
cd Ecommerce.WebApi
 dotnet ef database update
```

## 👨‍🏫 Sobre o Professor

**Prof. Danilo Aparecido** é instrutor na plataforma [Torne-se um Programador](https://www.torneseumprogramador.com.br/), especializado em arquiteturas de software e desenvolvimento de sistemas escaláveis.

## 📚 Curso Completo

Para aprender mais sobre arquiteturas de software e aprofundar seus conhecimentos, acesse o curso completo:

**[Arquiteturas de Software Modernas](https://www.torneseumprogramador.com.br/cursos/arquiteturas_software)**

## 🤝 Contribuição

Este projeto foi desenvolvido como parte de um desafio educacional. Contribuições são bem-vindas através de issues e pull requests.

## 📄 Licença

Este projeto está sob a licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.

---

**Desenvolvido com ❤️ para o curso de Arquiteturas de Software do [Torne-se um Programador](https://www.torneseumprogramador.com.br/)** 