






CREATE PROCEDURE [dbo].[uSP_Syn_ED_Alumno_Listar_X_Campo]
 /*
 ********************************************************************************************
	Procedimiento	: [uSP_Syn_ED_Alumno_Listar_X_Campo]
	Propósito		: Lista los datos de la familia 
	Inputs/Output	: N/A
	Se asume		: N/A
	Retorno		    : N/A
	Notas			: N/A
	Test			: [uSP_Syn_ED_Alumno_Listar_X_Campo] 'ApellidoPaterno', 'ABUS',1
	Modificaciones	: 
---------------------------------------------------------------------------------------------
-	Fecha			Inc/Pet 		Responsable 			Descripción Cambio
---------------------------------------------------------------------------------------------
-	[04/06/2014]	[P-001]			[Evercom]				1. Creacion
 ********************************************************************************************
*/
@as_Columna VARCHAR(20),
@as_Valor	VARCHAR(20),
@ai_Pagina	INT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @i_FilasXPagina Int
	DECLARE @i_TotalFilas	Int
	DECLARE @s_sql Varchar(1000)
	
	SET @as_Valor = RTRIM(@as_Valor)		
	CREATE TABLE #Temp(
			[Fila] [int],
			[AlumnoCodigo]  varchar(10),
			[AlumnoGrupo]  varchar(10),
			[ApellidoPaterno] varchar(20), 
			[ApellidoMaterno] varchar(20),
			[Nombre01]	varchar(20),
			[Sexo]	char(1),
			[Estado] char(1))
			
	SELECT @i_FilasXPagina = 10 --dbo.uFN_GALEXITO_GEN_Maestro_ObtenerTamahnoLista('ED_Alumnos_X_Apell')
	
	SET @s_sql ='	
					INSERT INTO #Temp (
							[Fila],
							[AlumnoCodigo],
							[AlumnoGrupo],
							[ApellidoPaterno], 
							[ApellidoMaterno],
							[Nombre01],
							[Sexo],
							[Estado])				
					SELECT  ROW_NUMBER() Over (Order by '+ @as_Columna +') As Fila,
							AlumnoCodigo ,           
							AlumnoGrupo ,           
							ApellidoPaterno ,           
							ApellidoMaterno ,           
							Nombre01 ,           
							Sexo,
							Estado    
					FROM	FDR_Spring.dbo.ED_Alumno       WITH (NOLOCK) 
					WHERE	IsNull(' + @as_Columna + ',' + ''' ''' + ') LIKE ''' + @as_Valor  + ''' + ''%'''

	EXEC(@s_sql)
			
	SELECT @i_TotalFilas = Count(1) FROM #Temp With(NoLock)
	--Si es 0 extrae todas las filas
	IF (@ai_Pagina = 0)
		BEGIN
			SET @i_FilasXPagina = @i_TotalFilas
			SET @ai_Pagina = 1
		END

	SELECT	Top (@i_FilasXPagina)
			T.[Fila],
			AlumnoCodigo ,           
			AlumnoGrupo ,           
			ApellidoPaterno ,           
			ApellidoMaterno ,           
			Nombre01 ,           
			Sexo, 
			Estado,    
			@i_FilasXPagina as FilasXPagina,
			@i_TotalFilas as TotalFilas

	FROM	#Temp T With(Nolock) 			
	WHERE	[Fila] >= (@ai_Pagina - 1)*@i_FilasXPagina + 1	

	DROP TABLE #TEMP		   
				
	SET NOCOUNT OFF
END







