USE [EMailAdmin]
GO
/****** Object:  StoredProcedure [dbo].[ContentAttachment_A]    Script Date: 08/05/2017 12:30:35 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Da Silva Marcela
-- Create date: 03/05/2017
-- =============================================
CREATE PROCEDURE [dbo].[ContentAttachment_A]
	@IdTemplate int,
	@IdAttachment int,
	@IdLanguage int,
	@CodeRPT varchar(20),
	@Body varchar(max),
	@IdUser int,
	@IdStatus int
AS
BEGIN
	INSERT INTO	ContentAttachment
	(
	IdTemplate,
	IdAttachment,
	IdLanguage,
	Body,
	CodeRPT,	
	CreationDate,
	ModifiedDate,
	IdUser,
	IdStatus
	)
	VALUES
	(
	@IdTemplate,
	@IdAttachment,
	@IdLanguage,
	@Body,
	@CodeRPT,	
	GETDATE(),
	GETDATE(),
	@IdUser,
	@IdStatus
	)
	
	RETURN @@IDENTITY	
END

GO
/****** Object:  StoredProcedure [dbo].[ContentAttachment_By_TemplateAttach]    Script Date: 08/05/2017 12:30:35 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Da Silva Marcela
-- Create date: 26/04/2017
-- =============================================
CREATE PROCEDURE [dbo].[ContentAttachment_By_TemplateAttach]
	@IdAttachment		INT,
	@IdTemplate			INT,
	@IdLanguage			INT = null,
	@CodeRPT			varchar(20) = null
AS
BEGIN
	SELECT
		*
	FROM
		ContentAttachment
	WHERE
	IdStatus = 25000
	and IdTemplate = @IdTemplate
	and IdAttachment = @IdAttachment
	and (@IdLanguage is null or IdLanguage = @IdLanguage)
	and (@CodeRPT is null or CodeRPT = @CodeRPT)
		
END

GO
/****** Object:  StoredProcedure [dbo].[ContentAttachment_E]    Script Date: 08/05/2017 12:30:35 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Da Silva Marcela
-- Create date: 03/05/2017
-- =============================================
CREATE PROCEDURE [dbo].[ContentAttachment_E]
	@IdContentAttachment int,
	@IdUser int,
	@IdStatus int
AS
BEGIN
	update ContentAttachment
	set	
		ModifiedDate = GETDATE(),
		IdUser = @IdUser,
		IdStatus = @IdStatus
	where
		IdContentAttachment = @IdContentAttachment

END

GO
/****** Object:  StoredProcedure [dbo].[ContentAttachment_M]    Script Date: 08/05/2017 12:30:35 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Da Silva Marcela
-- Create date: 03/05/2017
-- =============================================
CREATE PROCEDURE [dbo].[ContentAttachment_M]
	@IdContentAttachment int,
	@CodeRPT varchar(20),
	@Body varchar(max),
	@IdUser int,
	@IdStatus int
AS
BEGIN
	update ContentAttachment
	set	
	Body = @Body,	
	CodeRPT = @CodeRPT,
	ModifiedDate = GETDATE(),
	IdUser = @IdUser,
	IdStatus = @IdStatus
	where
	IdContentAttachment = @IdContentAttachment

END

GO