








CREATE VIEW [dbo].[VW_Syn_ED_Alumno_Fotocheck_Rep]
as
SELECT a.*,
	   ProcesadoFlag = (case when b.periodoacademico is not null then 'S' else 'N' end),
	   ----Agregado-------------------------------------------------------------------
	   --FotoFlag = (case when imgstudents IS NULL then 'N' else 'S' end),
	   FotoFlag = (case when RTrim(IsNull(RutaFoto,''))='' then 'N' else 'S' end),
	   b.FechaCreacion
FROM VW_Syn_ED_Alumno_Fotocheck a 
	left outer join Students b
	on a.PeriodoAcademico=b.PeriodoAcademico COLLATE DATABASE_DEFAULT
		and a.GradoCodigo=b.GradoCodigo COLLATE DATABASE_DEFAULT
		and a.seccion=b.seccion COLLATE DATABASE_DEFAULT
		and a.AlumnoCodigo=b.AlumnoCodigo COLLATE DATABASE_DEFAULT





