
USE [EMailAdmin]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Gustavo Suarez
-- Create date: 06/03/2014
-- Description:	SP Alta PointsReportHistory
-- =============================================
CREATE PROCEDURE PointsReportHistory_A
	@ReportDate				DATETIME = NULL,
	@IdUser					INT,
	@IdStatus				INT = 25000
	
AS
BEGIN
	INSERT INTO PointsReportHistory
	(
		ReportDate,
		IdUser,
		CreationDate,
		ModificationDate,
		IdStatus
	)
	VALUES
	(
		@ReportDate	,
		@IdUser		,
		GETDATE()	,
		GETDATE()	,
		@IdStatus
	)
	
	RETURN @@identity
END
GO


-- =============================================
-- Author:		Gustavo Suarez	
-- Create date: 06/03/2014
-- Description:	SP Modificar PointsReportHistory
-- =============================================
CREATE PROCEDURE PointsReportHistory_M
	@IdHistory				INT,
	@ReportDate				DATETIME = NULL,
	@IdUser					INT,
	@IdStatus				INT = 25001
AS
BEGIN

	UPDATE PointsReportHistory
		SET
			ReportDate			= @ReportDate,
			IdUser				= @IdUser,
			ModificationDate    = GETDATE(),
			IdStatus			= @IdStatus
		WHERE
			IdHistory	 = @IdHistory
END
GO


-- =============================================
-- Author:		Gustavo Suarez	
-- Create date: 06/03/2014
-- Description:	SP Baja PointsReportHistory
-- =============================================
CREATE PROCEDURE PointsReportHistory_E
	@IdHistory					INT,
	@IdUser						INT,
	@IdStatus					INT = 25002
AS
BEGIN
	
	UPDATE PointsReportHistory
		SET
			IdUser = @IdUser,
			ModificationDate = GETDATE(),
			IdStatus = @IdStatus
		WHERE
			IdHistory = @IdHistory
END
GO


-- =============================================
-- Author:		Gustavo Suarez
-- Create date: 10/09/2013
-- Description:	SP Traer-Todos PointsReportHistory
-- =============================================
CREATE PROCEDURE PointsReportHistory_TT
AS
BEGIN

	SELECT * 
	FROM PointsReportHistory
	WHERE IdStatus NOT IN (25002)	
END
GO

