﻿


CREATE VIEW [dbo].[VW_Syn_Photo_Students]
as
SELECT [Periodoacademico]
      ,[GradoCodigo]
      ,[Seccion]
      ,[alumnogrupo]
      ,[Familia]
      ,[Grado]
      ,[rol]
      ,[Alumnocodigo]
      ,[Apellidos]
      ,[Nombres]
      ,[fechanacimiento]
      ,[FechaNacimientoString]
      ,[fechaingreso]
      ,[lugarnacimiento]
      ,[Programa]
      ,[Sexo]
      ,[Nacionalidad]
      ,[Idioma]
      ,[IdiomaSecundario]
      ,[email]
      ,[Seguromedico]
      ,[SeguroMedicoPoliza]
      ,[SeguroMedicoFechaExpiracion]
      ,[EmergenciaContacto]
      ,[EmergenciaTelefono]
      ,[EmergenciaContacto2]
      ,[EmergenciaTelefono2]
      ,[CodigoInterno]
      ,[RutaFoto]
      ,[imgstudents]
      ,[Gate]
      ,[CheckReference]
  FROM [Photos].[dbo].[Students]


