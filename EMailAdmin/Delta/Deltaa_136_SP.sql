USE [EMailAdmin]
GO
/****** Object:  StoredProcedure [dbo].[Template_M]    Script Date: 30/03/2017 02:54:42 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Lorena Cominotti
-- Create date: 12/04/2012
-- Description:	Template creation
-- =============================================
ALTER PROCEDURE [dbo].[Template_M]
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
	@MergeAttachsWithEKit bit =0,
	@IdTemplatePDF int = null
AS
BEGIN
	UPDATE
		Template
	SET
		Name = @Name,
		IdTemplateType = @IdTemplateType,
		Hierarchy = @Hierarchy,
		IdModule = @IdModule,
		EffectiveStartDate = @EffectiveStartDate,
		EffectiveEndDate = @EffectiveEndDate,
		IdEMailFromAddress = @IdEMailFromAddress,
		IdUser = @IdUser,
		ModifiedDate = GETDATE(),
		IdStatus = @IdStatus,
		MergeAttachsWithEKit = @MergeAttachsWithEKit,
		IdTemplatePDF = @IdTemplatePDF
	WHERE
		IdTemplate = @IdTemplate
END
