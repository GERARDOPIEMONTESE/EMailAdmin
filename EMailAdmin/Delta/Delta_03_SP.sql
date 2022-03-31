USE [EMailAdmin]
GO

/****** Object:  StoredProcedure [dbo].[EMailListType_A]    Script Date: 03/06/2013 16:41:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Da Silva Marcela
-- Create date: 27/02/2013
-- =============================================
CREATE PROCEDURE [dbo].[EMailListType_A]
	@Code			VARCHAR(10),
	@Description	VARCHAR(200),
	@IdUser			INT,
	@IdStatus		INT
AS
BEGIN
	INSERT INTO EMailListType
	(
		Code,
		[Description],
		IdUser,
		CreationDate,
		ModifiedDate,
		IdStatus
	)
	VALUES
	(
		@Code,
		@Description,
		@IdUser,
		GETDATE(),
		GETDATE(),
		@IdStatus
	)
	
	RETURN @@IDENTITY	
END

GO

