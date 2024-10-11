# Instruções para Acesso e Execução do Projeto ProjetoReferencia

Este documento contém as instruções necessárias para clonar, configurar e executar o projeto ProjetoReferencia utilizando Docker.

## Pré-requisitos

- Certifique-se de que você possui o Docker instalado em sua máquina. Você pode baixar o Docker [aqui](https://www.docker.com/get-started).

## Acessando o Projeto

1. **Clone o repositório:**
   Abra o terminal e execute o seguinte comando para clonar o projeto:

   ```bash
   git clone <URL_DO_REPOSITORIO>
   
Substitua <URL_DO_REPOSITORIO> pela URL do seu repositório no Git.

Acesse a pasta raiz do projeto: Navegue até a pasta raiz do projeto onde está localizado o arquivo .sln e o docker-compose.yml:

Subindo o Ambiente
Suba o ambiente utilizando Docker Compose: No terminal, execute o seguinte comando para construir e iniciar os contêineres:

docker-compose up --build

Isso irá construir as imagens do Docker e iniciar os serviços necessários para o projeto.

Verificando o Ambiente
Acesse o RabbitMQ: Após iniciar os contêineres, você pode acessar a interface de gerenciamento do RabbitMQ no seu navegador:

http://localhost:15672

Utilize as credenciais padrão:

Usuário: guest
Senha: guest
Visualizando o Banco de Dados: Para visualizar o banco de dados PostgreSQL, recomendamos o uso do DBeaver. Você pode baixar o DBeaver aqui.

Conecte-se ao banco de dados utilizando as seguintes configurações:
Host: localhost
Porta: 5432
Usuário: postgres
Senha: <SUA_SENHA> (a senha que você definiu ao iniciar o contêiner do PostgreSQL)


A Api poderá ser acessada no link: http://localhost:5000/swagger/index.html


Para ajudar na validação:

## Script para Criar o Banco de Dados e a Tabela Bike

-- Criação do banco de dados
CREATE DATABASE moto_rental;

CREATE TABLE "Bike" (
    "Id" SERIAL PRIMARY KEY,
    "Identifier" VARCHAR(100) NOT NULL,
    "Year" INT NOT NULL,
    "Model" VARCHAR(100) NOT NULL,
    "Plate" VARCHAR(50) NOT NULL
);

INSERT INTO public."Bike"
("Identifier", "Year", "Model", "Plate") VALUES
('ABC123', 2021, 'Modelo X', 'XYZ-1234'),
('DEF456', 2020, 'Modelo Y', 'ABC-5678'),
('GHI789', 2022, 'Modelo Z', 'JKL-9101');


Utilizar o postman:
Exemplos:

Get - Todas as bikes
curl --location 'http://localhost:5000/api/bikes/all'

Post - cadastrar uma nova
curl --location 'http://localhost:5000/api/bikes' \
--header 'Content-Type: application/json' \
--data '{
  "identifier": "60",
  "year": 2024,
  "model": "delta",
  "plate": "DDD1234"
}'


