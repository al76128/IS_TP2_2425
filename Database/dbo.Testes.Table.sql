USE [Producao]
GO
/****** Object:  Table [dbo].[Testes]    Script Date: 30/03/2025 16:02:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Testes](
	[ID_Teste] [int] IDENTITY(1,1) NOT NULL,
	[ID_Produto] [int] NULL,
	[Codigo_Resultado] [char](2) NOT NULL,
	[Data_Teste] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID_Teste] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Testes]  WITH CHECK ADD FOREIGN KEY([ID_Produto])
REFERENCES [dbo].[Produto] ([ID_Produto])
GO
