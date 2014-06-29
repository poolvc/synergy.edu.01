


CREATE VIEW[dbo].[VW_Syn_ED_Familia_Fotocheck]
as

select distinct Periodoacademico='999999999',a.alumnogrupo,
Familia=rtrim(isnull(a.apellidopaterno,''))+' '+rtrim(isnull(a.apellidomaterno,'')),
Vinculo=(case b.tipopersona when 'M' then 'Mother' when 'P' then 'Father'

 when 'T' then 'Tutor' else Tipopersona end),
Apellidos=rtrim(isnull(b.apellidopaterno,''))+' '+rtrim(isnull(b.apellidomaterno,'')),
Nombres=rtrim(isnull(b.nombre01,''))+' '+rtrim(isnull(b.nombre02,'')),b.Direccion,b.ciudad,
Pais=c.descripcioncorta,b.codigopostal,
b.emailcasa,b.emailoficina,
Tipodocumento=(case tipodocumento when 'O' then 'Otros' when 'R' then 'RUC' when 'P' then 'Pasaporte' when 'X' then 'Carnet Ext' when 'D' then 'DNI' end),
b.documentoidentidad,b.fechanacimiento,
PaisNacimiento=d.descripcioncorta,
Nacionalidad=e.descripcionlocal,
Idioma=f.descripcionlocal,
GradoInstruccion= (case b.gradoinstruccion when 'UN' then 'Universitaria' when 'MA' then 'Master' when 'PH' then 'Doctorado' when 'SE' then 'Secundaria' when 'TE' then 'Tecnica' end)
,
b.telefono01,b.telefono02,b.telefonocelular,b.fax,
IdiomaSecundario=g.descripcionlocal,
Grupo=1
from FDR_Spring.dbo.ed_alumnogrupo a With(NoLock)
inner join FDR_Spring.dbo.ed_alumnogrupopersona b With(NoLock)
on a.alumnogrupo=b.alumnogrupo
left outer join FDR_Spring.dbo.pais c With(NoLock)
on c.pais=b.pais
left outer join FDR_Spring.dbo.pais d With(NoLock)
on d.pais=b.paisnacimiento
left join FDR_Spring.dbo.ed_nacionalidad e With(NoLock)
on e.nacionalidad=b.nacionalidad
left join FDR_Spring.dbo.ed_idioma f With(NoLock)
on f.idioma=b.idioma
left join FDR_Spring.dbo.ed_idioma g With(NoLock)
on 
g.idioma=b.IdiomaSecundario
inner join FDR_Spring.dbo.ed_alumno h With(NoLock)
on h.alumnogrupo=a.alumnogrupo
inner join FDR_Spring.dbo.ed_matricula i With(NoLock)
on i.alumnocodigo=h.alumnocodigo 
where a.estado='A'
and rtrim(isnull(b.apellidopaterno,''))+' '+rtrim(isnull(b.apellidomaterno,''))<>''
and b.empresacodigo!='0138'
union
select distinct '999999999',e.alumnogrupo,
Familia=rtrim(isnull(d.apellidopaterno,''))+' '+rtrim(isnull(d.apellidomaterno,'')),
Tipopersona=f.descripcionextranjera,e.apellidos,Nombres=e.nombres,
Direccion=null,ciudad=null,Pais=null,Codigopostal=null,emailcasa=null,emailoficina=null,
Tipodocumento=(case e.tipodoc when 'O' then '
Otros' when 'R' then 'RUC' when 'P' then 'Pasaporte' when 'X' then 'Carnet Ext' when 'D' then 'DNI' end),
Documento=e.idnumero,fechanacimiento=null,PaisNacimiento=null,Nacionalidad=null,Idioma=null,GradoInstruccion=null,
telefono01=null,telefono02=null,telefonocelular=null,fax=null,
IdiomaSecundario=null,Grupo=2
from FDR_Spring.dbo.ed_matricula a With(NoLock)
inner join FDR_Spring.dbo.ed_alumno b With(NoLock)
on a.alumnocodigo=b.alumnocodigo
inner join FDR_Spring.dbo.ed_alumnogrupopersona c With(NoLock)
on c.alumnogrupo=b.alumnogrupo
inner join FDR_Spring.dbo.ed_alumnogrupo d With(NoLock)
on d.alumnogrupo=c.alumnogrupo
inner join FDR_Spring.dbo.mt_personaautorizada e With(NoLock)
on e.alumnogrupo=c.alumnogrupo
inner join FDR_Spring.dbo.mt_miscelaneos f With(NoLock)
on f.valor=e.posicion
inner join FDR_Spring.dbo.ed_alumnogrupo g With(NoLock)
on g.alumnogrupo=b.alumnogrupo
where f.tipotabla='Personal Posicion'
and b.estado='A'
and upper(f.DescripcionExtranjera)!='PARENT'


