USE [DB_CarrinhoDeCompras]
GO 

CREATE OR ALTER PROCEDURE [dbo].[stp_ProdutosAtualizar]
	@Id int,
	@Nome NVARCHAR (50),
	@Valor DECIMAL (18,2),
	@Vendido BIT
AS
BEGIN
	UPDATE Produtos SET [Nome]=@Nome,[Valor]=@Valor, [Vendido]=@Vendido 
		WHERE Id=@Id
END