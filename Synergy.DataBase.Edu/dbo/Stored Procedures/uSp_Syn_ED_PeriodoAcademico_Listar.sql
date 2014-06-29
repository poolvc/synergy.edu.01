



CREATE PROCEDURE [dbo].[uSp_Syn_ED_PeriodoAcademico_Listar]
 /*
 ********************************************************************************************
	Procedimiento	: [uSp_Syn_ED_PeriodoAcademico_Listar]
	Propósito		: Lista los Periodos Academicos
	Inputs/Output	: N/A
	Se asume		: N/A
	Retorno		    : N/A
	Notas			: N/A
	Test			: [uSP_Syn_ED_PeriodoAcademico_Listar]
	Modificaciones	: 
---------------------------------------------------------------------------------------------
-	Fecha			Inc/Pet 		Responsable 			Descripción Cambio
---------------------------------------------------------------------------------------------
-	[04/06/2014]	[P-001]			[P.V.]				1. Creacion
 ********************************************************************************************
*/
AS
BEGIN
	SET NOCOUNT ON

	SELECT	 PeriodoAcademico , 
			 DescripcionLocal               
	FROM	 VW_Syn_ED_PeriodoAcademico With(Nolock)  
	ORDER BY PeriodoAcademico DESC
			
	SET NOCOUNT OFF
END




