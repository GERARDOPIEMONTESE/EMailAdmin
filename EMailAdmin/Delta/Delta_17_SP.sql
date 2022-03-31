USE [EMailAdmin]
GO

/****** Object:  StoredProcedure [dbo].[EMailList_Tx_Usuarios]    Script Date: 03/14/2013 10:01:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Da Silva Marcela
-- Create date: 27/02/2013
-- =============================================
CREATE PROCEDURE [dbo].[EMailList_Tx_Usuarios]
	@Nombre			VARCHAR(100)=NULL,
	@Apellido		VARCHAR(100)=NULL,
	@CorreoElectronico	VARCHAR(100)=NULL,
	@IdLocacion		INT=-1,
	@IdCategoriaUsuario	INT=-1,
	@IdEmailListType	INT=NULL
AS
BEGIN
	WITH ususExcluidos as (
		SELECT idusuario FROM EMailList 
		WHERE IdLocacion = @IdLocacion 
		AND IdEMailListType = @IdEmailListType 
		AND IdStatus <>25002
	)
	
	SELECT 
		Usuario.IdUsuario, Nombre, Apellido, CorreoElectronico,
		Usuario.IdIdioma,
		Idioma.Cultura
	FROM
		Portal.dbo.Usuario		
		INNER JOIN 
		Portal.dbo.Idioma on Portal.dbo.Idioma.IdIdioma = Portal.dbo.Usuario.IdIdioma
		LEFT JOIN 
		ususExcluidos on ususExcluidos.IdUsuario = Usuario.IdUsuario
	WHERE
		(@IdLocacion = -1 OR (@IdLocacion > -1 AND Usuario.IdLocacion = @IdLocacion))
		AND (@IdCategoriaUsuario = -1 OR (@IdCategoriaUsuario > -1 AND Usuario.IdUsuarioCategoria = @IdCategoriaUsuario))
		AND (@Nombre IS NULL OR (@Nombre IS NOT NULL AND Usuario.Nombre like '%' + @Nombre + '%' ))
		AND (@Apellido IS NULL OR (@Apellido IS NOT NULL AND Usuario.Apellido like '%' + @Apellido + '%' ))
		AND (@CorreoElectronico IS NULL OR (@CorreoElectronico IS NOT NULL AND Usuario.CorreoElectronico like '%' + @CorreoElectronico + '%' ))
		AND ususExcluidos.IdUsuario is null
	ORDER BY Apellido ASC,Nombre ASC
	
END
