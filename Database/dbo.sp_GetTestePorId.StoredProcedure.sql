USE [Producao]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetTestePorId]    Script Date: 30/03/2025 16:02:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_GetTestePorId]
    @ID_Teste INT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Retorna apenas os dados básicos do teste
    SELECT 
        t.ID_Teste,
        t.ID_Produto,
        t.Codigo_Resultado,
        t.Data_Teste
    FROM Producao.dbo.Testes t
    WHERE t.ID_Teste = @ID_Teste;
END;
GO
