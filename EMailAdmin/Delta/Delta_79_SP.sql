USE [EMailAdmin]
GO
/****** Object:  StoredProcedure [dbo].[Group_Tx_IdGroup]    Script Date: 01/03/2014 11:17:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Yoel Dan Etbul
-- Create date: 02/05/2012
-- Mod date: 18/05/2012
-- Mod: Marcela Da Silva
-- Mod date: 03/01/2014
-- =============================================
-- dbo.Group_Tx_IdGroup 672
ALTER PROCEDURE [dbo].[Group_Tx_IdGroup]
	@IdGroup			INT
AS
BEGIN
	declare @TotalWeight int
	set @TotalWeight = (SELECT SUM(distinct weight) 
		 FROM ConditionType, GroupCondition
		 WHERE 
		 IdStatus <> 25002 and
		 ConditionType.IdConditionType = GroupCondition.IdConditionType 
		 and IdGroup = @IdGroup)
		 
	SELECT
		[Group].*, ISNULL(@TotalWeight,0) TotalWeight
	FROM 
		[Group]
	WHERE 
	IdStatus <> 25002 and
	[Group].IdGroup = @IdGroup
END
