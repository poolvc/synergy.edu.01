


CREATE VIEW [dbo].[VW_Syn_ED_Empleado_Fotocheck_Rep]
AS
SELECT DISTINCT a.*,
				ProcesadoFlag=(CASE WHEN b.empleado IS NOT NULL then 'S' else 'N' end),
				FotoFlag = 'N',
				b.FechaCreacion
FROM			VW_Syn_ED_Empleado_Fotocheck a 
				LEFT JOIN Staff b
					ON (a.empleado=b.empleado) 




