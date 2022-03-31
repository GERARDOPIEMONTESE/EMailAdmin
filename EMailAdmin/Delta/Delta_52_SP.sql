USE [EMailAdmin]
GO

/****** Object:  StoredProcedure [dbo].[ConditionVariableTextContent_E]    Script Date: 07/18/2013 17:51:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Da Silva Marcela
-- Create date: 12/07/2013
-- =============================================
Create PROCEDURE [dbo].[ConditionVariableTextContent_E]
	@IdConditionVariableText	INT,
	@IdStatus					INT
AS
BEGIN
	UPDATE [ConditionVariableTextContent]
	SET
		IdStatus = @IdStatus,
		modifieddate = GETDATE()
	WHERE
		IdConditionVariableText = @IdConditionVariableText
END

GO


