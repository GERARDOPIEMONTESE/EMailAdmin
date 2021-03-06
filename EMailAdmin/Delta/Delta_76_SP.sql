USE [EMailAdmin]
GO
/****** Object:  StoredProcedure [dbo].[GroupCondition_Tx_IdGroupCondition]    Script Date: 12/17/2013 13:44:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Yoel Dan Etbul
-- Create date: 02/05/2012
-- =============================================
ALTER PROCEDURE [dbo].[GroupCondition_Tx_IdGroupCondition]
	@IdGroupCondition	INT = null
AS
BEGIN
	SELECT 
		gc.*,
		isnull(case when gc.IdConditionType = 1 THEN l.Nombre ELSE (CASE WHEN gc.IdConditionType = 2 THEN pj.RazonSocial ELSE p.Name end) end, 'All') as VisibleValue
		,isnull(case when gc.IdConditionType = 1 THEN pa.Codigo ELSE (CASE WHEN gc.IdConditionType = 2 THEN (c.Codigo+'-'+ cast(s.NumeroSucursal as varchar(10))) ELSE p.Code end) end, '') as VisibleCode
	FROM 
		GroupCondition gc
	LEFT JOIN Portal.Locacion.locacion l ON
		l.IdLocacion = gc.Value
	LEFT JOIN Portal.Locacion.Pais pa ON pa.IdLocacion = gc.Value
	LEFT JOIN Portal.Cuenta.Sucursal s ON
		s.idsucursal = gc.Value
	LEFT JOIN Product p ON
		p.IdProduct = gc.Value
	LEFT JOIN Portal.Cuenta.PersonaJuridica pj ON
		pj.IdPersona = s.IdPersona
	LEFT JOIN Portal.Cuenta.Cuenta c ON c.IdPersona = s.IdPersona
	WHERE
		gc.IdGroupCondition = @IdGroupCondition
END

