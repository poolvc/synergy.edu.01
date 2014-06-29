

CREATE VIEW [dbo].[VW_Syn_ED_Familia_Fotocheck_Rep_Atr]
/*
 ********************************************************************************************
	Vista			: [VW_ED_004_Familia_Fotocheck_Rep_Atr]
	Propósito		: Lista Atributos de la Familias con sus valores
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
as
SELECT 
	   (CASE WHEN b.periodoacademico IS NOT NULL THEN 'S' ELSE 'N' END) COLLATE DATABASE_DEFAULT  As ProcesadoFlag,
		--CONVERT(VARCHAR(100),(case when RTrim(IsNull(RutaFoto,''))='' then 'No' else 'Si' end)) COLLATE DATABASE_DEFAULT  As FotoFlag,
		CONVERT(VARCHAR(100),IsNull(b.RutaFoto,'') ) COLLATE DATABASE_DEFAULT As FotoFlag ,
		CONVERT(VARCHAR(100), a.Periodoacademico) COLLATE DATABASE_DEFAULT  As Periodoacademico,
		CONVERT(VARCHAR(100), a.alumnogrupo) COLLATE DATABASE_DEFAULT  As alumnogrupo,
		CONVERT(VARCHAR(100), a.Familia) COLLATE DATABASE_DEFAULT  As Familia,
		CONVERT(VARCHAR(100), a.Vinculo) COLLATE DATABASE_DEFAULT  As Vinculo,
		CONVERT(VARCHAR(100), IsNull(a.Apellidos,'')) COLLATE DATABASE_DEFAULT  As Apellidos,
		CONVERT(VARCHAR(100), IsNull(a.Nombres,'')) COLLATE DATABASE_DEFAULT  As Nombres,
		CONVERT(VARCHAR(100), IsNull(a.Direccion,'')) COLLATE DATABASE_DEFAULT  As Direccion,
		CONVERT(VARCHAR(100), IsNull(a.ciudad,'')) COLLATE DATABASE_DEFAULT  As ciudad,
		CONVERT(VARCHAR(100), IsNull(a.Pais,'')) COLLATE DATABASE_DEFAULT  As Pais,
		CONVERT(VARCHAR(100), IsNull(a.codigopostal,'')) COLLATE DATABASE_DEFAULT  As codigopostal,
		CONVERT(VARCHAR(100), IsNull(a.emailcasa,'')) COLLATE DATABASE_DEFAULT  As emailcasa,
		CONVERT(VARCHAR(100), IsNull(a.emailoficina,''))  COLLATE DATABASE_DEFAULT  As emailoficina,
		CONVERT(VARCHAR(100), IsNull(a.Tipodocumento,'')) COLLATE DATABASE_DEFAULT  As Tipodocumento,
		CONVERT(VARCHAR(100), IsNull(a.documentoidentidad,'')) COLLATE DATABASE_DEFAULT  As documentoidentidad,
		CONVERT(VARCHAR(100), IsNull(a.fechanacimiento,'')) COLLATE DATABASE_DEFAULT  As fechanacimiento,
		CONVERT(VARCHAR(100), IsNull(a.PaisNacimiento,'')) COLLATE DATABASE_DEFAULT  As PaisNacimiento,
		CONVERT(VARCHAR(100), IsNull(a.Nacionalidad,'')) COLLATE DATABASE_DEFAULT  As Nacionalidad,
		CONVERT(VARCHAR(100), IsNull(a.Idioma,'')) COLLATE DATABASE_DEFAULT  As Idioma,
		CONVERT(VARCHAR(100), IsNull(a.GradoInstruccion,'')) COLLATE DATABASE_DEFAULT  As GradoInstruccion,
		CONVERT(VARCHAR(100), IsNull(a.telefono01,'')) COLLATE DATABASE_DEFAULT  As telefono01,
		CONVERT(VARCHAR(100), IsNull(a.telefono02,'')) COLLATE DATABASE_DEFAULT  As telefono02,
		CONVERT(VARCHAR(100), IsNull(a.telefonocelular,'')) COLLATE DATABASE_DEFAULT  As telefonocelular,
		CONVERT(VARCHAR(100), IsNull(a.fax,'')) COLLATE DATABASE_DEFAULT  As fax,
		CONVERT(VARCHAR(100), IsNull(a.IdiomaSecundario,'')) COLLATE DATABASE_DEFAULT  As IdiomaSecundario,
		CONVERT(VARCHAR(100), IsNull(a.Grupo,'')) COLLATE DATABASE_DEFAULT  As Grupo		
FROM	VW_Syn_ED_familia_Fotocheck a left outer join Family b
		on a.PeriodoAcademico=b.PeriodoAcademico COLLATE DATABASE_DEFAULT 
		and a.documentoidentidad=b.documentoidentidad COLLATE DATABASE_DEFAULT 
		and a.Vinculo = b.Vinculo COLLATE DATABASE_DEFAULT










