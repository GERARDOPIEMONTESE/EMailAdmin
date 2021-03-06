USE [EMailAdmin]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Gustavo Suarez
-- Create date: 19/05/2014
-- Description:	Determinar si existen puntos x vencer
-- =============================================
CREATE PROCEDURE[dbo].[PointsToExpire]
AS 
BEGIN
	DECLARE @count INT

	SELECT @count = COUNT(IdBranchRatePoint)
		FROM ACCOM_Prod.Points.BranchRatePoint BRP
		--WHERE @date >= BRP.StartDate AND @date <= BRP.EndDate 
		WHERE DATEDIFF(day, GETDATE(), BRP.EndDate) BETWEEN 0 AND 5
		
	RETURN @count
END
