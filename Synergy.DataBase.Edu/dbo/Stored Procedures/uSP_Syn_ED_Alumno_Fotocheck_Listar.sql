




CREATE PROCEDURE [dbo].[uSP_Syn_ED_Alumno_Fotocheck_Listar]  
 /*  
 ********************************************************************************************  
 Procedimiento : [uSP_Syn_ED_Alumno_Fotocheck_Listar]  
 Propósito  : Lista los Datos del Alumno  
 Inputs/Output : N/A  
 Se asume  : N/A  
 Retorno      : N/A  
 Notas   : N/A  
 Test   : [uSP_Syn_ED_Alumno_Fotocheck_Listar] '2012-2013', 'N', 'N', 'EC3', '', '', '0276003   ', 1
 Modificaciones :   
---------------------------------------------------------------------------------------------  
- Fecha   Inc/Pet   Responsable    Descripción Cambio  
---------------------------------------------------------------------------------------------  
- [04/06/2014] [P-001]   [P.V.]    1. Creacion  
 ********************************************************************************************  
*/  
@as_PeriodoAcademico VARCHAR(20) = '',  
@as_Foto    CHAR(1),  
@as_Exportado   CHAR(1),  
@as_Grado    VARCHAR(10) = '',  
@as_Seccion    VARCHAR(10) = '',  
@as_Familia    VARCHAR(10) = '',  
@as_Alumno    VARCHAR(10 ) = '',  
--@as_SelTodo    CHAR(1),
@ai_Pagina    INT  
AS  
BEGIN  
 SET NOCOUNT ON  
  
DECLARE @i_FilasXPagina INT ,@i_TotalFilas INT  
  
SELECT @i_FilasXPagina = 20 --dbo.uFN_GALEXITO_GEN_Maestro_ObtenerTamahnoLista('ED_Familias_X_Apell')  
  
		DECLARE @a_RutaFoto varchar(1000)=''  
		--Obtiene La direccion de red donde esta la imagen    
		SELECT		@a_RutaFoto = DescripcionParametro      
		FROM		FDR_Spring.dbo.ParametrosMast   With(Nolock)  
		WHERE		CompaniaCodigo ='999999' AND  AplicacionCodigo ='ED'  AND  ParametroClave ='ALUMNODIR'     

		--select @a_RutaFoto
		
		CREATE TABLE #Files (subdirectory varchar(20) ,depth int,isfile bit);

		INSERT #Files (subdirectory,depth,isfile)
		EXEC xp_dirtree @a_RutaFoto,1,1;

		--select * from #Files
		--drop table #Files
		
  SELECT DISTINCT    
      ROW_NUMBER() Over (Order by a.Apellidos) As Fila,  
   a.Periodoacademico , 
   a.ProcesadoFlag,  
  (Case When F.subdirectory IS Null Then 'N' Else 'S' End)  As FotoFlag,                        
   a.GradoCodigo ,             
   a.Seccion ,   
   a.alumnogrupo  ,           
   a.Familia ,             
   a.Grado ,             
   a.Rol ,      
   a.Alumnocodigo ,           
   a.Apellidos ,             
   a.Nombres,
   a.codigointerno,              
   a.FechaCreacion            
   INTO #Temp       
   FROM VW_Syn_ED_Alumno_Fotocheck_Rep a With(NoLock) 
		LEFT JOIN #Files F
			ON (F.subdirectory = RTrim(a.codigointerno) + '.bmp' COLLATE DATABASE_DEFAULT AND     
				F.isfile = 1)
   WHERE ( @as_PeriodoAcademico = '' OR a.Periodoacademico = @as_PeriodoAcademico )  
   AND ( @as_Grado =   '' OR   a.GradoCodigo = @as_Grado     )  
   AND ( @as_Seccion = '' OR a.Seccion = @as_Seccion     )  
   AND ( @as_Familia = '' OR a.alumnoGrupo = @as_Familia )  
   AND ( @as_Alumno = '' OR a.AlumnoCodigo = @as_Alumno )  
   AND ( @as_Foto =    '' OR @as_Foto = (Case When F.subdirectory IS Null Then 'N' Else 'S' End))      
   AND ( @as_Exportado =    '' OR a.ProcesadoFlag = @as_Exportado  )  
     
   SELECT @i_TotalFilas = Count(1) FROM #Temp With(NoLock)  
 --Si es 0 extrae todas las filas  
 IF (@ai_Pagina = 0)  
  BEGIN  
   SET @i_FilasXPagina = @i_TotalFilas  
   SET @ai_Pagina = 1  
  END  
    
    
  SELECT Top (@i_FilasXPagina)  
   T.Fila,  
   T.Periodoacademico ,             
   T.ProcesadoFlag,  
   T.FotoFlag, 
   T.GradoCodigo ,             
   T.Seccion ,             
   T.AlumnoCodigo ,            
   T.Familia ,             
   T.Grado ,             
   T.Rol ,             
   T.alumnogrupo,        
   T.Apellidos ,             
   T.Nombres , 
   T.codigointerno,                      
   T.FechaCreacion,  
   @i_FilasXPagina as FilasXPagina,  
   @i_TotalFilas as TotalFilas  
  
 FROM #Temp T With(Nolock)  
 WHERE [Fila] >= (@ai_Pagina - 1)*@i_FilasXPagina + 1   
 ORDER BY [Fila]  
 DROP TABLE #TEMP       
   
   
      
 SET NOCOUNT OFF  
END






