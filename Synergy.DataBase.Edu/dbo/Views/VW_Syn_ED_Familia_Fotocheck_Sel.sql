

CREATE VIEW[dbo].[VW_Syn_ED_Familia_Fotocheck_Sel]
as

SELECT   
		Periodoacademico='999999999',
		a.alumnogrupo,
		Familia=rtrim(isnull(a.apellidopaterno,''))+' '+rtrim(isnull(a.apellidomaterno,'')),
		Vinculo=(case b.tipopersona when 'M' then 'Mother' when 'P' then 'Father' when 'T' then 'Tutor' else Tipopersona end),
		Apellidos=rtrim(isnull(b.apellidopaterno,''))+' '+rtrim(isnull(b.apellidomaterno,'')),
		Nombres=rtrim(isnull(b.nombre01,''))+' '+rtrim(isnull(b.nombre02,'')),
		b.Direccion,
		Tipodocumento=(case tipodocumento when 'O' then 'Otros' when 'R' then 'RUC' when 'P' then 'Pasaporte' when 'X' then 'Carnet Ext' when 'D' then 'DNI' end),
		b.documentoidentidad
from FDR_Spring.dbo.ed_alumnogrupo a With(NoLock)
		inner join FDR_Spring.dbo.ed_alumnogrupopersona b With(NoLock)
			on a.alumnogrupo=b.alumnogrupo
where	a.estado='A'
		and rtrim(isnull(b.apellidopaterno,''))+' '+rtrim(isnull(b.apellidomaterno,''))<>''
		--AND (rtrim(isnull(b.apellidopaterno,''))<> '' and rtrim(isnull(b.apellidomaterno,'')) <> '')
		and b.empresacodigo!='0138'
UNION
SELECT  
		'999999999',
		e.alumnogrupo,
		Familia=rtrim(isnull(d.apellidopaterno,''))+' '+rtrim(isnull(d.apellidomaterno,'')),
		Tipopersona=f.descripcionextranjera,
		e.apellidos,
		Nombres=e.nombres,
		Direccion=null,
		Tipodocumento=(case e.tipodoc when 'O' then 'Otros' when 'R' then 'RUC' when 'P' then 'Pasaporte' when 'X' then 'Carnet Ext' when 'D' then 'DNI' end),
		Documento=e.idnumero
from   FDR_Spring.dbo.ed_alumno b
		inner join FDR_Spring.dbo.ed_alumnogrupo d With(NoLock)
			on d.alumnogrupo=b.alumnogrupo
		inner join FDR_Spring.dbo.mt_personaautorizada e With(NoLock)
			on e.alumnogrupo=b.alumnogrupo
		inner join FDR_Spring.dbo.mt_miscelaneos f With(NoLock)
			on f.valor=e.posicion
where	f.tipotabla='Personal Posicion'
		and b.estado='A'
		and upper(f.DescripcionExtranjera)<>'PARENT'





