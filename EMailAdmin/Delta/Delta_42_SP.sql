USE [EMailAdmin]
GO

/****** Object:  StoredProcedure [dbo].[ConditionVariableText_L_A]    Script Date: 07/18/2013 17:48:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Da Silva Marcela
-- Create date: 12/07/2013
-- =============================================
CREATE PROCEDURE [dbo].[ConditionVariableText_L_A]
	@IdConditionVariableText	INT,
	@Name						VARCHAR(200),
	@IdUser						INT,
	@IdStatus					INT
AS
BEGIN
	INSERT INTO [ConditionVariableText_L]
	(
		IdConditionVariableText,
		Name,
		IdUser,
		CreationDate,
		modifieddate,
		IdStatus
	)
	VALUES
	(
		@IdConditionVariableText,
		@Name,
		@IdUser,
		GETDATE(),
		GETDATE(),
		@IdStatus
	)
	
	RETURN scope_identity()
END

GO


