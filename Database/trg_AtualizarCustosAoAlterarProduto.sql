USE [Producao]
GO
/****** Object:  Trigger [dbo].[trg_AtualizarCustosAoAlterarProduto]    Script Date: 30/03/2025 16:08:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER   TRIGGER [dbo].[trg_AtualizarCustosAoAlterarProduto]
ON [dbo].[Produto]
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Primeiro, apaga os registos existentes na tabela Custos_Peca para os produtos atualizados
    DELETE cp
    FROM Contabilidade.dbo.Custos_Peca cp
    JOIN inserted i ON cp.ID_Produto = i.ID_Produto;
    
    -- Depois, insere novos registos com os valores recalculados
    INSERT INTO Contabilidade.dbo.Custos_Peca (ID_Produto, Codigo_Peca, Tempo_Producao, Custo_Producao, Prejuizo, Lucro)
    SELECT 
        p.ID_Produto,
        p.Codigo_Peca,
        p.Tempo_Producao,
        -- Cálculo do Custo de Produção
        CASE 
            WHEN LEFT(p.Codigo_Peca, 2) = 'aa' THEN p.Tempo_Producao * 1.9
            WHEN LEFT(p.Codigo_Peca, 2) = 'ab' THEN p.Tempo_Producao * 1.3
            WHEN LEFT(p.Codigo_Peca, 2) = 'ba' THEN p.Tempo_Producao * 1.7
            WHEN LEFT(p.Codigo_Peca, 2) = 'bb' THEN p.Tempo_Producao * 1.2
            ELSE 0
        END AS Custo_Producao,

        -- Cálculo do Prejuízo (baseado no resultado do teste se existir)
        CASE 
            WHEN LEFT(p.Codigo_Peca, 2) = 'aa' THEN (p.Tempo_Producao * 0.9) + 
                ISNULL((SELECT 
                    CASE t.Codigo_Resultado 
                        WHEN '02' THEN 3  -- Falha na inspeção visual
                        WHEN '03' THEN 2  -- Falha na inspeção de resistência
                        WHEN '04' THEN 2.4  -- Falha na inspeção de dimensões
                        WHEN '05' THEN 1.7  -- Falha na inspeção de estanqueidade
                        WHEN '06' THEN 4.5  -- Desconhecido
                        ELSE 0 
                    END
                FROM Testes t WHERE t.ID_Produto = p.ID_Produto), 0)
            WHEN LEFT(p.Codigo_Peca, 2) = 'ab' THEN (p.Tempo_Producao * 1.1) + 
                ISNULL((SELECT 
                    CASE t.Codigo_Resultado 
                        WHEN '02' THEN 3 
                        WHEN '03' THEN 2 
                        WHEN '04' THEN 2.4 
                        WHEN '05' THEN 1.7 
                        WHEN '06' THEN 4.5 
                        ELSE 0 
                    END
                FROM Testes t WHERE t.ID_Produto = p.ID_Produto), 0)
            WHEN LEFT(p.Codigo_Peca, 2) = 'ba' THEN (p.Tempo_Producao * 1.2) + 
                ISNULL((SELECT 
                    CASE t.Codigo_Resultado 
                        WHEN '02' THEN 3 
                        WHEN '03' THEN 2 
                        WHEN '04' THEN 2.4 
                        WHEN '05' THEN 1.7 
                        WHEN '06' THEN 4.5 
                        ELSE 0 
                    END
                FROM Testes t WHERE t.ID_Produto = p.ID_Produto), 0)
            WHEN LEFT(p.Codigo_Peca, 2) = 'bb' THEN (p.Tempo_Producao * 1.3) + 
                ISNULL((SELECT 
                    CASE t.Codigo_Resultado 
                        WHEN '02' THEN 3 
                        WHEN '03' THEN 2 
                        WHEN '04' THEN 2.4 
                        WHEN '05' THEN 1.7 
                        WHEN '06' THEN 4.5 
                        ELSE 0 
                    END
                FROM Testes t WHERE t.ID_Produto = p.ID_Produto), 0)
            ELSE 0
        END AS Prejuizo,

        -- Cálculo do Lucro
        CASE 
            WHEN LEFT(p.Codigo_Peca, 2) = 'aa' THEN 120 - ((p.Tempo_Producao * 1.9) + 
                (p.Tempo_Producao * 0.9) + 
                ISNULL((SELECT 
                    CASE t.Codigo_Resultado 
                        WHEN '02' THEN 3 
                        WHEN '03' THEN 2 
                        WHEN '04' THEN 2.4 
                        WHEN '05' THEN 1.7 
                        WHEN '06' THEN 4.5 
                        ELSE 0 
                    END
                FROM Testes t WHERE t.ID_Produto = p.ID_Produto), 0))
            WHEN LEFT(p.Codigo_Peca, 2) = 'ab' THEN 100 - ((p.Tempo_Producao * 1.3) + 
                (p.Tempo_Producao * 1.1) + 
                ISNULL((SELECT 
                    CASE t.Codigo_Resultado 
                        WHEN '02' THEN 3 
                        WHEN '03' THEN 2 
                        WHEN '04' THEN 2.4 
                        WHEN '05' THEN 1.7 
                        WHEN '06' THEN 4.5 
                        ELSE 0 
                    END
                FROM Testes t WHERE t.ID_Produto = p.ID_Produto), 0))
            WHEN LEFT(p.Codigo_Peca, 2) = 'ba' THEN 110 - ((p.Tempo_Producao * 1.7) + 
                (p.Tempo_Producao * 1.2) + 
                ISNULL((SELECT 
                    CASE t.Codigo_Resultado 
                        WHEN '02' THEN 3 
                        WHEN '03' THEN 2 
                        WHEN '04' THEN 2.4 
                        WHEN '05' THEN 1.7 
                        WHEN '06' THEN 4.5 
                        ELSE 0 
                    END
                FROM Testes t WHERE t.ID_Produto = p.ID_Produto), 0))
            WHEN LEFT(p.Codigo_Peca, 2) = 'bb' THEN 90 - ((p.Tempo_Producao * 1.2) + 
                (p.Tempo_Producao * 1.3) + 
                ISNULL((SELECT 
                    CASE t.Codigo_Resultado 
                        WHEN '02' THEN 3 
                        WHEN '03' THEN 2 
                        WHEN '04' THEN 2.4 
                        WHEN '05' THEN 1.7 
                        WHEN '06' THEN 4.5 
                        ELSE 0 
                    END
                FROM Testes t WHERE t.ID_Produto = p.ID_Produto), 0))
            ELSE 0
        END AS Lucro
    FROM inserted p;
END;