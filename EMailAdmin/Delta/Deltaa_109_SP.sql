USE [EMAILADMIN]
GO
/****** Object:  StoredProcedure [dbo].[Template_R_Attachment_Tx_Filters]    Script Date: 19/01/2015 10:58:46 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Yoel Dan Etbul
-- Create date: 13/04/2012
-- Mod: Da Silva Marcela - 2015-01-19
-- =============================================
ALTER PROCEDURE [dbo].[Template_R_Attachment_Tx_Filters]
	@IdTemplate_R_Attachment	INT = null,
	@IdTemplate					INT = null,
	@IdAttachment				INT = null
AS
BEGIN
	SELECT 
		Template_R_Attachment.* 
	FROM 
		Template_R_Attachment
		inner join Attachment on Attachment.IdAttachment = Template_R_Attachment.IdAttachment
	WHERE
		Template_R_Attachment.idstatus = 25000 and Attachment.IdStatus = 25000 and
		((@IdTemplate is null) or (@IdTemplate is not null and Template_R_Attachment.IdTemplate = @IdTemplate)) and
		((@IdAttachment is null) or (@IdAttachment is not null and Template_R_Attachment.IdAttachment = @IdAttachment)) and
		((@IdTemplate_R_Attachment is null) or (@IdTemplate_R_Attachment is not null and Template_R_Attachment.IdTemplate_R_Attachment = @IdTemplate_R_Attachment))

END
