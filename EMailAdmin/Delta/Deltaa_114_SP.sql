USE [EMailAdmin]
GO
/****** Object:  StoredProcedure [dbo].[EMailListType_E]    Script Date: 15/10/2015 11:24:09 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Da Silva Marcela
-- Create date: 15/10/2015
-- =============================================
create PROCEDURE [dbo].[EMailListExclude_M]
	@IdEMailListExclude int,
	@CountryCode int,
	@AgencyCode varchar(5),
	@BranchNumber int,
	@IdUser int,
	@IdStatus int
AS
BEGIN
	UPDATE
		EMailListExclude
	SET 
		AgencyCode = @AgencyCode,
		BranchNumber = @BranchNumber,
		CountryCode = @CountryCode,
		ModifiedDate = GETDATE(),
		IdUser = @IdUser,
		IdStatus = @IdStatus
	WHERE 
		IdEMailListExclude = @IdEMailListExclude

END
