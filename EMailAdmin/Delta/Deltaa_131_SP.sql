USE [EMailAdmin]
GO
/****** Object:  StoredProcedure [dbo].[Content_M]    Script Date: 14/03/2017 05:29:09 p.m. ******/
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
ALTER PROCEDURE [dbo].[Content_M]
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
	UPDATE
		Content
	SET
		[Subject] = @Subject,
		Body = @Body,
		IdHeader = @IdHeader,
		IdFooter = @IdFooter,
		IdHeaderPDF = @IdHeaderPDF,
		IdFooterPDF = @IdFooterPDF,
		IdUser = @IdUser,
		ModifiedDate = GETDATE(),
		IdStatus = @IdStatus,
		color = @Color
	WHERE
		IdTemplate = @IdTemplate AND
		IdLanguage = @IdLanguage
END
