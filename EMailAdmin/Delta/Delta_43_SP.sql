USE [EMailAdmin]
GO

/****** Object:  StoredProcedure [dbo].[ConditionVariableText_M]    Script Date: 07/18/2013 17:48:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Da Silva Marcela
-- Create date: 12/07/2013
-- =============================================
CREATE PROCEDURE [dbo].[ConditionVariableText_M]
	@IdConditionVariableText INT,
	@Name			VARCHAR(200),
	@IdUser				INT
AS
BEGIN
	UPDATE [ConditionVariableText] SET
		Name = @Name,
		IdUser = @IdUser,
		ModifiedDate = GETDATE()
	WHERE 
		IdConditionVariableText = @IdConditionVariableText
END

GO


