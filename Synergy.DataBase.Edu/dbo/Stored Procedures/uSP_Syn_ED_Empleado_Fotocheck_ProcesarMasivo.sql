



CREATE PROCEDURE [dbo].[uSP_Syn_ED_Empleado_Fotocheck_ProcesarMasivo]  
@as_PeriodoAcademico VARCHAR(20) = '',      
@as_Foto    CHAR(1),      
@as_Exportado   CHAR(1),  
@as_Codigo VARCHAR(max), 
@adt_FechaCreacion DATETIME = NULL,
@as_UsuarioCreacion VARCHAR(10) =  NULL        
AS      
BEGIN      
	BEGIN TRY      
	      
		CREATE TABLE #Parametro (empleado INT)    
	    CREATE TABLE #Staff (empleado INT) 

	    DECLARE @as_PeriodoAcademicoDefault varchar(10) = '999999999'   --No se concidera el periodo academic
	    
		DECLARE @Posicion int        
		DECLARE @Parametro varchar(1000)        
		SET @as_Codigo = @as_Codigo + '|'        
	       
		WHILE patindex('%|%' , @as_Codigo) <> 0        
			BEGIN        
				SELECT @Posicion =  patindex('%|%' , @as_Codigo)        
				SELECT @Parametro = left(@as_Codigo, @Posicion - 1)        
				INSERT INTO #Parametro values (CONVERT(INT,@Parametro))        
				SELECT @as_Codigo =  stuff(@as_Codigo, 1, @Posicion, '')        
			END     
		
			---Obtiene La direccion de red donde esta la imagen    
			DECLARE @a_RutaFoto varchar(200) = ''    
		
  			SELECT		@a_RutaFoto = DescripcionParametro      
			FROM		FDR_Spring.dbo.ParametrosMast   With(Nolock)  
			WHERE		CompaniaCodigo ='999999' AND  AplicacionCodigo ='ED'  AND  ParametroClave ='P02DIR'     

			CREATE TABLE #Files (subdirectory varchar(100) ,depth int,isfile bit);

			INSERT #Files (subdirectory,depth,isfile)
			EXEC xp_dirtree @a_RutaFoto,1,1;


		 --SELECT * FROM #Parametro
		 INSERT INTO #Staff (empleado)
		 SELECT empleado    
		 FROM	VW_Syn_ED_Empleado_Fotocheck_Rep With(NoLock)  
			LEFT JOIN #Files F
					ON (F.subdirectory = 'PR-'+ CONVERT(Varchar(20),Empleado) + '.bmp' AND    
						F.isfile = 1)        
		 WHERE	Periodoacademico = @as_PeriodoAcademicoDefault  
				AND ( @as_Exportado =    '' OR ProcesadoFlag = @as_Exportado ) 
				AND ( @as_Foto =    '' OR @as_Foto = (Case When F.subdirectory IS Null Then 'N' Else 'S' End))                
				AND NOT EXISTS (SELECT 1 FROM #Parametro P
							    WHERE  P.empleado = VW_Syn_ED_Empleado_Fotocheck_Rep.empleado)  
		
		--SELECT * FROM #Staff
		
		UPDATE	Staff 
		SET		CheckReference = 0
		WHERE	PeriodoAcademico = @as_PeriodoAcademicoDefault  
				AND CheckReference = 1          
				AND EXISTS(SELECT 1 FROM #Staff  S WHERE Staff.empleado = S.empleado)
 
		INSERT INTO Staff ( Periodoacademico,  
			 empleado,   Apellidos,    Nombres,    
			 Sexo,     PaisNaciimiento, DepartamentoNacimiento, provincianacimiento,   
			 DistritoNacimiento,  fechanacimiento, Direccion,    Departamento,   
			 Provincia,    CodigoPostal,  Telefono,    Ceular,   
			 TipoDocumento,   documento,   nacionalidad,   CorreoElectronico,   
			 NombreEmergencia,  TelefonoEmergencia, celularemergencia,  NombreEmergencia2,   
			 TelefonoEmergencia2, CelularEmergencia2, CentroCostos,   Proyecto,   
			 correointerno,   anexo,    CodigoUsuario,   TipoPlanilla,   
			 TipoTrabajador,   FechaIngreso,  TipoContrato,   FechaInicioContrato,   
			 NombreCarnet,   ApellidoCarnet,  PuestoCarnet,   tipohorario,   
			 Cargo,     CategoriaFuncional, flagcentroformacion, Niveleducativo,   
			 CategoriaOcupacional,CheckReference,
			 FechaCreacion, UsuarioCreacion)   
		 SELECT      
			 @as_PeriodoAcademicoDefault,  
			 --PeriodoAcademico,
			 empleado,   Apellidos,    Nombres,    
			 Sexo,     PaisNaciimiento, DepartamentoNacimiento, provincianacimiento,   
			 DistritoNacimiento,  fechanacimiento, Direccion,    Departamento,   
			 Provincia,    CodigoPostal,  Telefono,    Ceular,   
			 TipoDocumento,   documento,   nacionalidad,   CorreoElectronico,   
			 NombreEmergencia,  TelefonoEmergencia, celularemergencia,  NombreEmergencia2,   
			 TelefonoEmergencia2, CelularEmergencia2, CentroCostos,   Proyecto,   
			 correointerno,   anexo,    CodigoUsuario,   TipoPlanilla,   
			 TipoTrabajador,   FechaIngreso,  TipoContrato,   FechaInicioContrato,   
			 NombreCarnet,   ApellidoCarnet,  PuestoCarnet,   tipohorario,   
			 Cargo,     CategoriaFuncional, flagcentroformacion, Niveleducativo,   
			 CategoriaOcupacional  ,1,
			 GETDATE(), @as_UsuarioCreacion
		 FROM	VW_Syn_ED_Empleado_Fotocheck_Rep a With(NoLock)       
		 WHERE	Periodoacademico = @as_PeriodoAcademicoDefault  
				AND EXISTS(SELECT 1 FROM #Staff  S WHERE a.empleado = S.empleado)
          
		UPDATE a 
		SET a.imgstaff=b.imgstaff from Staff a  With(Nolock)   
			INNER JOIN Staff b With(Nolock)
				ON (a.PeriodoAcademico = b.PeriodoAcademico and a.empleado=b.empleado and b.checkreference=0 ) 
			INNER JOIN  #Staff S
				ON (a.empleado=S.empleado)  
		 WHERE	a.PeriodoAcademico = @as_PeriodoAcademicoDefault and 
				----a.PeriodoAcademico = @as_PeriodoAcademico AND
				a.checkreference=1    
				AND EXISTS(SELECT 1 FROM #Staff  S WHERE a.empleado = S.empleado)	      

		DELETE FROM Staff 
		WHERE	PeriodoAcademico = @as_PeriodoAcademicoDefault AND 
				--PeriodoAcademico = @as_PeriodoAcademico AND
				CheckReference=0  
				AND EXISTS(SELECT 1 FROM #Staff  S WHERE Staff.empleado = S.empleado)   
		      
		UPDATE	Staff 
		SET		--RutaFoto = 'PR-'+ Rtrim(@a_RutaFoto) + RTRIM(CONVERT(VARCHAR(10),empleado))+'.bmp'		
				RutaFoto = (CASE  WHEN dbo.uFN_Syn_ED_ExisteArchivo(RTrim(@a_RutaFoto) + 'PR-' + RTRIM(CONVERT(VARCHAR(10),empleado))+'.bmp') = 1 THEN RTrim(@a_RutaFoto) + 'PR-'+ RTRIM(CONVERT(VARCHAR(10),empleado))+'.bmp' ELSE '' END)
		WHERE	PeriodoAcademico = @as_PeriodoAcademicoDefault AND 
				--PeriodoAcademico = @as_PeriodoAcademico AND
				CheckReference=1 
				AND EXISTS(SELECT 1 FROM #Staff  S WHERE Staff.empleado = S.empleado)
	    
		DECLARE @i_Cantidad INT = 0

		SET @i_Cantidad = (SELECT  COUNT(1)  FROM #Staff)	    

		SELECT  CONVERT(VARCHAR(10),@i_Cantidad) AS Codigo,    
					0 AS CodigoError,    
					'' AS  MensajeError    
		      
		END TRY      
		      
	      
	      
	BEGIN CATCH      
		SELECT	'' AS Codigo,    
				ERROR_NUMBER() AS CodigoError,      
				ERROR_MESSAGE() AS  MensajeError      
	      
	END CATCH       
       
 --   SELECT Empleado, COUNT(1) 
	--FROM VW_Syn_ED_Empleado_Fotocheck_Rep
	--where Periodoacademico = '2012-2013' 
	--group by Empleado
	--having COUNT(1) > 1
	  
END
       

