USE [Producao]
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateProduto]    Script Date: 30/03/2025 16:02:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_UpdateProduto]
    @ID_Produto INT,
    @Codigo_Peca CHAR(8),
    @Data_Producao DATE,
    @Hora_Producao TIME,
    @Tempo_Producao INT
AS
BEGIN
    SET NOCOUNT ON;

    -- 1. Verifica se o produto existe
    IF NOT EXISTS (SELECT 1 FROM Produto WHERE ID_Produto = @ID_Produto)
    BEGIN
        RAISERROR ('Produto não encontrado.', 16, 1);
        RETURN;
    END

    -- 2. Verifica se o Código_Peca já existe em outro produto
    IF EXISTS (SELECT 1 FROM Produto WHERE Codigo_Peca = @Codigo_Peca AND ID_Produto <> @ID_Produto)
    BEGIN
        RAISERROR ('Código de peça já existe em outro produto.', 16, 1);
        RETURN;
    END

    -- 3. Valida o formato do código (deve começar com aa, ab, bb ou ba)
    IF @Codigo_Peca NOT LIKE 'aa%' 
       AND @Codigo_Peca NOT LIKE 'ab%' 
       AND @Codigo_Peca NOT LIKE 'bb%' 
       AND @Codigo_Peca NOT LIKE 'ba%'
    BEGIN
        RAISERROR ('Código de peça inválido. Deve começar com aa, ab, bb ou ba.', 16, 1);
        RETURN;
    END

    -- 4. Valida o tempo de produção (entre 10 e 50)
    IF @Tempo_Producao < 10 OR @Tempo_Producao > 50
    BEGIN
        RAISERROR ('Tempo de produção inválido. Deve estar entre 10 e 50.', 16, 1);
        RETURN;
    END

    -- 5. Verifica se há alterações antes de atualizar
    IF EXISTS (
        SELECT 1 FROM Produto 
        WHERE ID_Produto = @ID_Produto 
        AND Codigo_Peca = @Codigo_Peca
        AND Data_Producao = @Data_Producao
        AND Hora_Producao = @Hora_Producao
        AND Tempo_Producao = @Tempo_Producao
    )
    BEGIN
        RAISERROR ('Nenhuma alteração detectada. O produto já contém esses valores.', 16, 1);
        RETURN;
    END

    -- 6. Atualiza o produto
    UPDATE Produto
    SET Codigo_Peca = @Codigo_Peca,
        Data_Producao = @Data_Producao,
        Hora_Producao = @Hora_Producao,
        Tempo_Producao = @Tempo_Producao
    WHERE ID_Produto = @ID_Produto;

    PRINT 'Produto atualizado com sucesso.';
END;
GO
