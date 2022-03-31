USE [EMailAdmin]
GO

/****** Object:  View [dbo].[GroupCondition_Tx_IdGroupComplete_view]    Script Date: 04/15/2013 10:05:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[GroupCondition_Tx_IdGroupComplete_view]
AS
SELECT     gc.IdGroupCondition, gc.IdGroup, gc.Value, 
                      ISNULL(CASE WHEN gc.IdConditionType = 1 THEN l.Nombre ELSE (CASE WHEN gc.IdConditionType = 2 THEN pj.RazonSocial ELSE (CASE WHEN gc.IdConditionType =
                       3 THEN p.Nombre ELSE (CASE WHEN gc.IdConditionType = 4 THEN t .Nombre END) END) END) END, 'All') AS VisibleValue, 
                      ISNULL(CASE WHEN gc.IdConditionType = 2 THEN lc.Nombre ELSE (CASE WHEN gc.IdConditionType = 3 THEN lp.Nombre ELSE (CASE WHEN gc.IdConditionType = 4 THEN
                       lt.Nombre END) END) END, '') AS VisibleCountryOfValue, ISNULL(CASE WHEN gc.IdConditionType = 4 THEN pp.Nombre END, '') AS VisibleProductOfValue, 
                      gc.IdStatus
FROM         dbo.GroupCondition AS gc LEFT OUTER JOIN
                      Portal.Locacion.Locacion AS l ON l.IdLocacion = gc.Value LEFT OUTER JOIN
                      Portal.Cuenta.Sucursal AS s ON s.IdSucursal = gc.Value LEFT OUTER JOIN
                      Condiciones.dbo.Producto AS p ON p.IdProducto = gc.Value LEFT OUTER JOIN
                      Condiciones.dbo.Tarifa AS t ON t.IdTarifa = gc.Value LEFT OUTER JOIN
                      Portal.Cuenta.PersonaJuridica AS pj ON pj.IdPersona = s.IdPersona LEFT OUTER JOIN
                      Portal.Locacion.Locacion AS lc ON lc.IdLocacion = s.IdLocacion LEFT OUTER JOIN
                      Portal.Locacion.Pais AS lpp ON lpp.Codigo = p.CodigoPais LEFT OUTER JOIN
                      Portal.Locacion.Locacion AS lp ON lp.IdLocacion = lpp.IdLocacion LEFT OUTER JOIN
                      Portal.Locacion.Pais AS ltt ON ltt.Codigo = t.CodigoPais LEFT OUTER JOIN
                      Portal.Locacion.Locacion AS lt ON lt.IdLocacion = ltt.IdLocacion LEFT OUTER JOIN
                      Condiciones.dbo.Producto AS pp ON p.IdProducto = t.IdProducto
WHERE     (gc.IdStatus <> 25002)

GO