USE [EMailAdmin]
GO

/****** Object:  StoredProcedure [dbo].[EMailList_L_A]    Script Date: 03/06/2013 16:40:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Da Silva Marcela
-- Create date: 05/03/2013
-- =============================================
CREATE PROCEDURE [dbo].[EMailList_L_A]
	@IdEmailList		INT,
	@IdEmailListType	INT,
	@IdLocacion			INT,
	@IdUsuario			INT,
	@IdUser				INT,
	@IdStatus			INT
AS
BEGIN
	INSERT INTO EMailList_L
	(
		IdEmailList,
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
		@IdEmailList,
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

