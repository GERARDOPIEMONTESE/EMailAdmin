USE [EMailAdmin]
GO

/****** Object:  StoredProcedure [dbo].[EMailList_A]    Script Date: 03/06/2013 16:40:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Da Silva Marcela
-- Create date: 01/03/2013
-- =============================================
CREATE PROCEDURE [dbo].[EMailList_A]
	@IdEmailListType	INT,
	@IdLocacion			INT,
	@IdUsuario			INT,
	@IdUser				INT,
	@IdStatus			INT
AS
BEGIN
	INSERT INTO EMailList
	(
		IdEmailListType,
		IdLocacion,
		IdUsuario,
		IdUser,
		CreationDate,
		ModifiedDate,
		IdStatus
	)
	VALUES
	(
		@IdEmailListType,
		@IdLocacion,
		@IdUsuario,
		@IdUser,
		GETDATE(),
		GETDATE(),
		@IdStatus
	)
	
	RETURN @@IDENTITY	
END

GO

