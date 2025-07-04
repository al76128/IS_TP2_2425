USE [Producao]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetProdutos]    Script Date: 30/03/2025 16:02:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Criando o Stored Procedure
CREATE PROCEDURE [dbo].[sp_GetProdutos]
AS
BEGIN
    SET NOCOUNT ON;

    -- Seleciona os produtos ordenados por Data e Hora de Produção
    SELECT 
        ID_Produto,
        Codigo_Peca,
        Data_Producao,
        Hora_Producao,
        Tempo_Producao
    FROM Produto
    ORDER BY Data_Producao DESC, Hora_Producao DESC;
END;
GO
