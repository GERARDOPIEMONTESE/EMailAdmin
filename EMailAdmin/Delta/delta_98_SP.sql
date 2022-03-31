SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:	María de los Ángeles Fortelli
-- Create date: 23/07/2014
-- Description: Link by Name and URL
-- =============================================
CREATE PROCEDURE [dbo].[Link_Tx_NameUrl]
	@Name					VARCHAR(50) = NULL,
	@Url					VARCHAR(150) = NULL,
	@IdDeleteStatus				INT = 25002
AS
BEGIN
	SELECT 
		 [IdLink]
		,[IdLinkType]
		,[Name]
		,[Url]
		,[IdUser]
		,[CreationDate]
		,[ModifiedDate]
		,[IdStatus]
	FROM
		Link
	WHERE
		(Name LIKE '%'+@Name+'%'
	AND
		 Url LIKE '%'+@Url+'%')
	AND
		IdStatus <> @IdDeleteStatus
	AND 
		IdLinkType <> 2
END



GO