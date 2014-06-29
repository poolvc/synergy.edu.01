








CREATE VIEW [dbo].[VW_Syn_ED_Alumno_Fotocheck_Rep_Atr]
/*
 ********************************************************************************************
	Vista			: [VW_ED_004_Alumno_Fotocheck_Rep_Atr]
	Propósito		: Lista Atributos de la Alumno con sus valores
					  Si se deaea agregar mas campos se debe agregar ala Vista. Adiconalmente ingresar en detalle de 
					  parametro 
	Inputs/Output	: N/A
	Se asume		: N/A
	Retorno		    : N/A
	Notas			: N/A
	Modificaciones	: 
---------------------------------------------------------------------------------------------
-	Fecha			Inc/Pet 		Responsable 			Descripción Cambio
---------------------------------------------------------------------------------------------
-	[15/02/2012]	[0.00]			[]				1. Creacion
 ********************************************************************************************
*/
AS
	SELECT	(CASE WHEN b.periodoacademico IS NOT NULL THEN 'S' ELSE 'N' END) COLLATE DATABASE_DEFAULT As ProcesadoFlag ,
			CONVERT(VARCHAR(100),IsNull(b.RutaFoto,'') ) COLLATE DATABASE_DEFAULT As FotoFlag ,
			
			CONVERT(VARCHAR(100), A.PeriodoAcademico) COLLATE DATABASE_DEFAULT As PeriodoAcademico,
			CONVERT(VARCHAR(100), IsNull(A.GradoCodigo,'')) COLLATE DATABASE_DEFAULT As GradoCodigo,
			CONVERT(VARCHAR(100), IsNull(A.Seccion,'')) COLLATE DATABASE_DEFAULT As Seccion,
			CONVERT(VARCHAR(100), IsNull(A.alumnogrupo,'')) COLLATE DATABASE_DEFAULT As alumnogrupo,
			CONVERT(VARCHAR(100), IsNull(A.Grado,'')) COLLATE DATABASE_DEFAULT As Grado,
			CONVERT(VARCHAR(100), IsNull(A.Rol,'')) COLLATE DATABASE_DEFAULT As Rol,
			CONVERT(VARCHAR(100), A.Alumnocodigo) COLLATE DATABASE_DEFAULT As Alumnocodigo,
			CONVERT(VARCHAR(100), IsNull(A.Apellidos,'')) COLLATE DATABASE_DEFAULT As Apellidos,
			CONVERT(VARCHAR(100), IsNull(A.Nombres,'')) COLLATE DATABASE_DEFAULT As Nombres,
			CONVERT(VARCHAR(100), IsNull(A.Familia,'')) COLLATE DATABASE_DEFAULT As Familia,
			CONVERT(VARCHAR(100), IsNull(A.Fechanacimiento,'')) COLLATE DATABASE_DEFAULT As Fechanacimiento,
			CONVERT(VARCHAR(100), IsNull(A.Fechaingreso,'')) COLLATE DATABASE_DEFAULT As Fechaingreso,
			CONVERT(VARCHAR(100), IsNull(A.Lugarnacimiento,'')) COLLATE DATABASE_DEFAULT As  Lugarnacimiento ,
			CONVERT(VARCHAR(100), IsNull(A.Programa,'')) COLLATE DATABASE_DEFAULT As Programa ,
			CONVERT(VARCHAR(100), IsNull(A.Sexo,'')) COLLATE DATABASE_DEFAULT As Sexo ,
			CONVERT(VARCHAR(100), IsNull(A.Nacionalidad,'')) COLLATE DATABASE_DEFAULT As Nacionalidad,
			CONVERT(VARCHAR(100), IsNull(A.Idioma,'')) COLLATE DATABASE_DEFAULT As Idioma,
			CONVERT(VARCHAR(100), IsNull(A.IdiomaSecundario,'')) COLLATE DATABASE_DEFAULT As IdiomaSecundario,
			CONVERT(VARCHAR(100), IsNull(A.Email,'')) COLLATE DATABASE_DEFAULT As Email,
			CONVERT(VARCHAR(100), IsNull(A.Seguromedico,'')) COLLATE DATABASE_DEFAULT As Seguromedico,
			CONVERT(VARCHAR(100), IsNull(A.SeguroMedicoPoliza,'')) COLLATE DATABASE_DEFAULT As SeguroMedicoPoliza,
			CONVERT(VARCHAR(100), IsNull(A.SeguroMedicoFechaExpiracion,'')) COLLATE DATABASE_DEFAULT As SeguroMedicoFechaExpiracion ,
			CONVERT(VARCHAR(100), IsNull(A.EmergenciaContacto,'')) COLLATE DATABASE_DEFAULT As EmergenciaContacto,
			CONVERT(VARCHAR(100), IsNull(A.EmergenciaTelefono,'') ) COLLATE DATABASE_DEFAULT As EmergenciaTelefono,
			CONVERT(VARCHAR(100), IsNull(A.EmergenciaContacto2,'')) COLLATE DATABASE_DEFAULT As EmergenciaContacto2,
			CONVERT(VARCHAR(100), IsNull(A.EmergenciaTelefono2,'')) COLLATE DATABASE_DEFAULT As EmergenciaTelefono2,
			CONVERT(VARCHAR(100), IsNull(A.CodigoInterno,'')) COLLATE DATABASE_DEFAULT As CodigoInterno			
		    ----Agregar mas campos si se desea---
			
	FROM VW_Syn_ED_Alumno_Fotocheck a 
		LEFT OUTER JOIN Students b
	on a.PeriodoAcademico=b.PeriodoAcademico COLLATE DATABASE_DEFAULT
		and a.GradoCodigo=b.GradoCodigo COLLATE DATABASE_DEFAULT
		and a.seccion=b.seccion COLLATE DATABASE_DEFAULT
		and a.AlumnoCodigo=b.AlumnoCodigo COLLATE DATABASE_DEFAULT






