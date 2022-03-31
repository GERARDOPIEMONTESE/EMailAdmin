USE [EMailAdmin]
GO

/****** Object:  StoredProcedure [dbo].[EMailListType_M]    Script Date: 03/06/2013 16:42:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Da Silva Marcela
-- Create date: 27/02/2013
-- =============================================
CREATE PROCEDURE [dbo].[EMailListType_M]
	@IdEmailListType INT,
	@Code			VARCHAR(10),
	@Description	VARCHAR(200),
	@IdUser			INT,
	@IdStatus		INT
AS
BEGIN
	UPDATE
		EMailListType
	SET 
		Code = @Code,
		[Description] = @Description,
		ModifiedDate = GETDATE(),
		IdUser = @IdUser
	WHERE 
		IdEmailListType = @IdEmailListType
	
	RETURN @@IDENTITY	
END

GO

