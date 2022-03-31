USE [EMailAdmin]
GO

/****** Object:  StoredProcedure [dbo].[ConditionVariableText_R_VariableText_Tx_Filters]    Script Date: 07/18/2013 17:50:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Da Silva Marcela
-- Create date: 15/07/2013
-- =============================================
CREATE PROCEDURE [dbo].[ConditionVariableText_R_VariableText_Tx_Filters]
	@IdConditionVariableText_R_VariableText	INT = null,
	@IdConditionVariableText				INT = null	
AS
BEGIN
	SELECT 
		* 
	FROM 
		[ConditionVariableText_R_VariableText]
	WHERE
		IdStatus <> 25002 and
		((@IdConditionVariableText_R_VariableText is null) or (@IdConditionVariableText_R_VariableText is not null and IdConditionVariableText_R_VariableText = @IdConditionVariableText_R_VariableText)) and
		((@IdConditionVariableText is null) or (@IdConditionVariableText is not null and IdConditionVariableText = @IdConditionVariableText)) 
END