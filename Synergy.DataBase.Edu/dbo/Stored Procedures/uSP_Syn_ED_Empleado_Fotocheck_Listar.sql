





CREATE PROCEDURE [dbo].[uSP_Syn_ED_Empleado_Fotocheck_Listar]  
 /*    
 ********************************************************************************************    
 Procedimiento : [uSP_Syn_ED_Empleado_Fotocheck_Listar]    
 Propósito  : Lista los datos de los Empleados     
 Inputs/Output : N/A    
 Se asume  : N/A    
 Retorno      : N/A    
 Notas   : N/A    
 Test   : [uSP_Syn_ED_Empleado_Fotocheck_Listar] '2012-2013', 'N', 'N', 1    
 Modificaciones :     
---------------------------------------------------------------------------------------------    
- Fecha   Inc/Pet   Responsable    Descripción Cambio    
---------------------------------------------------------------------------------------------    
- [04/06/2014] [P-001]   [Evercom]    1. Creacion    
 ********************************************************************************************    
*/    
@as_PeriodoAcademico VARCHAR(20),    
@as_Foto    CHAR(1),    
@as_Exportado   CHAR(1),    
@ai_Pagina    INT    
AS    
BEGIN    
		SET NOCOUNT ON    
    
		DECLARE @i_FilasXPagina INT ,@i_TotalFilas INT        
    	DECLARE @as_PeriodoAcademicoDefault varchar(10) = '999999999'   --No se concidera el periodo academic
  
    
		SELECT @i_FilasXPagina = 20 --dbo.uFN_GALEXITO_GEN_Maestro_ObtenerTamahnoLista('ED_Empleado')     

		DECLARE @a_RutaFoto varchar(1000)=''  
		--Obtiene La direccion de red donde esta la imagen    
		SELECT		@a_RutaFoto = DescripcionParametro      
		FROM		FDR_Spring.dbo.ParametrosMast   With(Nolock)  
		WHERE		CompaniaCodigo ='999999' AND  AplicacionCodigo ='ED'  AND  ParametroClave ='P02DIR'     

		--select @a_RutaFoto
		--SET @a_RutaFoto= 'F:\Pool\job\Evercom\AM\01-FDR\03-EDU\PET-01\Photos\'
		
		CREATE TABLE #Files (subdirectory nvarchar(512) ,depth int,isfile bit);

		INSERT #Files (subdirectory,depth,isfile)
		EXEC xp_dirtree @a_RutaFoto,1,1;

		--select * from #Files
		--drop table #Files
	    
		SELECT  ROW_NUMBER() Over (Order by R.Apellidos) As Fila,         
				R.Periodoacademico ,  
				R.ProcesadoFlag ,  
				(Case When F.subdirectory IS Null Then 'N' Else 'S' End)  As FotoFlag,              
				R.Empleado ,               
				R.Apellidos ,               
				R.Nombres ,               
				R.Sexo ,               
				R.PaisNaciimiento,  
				R.TipoDocumento ,               
				R.Documento,
				R.FechaCreacion                              
	 INTO		#Temp 
	 FROM		VW_Syn_ED_Empleado_Fotocheck_Rep R  With(NoLock)  
		LEFT JOIN #Files F
			ON (F.subdirectory = 'PR-'+ CONVERT(Varchar(20),R.Empleado) + '.bmp' AND
				F.isfile = 1)       
	 WHERE Periodoacademico = @as_PeriodoAcademicoDefault
			--Periodoacademico = @as_PeriodoAcademico     
		AND ( @as_Foto =    '' OR @as_Foto = (Case When F.subdirectory IS Null Then 'N' Else 'S' End))       
		AND ( @as_Exportado =    '' OR R.ProcesadoFlag = @as_Exportado  )      
       
 SELECT @i_TotalFilas = Count(1) FROM #Temp With(NoLock)        
  --Si es 0 extrae todas las filas        
  IF (@ai_Pagina = 0)        
   BEGIN        
    SET @i_FilasXPagina = @i_TotalFilas        
    SET @ai_Pagina = 1        
   END       
    
    
       
   SELECT   Top (@i_FilasXPagina)       
		Fila,      
		Periodoacademico ,  
		ProcesadoFlag ,  
		FotoFlag,              
		Empleado ,               
		Apellidos ,               
		Nombres ,               
		Sexo ,               
		PaisNaciimiento,  
		TipoDocumento ,               
		Documento,
		FechaCreacion,          
		@i_FilasXPagina as FilasXPagina,        
		@i_TotalFilas as TotalFilas     
  FROM #Temp T With(Nolock)        
  WHERE [Fila] >= (@ai_Pagina - 1)*@i_FilasXPagina + 1         
  ORDER BY [Fila]
        
 DROP TABLE #Temp      
    
     
 SET NOCOUNT OFF    
END





