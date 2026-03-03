# Gastos Residenciais

## Sobre o projeto

O **Gastos Residenciais** é uma aplicação para gerenciamento de finanças pessoais, composta por uma **API em .NET 8** e um **Front-end em React (Vite)**, utilizando **PostgreSQL** como banco de dados.

A aplicação foi construída seguindo:
- **Paradigma DDD (Domain-Driven Design)**, com separação clara entre **Domínio**, **Aplicação**, **Infraestrutura** e **API**, mantendo as regras de negócio centralizadas no domínio e evitando acoplamento com detalhes de implementação.
- **Princípios SOLID**, garantindo um código mais manutenível, extensível e testável:
  - **S (Single Responsibility)**: classes e serviços com responsabilidades bem definidas (ex.: use cases, helpers, repositories, validators).
  - **O (Open/Closed)**: evolução das funcionalidades por extensão (interfaces/implementações) sem alterar regras existentes desnecessariamente.
  - **L (Liskov Substitution)**: uso consistente de abstrações (interfaces) permitindo troca de implementações sem quebrar contratos.
  - **I (Interface Segregation)**: interfaces específicas por necessidade (ex.: repositórios e contratos separados por leitura/escrita quando aplicável).
  - **D (Dependency Inversion)**: dependência de abstrações (interfaces) e injeção de dependência, reduzindo acoplamento e facilitando testes.

Toda a API é executada via **Docker Compose** e o **Nginx (Reverse Proxy)** centraliza os acessos para a API/Swagger.

**As migrations são executadas automaticamente na inicialização da API**, então não é necessário rodar comandos manuais para criar/atualizar o banco.

---

## Tecnologias

- .NET 8
- Entity Framework Core
- PostgreSQL
- React + Vite
- Docker + Docker Compose
- Nginx (Reverse Proxy)
- Swagger (documentação da API)

---

## Como rodar (Docker)

### Requisitos
- Docker
- Docker Compose

> Não é necessário instalar .NET, Node ou PostgreSQL na máquina. Tudo roda via Docker.

### Passo a passo

1) Clone o repositório e acesse a pasta:
    ```bash
    git clone https://github.com/matheusdamacena593/gastos-residenciais-api.git
    cd gastos-residenciais-api
    ```

2) Suba os containers:
    ```bash
    docker compose up --build
    ```

> Pronto! A aplicação estará disponível e o banco será preparado automaticamente.

---

## Acessos

### Front-end (Vite)
- http://localhost:3000

### API / Swagger (sempre pelo Nginx)
- Swagger:
  - https://localhost:5443/swagger
- API (base):
  - https://localhost:5443/

---

## Reset completo do banco (opcional)

Se quiser apagar dados e recriar tudo do zero:

- docker compose down -v
- docker compose up --build

---

## 👨‍💻 Autor

- Matheus Damacena
- [LinkedIn][linkedin]
- mateusdamacena593@gmail.com



[linkedin]: https://www.linkedin.com/in/matheus-damacena-carvalho-19bb74255/