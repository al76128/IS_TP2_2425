USE [Producao]
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateTeste]    Script Date: 30/03/2025 16:02:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_UpdateTeste]
    @ID_Teste INT,
    @Codigo_Resultado VARCHAR(2)
AS
BEGIN
    SET NOCOUNT ON;

    -- 1. Verifica se o teste existe
    IF NOT EXISTS (SELECT 1 FROM Testes WHERE ID_Teste = @ID_Teste)
    BEGIN
        RAISERROR ('Teste não encontrado.', 16, 1);
        RETURN;
    END

    -- 2. Valida o formato do código de resultado
    IF LEN(@Codigo_Resultado) <> 2
    BEGIN
        RAISERROR ('Código de resultado inválido. Deve ter exatamente 2 caracteres.', 16, 1);
        RETURN;
    END

    -- 3. Verifica se há alterações
    IF EXISTS (
        SELECT 1 FROM Testes 
        WHERE ID_Teste = @ID_Teste 
        AND Codigo_Resultado = @Codigo_Resultado
    )
    BEGIN
        RAISERROR ('Nenhuma alteração detectada. O teste já contém esses valor.', 16, 1);
        RETURN;
    END

    -- 4. Atualiza o teste
    UPDATE Testes
    SET Codigo_Resultado = @Codigo_Resultado,
        Data_Teste = GETDATE()
    WHERE ID_Teste = @ID_Teste;
END;

GO
