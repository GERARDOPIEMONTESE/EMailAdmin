USE [EMailAdmin]
GO

/****** Object:  StoredProcedure [dbo].[ConditionVariableTextContent_A]    Script Date: 07/18/2013 17:50:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Da Silva Marcela
-- Create date: 12/07/2013
-- =============================================
CREATE PROCEDURE [dbo].[ConditionVariableTextContent_A]
	@IdConditionVariableText	INT,
	@IdLanguage				INT,
	@ContentText			VARCHAR(max),
	@IdUser					INT,
	@IdStatus				INT
AS
BEGIN
	INSERT INTO [ConditionVariableTextContent]
	(
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


