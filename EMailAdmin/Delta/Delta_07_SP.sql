USE [EMailAdmin]
GO

/****** Object:  StoredProcedure [dbo].[EMailListType_Tx_Code]    Script Date: 03/06/2013 16:42:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Da Silva Marcela
-- Create date: 30/03/2012
-- Description:	Looks for EmailContact types by code
-- =============================================
Create PROCEDURE [dbo].[EMailListType_Tx_Code]
	@Code			VARCHAR(10) = NULL
AS
BEGIN
	SELECT *
	FROM
		EMailListType
	WHERE
		(@Code IS NULL OR Code = @Code)
		AND IdStatus <> 25002
END

GO

