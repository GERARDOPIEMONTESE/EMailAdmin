USE [EMailAdmin]
GO
/****** Object:  StoredProcedure [dbo].[Content_A]    Script Date: 14/03/2017 05:27:15 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Lorena Cominotti
-- Modified by: Yoel Dan Etbul
-- Create date: 12/04/2012
-- Mod date:	10/10/2012
-- Description:	Content creation
-- =============================================
ALTER PROCEDURE [dbo].[Content_A]
	@IdTemplate				INT,
	@IdLanguage				INT,
	@Subject				VARCHAR(250),
	@Body					VARCHAR(MAX),
	@IdHeader				INT,
	@IdFooter				INT,
	@IdHeaderPDF			INT,
	@IdFooterPDF			INT,
	@IdUser					INT,
	@IdStatus				INT,
	@Color					varchar(20) = NULL
AS
BEGIN
	INSERT INTO
		Content
	(
		IdTemplate,
		IdLanguage,
		Subject,
		Body,
		IdHeader,
		IdFooter,
		IdHeaderPDF,
		IdFooterPDF,
		IdUser,
		CreationDate,
		ModifiedDate,
		IdStatus,
		Color
	)
	VALUES
	(
		@IdTemplate,
		@IdLanguage,
		@Subject,
		@Body,
		@IdHeader,
		@IdFooter,
		@IdHeaderPDF,
		@IdFooterPDF,
		@IdUser,
		GETDATE(),
		GETDATE(),
		@IdStatus,
		@Color
	)
	
	RETURN @@IDENTITY
END
