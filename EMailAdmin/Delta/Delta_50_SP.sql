USE [EMailAdmin]
GO

/****** Object:  StoredProcedure [dbo].[ConditionVariableText_Tx_IdConditionVariableText]    Script Date: 07/18/2013 17:50:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Da Silva Marcela
-- Create date: 12/07/2013
-- =============================================
CREATE PROCEDURE [dbo].[ConditionVariableText_Tx_IdConditionVariableText]
	@IdConditionVariableText INT
AS
BEGIN
	select * 
	from [ConditionVariableText]
	where IdConditionVariableText = @IdConditionVariableText
END

GO


