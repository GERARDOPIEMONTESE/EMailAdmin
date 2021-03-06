USE [EMailAdmin]
GO
/****** Object:  StoredProcedure [dbo].[Link_A]    Script Date: 30/03/2016 12:16:57 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Lorena Cominotti
-- Create date: 17/04/2012
-- Description:	Link type by id
-- =============================================
ALTER PROCEDURE [dbo].[Link_A]
	@IdLinkType				INT,
	@Name					VARCHAR(50),
	@Url					VARCHAR(300),
	@IdUser					INT,
	@IdStatus				INT
AS
BEGIN
	INSERT INTO
		Link
	(
		IdLinkType,
		Name,
		Url,
		IdUser,
		CreationDate,
		ModifiedDate,
		IdStatus
	)
	VALUES
	(
		@IdLinkType,
		@Name,
		@Url,
		@IdUser,
		GETDATE(),
		GETDATE(),
		@IdStatus
	)
	
	RETURN @@IDENTITY
END


GO
/****** Object:  StoredProcedure [dbo].[Link_M]    Script Date: 30/03/2016 12:17:06 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Lorena Cominotti
-- Create date: 17/04/2012
-- Description:	Link type by id
-- =============================================
ALTER PROCEDURE [dbo].[Link_M]
	@IdLink					INT,
	@IdLinkType				INT,
	@Name					VARCHAR(50),
	@Url					VARCHAR(300),
	@IdUser					INT,
	@IdStatus				INT
AS
BEGIN
	UPDATE
		Link
	SET
		IdLinkType = @IdLinkType,
		Name = @Name,
		Url = @Url,
		IdUser = @IdUser,
		ModifiedDate = GETDATE(),
		IdStatus = @IdStatus
	WHERE
		IdLink = @IdLink
END
