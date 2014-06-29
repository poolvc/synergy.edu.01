



CREATE VIEW[dbo].[VW_Syn_ED_Alumno_Fotocheck]
as
SELECT  a.Periodoacademico,a.GradoCodigo,a.Seccion,b.alumnogrupo,
Familia=rtrim(isnull(c.apellidopaterno,''))+' '+rtrim(isnull(c.apellidomaterno,'')),
Grado=d.descripcionlocal,a.rol,
b.Alumnocodigo,
Apellidos=rtrim(b.Apellidopaterno)+' '+rtrim(b.apellidomaterno),
Nombres=rtrim(isnull(b.nombre01,''))+' '+rtrim(isnull(b.nombre02,'')),
b.fechanacimiento,b.fechaingreso,b.lugarnacimiento,
Programa=(case b.idprograma when 'P' then 'Peruano' when 'A' then 'Americano' end),
Sexo=( case b.sexo when 'M' then 'Masculino' when 'F' then 'Femenino' end),
Nacionalidad=e.descripcionlocal,Idioma=f.descripcionlocal,IdiomaSecundario=g.descripcionlocal,
b.email,
Seguromedico=h.descripcion,b.SeguroMedicoPoliza,b.SeguroMedicoFechaExpiracion,b.EmergenciaContacto,
b.EmergenciaTelefono,b.EmergenciaContacto2,
b.EmergenciaTelefono2,b.CodigoInterno
FROM FDR_Spring.dbo.ED_Matricula a inner join  FDR_Spring.dbo.ED_Alumno b
on a.alumnocodigo=b.alumnocodigo
inner join FDR_Spring.dbo.ed_alumnogrupo c
on c.alumnogrupo=b.alumnogrupo 
left join FDR_Spring.dbo.ed_grado d
on d.gradocodigo=a.gradocodigo
left join FDR_Spring.dbo.ed_nacionalidad e
on e.nacionalidad=b.nacionalidad
left join FDR_Spring.dbo.ed_idioma f
on f.idioma=b.idioma
left join FDR_Spring.dbo.ed_idioma g
on g.idioma=b.IdiomaSecundario
left join FDR_Spring.dbo.ed_seguromedico h
on h.SeguroMedicoCodigo=b.SeguroMedicoCodigo
where b.estado='A'
and c.estado='A'



