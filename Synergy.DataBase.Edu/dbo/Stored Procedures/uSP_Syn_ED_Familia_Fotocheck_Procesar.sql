



CREATE PROCEDURE [dbo].[uSP_Syn_ED_Familia_Fotocheck_Procesar]      
@as_codigo VARCHAR(10),
@as_Vinculo VARCHAR(100),
@as_DocumentoIdentidad VARCHAR(100),
@as_PeriodoAcademico VARCHAR(10) , 
@as_UsuarioCreacion VARCHAR(10) =  NULL           
AS        
        
BEGIN TRY        
        
	DECLARE @a_RutaFoto varchar(200)        
	DECLARE @as_PeriodoAcademicoDefault varchar(10) = '999999999'   --No se concidera el periodo academic
	
	UPDATE Family
	SET CheckReference = 0
	WHERE PeriodoAcademico = @as_PeriodoAcademicoDefault AND
		  --PeriodoAcademico = @as_PeriodoAcademico AND 
		  alumnogrupo = @as_codigo and Vinculo = @as_Vinculo and
		  documentoidentidad = @as_DocumentoIdentidad

       
	 INSERT INTO Family ( Periodoacademico,   alumnogrupo,   Familia,    Vinculo,     
		  Apellidos,     Nombres,    Direccion,    ciudad,     
		  Pais,      codigopostal,   emailcasa,    emailoficina,     
		  Tipodocumento,    documentoidentidad,  fechanacimiento,  PaisNacimiento,     
		  Nacionalidad,    Idioma,     GradoInstruccion,  telefono01,     
		  telefono02,     telefonocelular,  fax,     IdiomaSecundario, Grupo,CheckReference, 
		  FechaCreacion, UsuarioCreacion)     
	SELECT DISTINCT      
	  --Periodoacademico ,               
	  @as_PeriodoAcademicoDefault,
	  Alumnogrupo ,               
	  Familia ,               
	  Vinculo ,               
	  Apellidos ,               
	  Nombres ,               
	  Direccion ,               
	  Ciudad ,               
	  Pais ,               
	  CodigoPostal ,               
	  Emailcasa ,               
	  Emailoficina ,               
	  Tipodocumento ,               
	  DocumentoIdentidad ,               
	  Fechanacimiento ,               
	  PaisNacimiento ,               
	  Nacionalidad ,               
	  Idioma ,               
	  GradoInstruccion ,               
	  Telefono01 ,               
	  Telefono02 ,               
	  Telefonocelular ,               
	  Fax ,               
	  IdiomaSecundario ,               
	  Grupo ,  
	  1 ,
	  GETDATE(),
	  @as_UsuarioCreacion 
	FROM VW_Syn_ED_Familia_Fotocheck_Rep     
	WHERE AlumnoGrupo = @as_codigo    
		AND Vinculo = @as_Vinculo
		AND documentoidentidad = @as_DocumentoIdentidad
		--AND Periodoacademico = @as_PeriodoAcademico 
		AND Periodoacademico = @as_PeriodoAcademicoDefault 
		     
      
	--Obtiene La direccion de red donde esta la imagen    
	SELECT		@a_RutaFoto = DescripcionParametro      
	FROM		FDR_Spring.dbo.ParametrosMast   With(Nolock)  
	WHERE		CompaniaCodigo ='999999' AND  AplicacionCodigo ='ED'  AND  ParametroClave ='FAMILIADIR'     
         
              
	UPDATE a 
	SET a.imgfamily=b.imgfamily from Family a  With(Nolock)   
		INNER JOIN Family b With(Nolock)
			ON (a.Periodoacademico = b.Periodoacademico and
				a.alumnogrupo = b.alumnogrupo and 
				a.Vinculo = b.Vinculo and 
				a.documentoidentidad = b.documentoidentidad and
				b.checkreference=0 )    
	 WHERE  --a.PeriodoAcademico = @as_PeriodoAcademico and 
			a.PeriodoAcademico = @as_PeriodoAcademicoDefault and 
			a.documentoidentidad = @as_DocumentoIdentidad and
			a.checkreference=1 AND a.alumnogrupo = @as_codigo and a.Vinculo =  @as_Vinculo 
      
	DELETE FROM Family 
	WHERE	CheckReference=0 AND alumnogrupo = @as_codigo  and Vinculo = @as_Vinculo and documentoidentidad = @as_DocumentoIdentidad
	      
	UPDATE	Family 
	SET		RutaFoto = CASE WHEN RTrim(IsNull(documentoidentidad,'')) = '' THEN '' 
								ELSE ( CASE WHEN  dbo.uFN_Syn_ED_ExisteArchivo(RTRIM(@a_RutaFoto) + RTRIM(documentoidentidad)+'.bmp')=1 THEN
											RTRIM(@a_RutaFoto) + RTRIM(documentoidentidad)+'.bmp'
										ELSE
											''
										END
										)  
								
						END,     
			Documentoidentidad=RTrim(documentoidentidad)
	WHERE	--PeriodoAcademico = @as_PeriodoAcademico AND 
			PeriodoAcademico = @as_PeriodoAcademicoDefault AND 
			alumnogrupo = @as_codigo  and Vinculo = @as_Vinculo and documentoidentidad = @as_DocumentoIdentidad and
			CheckReference=1 
     

        
	SELECT          
	0 AS CodigoError,        
	'' AS  MensajeError        
	        
	END TRY        
	        
        
        
BEGIN CATCH        
	SELECT          
	  ERROR_NUMBER() AS CodigoError,        
	  ERROR_MESSAGE() AS  MensajeError        
        
END CATCH




