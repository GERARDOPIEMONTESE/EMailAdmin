USE [EMailAdmin]
GO

/****** Object:  StoredProcedure [dbo].[ConditionVariableText_A]    Script Date: 07/18/2013 17:47:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Da Silva Marcela
-- Create date: 12/07/2013
-- =============================================
CREATE PROCEDURE [dbo].[ConditionVariableText_A]
	@Name			VARCHAR(200),	
	@IdUser			INT,
	@IdStatus		INT
AS
BEGIN
	INSERT INTO [ConditionVariableText]
	(
		Name,
		IdUser,
		CreationDate,
		modifieddate,
		IdStatus
	)
	VALUES
	(
		@Name,
		@IdUser,
		GETDATE(),
		GETDATE(),
		@IdStatus
	)
	
	RETURN scope_identity()
END

GO


