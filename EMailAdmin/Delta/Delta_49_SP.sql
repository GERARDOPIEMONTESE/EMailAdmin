USE [EMailAdmin]
GO

/****** Object:  StoredProcedure [dbo].[ConditionVariableText_Tx_Filters]    Script Date: 07/18/2013 17:50:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Da Silva Marcela
-- Create date: 12/07/2013
-- =============================================
CREATE PROCEDURE [dbo].[ConditionVariableText_Tx_Filters]
	@IdVariableText int = NULL,
	@Name Varchar(30) = NULL,
	@Condicion varchar(1000) = NULL	
AS
BEGIN
	select *
	from [ConditionVariableText] cvt
	inner join ConditionVariableText_R_VariableText crv on crv.IdConditionVariableText = cvt.IdConditionVariableText
	where 
	((@Name IS NULL) OR (NOT @Name IS NULL AND cvt.Name like '%' + @Name + '%'))
	AND ((@IdVariableText IS NULL) OR (NOT @IdVariableText IS NULL AND crv.IdVariableText = @IdVariableText))	
	AND ((@Condicion IS NULL) OR (NOT @Condicion IS NULL AND crv.Condition like '%' + @Condicion + '%'))
	AND cvt.IdStatus<>25002 and crv.IdStatus<>25002
END