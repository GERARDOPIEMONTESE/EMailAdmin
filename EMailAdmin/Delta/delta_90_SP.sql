USE [ACCOM_Prod]
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
CREATE PROCEDURE [Points].[BranchRatePoint_Tx_PointsToExpire]
AS 
BEGIN
	SELECT COUNT(*) AS VOUCHERS_COUNT
		FROM ACCOM_Prod.Points.BranchRatePoint BRP
		--WHERE @date >= BRP.StartDate AND @date <= BRP.EndDate 
		WHERE DATEDIFF(day, GETDATE(), BRP.EndDate) BETWEEN 0 AND 5
END
GO
