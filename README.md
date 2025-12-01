# **Validador de Cartão de Crédito - Miniprojeto .NET 8**

## **Descrição**

Este é um miniprojeto para validar números de cartões de crédito usando o **algoritmo de Luhn**.
Ele suporta validação para cartões como Visa (16 dígitos), Mastercard, etc., verificando:

* Comprimento
* Prefixo
* Checksum

O projeto é dividido em:

* **Minimal API em .NET 8** para lógica de validação
* **Aplicativo Razor Pages** que consome essa API via HTTP

Não há persistência de dados (sem banco de dados), pois lida com dados sensíveis.
Testes são realizados com números gerados aleatoriamente (ex: via *4devs.com.br*).

O foco é em boas práticas:

* Separação de camadas (Models, Services, Controllers/Endpoints)
* Injeção de dependências
* Código comentado
* Documentação via Swagger
* Testes unitários
* Containerização com Docker

---

## **Tecnologias Utilizadas**

* .NET 8 (Minimal API para o backend)
* ASP.NET Core Razor Pages (frontend consumidor)
* Swagger/OpenAPI (documentação da API)
* xUnit ou NUnit (testes unitários)
* Docker (containerização)
* Algoritmo de Luhn (validação de cartões)

---

## **Pré-requisitos**

* .NET SDK 8.0 ou superior
* Docker Desktop
* Visual Studio 2022 ou VS Code com extensões .NET
* Gerador de números de cartão para testes (ex: [https://www.4devs.com.br/gerador_de_numero_cartao_credito](https://www.4devs.com.br/gerador_de_numero_cartao_credito))

---

## **Instalação**

### **Clone o repositório:**

```bash
git clone https://github.com/seu-usuario/validador-cartao-credito.git
cd validador-cartao-credito
```

### **Restaure as dependências da API:**

```bash
cd Api
dotnet restore
```

### **Restaure as dependências do Razor Pages:**

```bash
cd ../RazorPagesApp
dotnet restore
```

### **Construa e execute a API:**

```bash
cd ../Api
dotnet build
dotnet run
```

A API estará disponível em:

* [https://localhost:5001](https://localhost:5001)
* Swagger: [https://localhost:5001/swagger](https://localhost:5001/swagger)

### **Execute o Razor Pages:**

```bash
cd ../RazorPagesApp
dotnet run
```

Acesse em: [https://localhost:5002](https://localhost:5002)

---

## **Uso**

### **API**

**Endpoint principal:**
`POST /api/validar-cartao`

**Corpo da requisição (JSON):**

```json
{
  "numero": "4111111111111111"
}
```

**Resposta:**
JSON com resultado da validação (válido/inválido + mensagens).

---

### **Razor Pages**

* Acesse a página inicial para um formulário simples.
* Insira o número do cartão.
* Envie para validação via API.
* Resultado exibido na página.

---

## **Testes**

* Gere números de teste no 4devs.
* Use o formulário no Razor Pages ou ferramentas como Postman para testar a API.

---

## **Executando Testes Unitários**

```bash
cd ../Tests
dotnet test
```

---

## **Containerização com Docker**

### **Construa a imagem da API:**

```bash
cd ../Api
docker build -t validador-api .
```

### **Construa a imagem do Razor Pages:**

```bash
cd ../RazorPagesApp
docker build -t razor-pages-app .
```

### **Execute os containers:**

```bash
docker run -d -p 8080:80 --name api-container validador-api
docker run -d -p 8081:80 --name razor-container razor-pages-app
```

### **Usando docker-compose (se configurado):**

```bash
cd ..
docker-compose up -d
```

---

## **Contribuição**

1. Faça um *fork* do repositório.

2. Crie uma branch:

   ```bash
   git checkout -b feature/nova-funcionalidade
   ```

3. Commit:

   ```bash
   git commit -m "Adiciona nova funcionalidade"
   ```

4. Push:

   ```bash
   git push origin feature/nova-funcionalidade
   ```

5. Abra um Pull Request.

---

## **Licença**

**MIT License** – veja o arquivo LICENSE para mais detalhes.

---

## **Autor**
[![GitHub](https://img.shields.io/badge/GitHub-000?style=for-the-badge&logo=github&logoColor=white)](https://github.com/vandersant7)

**Evandro Santos – Desenvolvedor Fullstack .NET/Angular**

---
