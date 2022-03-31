USE [EMailAdmin]
GO

/****** Object:  StoredProcedure [dbo].[EMailList_Tx_Filters]    Script Date: 03/06/2013 16:41:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Da Silva Marcela
-- Create date: 27/02/2013
-- =============================================
CREATE PROCEDURE [dbo].[EMailList_Tx_Filters]
	@IdEmailListType	INT,
	@IdLocacion		INT,
	@CorreoElectronico	VARCHAR(100)=NULL
AS 
BEGIN
	SELECT
		EMailList.IdEmailList,
		EMailList.IdLocacion,
		Locacion.Nombre LocacionNombre,
		EMailList.IdEMailListType,
		EMailListType.Code EmailListTypeCode,
		EMailListType.[Description] EmailListTypeDescription,
		EMailList.IdUsuario,
		Usuario.Nombre UsuarioNombre,
		Usuario.Apellido UsuarioApellido,
		Usuario.CorreoElectronico UsuarioCorreoElectronico
	FROM 
		EMailList
	INNER JOIN 
		EMailListType on EMailList.IdEMailListType = EMailListType.IdEMailListType
	INNER JOIN
		Portal.Locacion.Locacion on EMailList.IdLocacion = Portal.Locacion.Locacion.IdLocacion
	INNER JOIN 
		Portal.dbo.Usuario on Portal.dbo.Usuario.IdUsuario = EMailList.IdUsuario
	WHERE
		EMailList.IdStatus <> 25002 AND
		((@IdEmailListType = -1) or (EMailList.IdEMailListType = @IdEmailListType)) AND
		((@IdLocacion = -1) or (EMailList.IdLocacion = @IdLocacion)) AND
		(@CorreoElectronico IS NULL OR (@CorreoElectronico IS NOT NULL AND Usuario.CorreoElectronico like '%' + @CorreoElectronico + '%' ))
END
