



CREATE PROCEDURE [dbo].[uSp_Syn_ED_Grado_Listar]
 /*
 ********************************************************************************************
	Procedimiento	: [uSp_Syn_ED_PeriodoAcademico_Listar]
	Propósito		: Lista los Periodos Academicos
	Inputs/Output	: N/A
	Se asume		: N/A
	Retorno		    : N/A
	Notas			: N/A
	Test			: [uSp_Syn_ED_Grado_Listar]
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

	SELECT	GradoCodigo , 
			CONVERT(CHAR(6),GradoCodigo) + ' -  ' + DescripcionLocal As Descripcion              
	FROM	FDR_Spring.dbo.ED_Grado With(Nolock) 
	WHERE   Estado = 'A'   
			
	SET NOCOUNT OFF
END




