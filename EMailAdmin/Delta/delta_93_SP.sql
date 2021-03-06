USE [EMailAdmin]
GO
/****** Object:  StoredProcedure [dbo].[Capitas_Tx_Filters]    Script Date: 06/18/2014 12:43:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Marcela Da Silva
-- Create date: 09/06/2014
-- Description:	productos y tarifa capitas
-- =============================================
-- Capitas_Tx_Filters 540, 'Visa', '111'
ALTER PROCEDURE [dbo].[Capitas_Tx_Filters]
	@CodigoPais				INT,
	@CapitaNombre			VARCHAR(255) = NULL,
	@PlanNombre				VARCHAR(255) = NULL
AS
BEGIN

DECLARE @IdTipoGrupoClausula INT

SELECT @IdTipoGrupoClausula = IdTipoGrupoClausula FROM Condiciones.dbo.TipoGrupoClausula WHERE Nombre = 'Producto Sin Emision'

select t.IdTarifa, t.Codigo PlanCodigo, t.Nombre PlanNombre,
p.IdProducto, p.Codigo CapitaCodigo, p.Nombre CapitaNombre
from 
Condiciones.dbo.Tarifa t
inner join Condiciones.dbo.Producto p on p.IdProducto = t.IdProducto
where t.CodigoPais = @CodigoPais
and (@CapitaNombre is null or (p.Nombre like '%'+@CapitaNombre+'%'))
and (@PlanNombre is null or (t.Nombre like '%'+@PlanNombre+'%'))
and p.IdTipoGrupoClausula = @IdTipoGrupoClausula
and t.IdTipoGrupoClausula = @IdTipoGrupoClausula

END