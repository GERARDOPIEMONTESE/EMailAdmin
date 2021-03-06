USE [EMailAdmin]
GO
/****** Object:  StoredProcedure [dbo].[EMailList_Prepurchace]    Script Date: 03/11/2013 16:50:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Da Silva Marcela
-- Create date: 11/03/2013
-- =============================================
CREATE PROCEDURE [dbo].[EMailList_Prepurchace]
	@Pais			INT
AS 
BEGIN	
	declare @IdLocacion int 
	set @IdLocacion = (select IdLocacion from Portal.Locacion.Pais where Codigo = @Pais);
			
	SELECT
		EMailList.IdUsuario,
		Usuario.Nombre,
		Usuario.Apellido,
		Usuario.CorreoElectronico,
		Usuario.IdIdioma,
		Idioma.Cultura
	FROM 
		EMailList
	INNER JOIN 
		EMailListType on EMailList.IdEMailListType = EMailListType.IdEMailListType
	INNER JOIN 
		Portal.dbo.Usuario on Portal.dbo.Usuario.IdUsuario = EMailList.IdUsuario
	INNER JOIN 
		Portal.dbo.Idioma on Portal.dbo.Idioma.IdIdioma = Portal.dbo.Usuario.IdIdioma
	WHERE
		EMailList.IdStatus <> 25002 AND
		EMailListType.Code = 'LPC' AND
		EMailList.IdLocacion = @IdLocacion
END
