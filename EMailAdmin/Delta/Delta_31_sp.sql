USE [EMailAdmin]
GO
/****** Object:  StoredProcedure [dbo].[TableVariableTextContent_Tx_IdTableVariableTextContent]    Script Date: 06/05/2013 11:02:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Marcela Da Silva
-- Create date: 06/05/2013
-- =============================================
Create PROCEDURE [dbo].[TableVariableTextContent_Tx_IdTableVariableTextContent]
	@IdTableVariableText INT = -1
AS
BEGIN
	SELECT * 
	FROM TableVariableTextContent		
	WHERE
		IdStatus <> 25002 and
		((@IdTableVariableText = -1) or (@IdTableVariableText <> -1 and IdTableVariableText = @IdTableVariableText)) 
END
