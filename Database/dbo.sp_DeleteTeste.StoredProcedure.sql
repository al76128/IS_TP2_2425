USE [Producao]
GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteTeste]    Script Date: 30/03/2025 16:02:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_DeleteTeste]
    @ID_Teste INT
AS
BEGIN
    DELETE FROM Producao.dbo.Testes
    WHERE ID_Teste = @ID_Teste;
    
    RETURN @@ROWCOUNT;
END
GO
