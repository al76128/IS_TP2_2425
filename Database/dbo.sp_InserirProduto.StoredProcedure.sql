USE [Producao]
GO
/****** Object:  StoredProcedure [dbo].[sp_InserirProduto]    Script Date: 30/03/2025 16:02:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_InserirProduto]
    @Codigo_Peca CHAR(8),
    @Data_Producao DATE,
    @Hora_Producao TIME,
    @Tempo_Producao INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Verifica se o código da peça tem exatamente 8 caracteres
    IF LEN(@Codigo_Peca) <> 8
    BEGIN
        RAISERROR ('Código da peça deve ter exatamente 8 caracteres.', 16, 1);
        RETURN;
    END

    -- Verifica se os dois primeiros caracteres são válidos (aa, ab, ba, bb)
    IF LEFT(@Codigo_Peca, 2) NOT IN ('aa', 'ab', 'ba', 'bb')
    BEGIN
        RAISERROR ('Os dois primeiros caracteres do código da peça devem ser aa, ab, ba ou bb.', 16, 1);
        RETURN;
    END

    -- Verifica se a peça já existe na base de dados
    IF EXISTS (SELECT 1 FROM Produto WHERE Codigo_Peca = @Codigo_Peca)
    BEGIN
        RAISERROR ('Código de peça já existe.', 16, 1);
        RETURN;
    END

    -- Verifica se o tempo de produção está entre 10 e 50 segundos
    IF @Tempo_Producao < 10 OR @Tempo_Producao > 50
    BEGIN
        RAISERROR ('O tempo de produção deve estar entre 10 e 50 segundos.', 16, 1);
        RETURN;
    END

    -- Insere o novo produto na tabela Produto
    INSERT INTO Produto (Codigo_Peca, Data_Producao, Hora_Producao, Tempo_Producao)
    VALUES (@Codigo_Peca, @Data_Producao, @Hora_Producao, @Tempo_Producao);
END;
GO
