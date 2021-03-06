USE [EMailAdmin]
GO
/****** Object:  StoredProcedure [dbo].[TableVariableText_A]    Script Date: 06/05/2013 10:48:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Marcela Da Silva
-- Create date: 06/05/2013
-- =============================================
CREATE PROCEDURE [dbo].[TableVariableText_A]
	@Name			VARCHAR(255),
	@IdUser			INT = 0,
	@IdStatus		INT = 25000
AS
BEGIN
	INSERT INTO TableVariableText
	(
		Name
        ,CreationDate
        ,ModifiedDate
        ,IdUser
        ,IdStatus
	)
	VALUES
	(
		@Name,		
		GETDATE(),
		GETDATE(),
		@IdUser,
		@IdStatus
	)
	
	RETURN @@IDENTITY	
END
