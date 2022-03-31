USE [EMailAdmin]
GO

/****** Object:  StoredProcedure [dbo].[ConditionVariableText_E]    Script Date: 07/18/2013 17:47:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Da Silva Marcela
-- Create date: 12/07/2013
-- =============================================
CREATE PROCEDURE [dbo].[ConditionVariableText_E]
	@IdConditionVariableText INT,
	@IdUser			INT,
	@IdStatus		INT
AS
BEGIN
	UPDATE [ConditionVariableText]
	SET IdStatus = @IdStatus,
	IdUser = @IdUser
	WHERE IdConditionVariableText = @IdConditionVariableText
	
END

GO


