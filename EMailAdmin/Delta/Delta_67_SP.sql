USE [EMailAdmin]
GO
/****** Object:  StoredProcedure [dbo].[TemplateType_Tx_Code]    Script Date: 11/15/2013 10:57:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Lorena Cominotti
-- Create date: 12/04/2012
-- Description:	Looks for template types
-- =============================================
ALTER PROCEDURE [dbo].[TemplateType_Tx_Code]
	@Code			VARCHAR(50) = NULL,
	@Prefijo		VARCHAR(50) = NULL
AS

BEGIN
	SELECT *
	FROM
		TemplateType
	WHERE
		(@Code IS NULL OR Code = @Code)
		AND (@Prefijo IS NULL OR (NOT @Prefijo IS NULL AND Code like @Prefijo +'%'))
END
