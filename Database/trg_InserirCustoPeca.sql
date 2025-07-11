USE [Producao]
GO
/****** Object:  Trigger [dbo].[trg_InserirCustoPeca]    Script Date: 30/03/2025 16:07:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER TRIGGER [dbo].[trg_InserirCustoPeca]
ON [Producao].[dbo].[Produto]
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Contabilidade.dbo.Custos_Peca (ID_Produto, Codigo_Peca, Tempo_Producao, Custo_Producao, Prejuizo, Lucro)
    SELECT 
        i.ID_Produto,
        i.Codigo_Peca,
        i.Tempo_Producao,
        -- Cálculo do Custo de Produção
        CASE 
            WHEN LEFT(i.Codigo_Peca, 2) = 'aa' THEN i.Tempo_Producao * 1.9
            WHEN LEFT(i.Codigo_Peca, 2) = 'ab' THEN i.Tempo_Producao * 1.3
            WHEN LEFT(i.Codigo_Peca, 2) = 'ba' THEN i.Tempo_Producao * 1.7
            WHEN LEFT(i.Codigo_Peca, 2) = 'bb' THEN i.Tempo_Producao * 1.2
            ELSE 0
        END AS Custo_Producao,

        -- Cálculo do Prejuízo
        CASE 
            WHEN LEFT(i.Codigo_Peca, 2) = 'aa' THEN (i.Tempo_Producao * 0.9) + 
                (CASE t.Codigo_Resultado 
                    WHEN '02' THEN 3  -- Falha na inspeção visual
                    WHEN '03' THEN 2  -- Falha na inspeção de resistência
                    WHEN '04' THEN 2.4  -- Falha na inspeção de dimensões
                    WHEN '05' THEN 1.7  -- Falha na inspeção de estanqueidade
                    WHEN '06' THEN 4.5  -- Desconhecido
                    ELSE 0 
                 END)
            WHEN LEFT(i.Codigo_Peca, 2) = 'ab' THEN (i.Tempo_Producao * 1.1) + 
                (CASE t.Codigo_Resultado 
                    WHEN '02' THEN 3 
                    WHEN '03' THEN 2 
                    WHEN '04' THEN 2.4 
                    WHEN '05' THEN 1.7 
                    WHEN '06' THEN 4.5 
                    ELSE 0 
                 END)
            WHEN LEFT(i.Codigo_Peca, 2) = 'ba' THEN (i.Tempo_Producao * 1.2) + 
                (CASE t.Codigo_Resultado 
                    WHEN '02' THEN 3 
                    WHEN '03' THEN 2 
                    WHEN '04' THEN 2.4 
                    WHEN '05' THEN 1.7 
                    WHEN '06' THEN 4.5 
                    ELSE 0 
                 END)
            WHEN LEFT(i.Codigo_Peca, 2) = 'bb' THEN (i.Tempo_Producao * 1.3) + 
                (CASE t.Codigo_Resultado 
                    WHEN '02' THEN 3 
                    WHEN '03' THEN 2 
                    WHEN '04' THEN 2.4 
                    WHEN '05' THEN 1.7 
                    WHEN '06' THEN 4.5 
                    ELSE 0 
                 END)
            ELSE 0
        END AS Prejuizo,

        -- Cálculo do Lucro
        CASE 
            WHEN LEFT(i.Codigo_Peca, 2) = 'aa' THEN 120 - ((i.Tempo_Producao * 1.9) + 
                (i.Tempo_Producao * 0.9) + 
                (CASE t.Codigo_Resultado 
                    WHEN '02' THEN 3 
                    WHEN '03' THEN 2 
                    WHEN '04' THEN 2.4 
                    WHEN '05' THEN 1.7 
                    WHEN '06' THEN 4.5 
                    ELSE 0 
                 END))
            WHEN LEFT(i.Codigo_Peca, 2) = 'ab' THEN 100 - ((i.Tempo_Producao * 1.3) + 
                (i.Tempo_Producao * 1.1) + 
                (CASE t.Codigo_Resultado 
                    WHEN '02' THEN 3 
                    WHEN '03' THEN 2 
                    WHEN '04' THEN 2.4 
                    WHEN '05' THEN 1.7 
                    WHEN '06' THEN 4.5 
                    ELSE 0 
                 END))
            WHEN LEFT(i.Codigo_Peca, 2) = 'ba' THEN 110 - ((i.Tempo_Producao * 1.7) + 
                (i.Tempo_Producao * 1.2) + 
                (CASE t.Codigo_Resultado 
                    WHEN '02' THEN 3 
                    WHEN '03' THEN 2 
                    WHEN '04' THEN 2.4 
                    WHEN '05' THEN 1.7 
                    WHEN '06' THEN 4.5 
                    ELSE 0 
                 END))
            WHEN LEFT(i.Codigo_Peca, 2) = 'bb' THEN 90 - ((i.Tempo_Producao * 1.2) + 
                (i.Tempo_Producao * 1.3) + 
                (CASE t.Codigo_Resultado 
                    WHEN '02' THEN 3 
                    WHEN '03' THEN 2 
                    WHEN '04' THEN 2.4 
                    WHEN '05' THEN 1.7 
                    WHEN '06' THEN 4.5 
                    ELSE 0 
                 END))
            ELSE 0
        END AS Lucro
    FROM inserted i
    LEFT JOIN Producao.dbo.Testes t ON i.ID_Produto = t.ID_Produto;
END;


