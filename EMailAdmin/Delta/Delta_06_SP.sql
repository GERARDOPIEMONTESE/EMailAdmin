USE [EMailAdmin]
GO

/****** Object:  StoredProcedure [dbo].[EMailListType_L_A]    Script Date: 03/06/2013 16:41:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Da Silva Marcela
-- Create date: 27/02/2013
-- =============================================
CREATE PROCEDURE [dbo].[EMailListType_L_A]
	@IdEMailListType INT,
	@Code			VARCHAR(10),
	@Description	VARCHAR(200),
	@IdUser			INT,
	@IdStatus		INT
AS
BEGIN
	INSERT INTO EMailListType_L
	(
		IdEMailListType,
		Code,
		[Description],
		IdUser,
		CreationDate,
		ModifiedDate,
		IdStatus
	)
	VALUES
	(
		@IdEMailListType,
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

