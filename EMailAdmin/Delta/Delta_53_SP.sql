USE [EMailAdmin]
GO

/****** Object:  StoredProcedure [dbo].[ConditionVariableTextContent_L_A]    Script Date: 07/18/2013 17:51:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Da Silva Marcela
-- Create date: 12/07/2013
-- =============================================
CREATE PROCEDURE [dbo].[ConditionVariableTextContent_L_A]				   
	@IdConditionVariableTextContent	INT,
	@IdConditionVariableText		INT,
	@IdLanguage						INT,
	@ContentText					VARCHAR(max),
	@IdUser							INT,
	@IdStatus						INT
AS
BEGIN
	INSERT INTO [ConditionVariableTextContent_L]
	(
		IdConditionVariableTextContent,
		IdConditionVariableText,
		IdLanguage,
		ContentText,
		CreationDate,
		modifieddate,
		IdUser,
		IdStatus
	)
	VALUES
	(
		@IdConditionVariableTextContent,
		@IdConditionVariableText,
		@IdLanguage,
		@ContentText,
		GETDATE(),
		GETDATE(),
		@IdUser,
		@IdStatus
	)
	
	RETURN scope_identity()
END

GO


