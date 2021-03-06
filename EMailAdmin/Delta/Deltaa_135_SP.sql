USE [EMailAdmin]
GO
/****** Object:  StoredProcedure [dbo].[Template_L_A]    Script Date: 30/03/2017 02:54:03 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Lorena Cominotti
-- Create date: 12/04/2012
-- Description:	Template creation
-- =============================================
ALTER PROCEDURE [dbo].[Template_L_A]
	@IdTemplate			INT,
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
		Template_L
	(
		IdTemplate,
		Name,
		IdTemplateType,
		Hierarchy,
		IdModule,
		EffectiveStartDate,
		EffectiveEndDate,
		IdEMailFromAdrress,
		IdUser,
		CreationDate,
		ModifiedDate,
		IdStatus,
		MergeAttachsWithEKit,
		IdTemplatePDF
	)
	VALUES
	(
		@IdTemplate,
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
