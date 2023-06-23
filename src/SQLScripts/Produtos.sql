CREATE TABLE [dbo].[Produtos] (
	[Id] [int] IDENTITY (1,1) NOT NULL,
	[Nome] [NVARCHAR] (50) NOT NULL,
	[Valor] [DECIMAL] (18,2) NOT NULL,
	[Vendido] [BIT] NOT NULL,
CONSTRAINT PK_Produtos PRIMARY KEY CLUSTERED 
(
	Id ASC 
)
) 


