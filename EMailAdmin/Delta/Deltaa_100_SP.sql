USE [EMailAdmin]
GO
/****** Object:  StoredProcedure [dbo].[VariableText_A]    Script Date: 07/18/2014 16:05:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Lorena Cominotti
-- Create date: 13/04/2012
-- Description:	Variable text creation
-- =============================================
ALTER PROCEDURE [dbo].[VariableText_A]
	@Name				VARCHAR(150),
	@IdVariableTextType INT,
	@IdUser				INT,
	@IdStatus			INT
AS
BEGIN
	INSERT INTO
		VariableText
	(
		Name,
		IdVariableTextType,
		IdUser,
		CreationDate,
		ModifiedDate,
		IdStatus
	)
	VALUES
	(
		@Name,
		@IdVariableTextType,
		@IdUser,
		GETDATE(),
		GETDATE(),
		@IdStatus
	)
	
	RETURN @@IDENTITY
END
