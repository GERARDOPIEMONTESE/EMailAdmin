USE [EMailAdmin]
GO

/****** Object:  StoredProcedure [dbo].[EMailList_E]    Script Date: 03/06/2013 16:40:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Da Silva Marcela
-- Create date: 05/03/2013
-- =============================================
CREATE PROCEDURE [dbo].[EMailList_E]
	@IdEMailList	INT = null,
	@IdUser			INT = null,
	@IdStatus		INT = null
AS
BEGIN
	UPDATE EMailList
	SET
		IdStatus = @IdStatus,
		IdUser = @IdUser
	WHERE
		IdEMailList = @IdEMailList
END

GO

