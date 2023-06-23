USE [DB_CarrinhoDeCompras]
GO 

CREATE OR ALTER PROCEDURE [dbo].[stp_ProdutosDeletar]
	@Id int 
AS
BEGIN
	DELETE FROM Produtos WHERE[Id]=@Id
END