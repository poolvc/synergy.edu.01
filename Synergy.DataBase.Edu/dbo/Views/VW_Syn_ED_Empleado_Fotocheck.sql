


CREATE VIEW[dbo].[VW_Syn_ED_Empleado_Fotocheck]
as
select distinct Periodoacademico= '999999999',Empleado=convert(numeric(9,0),a.empleado),Apellidos=rtrim(isnull(b.apellidopaterno,''))+' '+rtrim(isnull(b.apellidomaterno,'')),
Nombres=b.nombres,Sexo=(case when b.sexo='M' then 'Masculino' else 'Femenino' end),
PaisNaciimiento=h.descripcioncorta,
DepartamentoNacimiento=j5.descripcioncorta , 
provincianacimiento=j6.descripcioncorta,DistritoNacimiento=j7.descripcioncorta,b.fechanacimiento,b.Direccion,
Departamento=h1.descripcioncorta,
Provincia=h2.descripcioncorta,
CodigoPostal=h3.descripcioncorta,b.Telefono,b.Fax,Ceular=b.Celular,
TipoDocumento=( case b.tipodocumento when  'E' then 'DNI' when 'A' then 'Autogenerado' 
		when 'F' then 'Carné Fuerzas Policiales' when 'D' then 'Documento Prov. de Identidad' 
		when 'M' then 'Carné Fuerzas Armadas' when 'M' then 'Partida Nacimiento' when 'P' then 'Pasaporte'
		when 'R' then 'RUC' when 'X' then 'Carné Extranjeria'
		else b.tipodocumento end),
b.documento,b.nacionalidad,b.CorreoElectronico,
GrupoSanguineo=i.descripcionlocal,
b.NombreEmergencia,b.DireccionEmergencia,b.TelefonoEmergencia,b.celularemergencia,
NombreEmergencia2=c.NombreEmergencia,DireccionEmergencia2=c.DireccionEmergencia ,
TelefonoEmergencia2=c.TelefonoEmergencia,CelularEmergencia2=c.CelularEmergencia,
CentroCostos=j.localname,
Proyecto=j1.localname,
a.correointerno,c.anexo,a.CodigoUsuario,
TipoPlanilla=g.descripcion,
TipoTrabajador=d.descripcionlocal,
a.FechaIngreso,a.fechacese,a.fechareingreso,
TipoContrato=e.descripcion,
FechaInicioContrato,FechaFinContrato,c.numerocontrato,
c.NombreCarnet,c.ApellidoCarnet,c.PuestoCarnet,
tipohorario=f.descripcionlocal,
Cargo=j3.descripcion,
CategoriaFuncional=a.cargo,
a.flagcentroformacion,
Niveleducativo=j4.descripcionlocal,
CategoriaOcupacional=j2.descripcionlocal
from  FDR_Spring.dbo.empleadomast a inner join  FDR_Spring.dbo.personamast b
on a.empleado=b.persona
left join   FDR_Spring.dbo.hr_empleado c
on c.empleado=a.empleado
left join  FDR_Spring.dbo.hr_tipotrabajador d
on d.tipotrabajador=a.TipoTrabajador
left outer join  FDR_Spring.dbo.hr_tipocontrato e
on e.TipoContrato=a.TipoContrato
left join  FDR_Spring.dbo.AS_TipoHorario f
on f.tipohorario=a.tipohorario
left join  FDR_Spring.dbo.PR_Tipoplanilla g
on g.tipoplanilla=a.tipoplanilla
left join  FDR_Spring.dbo.pais h
on h.pais=c.paisnacimiento
left join  FDR_Spring.dbo.departamento h1
on h1.departamento=b.departamento
left join  FDR_Spring.dbo.provincia h2
on h2.provincia=b.provincia
and h2.departamento=b.departamento
left join  FDR_Spring.dbo.zonapostal h3
on h3.departamento=b.departamento
and h3.provincia=b.provincia
and h3.codigopostal=b.codigopostal
left join  FDR_Spring.dbo.MA_MISCELANEOSDETALLE i	--Grupo Sanguineo
on i.codigoelemento=c.gruposanguineo 
	AND APLICACIONCODIGO='HR'
	AND CODIGOTABLA='GRUPOSANGR'
	AND COMPANIA = '999999'
left join  FDR_Spring.dbo.ac_costcentermst j
on j.costcenter=a.centrocostos
left join  FDR_Spring.dbo.afemst j1
on j1.afe=a.afe
left join  FDR_Spring.dbo.MA_MISCELANEOSDETALLE j2
on j2. codigotabla='RTPS_TB_24'
and j2.CodigoElemento=a.categoriaocupacional
left join  FDR_Spring.dbo.HR_PuestoEmpresa j3
on j3.codigopuesto=a.codigocargo
left join  FDR_Spring.dbo.ma_miscelaneosdetalle j4
	on j4.codigotabla='RTPS_TB_9'
and j4.CodigoElemento=a.niveleducativortps
left join  FDR_Spring.dbo.departamento j5
on j5.departamento=b.ciudadnacimiento
left join  FDR_Spring.dbo.provincia j6
on j6.provincia=c.provincianacimiento
and j6.departamento=b.ciudadnacimiento
left join  FDR_Spring.dbo.zonapostal j7
on j7.departamento=b.ciudadnacimiento
and j7.provincia=c.provincianacimiento
and j7.codigopostal=c.distritonacimiento
where  a.EstadoEmpleado 	=  '0'
and a.CompaniaSocio 	=  '02000000' 
and (a.TipoPlanilla = 'AD' OR a.TipoPlanilla = 'DO' OR a.TipoPlanilla = 'DE' OR a.TipoPlanilla = 'EM' OR a.TipoPlanilla = 'EV' OR a.TipoPlanilla = 'OB') 

