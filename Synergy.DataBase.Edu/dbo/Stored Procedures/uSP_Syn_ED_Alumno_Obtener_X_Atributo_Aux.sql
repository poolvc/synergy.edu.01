




CREATE PROCEDURE [dbo].[uSP_Syn_ED_Alumno_Obtener_X_Atributo_Aux]
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

	
	DECLARE @s_sql Varchar(4000)
	DECLARE @s_sql_columnas Varchar(4000) = ''
	DECLARE @s_sql_columnas_sin_principal Varchar(4000) = ''
	DECLARE @s_Parametro VARCHAR(20) = 'COLUMNASALUMNO'
	
	SELECT	@s_sql_columnas = @s_sql_columnas + '' + Descripcion  + ','   
--			ValorTexto1 
	FROM	GENV_ParametroDetalle
	WHERE	IdCompania = 1 AND IdAplicacion = 39 AND Parametro = @s_Parametro AND Estado = 'A'
	ORDER BY ParametroDetalle
	
	SET @s_sql_columnas = LEFT(@s_sql_columnas,LEN(@s_sql_columnas)-1)
	SET @s_sql_columnas_sin_principal = SUBSTRING(@s_sql_columnas, CHARINDEX(',',@s_sql_columnas, 1)+1, 3000)
	
	--select @s_sql_columnas, @s_sql_columnas_sin_principal,  CHARINDEX(',',@s_sql_columnas, 1), @s_sql_columna_inicial

	--Unpivot la tabla.
	SET @s_sql ='SELECT  ROW_NUMBER() Over (Order by PD.Parametrodetalle) As Fila,
						Columna , PD.ValorTexto1 As Descripcion ,Valor FROM
						 (
							SELECT Columna , Valor 
							FROM 
							   (SELECT ' +  @s_sql_columnas + '
								FROM VW_Syn_ED_Alumno_Fotocheck_Rep_Atr WHERE PeriodoAcademico = ''' + @as_PeriodoAcademico + ''' AND AlumnoCodigo = ''' + @as_CodigoAlumno + ''') P
							UNPIVOT
							   ( Valor FOR Columna IN 
								  ('+ @s_sql_columnas_sin_principal + ')
							)AS unpvt 
						) As UP		
					LEFT JOIN GENV_ParametroDetalle PD			
						ON (PD.IdCompania = 1 AND PD.IdAplicacion = 39 AND PD.Parametro = ''' + @s_Parametro + ''' AND Estado ='''+ 'A' +'''
						    AND UP.Columna = PD.Descripcion)		    
			  '
	PRINT @s_sql
	EXEC( @s_sql)
				
	SET NOCOUNT OFF
END





