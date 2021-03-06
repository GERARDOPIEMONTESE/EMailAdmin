USE [EMailAdmin]
GO
/****** Object:  StoredProcedure [dbo].[Template_A]    Script Date: 30/03/2017 02:53:09 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Lorena Cominotti
-- Create date: 12/04/2012
-- Description:	Template creation
-- =============================================
ALTER PROCEDURE [dbo].[Template_A]
	@Name				VARCHAR(150),
	@IdTemplateType		INT,
	@Hierarchy			INT,
	@IdModule			INT,
	@EffectiveStartDate	DATETIME,
	@EffectiveEndDate	DATETIME,
	@IdEMailFromAddress	INT,
	@IdUser				INT,
	@IdStatus			INT,
	@MergeAttachsWithEKit bit = 0,
	@IdTemplatePDF int = null
AS
BEGIN
	INSERT INTO
		Template
	(
		Name,
		IdTemplateType,
		Hierarchy,
		IdModule,
		EffectiveStartDate,
		EffectiveEndDate,
		IdEMailFromAddress,
		IdUser,
		CreationDate,
		ModifiedDate,
		IdStatus,
		MergeAttachsWithEKit,
		IdTemplatePDF
	)
	VALUES
	(
		@Name,
		@IdTemplateType,
		@Hierarchy,
		@IdModule,
		@EffectiveStartDate,
		@EffectiveEndDate,
		@IdEMailFromAddress,
		@IdUser,
		GETDATE(),
		GETDATE(),
		@IdStatus,
		@MergeAttachsWithEKit,
		@IdTemplatePDF
	)
	
	RETURN @@IDENTITY	
END
