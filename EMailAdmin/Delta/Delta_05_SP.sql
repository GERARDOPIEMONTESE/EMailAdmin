USE [EMailAdmin]
GO

/****** Object:  StoredProcedure [dbo].[EMailListType_E]    Script Date: 03/06/2013 16:41:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Da Silva Marcela
-- Create date: 27/02/2013
-- =============================================
CREATE PROCEDURE [dbo].[EMailListType_E]
	@IdEmailListType INT,
	@IdUser			INT,
	@IdStatus		INT
AS
BEGIN
	UPDATE
		EMailListType
	SET 
		ModifiedDate = GETDATE(),
		IdUser = @IdUser,
		IdStatus = @IdStatus
	WHERE 
		IdEmailListType = @IdEmailListType
	
	RETURN @@IDENTITY	
END

GO

