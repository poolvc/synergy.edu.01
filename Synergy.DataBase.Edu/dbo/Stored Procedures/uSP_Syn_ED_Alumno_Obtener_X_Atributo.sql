




CREATE PROCEDURE [dbo].[uSP_Syn_ED_Alumno_Obtener_X_Atributo]
 /*
 ********************************************************************************************
	Procedimiento	: [uSP_Syn_ED_Alumno_Obtener_X_Atributo]
	Propósito		: Lista de Campos y Sus valores
	Inputs/Output	: N/A
	Se asume		: N/A
	Retorno		    : N/A
	Notas			: N/A
	Test			: [uSP_Syn_ED_Alumno_Obtener_X_Atributo] '2012-2013', '0411601'
	Modificaciones	: 
---------------------------------------------------------------------------------------------
-	Fecha			Inc/Pet 		Responsable 			Descripción Cambio
---------------------------------------------------------------------------------------------
-	[04/06/2014]	[P-001]			[Evercom]				1. Creacion
 ********************************************************************************************
*/
@as_PeriodoAcademico VARCHAR(20),
@as_CodigoAlumno	VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON
	
	EXEC [FDR_GALEXITO_MAIN].dbo.[uSP_Syn_ED_Alumno_Obtener_X_Atributo] @as_PeriodoAcademico, @as_CodigoAlumno
				
	SET NOCOUNT OFF
END




