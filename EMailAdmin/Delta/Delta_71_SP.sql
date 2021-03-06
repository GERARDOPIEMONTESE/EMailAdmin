USE [EMailAdmin]
GO
/****** Object:  StoredProcedure [dbo].[Group_Tx_IdGroup]    Script Date: 12/12/2013 17:12:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Yoel Dan Etbul
-- Create date: 02/05/2012
-- Mod date: 18/05/2012
-- =============================================
-- dbo.Group_Tx_IdGroup 1
ALTER PROCEDURE [dbo].[Group_Tx_IdGroup]
	@IdGroup			INT
AS
BEGIN
	SELECT
		[Group].*, TotalWeight
	FROM 
		[Group], 
		(SELECT SUM(distinct weight) TotalWeight, IdGroup
		 FROM ConditionType, GroupCondition
		 WHERE 
		 IdStatus <> 25002 and
		 ConditionType.IdConditionType = GroupCondition.IdConditionType 
		 GROUP BY IdGroup) Weight
	WHERE 
	IdStatus <> 25002 and
	[Group].IdGroup = @IdGroup
	AND Weight.IdGroup = [Group].IdGroup
END
