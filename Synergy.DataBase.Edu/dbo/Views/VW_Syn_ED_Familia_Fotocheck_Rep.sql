


CREATE VIEW [dbo].[VW_Syn_ED_Familia_Fotocheck_Rep]
as
SELECT  a.*,
		ProcesadoFlag=(case when b.periodoacademico is not null then 'S' else 'N' end),
		FotoFlag = 'N',
		b.FechaCreacion
FROM	VW_Syn_ED_familia_Fotocheck a 
			left join Family b
							on (  a.AlumnoGrupo = b.AlumnoGrupo COLLATE DATABASE_DEFAULT and	
								  a.documentoidentidad=b.documentoidentidad COLLATE DATABASE_DEFAULT AND
								  a.Vinculo = b.Vinculo COLLATE DATABASE_DEFAULT )




