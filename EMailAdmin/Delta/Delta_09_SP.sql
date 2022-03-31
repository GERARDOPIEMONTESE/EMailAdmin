USE [EMailAdmin]
GO

/****** Object:  StoredProcedure [dbo].[EmailListType_Tx_IdEmailListType]    Script Date: 03/06/2013 16:42:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Da Silva Marcela
-- Create date: 28/02/2013
-- =============================================
CREATE PROCEDURE [dbo].[EmailListType_Tx_IdEmailListType]
	@IdEmailListType			INT
AS
BEGIN	
	DECLARE @UsuariosAsignados int
	SET @UsuariosAsignados = (select COUNT(EMailList.IdUsuario) 
			from EMailList 
			where IdStatus <>25002 
			AND IdEMailListType = @IdEmailListType
			);
			
	SELECT *, @UsuariosAsignados UsuariosAsignados
	FROM
		EMailListType
	WHERE
		EMailListType.IdEMailListType = @IdEmailListType	
END