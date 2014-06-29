﻿








CREATE PROCEDURE [dbo].[uSP_Syn_ED_Empleado_Obtener_X_Atributo]
 /*
 ********************************************************************************************
	Procedimiento	: [uSP_Syn_ED_Empleado_Obtener_X_Atributo]
	Propósito		: Lista de Campos de Empleado y Sus valores
	Inputs/Output	: N/A
	Se asume		: N/A
	Retorno		    : N/A
	Notas			: N/A
	Test			: [uSP_Syn_ED_Empleado_Obtener_X_Atributo] '2012-2013', '480'
	Modificaciones	: 
---------------------------------------------------------------------------------------------
-	Fecha			Inc/Pet 		Responsable 			Descripción Cambio
---------------------------------------------------------------------------------------------
-	[04/06/2014]	[P-001]			[Evercom]				1. Creacion
 ********************************************************************************************
*/
@as_PeriodoAcademico VARCHAR(20),
@as_CodigoEmpleado	VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @as_PeriodoAcademicoDefault varchar(10) = '999999999'   --No se concidera el periodo academic
	
	EXEC FDR_GALEXITO_MAIN.dbo.[uSP_Syn_ED_Empleado_Obtener_X_Atributo] @as_PeriodoAcademicoDefault, @as_CodigoEmpleado
				
	SET NOCOUNT OFF
END








