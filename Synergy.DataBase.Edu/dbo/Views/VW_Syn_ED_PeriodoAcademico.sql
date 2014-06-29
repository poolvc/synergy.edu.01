
CREATE VIEW  [dbo].[VW_Syn_ED_PeriodoAcademico]
as
SELECT  P.PeriodoAcademico ,           
		P.DescripcionLocal     
FROM	FDR_Spring.dbo.ED_PeriodoAcademico P    
WHERE  (P.Estado = 'A' )

