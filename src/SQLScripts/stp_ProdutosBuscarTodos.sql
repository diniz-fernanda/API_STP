USE [DB_CarrinhoDeCompras]
GO 

CREATE OR ALTER PROCEDURE [dbo].[stp_ProdutosBuscarTodos]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM Produtos;
END