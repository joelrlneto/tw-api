# Projeto de API para gerenciamento interno de companhias aéreas

## Como executar o projeto

**Clonar o repositório**
```
git clone https://github.com/joelrlneto/tw-api.git
```

**Clonar o repositório**
```
dotnet restore
```

**Clonar o repositório**
```
dotnet run
```

## Como testar a API

**Acessar a interface de teste do Swagger***
A UI do Swagger estará disponível na URL https://localhost:7096/swagger (a porta pode variar e deve ser observada no terminal ao executar o projeto).

**Consumir os endpoints**
Sugestão de ordem para testar os endpoints:

Aeronave
- POST (adicionar)
- GET (listar todos)
- PUT (editar)
- GET/id (listar pelo id)
- DELETE (excluir)

Piloto
- POST (adicionar)
- GET (listar todos)
- PUT (editar)
- GET/id (listar pelo id)
- DELETE (excluir)

Manutencao
- POST (adicionar)
- GET (listar todos)
- PUT (editar)
- DELETE (excluir)

Voo
- POST (adicionar)
- GET (listar todos)
- PUT (editar)
- GET /id (listar pelo id)
- POST /cancelar (cancelar um voo)
- GET /id/ficha (imprimir a ficha do voo em PDF) - essa URL deve ser acessada diretamente no browser, pois o Swagger não suporta o tipo de retorno.
- DELETE (excluir)
