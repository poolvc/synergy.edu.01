



CREATE PROCEDURE [dbo].[uSP_Syn_ED_Empleado_Fotocheck_Procesar]      
@as_codigo VARCHAR(10),      
@as_PeriodoAcademico VARCHAR(10), 
@as_UsuarioCreacion VARCHAR(10) =  NULL            
AS        
        
BEGIN TRY        
        
	DECLARE @a_RutaFoto varchar(200) = ''      
	DECLARE @i_Empleado INT = 0	
	SET @i_Empleado = CONVERT(INT, Rtrim(@as_codigo))
	
	DECLARE @as_PeriodoAcademicoDefault varchar(10) = '999999999'   --No se concidera el periodo academic

	UPDATE	Staff 
	SET		CheckReference = 0
	WHERE	PeriodoAcademico = @as_PeriodoAcademicoDefault AND 
			--PeriodoAcademico = @as_PeriodoAcademico AND
			empleado = @i_Empleado    

       
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
		 CategoriaOcupacional ,CheckReference,
		 UsuarioCreacion, FechaCreacion)   
	 SELECT      
		 @as_PeriodoAcademicoDefault,  
		 --PeriodoAcademico,
		 @i_Empleado,   Apellidos,    Nombres,    
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
		 CategoriaOcupacional ,1,
		 @as_UsuarioCreacion, GETDATE() 
	 FROM VW_Syn_ED_Empleado_Fotocheck_Rep With(NoLock)       
	 WHERE Periodoacademico = @as_PeriodoAcademicoDefault   AND  Empleado = @i_Empleado  
          
	--Obtiene La direccion de red donde esta la imagen        
  	SELECT		@a_RutaFoto = DescripcionParametro      
	FROM		FDR_Spring.dbo.ParametrosMast   With(Nolock)  
	WHERE		CompaniaCodigo ='999999' AND  AplicacionCodigo ='ED'  AND  ParametroClave ='ALUMNODIR'     
                   
	UPDATE a 
	SET a.imgstaff=b.imgstaff from Staff a  With(Nolock)   
		INNER JOIN Staff b With(Nolock)
			ON (a.empleado=b.empleado and b.checkreference=0 )    
	 WHERE	a.PeriodoAcademico = @as_PeriodoAcademicoDefault and 
		    --a.PeriodoAcademico = @as_PeriodoAcademico AND
			a.checkreference=1 AND a.empleado = @i_Empleado    
      
	DELETE FROM Staff 
	WHERE	PeriodoAcademico = @as_PeriodoAcademicoDefault AND 
			--PeriodoAcademico = @as_PeriodoAcademico AND
			CheckReference=0 AND empleado = @i_Empleado    
	      
	      --select @a_RutaFoto
	UPDATE	Staff 
	SET		--RutaFoto ='PR-'+ RTrim(@a_RutaFoto) + RTRIM(CONVERT(VARCHAR(10),empleado))+'.bmp'
			RutaFoto = (CASE  WHEN dbo.uFN_Syn_ED_ExisteArchivo(RTrim(@a_RutaFoto) + 'PR-'+  + RTRIM(CONVERT(VARCHAR(10),empleado))+'.bmp') = 1 THEN RTrim(@a_RutaFoto) + RTRIM(CONVERT(VARCHAR(10),empleado)) + 'PR-'+'.bmp' ELSE '' END)
	WHERE	PeriodoAcademico = @as_PeriodoAcademicoDefault AND 
			--PeriodoAcademico = @as_PeriodoAcademico AND 
			CheckReference=1 AND empleado = @i_Empleado    

	SELECT          
			0 AS CodigoError,        
			'' AS  MensajeError        
	        
	END TRY        	      
        
BEGIN CATCH        

	SELECT          
	  ERROR_NUMBER() AS CodigoError,        
	  ERROR_MESSAGE() AS  MensajeError        
        
END CATCH 
/*
select 
* 
--delete 
from Staff
*/




