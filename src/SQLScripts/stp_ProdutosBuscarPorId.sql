USE [DB_CarrinhoDeCompras]
GO 

CREATE OR ALTER PROCEDURE [dbo].[stp_ProdutosBuscarPorId]
	@Id int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT TOP(1) * FROM Produtos WHERE [Id]=@Id;
END