USE [EMailAdmin]
GO

/****** Object:  StoredProcedure [dbo].[ConditionVariableText_R_VariableText_E_IdConditionVariableText]    Script Date: 07/18/2013 17:49:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Da Silva Marcela
-- Create date: 12/07/2013
-- =============================================
create PROCEDURE [dbo].[ConditionVariableText_R_VariableText_E_IdConditionVariableText]
	@IdConditionVariableText	INT,
	@IdStatus					INT
AS
BEGIN
	UPDATE 
		ConditionVariableText_R_VariableText 
	SET
		IdStatus = @IdStatus,
		modifieddate = GETDATE()
	WHERE
		IdConditionVariableText = @IdConditionVariableText
END

GO


