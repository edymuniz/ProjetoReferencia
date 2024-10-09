# ProjetoReferencia

# README.txt

## Como Executar o Docker Compose

Para executar os serviços do Docker definidos no arquivo `docker-compose.yml`, siga estas etapas:

1. **Certifique-se de que o Docker e o Docker Compose estão instalados.**
   - Você pode verificar a instalação do Docker com o comando:
     ```
     docker --version
     ```
   - Para o Docker Compose, utilize:
     ```
     docker-compose --version
     ```

2. **Navegue até o diretório onde o arquivo `docker-compose.yml` está localizado.**

cd /caminho/para/o/ProjetoReferencia

3. **Execute o Docker Compose.**
Para iniciar os serviços definidos no arquivo `docker-compose.yml`, utilize o comando:

docker-compose up -d

4. **Verifique se os serviços estão em execução.**
Para listar os containers em execução, use:

RabbitMQ ->http://localhost:15672/ 
user: guest
pwd:guest

Com DBeaver: acesse o banco de dados:
Host=localhost;Port=5432;Username=admin;Password=admin123;Database=moto_rental

## Script para Criar o Banco de Dados e a Tabela Bike

-- Criação do banco de dados
CREATE DATABASE moto_rental;

CREATE TABLE Bike (
 Id SERIAL PRIMARY KEY,
 Identifier VARCHAR(100) NOT NULL,
 Year INT NOT NULL,
 Model VARCHAR(100) NOT NULL,
 Plate VARCHAR(50) NOT NULL
);

INSERT INTO Bike (Identifier, Year, Model, Plate) VALUES
('ABC123', 2021, 'Modelo X', 'XYZ-1234'),
('DEF456', 2020, 'Modelo Y', 'ABC-5678'),
('GHI789', 2022, 'Modelo Z', 'JKL-9101');



