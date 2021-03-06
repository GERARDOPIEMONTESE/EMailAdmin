USE [EMailAdmin]
GO
/****** Object:  StoredProcedure [dbo].[TableVariableTextContent_A]    Script Date: 06/05/2013 10:52:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Marcela Da Silva
-- Create date: 06/05/2013
-- =============================================
CREATE PROCEDURE [dbo].[TableVariableTextContent_A]
	@IdTableText	int,
	@IdLanguage		int,
	@ContentText	VARCHAR(255),
	@IdUser			INT = 0,
	@IdStatus		INT = 25000
AS
BEGIN
	INSERT INTO TableVariableTextContent
	(
		IdTableVariableText,
		IdLanguage
       ,ContentText
       ,CreationDate
       ,ModifiedDate
       ,IdUser
       ,IdStatus
	)
	VALUES
	(
		@IdTableText,
		@IdLanguage,
        @ContentText,
		GETDATE(),
		GETDATE(),
		@IdUser,
		@IdStatus
	)
	
	RETURN @@IDENTITY	
END
