



---uSP_Syn_ED_Familia_Fotocheck_ProcesarMasivo '','','','c|d|1|','1|2|3|'
CREATE PROCEDURE [dbo].[uSP_Syn_ED_Familia_Fotocheck_ProcesarMasivo]  
@as_PeriodoAcademico VARCHAR(20) = '',      
@as_Foto    CHAR(1),      
@as_Exportado   CHAR(1),  
@as_Codigo VARCHAR(max) ,
@as_Vinculo VARCHAR(max),
@as_Familia VARCHAR(10) = '',
@as_DocumentoIdentidad VARCHAR(max), 
@as_UsuarioCreacion VARCHAR(10) =  NULL     
AS      
BEGIN      
	BEGIN TRY      
	      
		CREATE TABLE #Parametro (AlumnoGrupo VARCHAR(10),Vinculo VARCHAR(20), DocumentoIdentidad VARCHAR(20))
		CREATE TABLE #Family (AlumnoGrupo VARCHAR(10),Vinculo VARCHAR(20), DocumentoIdentidad VARCHAR(20))
		
	    DECLARE @as_PeriodoAcademicoDefault varchar(10) = '999999999'   --No se concidera el periodo academic

	   DECLARE @Posicion int     
	   DECLARE @Posicion2 int
	   DECLARE @Posicion3 int
	   
	   DECLARE @Parametro varchar(3000)     
	   DECLARE @Parametro2 varchar(3000)
	   DECLARE @Parametro3 varchar(3000)
	   
	   SET @as_Codigo = @as_Codigo + '|'        
	   SET @as_Vinculo = @as_Vinculo + '|'  
	   SET @as_DocumentoIdentidad = @as_DocumentoIdentidad + '|'  
	       
		   WHILE patindex('%|%' , @as_Codigo) <> 0        
				BEGIN        
					SELECT @Posicion =  patindex('%|%' , @as_Codigo)        
					SELECT @Posicion2 =  patindex('%|%' , @as_Vinculo)        
					SELECT @Posicion3 =  patindex('%|%' , @as_DocumentoIdentidad)        
				    
					SELECT @Parametro = left(@as_Codigo, @Posicion - 1)        
					SELECT @Parametro2 = left(@as_Vinculo, @Posicion2 - 1)  
					SELECT @Parametro3 = left(@as_DocumentoIdentidad, @Posicion3 - 1)  
				    IF (Rtrim(@Parametro3) <> '')
						BEGIN
							INSERT INTO #Parametro values (@Parametro,@Parametro2, @Parametro3)
						END
				     
					SELECT @as_Codigo = stuff(@as_Codigo, 1, @Posicion, '')        
					SELECT @as_Vinculo = stuff(@as_Vinculo, 1, @Posicion2, '')  
					SELECT @as_DocumentoIdentidad = stuff(@as_DocumentoIdentidad, 1, @Posicion3, '')  
				END     
			
			--Obtiene La direccion de red donde esta la imagen
			DECLARE @a_RutaFoto varchar(200) = '' 
		      
			SELECT	@a_RutaFoto = DescripcionParametro      
			FROM	FDR_Spring.dbo.ParametrosMast   With(Nolock)  
			WHERE	CompaniaCodigo ='999999' AND  AplicacionCodigo ='ED'  AND  ParametroClave ='FAMILIADIR'     

			CREATE TABLE #Files (subdirectory varchar(100) ,depth int,isfile bit);

			INSERT #Files (subdirectory,depth,isfile)
			EXEC xp_dirtree @a_RutaFoto,1,1;

			--select * from #Files
			
			INSERT INTO #Family(AlumnoGrupo,Vinculo, DocumentoIdentidad)
			SELECT  Alumnogrupo ,  Vinculo ,  ISNULL(DocumentoIdentidad,'')
			FROM  VW_Syn_ED_Familia_Fotocheck_Rep   
				LEFT JOIN #Files F
					ON (F.subdirectory = RTrim(DocumentoIdentidad) + '.bmp' COLLATE DATABASE_DEFAULT AND     
						F.isfile = 1)           
			WHERE	--PeriodoAcademico = @as_PeriodoAcademico
				PeriodoAcademico = @as_PeriodoAcademicoDefault
				AND ( @as_Foto =    '' OR @as_Foto = (Case When F.subdirectory IS Null Then 'N' Else 'S' End))           
				AND ( @as_Exportado = '' OR ProcesadoFlag = @as_Exportado )  
			    AND ( @as_Familia = '' OR AlumnoGrupo = @as_Familia)  
				AND RTrim(documentoidentidad) <> '' 
				AND NOT EXISTS (SELECT 1 FROM #Parametro P 
								  WHERE VW_Syn_ED_Familia_Fotocheck_Rep.alumnogrupo  = P.alumnogrupo COLLATE DATABASE_DEFAULT AND
										VW_Syn_ED_Familia_Fotocheck_Rep.Vinculo  = P.Vinculo COLLATE DATABASE_DEFAULT and
										VW_Syn_ED_Familia_Fotocheck_Rep.documentoidentidad  = P.documentoidentidad Collate DATABASE_DEFAULT )
			
			--select * from #Family
			 
			UPDATE Family 
			SET CheckReference = 0
			WHERE 
				  --PeriodoAcademico = @as_PeriodoAcademico AND 
				  PeriodoAcademico = @as_PeriodoAcademicoDefault AND
				  CheckReference=1 
				AND EXISTS(SELECT 1 FROM #Family  S 
						   WHERE Family.Alumnogrupo = S.Alumnogrupo COLLATE DATABASE_DEFAULT AND
								 Family.Vinculo = S.Vinculo COLLATE DATABASE_DEFAULT AND
								 Family.documentoidentidad = S.documentoidentidad COLLATE DATABASE_DEFAULT)
	     
	       
			INSERT INTO Family ( Periodoacademico,   alumnogrupo,   Familia,    Vinculo,     
			  Apellidos,     Nombres,    Direccion,    ciudad,     
			  Pais,      codigopostal,   emailcasa,    emailoficina,     
			  Tipodocumento,    documentoidentidad,  fechanacimiento,  PaisNacimiento,     
			  Nacionalidad,    Idioma,     GradoInstruccion,  telefono01,     
			  telefono02,     telefonocelular,  fax,     IdiomaSecundario, Grupo,CheckReference, 
			  UsuarioCreacion, FechaCreacion)     
			SELECT       
			  @as_PeriodoAcademicoDefault,	
			  --a.Periodoacademico ,               
			  a.Alumnogrupo ,               
			  a.Familia ,               
			  a.Vinculo ,               
			  a.Apellidos ,               
			  a.Nombres ,               
			  a.Direccion ,               
			  a.Ciudad ,               
			  a.Pais ,               
			  a.CodigoPostal ,               
			  a.Emailcasa ,               
			  a.Emailoficina ,               
			  Tipodocumento ,               
			  ISNULL(a.DocumentoIdentidad,''),  
			  a.Fechanacimiento ,               
			  a.PaisNacimiento ,               
			  a.Nacionalidad ,               
			  a.Idioma ,               
			  a.GradoInstruccion ,               
			  a.Telefono01 ,               
			  a.Telefono02 ,               
			  a.Telefonocelular ,               
			  a.Fax ,               
			  a.IdiomaSecundario ,               
			  a.Grupo,  
			  1,     
			  @as_UsuarioCreacion,
			  GETDATE()  
			FROM VW_Syn_ED_Familia_Fotocheck_Rep a    	    
			WHERE	--a.Periodoacademico = @as_PeriodoAcademico
					a.Periodoacademico = @as_PeriodoAcademicoDefault 
				AND EXISTS(SELECT 1 FROM #Family  S 
						   WHERE a.Alumnogrupo = S.Alumnogrupo COLLATE DATABASE_DEFAULT AND
								 a.Vinculo = S.Vinculo COLLATE DATABASE_DEFAULT AND
								 a.documentoidentidad = S.documentoidentidad COLLATE DATABASE_DEFAULT )
		    

			

	  		UPDATE a 
			SET a.imgfamily=b.imgfamily from Family a  With(Nolock)   
			INNER JOIN Family b With(Nolock)
				ON (a.Periodoacademico = @as_PeriodoAcademicoDefault and
					a.alumnogrupo = b.alumnogrupo and 
					a.Vinculo = b.Vinculo and 
					a.documentoidentidad = b.documentoidentidad and
					b.checkreference=0 )    
			WHERE	--a.PeriodoAcademico = @as_PeriodoAcademico 
					a.PeriodoAcademico = @as_PeriodoAcademicoDefault
				    AND a.checkreference=1 
					AND EXISTS(SELECT 1 FROM #Family  S 
						   WHERE a.Alumnogrupo = S.Alumnogrupo COLLATE DATABASE_DEFAULT AND
								 a.Vinculo = S.Vinculo COLLATE DATABASE_DEFAULT AND
								 a.documentoidentidad = S.documentoidentidad COLLATE DATABASE_DEFAULT)
	    		      

		  DELETE FROM Family 
		  WHERE		--PeriodoAcademico = @as_PeriodoAcademico 
				    PeriodoAcademico = @as_PeriodoAcademicoDefault 
					AND CheckReference=0 
					AND EXISTS(SELECT 1 FROM #Family  S 
						   WHERE Family.Alumnogrupo = S.Alumnogrupo COLLATE DATABASE_DEFAULT AND
								 Family.Vinculo = S.Vinculo COLLATE DATABASE_DEFAULT AND
								 Family.documentoidentidad = S.documentoidentidad COLLATE DATABASE_DEFAULT)
	    		      	
	    
					      
			UPDATE	Family 
			SET		--RutaFoto = CASE WHEN RTrim(IsNull(documentoidentidad,'')) = '' THEN '' ELSE RTRIM(@a_RutaFoto) + RTRIM(documentoidentidad)+'.bmp' END,     
						RutaFoto = CASE WHEN RTrim(IsNull(documentoidentidad,'')) = '' THEN '' 
											ELSE ( CASE WHEN  dbo.uFN_Syn_ED_ExisteArchivo(RTRIM(@a_RutaFoto) + RTRIM(documentoidentidad)+'.bmp')=1 THEN
														RTRIM(@a_RutaFoto) + RTRIM(documentoidentidad)+'.bmp'
													ELSE
														''
													END
													)  
								
						END,     
					Documentoidentidad=RTrim(documentoidentidad)
			WHERE	--PeriodoAcademico = @as_PeriodoAcademico 
					PeriodoAcademico = @as_PeriodoAcademicoDefault
					AND CheckReference=1  
					AND EXISTS(SELECT 1 FROM #Family  S 
						   WHERE Family.Alumnogrupo = S.Alumnogrupo COLLATE DATABASE_DEFAULT AND
								 Family.Vinculo = S.Vinculo COLLATE DATABASE_DEFAULT AND
								 Family.documentoidentidad = S.documentoidentidad COLLATE DATABASE_DEFAULT)
	    		      	
			DECLARE @i_Cantidad INT = 0

			SET @i_Cantidad = (SELECT  COUNT(1)  FROM #Family)	    

			SELECT  CONVERT(VARCHAR(10),@i_Cantidad) AS AlumnoCodigo,    
					0 AS CodigoError,    
					'' AS  MensajeError    
					      
			END TRY      
			      
	      
	      
	BEGIN CATCH      

			SELECT     
			  ERROR_NUMBER() AS CodigoError,      
			  ERROR_MESSAGE() AS  MensajeError      
	      
	END CATCH   


	--SELECT AlumnoGrupo,Vinculo,documentoidentidad , COUNT(1) 
	--FROM VW_Syn_ED_Familia_Fotocheck_Rep
	--where Periodoacademico = '2012-2013' AND RTrim(documentoidentidad) <> '' 
	--group by AlumnoGrupo,Vinculo,documentoidentidad
	--having COUNT(1) > 1

END





