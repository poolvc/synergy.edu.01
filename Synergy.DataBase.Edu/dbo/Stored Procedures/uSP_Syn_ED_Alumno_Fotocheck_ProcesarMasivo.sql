


CREATE PROCEDURE [dbo].[uSP_Syn_ED_Alumno_Fotocheck_ProcesarMasivo]    
@as_PeriodoAcademico VARCHAR(20) = '',      
@as_Foto    CHAR(1),      
@as_Exportado   CHAR(1),      
@as_Grado    VARCHAR(10) = '',      
@as_Seccion    VARCHAR(10) = '',      
@as_Familia    VARCHAR(10) = '',      
@as_AlumnoCodigo VARCHAR(max), 
@as_UsuarioCreacion VARCHAR(10) =  NULL       
AS      
      
BEGIN TRY      
      
	CREATE TABLE #Parametro (AlumnoCodigo CHAR(10))   
	CREATE TABLE #Students (AlumnoCodigo CHAR(10)) 
    
	DECLARE @Posicion int        
	DECLARE @Parametro varchar(1000)        
	SET @as_Alumnocodigo = @as_Alumnocodigo + '|'        
       
   WHILE patindex('%|%' , @as_Alumnocodigo) <> 0        
    BEGIN        
      SELECT @Posicion =  patindex('%|%' , @as_Alumnocodigo)        
      SELECT @Parametro = left(@as_Alumnocodigo, @Posicion - 1)        
      INSERT INTO #Parametro values (@Parametro)        
      SELECT @as_Alumnocodigo = stuff(@as_Alumnocodigo, 1, @Posicion, '')        
    END     
         

		  --Obtiene La direccion de red donde esta la imagen
		DECLARE @a_RutaFoto varchar(200) = '' 
		      
		SELECT	@a_RutaFoto = DescripcionParametro      
		FROM		FDR_Spring.dbo.ParametrosMast   With(Nolock)  
		WHERE		CompaniaCodigo ='999999' AND  AplicacionCodigo ='ED'  AND  ParametroClave ='ALUMNODIR'     

		CREATE TABLE #Files (subdirectory varchar(20) ,depth int,isfile bit);

		INSERT #Files (subdirectory,depth,isfile)
		EXEC xp_dirtree @a_RutaFoto,1,1;
		
		INSERT INTO #Students (Alumnocodigo)
		SELECT	Alumnocodigo COLLATE DATABASE_DEFAULT
		FROM VW_Syn_ED_Alumno_Fotocheck_Rep a With(NoLock)   
			LEFT JOIN #Files F
				ON (F.subdirectory = RTrim(a.codigointerno) + '.bmp' COLLATE DATABASE_DEFAULT AND     
					F.isfile = 1)    
		WHERE (a.Periodoacademico = @as_PeriodoAcademico     )      
		AND ( @as_Grado =   '' OR a.GradoCodigo = @as_Grado     )      
		AND ( @as_Seccion = '' OR a.Seccion = @as_Seccion     )      
		AND ( @as_Familia = '' OR a.alumnoGrupo = @as_Familia )           
		AND ( @as_Foto =    '' OR @as_Foto = (Case When F.subdirectory IS Null Then 'N' Else 'S' End))           
		AND NOT EXISTS (SELECT 1 FROM #Parametro P
						WHERE  P.AlumnoCodigo = a.AlumnoCodigo COLLATE DATABASE_DEFAULT )

		--select * from #Parametro
		--select * from #Students
		
		UPDATE Students 
		SET		CheckReference = 0
		WHERE PeriodoAcademico = @as_PeriodoAcademico  
				AND CheckReference = 1 
				AND EXISTS(SELECT 1 FROM #Students  S WHERE Students.AlumnoCodigo = S.AlumnoCodigo COLLATE DATABASE_DEFAULT)
	     
		INSERT INTO Students ( Periodoacademico, GradoCodigo,   Seccion,     Alumnogrupo,   Familia,       
		 Grado,     rol,     Alumnocodigo,    Apellidos,   Nombres,       
		 fechanacimiento,  fechaingreso,  lugarnacimiento,  Programa,    Sexo,       
		 Nacionalidad,  Idioma,    IdiomaSecundario,   Seguromedico,  SeguroMedicoPoliza,       
		 SeguroMedicoFechaExpiracion,   EmergenciaContacto,  EmergenciaTelefono, EmergenciaContacto2,       
		 EmergenciaTelefono2, CodigoInterno,CheckReference,
		 FechaCreacion, UsuarioCreacion)     
	   SELECT    
			   a.Periodoacademico ,                 
			   a.GradoCodigo ,                 
			   a.Seccion ,       
			   a.alumnogrupo  ,               
			   a.Familia ,                 
			   a.Grado ,                 
			   a.Rol ,          
			   a.Alumnocodigo ,               
			   a.Apellidos ,                 
			   a.Nombres ,                 
			   a.Fechanacimiento ,                 
			   a.Fechaingreso ,                 
			   a.Lugarnacimiento ,                 
			   a.Programa ,                 
			   a.Sexo ,                 
			   a.Nacionalidad ,                 
			   a.Idioma ,                 
			   a.IdiomaSecundario ,                           
			   a.Seguromedico ,                 
			   a.SeguroMedicoPoliza ,                 
			   a.SeguroMedicoFechaExpiracion ,                 
			   a.EmergenciaContacto ,                 
			   a.EmergenciaTelefono ,                 
			   a.EmergenciaContacto2 ,                 
			   a.EmergenciaTelefono2 ,                 
			   IsNull(a.CodigoInterno,''),    
			   1,  
			   GETDATE(),
			   @as_UsuarioCreacion    
	   FROM VW_Syn_ED_Alumno_Fotocheck_Rep a With(NoLock)       
	   WHERE a.Periodoacademico = @as_PeriodoAcademico     
			 AND EXISTS(SELECT 1 FROM #Students  S WHERE a.AlumnoCodigo = S.AlumnoCodigo COLLATE DATABASE_DEFAULT)
	    
	    
	  UPDATE a 
	  SET a.imgstudents=b.imgstudents from Students a  With(Nolock)   
			INNER JOIN Students b With(Nolock)
				on (a.PeriodoAcademico = b.PeriodoAcademico COLLATE DATABASE_DEFAULT and 
					a.alumnocodigo=b.alumnocodigo COLLATE DATABASE_DEFAULT and 
					b.checkreference=0 )    
	  WHERE a.PeriodoAcademico = @as_PeriodoAcademico  		
			  AND a.checkreference=1 
			  AND EXISTS(SELECT 1 FROM #Students  S WHERE A.AlumnoCodigo = S.AlumnoCodigo COLLATE DATABASE_DEFAULT)

	  DELETE FROM Students 
	  WHERE		PeriodoAcademico = @as_PeriodoAcademico 
				AND CheckReference=0 
				AND EXISTS(SELECT 1 FROM #Students  S WHERE Students.AlumnoCodigo = S.AlumnoCodigo COLLATE DATABASE_DEFAULT)

				      
		UPDATE Students 
		SET		--RutaFoto = RTrim(@a_RutaFoto) + rtrim(codigointerno)+'.bmp',  
				RutaFoto = (CASE  WHEN dbo.uFN_Syn_ED_ExisteArchivo(Rtrim(@a_RutaFoto) + RTRIM(codigointerno)+'.bmp') = 1 THEN Rtrim(@a_RutaFoto) + RTRIM(codigointerno)+'.bmp' ELSE '' END),       
				FechanacimientoString = left(convert(char,fechanacimiento,113),12),    
				CodigoInterno = rtrim(codigointerno)    
		WHERE PeriodoAcademico = @as_PeriodoAcademico COLLATE DATABASE_DEFAULT 
				AND CheckReference=1 
				AND EXISTS(SELECT 1 FROM #Students  S WHERE Students.AlumnoCodigo = S.AlumnoCodigo COLLATE DATABASE_DEFAULT)     
	     
    
		DECLARE @i_Cantidad INT = 0

		SET @i_Cantidad = (SELECT  COUNT(1)  FROM #Students)	    

		SELECT    
				CONVERT(VARCHAR(10),@i_Cantidad) AS AlumnoCodigo,    
				0 AS CodigoError,    
				'' AS  MensajeError          
	      
	END TRY      
      
      
      
BEGIN CATCH      

	SELECT		'' AS Codigo,    
				ERROR_NUMBER() AS CodigoError,      
				ERROR_MESSAGE() AS  MensajeError      
      
END CATCH





