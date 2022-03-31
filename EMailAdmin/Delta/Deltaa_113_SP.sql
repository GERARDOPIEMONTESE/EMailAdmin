USE [EMailAdmin]
GO

/****** Object:  StoredProcedure [dbo].[EMailList_A]    Script Date: 15/10/2015 11:18:04 a.m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Da Silva Marcela
-- Create date: 15/10/2015
-- =============================================
CREATE PROCEDURE [dbo].[EMailListExclude_A]
	@CountryCode int,
	@AgencyCode varchar(5),
	@BranchNumber int,
	@IdUser int,
	@IdStatus int
AS
BEGIN
	INSERT INTO EMailListExclude
	(
		CountryCode,
		AgencyCode,
		BranchNumber,
		IdUser,
		IdStatus,
		CreationDate
	)
	VALUES
	(
		@CountryCode,
		@AgencyCode,
		@BranchNumber,
		@IdUser,
		@IdStatus,
		GETDATE()
	)
	
	RETURN(SCOPE_IDENTITY());	
END

GO


