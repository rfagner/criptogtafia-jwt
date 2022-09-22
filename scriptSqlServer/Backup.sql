CREATE DATABASE Cripto;
GO

USE Cripto;
GO

CREATE TABLE Usuarios(
	Id INT PRIMARY KEY IDENTITY,
	Nome NVARCHAR(MAX),
	Email NVARCHAR(MAX),
	Senha NVARCHAR(MAX),
);
GO

INSERT INTO Usuarios (Nome, Email, Senha) VALUES 
	('Paulo','paulo@email.com','123456789'),
	('Roberto','roberto@email.com','987654321');
GO