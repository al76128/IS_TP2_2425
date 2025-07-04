USE [Producao]
GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteProduto]    Script Date: 30/03/2025 16:02:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_DeleteProduto]
    @ID_Produto INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Verifica se o produto existe
    IF NOT EXISTS (SELECT 1 FROM Produto WHERE ID_Produto = @ID_Produto)
    BEGIN
        RAISERROR ('Produto não encontrado.', 16, 1);
        RETURN;
    END

    -- Verifica se o produto tem dependências em outras tabelas (exemplo: Custos_Peca, Testes)
    IF EXISTS (SELECT 1 FROM Testes WHERE ID_Produto = @ID_Produto) 
       --OR EXISTS (SELECT 1 FROM Custos_Peca WHERE ID_Produto = @ID_Produto) 
    BEGIN
        RAISERROR ('Produto não pode ser excluído, pois possui registos dependentes.', 16, 1);
        RETURN;
    END

    -- Exclui o produto
    DELETE FROM Produto WHERE ID_Produto = @ID_Produto;

    PRINT 'Produto eliminado com sucesso.';
END;
GO
