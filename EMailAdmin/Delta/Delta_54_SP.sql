USE [EMailAdmin]
GO

/****** Object:  StoredProcedure [dbo].[ConditionVariableTextContent_Tx_IdConditionVariableText]    Script Date: 07/18/2013 17:51:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Da Silva Marcela
-- Create date: 15/07/2013
-- =============================================
CREATE PROCEDURE [dbo].[ConditionVariableTextContent_Tx_IdConditionVariableText]
	@IdConditionVariableText int
AS
BEGIN
	select * 
	from [ConditionVariableTextContent]
	where ((@IdConditionVariableText IS NULL) OR (NOT @IdConditionVariableText IS NULL AND IdConditionVariableText = @IdConditionVariableText))
	AND IdStatus<>25002
END


