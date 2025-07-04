USE [Producao]
GO
/****** Object:  Trigger [dbo].[trg_GerarTeste]    Script Date: 30/03/2025 16:08:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER TRIGGER [dbo].[trg_GerarTeste]
ON [dbo].[Produto]
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    -- Gerar código de resultado com distribuição realista
    DECLARE @CodigoResultado CHAR(2);
    DECLARE @Chance INT;
    
    -- Gera um número aleatório entre 0 e 99
    SET @Chance = CAST(RAND() * 100 AS INT);

    -- Define o código de acordo com a probabilidade
    SET @CodigoResultado = 
        CASE 
            WHEN @Chance < 70 THEN '01' -- 70% de chance de ser "01 - Ok"
            WHEN @Chance < 75 THEN '02' -- 5% para "02 - Falha na inspeção visual"
            WHEN @Chance < 80 THEN '03' -- 5% para "03 - Falha na inspeção de resistência"
            WHEN @Chance < 85 THEN '04' -- 5% para "04 - Falha na inspeção de dimensões"
            WHEN @Chance < 90 THEN '05' -- 5% para "05 - Falha na inspeção de estanqueidade"
            ELSE '06' -- 10% para "06 - Desconhecido"
        END;

    -- Inserção dos testes garantindo que não haja duplicação
    INSERT INTO Testes (ID_Produto, Codigo_Resultado, Data_Teste)
    SELECT 
        i.ID_Produto,
        @CodigoResultado,  -- Código de resultado gerado
        GETDATE()  -- Data atual
    FROM inserted i
    WHERE NOT EXISTS (
        SELECT 1 
        FROM Producao.dbo.Testes t 
        WHERE t.ID_Produto = i.ID_Produto
    );
END;