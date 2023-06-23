USE [DB_CarrinhoDeCompras]
GO

CREATE OR ALTER PROCEDURE [dbo].[stp_ProdutosCadastrar]
	@Nome NVARCHAR (50),
	@Valor DECIMAL(18,2),
	@Vendido BIT
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO Produtos
	(
		[Nome], 
		[Valor], 
		[Vendido]
	)
	VALUES
	(
		@Nome,
		@Valor,
		@Vendido
	)
	SELECT * FROM Produtos WHERE Id=SCOPE_IDENTITY();
END