CREATE VIEW [dbo].[GENV_ParametroDetalle]
AS
SELECT 
			P.IdParametro,
			P.Parametro,
			P.IdCompania,
			P.IdAplicacion,
			P.TipoDato,
			P.Texto,
			P.Numero,
			P.Fecha,
			P.Tipo,
			D.ParametroDetalle,
			D.Descripcion,
			D.ValorTexto1,
			D.ValorTexto2,
			D.ValorTexto3,
			D.ValorTexto4,
			D.ValorTexto5,
			D.ValorTexto6,
			D.ValorNumero,
			D.ValorFecha,
			D.Estado		
	FROM	FDR_GALEXITO_MAIN.dbo.GEN_ParametroDetalle AS D With(NoLock)
		INNER JOIN FDR_GALEXITO_MAIN.dbo.GEN_Parametro AS P With(NoLock)
			ON (D.IdParametro = P.IdParametro)


