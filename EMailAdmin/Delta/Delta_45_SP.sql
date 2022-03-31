USE [EMailAdmin]
GO

/****** Object:  StoredProcedure [dbo].[ConditionVariableText_R_VariableText_E]    Script Date: 07/18/2013 17:49:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Da Silva Marcela
-- Create date: 12/07/2013
-- =============================================
CREATE PROCEDURE [dbo].[ConditionVariableText_R_VariableText_E]
	@IdConditionVariableText_R_VariableText	INT,
	@IdStatus								INT
AS
BEGIN
	UPDATE 
		ConditionVariableText_R_VariableText 
	SET
		IdStatus = @IdStatus,
		modifieddate = GETDATE()
	WHERE
		IdConditionVariableText_R_VariableText = @IdConditionVariableText_R_VariableText
END

GO


