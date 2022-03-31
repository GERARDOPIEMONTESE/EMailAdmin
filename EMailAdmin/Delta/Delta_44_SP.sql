USE [EMailAdmin]
GO

/****** Object:  StoredProcedure [dbo].[ConditionVariableText_R_VariableText_A]    Script Date: 07/18/2013 17:48:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Da Silva Marcela
-- Create date: 12/07/2013
-- =============================================
CREATE PROCEDURE [dbo].[ConditionVariableText_R_VariableText_A]
	@IdConditionVariableText	INT,
	@IdVariableText				INT,
	@Condition					VARCHAR(1000),
	@IdUser						INT,
	@IdStatus					INT
AS
BEGIN
	INSERT INTO [ConditionVariableText_R_VariableText]
	(
		IdConditionVariableText,
		IdVariableText,
		Condition,
		CreationDate,
		ModifiedDate,
		IdUser,
		IdStatus
	)
	VALUES
	(
		@IdConditionVariableText,
		@IdVariableText,
		@Condition,
		GETDATE(),
		GETDATE(),
		@IdUser,
		@IdStatus
	)
	
	RETURN scope_identity()	
END

GO


