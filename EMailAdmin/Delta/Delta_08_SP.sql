USE [EMailAdmin]
GO

/****** Object:  StoredProcedure [dbo].[EMailListType_Tx_Filters]    Script Date: 03/06/2013 16:42:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Da Silva Marcela
-- Create date: 27/02/2013
-- =============================================
ALTER PROCEDURE [dbo].[EMailListType_Tx_Filters]
	@Description varchar(100) = '',
	@Code		VARCHAR(10) = ''
AS 
BEGIN
	SELECT *
	FROM 
		EMailListType
	WHERE
		IdStatus <> 25002
		AND ((@Description = '') or ([Description] like '%' + @Description + '%'))
		AND ((@Code = '') or (Code = @Code))
END

