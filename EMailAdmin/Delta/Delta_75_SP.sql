USE [EMailAdmin]
GO
/****** Object:  StoredProcedure [dbo].[GroupCondition_Tx_IdGroup]    Script Date: 12/17/2013 13:08:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Lorena Cominotti
-- Create date: 04/05/2012
-- Mod By:		Yoel Dan Etbul
-- Mod date:	15/08/2012
-- Description:	Looks for GroupConditions by group id
-- =============================================
ALTER PROCEDURE [dbo].[GroupCondition_Tx_IdGroup]
	@IdGroup			INT
AS
BEGIN
	SELECT distinct top 10
		gc.*, ''VisibleValue, '' VisibleCountryOfValue, '' VisibleProductOfValue, '' VisibleCode --,
		--isnull(case when gc.IdConditionType = 1 THEN l.Nombre ELSE 
		--(
		--	CASE WHEN gc.IdConditionType = 2 THEN pj.RazonSocial ELSE 
		--	(
		--		CASE WHEN gc.IdConditionType = 3 then p.Nombre ELSE
		--		(
		--			CASE WHEN gc.IdConditionType = 4 then t.Nombre end
		--		) end
		--	) end
		--) end, 'All') as VisibleValue,
		--isnull(CASE WHEN gc.IdConditionType = 2 THEN lc.Nombre ELSE
		--(
		--	CASE WHEN gc.IdConditionType = 3 then lp.Nombre ELSE
		--	(
		--		CASE WHEN gc.IdConditionType = 4 then lt.Nombre end
		--	)end
		--)end, '') as VisibleCountryOfValue,
		--isnull(CASE WHEN gc.IdConditionType = 4 then pp.Nombre end, '') as VisibleProductOfValue
	FROM 
		GroupCondition gc
	--LEFT JOIN Portal.Locacion.locacion l ON
	--	l.IdLocacion = gc.Value
	--LEFT JOIN Portal.Cuenta.Sucursal s ON
	--	s.idsucursal = gc.Value
	--LEFT JOIN Condiciones..Producto p ON
	--	p.IdProducto = gc.Value
	--LEFT JOIN Condiciones..Tarifa t ON
	--	t.IdTarifa = gc.Value
	--LEFT JOIN Portal.Cuenta.PersonaJuridica pj ON
	--	pj.IdPersona = s.IdPersona
	--LEFT JOIN Portal.Locacion.locacion lc ON
	--	lc.IdLocacion = s.IdLocacion
	--LEFT JOIN Portal.Locacion.Pais lpp ON
	--	lpp.codigo = p.CodigoPais
	--LEFT JOIN Portal.Locacion.locacion lp ON
	--	lp.IdLocacion = lpp.IdLocacion
	--LEFT JOIN Portal.Locacion.Pais ltt ON
	--	ltt.codigo = t.CodigoPais
	--LEFT JOIN Portal.Locacion.locacion lt ON
	--	lt.IdLocacion = ltt.IdLocacion
	--LEFT JOIN Condiciones..Producto pp ON
	--	p.IdProducto = t.IdProducto
	WHERE
		gc.IdStatus <> 25002 and
		gc.IdGroup = @IdGroup
END
