USE [EMailAdmin]
GO
/****** Object:  StoredProcedure [dbo].[Attachment_Tx_Group_Filters]    Script Date: 06/09/2014 11:30:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Marcela Da Silva
-- Create date: 09/06/2014
-- Description:	productos y tarifa capitas
-- =============================================
-- Capitas_Tx_Filters 'TARIFA', 540, 'ca'
Create PROCEDURE [dbo].[Capitas_Tx_Filters]
	@Tipo						VARCHAR(10),
	@CodigoPais					INT,
	@Descripcion				VARCHAR(255) = NULL
AS
BEGIN

DECLARE @IdTipoGrupoClausula INT
SELECT @IdTipoGrupoClausula = IdTipoGrupoClausula FROM Condiciones.dbo.TipoGrupoClausula WHERE Nombre = 'Producto Sin Emision'

if (@Tipo='PRODUCTO')
	begin
		select IdProducto id, Codigo, Nombre 
		from Condiciones.dbo.Producto 
		where IdTipoGrupoClausula = @IdTipoGrupoClausula
		AND CodigoPais = @CodigoPais
		AND upper(Nombre) like '%'+upper(@Descripcion)+'%'
		order by Nombre
	END
ELSE
	begin
		select IdTarifa id, Codigo, Nombre 
		from condiciones.dbo.Tarifa 
		where IdTipoGrupoClausula =@IdTipoGrupoClausula
		AND CodigoPais = @CodigoPais
		AND upper(Nombre) like '%'+upper(@Descripcion)+'%'
		order by Nombre
	END
END
