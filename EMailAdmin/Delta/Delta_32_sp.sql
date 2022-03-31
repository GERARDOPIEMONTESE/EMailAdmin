USE [EMailAdmin]
GO
/****** Object:  StoredProcedure [dbo].[TableVariableText_Tx_Filters]    Script Date: 06/10/2013 09:10:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Marcela Da Silva
-- Create date: 06/05/2013
-- =============================================
CREATE PROCEDURE [dbo].[TableVariableText_Tx_Filters]
	@NAME varchar(255)=NULL
AS
BEGIN
	SELECT * 
	FROM 
		TableVariableText
	WHERE
		IdStatus <> 25002 and
		(@NAME IS NULL OR Name = @NAME)
END
