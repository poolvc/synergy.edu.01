



CREATE PROCEDURE [dbo].[uSP_Syn_ED_Alumno_Fotocheck_Procesar]  
@as_Alumnocodigo VARCHAR(10),  
@as_PeriodoAcademico VARCHAR(10), 
@as_UsuarioCreacion VARCHAR(10) =  NULL     
AS    
    
BEGIN TRY    
    
	DECLARE @a_RutaFoto varchar(200)    
   
	UPDATE Students 
	SET CheckReference = 0
	WHERE PeriodoAcademico = @as_PeriodoAcademico AND Alumnocodigo = @as_Alumnocodigo    

 
	INSERT INTO Students (	 
			Periodoacademico, 		GradoCodigo,   			Seccion,			Alumnogrupo,   
			Familia,     			Grado,     				rol,				Alumnocodigo,    
			Apellidos,				Nombres,				fechanacimiento,	fechaingreso,  
			lugarnacimiento,		Programa,				Sexo,     			Nacionalidad,  
			Idioma,					IdiomaSecundario,   	Seguromedico,		SeguroMedicoPoliza,     
			SeguroMedicoFechaExpiracion,   EmergenciaContacto,  EmergenciaTelefono, EmergenciaContacto2,     
			EmergenciaTelefono2, CodigoInterno,CheckReference,
			FechaCreacion, UsuarioCreacion)     
    SELECT     
			Periodoacademico    
			,GradoCodigo    
			,Seccion    
			,alumnogrupo    
			,Familia    
			,Grado    
			,rol    
			,Alumnocodigo    
			,Apellidos    
			,Nombres    
			,fechanacimiento    
			,fechaingreso    
			,lugarnacimiento    
			,Programa    
			,Sexo     
			,Nacionalidad    
			,Idioma    
			,IdiomaSecundario    
			,Seguromedico    
			,SeguroMedicoPoliza    
			,SeguroMedicoFechaExpiracion    
			,EmergenciaContacto    
			,EmergenciaTelefono    
			,EmergenciaContacto2    
			,EmergenciaTelefono2    
			,CodigoInterno    
			,1    
			,GETDATE()
			,@as_UsuarioCreacion
    FROM VW_Syn_ED_Alumno_Fotocheck_Rep    
    WHERE Alumnocodigo = @as_Alumnocodigo AND Periodoacademico = @as_PeriodoAcademico  
      
	--Obtiene La direccion de red donde esta la imagen    
	SELECT		@a_RutaFoto = DescripcionParametro      
	FROM		FDR_Spring.dbo.ParametrosMast   With(Nolock)  
	WHERE		CompaniaCodigo ='999999' AND  AplicacionCodigo ='ED'  AND  ParametroClave ='ALUMNODIR'     
         
          
	UPDATE a 
	SET a.imgstudents=b.imgstudents from Students a  With(Nolock)   
		INNER JOIN Students b With(Nolock)
			ON (a.PeriodoAcademico = b.PeriodoAcademico and a.alumnocodigo=b.alumnocodigo and b.checkreference=0 )    
	 WHERE a.PeriodoAcademico = @as_PeriodoAcademico and a.checkreference=1 AND a.Alumnocodigo = @as_Alumnocodigo    
      
	DELETE FROM Students 
	WHERE	PeriodoAcademico = @as_PeriodoAcademico AND CheckReference=0 AND Alumnocodigo = @as_Alumnocodigo    
	      
	UPDATE	Students 
	SET		RutaFoto = (CASE  WHEN dbo.uFN_Syn_ED_ExisteArchivo(Rtrim(@a_RutaFoto) + RTRIM(codigointerno)+'.bmp') = 1 THEN Rtrim(@a_RutaFoto) + RTRIM(codigointerno)+'.bmp' ELSE '' END),    
			FechanacimientoString = LEFT(CONVERT(CHAR,fechanacimiento,113),12),    
			CodigoInterno = RTRIM(codigointerno)    
	WHERE	PeriodoAcademico = @as_PeriodoAcademico AND CheckReference=1 AND Alumnocodigo = @as_Alumnocodigo    
     
    
	SELECT  @as_Alumnocodigo as AlumnoCodigo,    
			0 AS CodigoError,    
			'' AS  MensajeError    
			    
	END TRY    
	
		

    
BEGIN CATCH    
	SELECT	  @as_Alumnocodigo as AlumnoCodigo,    
			  ERROR_NUMBER() AS CodigoError,    
			  ERROR_MESSAGE() AS  MensajeError    
    
END CATCH




