CREATE DATABASE CadastroDB;

CREATE TABLE Produtos (
	ProdutoId int IDENTITY(1,1) NOT NULL,
	Estoque int NOT NULL,
	Nome varchar(100) NOT NULL,
	Preco decimal(18,2) NOT NULL
)

INSERT INTO Produtos (Nome, Estoque, Preco)
VALUES ('Caneta', 50, 2.50)

SELECT * FROM Produtos;