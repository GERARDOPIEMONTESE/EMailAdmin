USE [EMailAdmin]
GO
/****** Object:  StoredProcedure [dbo].[EMailListExclude_E]    Script Date: 15/10/2015 11:27:33 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Da Silva Marcela
-- Create date: 15/10/2015
-- =============================================
create PROCEDURE [dbo].[EMailListExclude_E]
	@IdEMailListExclude int,	
	@IdUser int,
	@IdStatus int
AS
BEGIN
	UPDATE
		EMailListExclude
	SET 
		ModifiedDate = GETDATE(),
		IdUser = @IdUser,
		IdStatus = @IdStatus
	WHERE 
		IdEMailListExclude = @IdEMailListExclude

END
