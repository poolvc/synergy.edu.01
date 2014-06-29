


CREATE VIEW[dbo].[VW_Syn_ED_Empleado_Fotocheck_Rep_Atr]
/*
 ********************************************************************************************
	Vista			: [VW_ED_004_Empleado_Fotocheck_Rep_Atr]
	Propósito		: Lista Atributos de la Empleado con sus valores
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
SELECT DISTINCT 
		(case when b.periodoacademico is not null then 'S' else 'N' end) COLLATE DATABASE_DEFAULT AS ProcesadoFlag,
		--CONVERT(VARCHAR(100),(case when RTrim(IsNull(RutaFoto,''))='' then 'No' else 'Si' end))COLLATE DATABASE_DEFAULT  As FotoFlag,
		CONVERT(VARCHAR(100),IsNull(b.RutaFoto,'') ) COLLATE DATABASE_DEFAULT As FotoFlag ,
		CONVERT(VARCHAR(100), a.Periodoacademico) COLLATE DATABASE_DEFAULT As Periodoacademico,
		CONVERT(VARCHAR(100), a.Empleado) COLLATE DATABASE_DEFAULT As Empleado,
		CONVERT(VARCHAR(100), IsNull(a.Apellidos,'')) COLLATE DATABASE_DEFAULT As Apellidos,
		CONVERT(VARCHAR(100), IsNull(a.Nombres,'')) COLLATE DATABASE_DEFAULT As Nombres,
		CONVERT(VARCHAR(100), IsNull(a.Sexo,'')) COLLATE DATABASE_DEFAULT As Sexo,
		CONVERT(VARCHAR(100), IsNull(a.PaisNaciimiento,'')) COLLATE DATABASE_DEFAULT As PaisNaciimiento,
		CONVERT(VARCHAR(100), IsNull(a.DepartamentoNacimiento,'')) COLLATE DATABASE_DEFAULT  As DepartamentoNacimiento,
		CONVERT(VARCHAR(100), IsNull(a.provincianacimiento,'')) COLLATE DATABASE_DEFAULT  As provincianacimiento,
		CONVERT(VARCHAR(100), IsNull(a.DistritoNacimiento,'')) COLLATE DATABASE_DEFAULT  As DistritoNacimiento,
		CONVERT(VARCHAR(100), IsNull(a.fechanacimiento,'')) COLLATE DATABASE_DEFAULT As fechanacimiento,
		CONVERT(VARCHAR(100), IsNull(a.Direccion,'')) COLLATE DATABASE_DEFAULT As Direccion,
		CONVERT(VARCHAR(100), IsNull(a.Departamento,'')) COLLATE DATABASE_DEFAULT As Departamento,
		CONVERT(VARCHAR(100), IsNull(a.Provincia,'')) COLLATE DATABASE_DEFAULT As Provincia,
		CONVERT(VARCHAR(100), IsNull(a.CodigoPostal,'')) COLLATE DATABASE_DEFAULT As CodigoPostal,
		CONVERT(VARCHAR(100), IsNull(a.Telefono,'')) COLLATE DATABASE_DEFAULT As Telefono,
		CONVERT(VARCHAR(100), IsNull(a.Fax,'')) COLLATE DATABASE_DEFAULT As Fax,
		CONVERT(VARCHAR(100), IsNull(a.Ceular,'')) COLLATE DATABASE_DEFAULT As Ceular,
		CONVERT(VARCHAR(100), IsNull(a.TipoDocumento,'')) COLLATE DATABASE_DEFAULT As TipoDocumento,
		CONVERT(VARCHAR(100), IsNull(a.documento,'')) COLLATE DATABASE_DEFAULT As documento,
		CONVERT(VARCHAR(100), IsNull(a.nacionalidad,'')) COLLATE DATABASE_DEFAULT As nacionalidad,
		CONVERT(VARCHAR(100), IsNull(a.CorreoElectronico,'')) COLLATE DATABASE_DEFAULT As CorreoElectronico,
		CONVERT(VARCHAR(100), IsNull(a.GrupoSanguineo,'')) COLLATE DATABASE_DEFAULT As GrupoSanguineo,
		CONVERT(VARCHAR(100), IsNull(a.NombreEmergencia,'')) COLLATE DATABASE_DEFAULT As NombreEmergencia,
		CONVERT(VARCHAR(100), IsNull(a.DireccionEmergencia,'')) COLLATE DATABASE_DEFAULT As DireccionEmergencia,
		CONVERT(VARCHAR(100), IsNull(a.TelefonoEmergencia,'')) COLLATE DATABASE_DEFAULT As TelefonoEmergencia,
		CONVERT(VARCHAR(100), IsNull(a.celularemergencia,'')) COLLATE DATABASE_DEFAULT As celularemergencia,
		CONVERT(VARCHAR(100), IsNull(a.NombreEmergencia2,'')) COLLATE DATABASE_DEFAULT As NombreEmergencia2,
		CONVERT(VARCHAR(100), IsNull(a.DireccionEmergencia2,'')) COLLATE DATABASE_DEFAULT As DireccionEmergencia2,
		CONVERT(VARCHAR(100), IsNull(a.TelefonoEmergencia2,'')) COLLATE DATABASE_DEFAULT As TelefonoEmergencia2,
		CONVERT(VARCHAR(100), IsNull(a.CelularEmergencia2,'')) COLLATE DATABASE_DEFAULT As CelularEmergencia2,
		CONVERT(VARCHAR(100), IsNull(a.CentroCostos,'')) COLLATE DATABASE_DEFAULT As CentroCostos,
		CONVERT(VARCHAR(100), IsNull(a.Proyecto,'')) COLLATE DATABASE_DEFAULT As Proyecto,
		CONVERT(VARCHAR(100), IsNull(a.correointerno,'')) COLLATE DATABASE_DEFAULT As correointerno,
		CONVERT(VARCHAR(100), IsNull(a.anexo,'')) COLLATE DATABASE_DEFAULT As anexo,
		CONVERT(VARCHAR(100), IsNull(a.CodigoUsuario,'')) COLLATE DATABASE_DEFAULT As CodigoUsuario,
		CONVERT(VARCHAR(100), IsNull(a.TipoPlanilla,'')) COLLATE DATABASE_DEFAULT As TipoPlanilla,
		CONVERT(VARCHAR(100), IsNull(a.TipoTrabajador,'')) COLLATE DATABASE_DEFAULT As TipoTrabajador,
		CONVERT(VARCHAR(100), IsNull(a.FechaIngreso,'')) COLLATE DATABASE_DEFAULT As FechaIngreso,
		CONVERT(VARCHAR(100), IsNull(a.fechacese,'')) COLLATE DATABASE_DEFAULT As fechacese,
		CONVERT(VARCHAR(100), IsNull(a.fechareingreso,'')) COLLATE DATABASE_DEFAULT As fechareingreso,
		CONVERT(VARCHAR(100), IsNull(a.TipoContrato,'')) COLLATE DATABASE_DEFAULT As TipoContrato,
		CONVERT(VARCHAR(100), IsNull(a.FechaInicioContrato,'')) COLLATE DATABASE_DEFAULT As FechaInicioContrato,
		CONVERT(VARCHAR(100), IsNull(a.FechaFinContrato,'')) COLLATE DATABASE_DEFAULT As FechaFinContrato,
		CONVERT(VARCHAR(100), IsNull(a.numerocontrato,'')) COLLATE DATABASE_DEFAULT As numerocontrato,
		CONVERT(VARCHAR(100), IsNull(a.NombreCarnet,'')) COLLATE DATABASE_DEFAULT As NombreCarnet,
		CONVERT(VARCHAR(100), IsNull(a.ApellidoCarnet,'')) COLLATE DATABASE_DEFAULT As ApellidoCarnet,
		CONVERT(VARCHAR(100), IsNull(a.PuestoCarnet,'')) COLLATE DATABASE_DEFAULT As PuestoCarnet,
		CONVERT(VARCHAR(100), IsNull(a.tipohorario,'')) COLLATE DATABASE_DEFAULT As tipohorario,
		CONVERT(VARCHAR(100), IsNull(a.Cargo,'')) COLLATE DATABASE_DEFAULT As Cargo,
		CONVERT(VARCHAR(100), IsNull(a.CategoriaFuncional,'')) COLLATE DATABASE_DEFAULT As CategoriaFuncional,
		CONVERT(VARCHAR(100), IsNull(a.flagcentroformacion,'')) COLLATE DATABASE_DEFAULT As flagcentroformacion,
		CONVERT(VARCHAR(100), IsNull(a.Niveleducativo,'')) COLLATE DATABASE_DEFAULT As Niveleducativo,
		CONVERT(VARCHAR(100), IsNull(a.CategoriaOcupacional,'')) COLLATE DATABASE_DEFAULT As CategoriaOcupacional

from VW_Syn_ED_Empleado_Fotocheck a left outer join Staff b
		on a.PeriodoAcademico=b.PeriodoAcademico COLLATE DATABASE_DEFAULT
		and a.empleado=b.empleado 







