USE [EMailAdmin]
GO
/****** Object:  StoredProcedure [dbo].[EmailLog_R_PrepurchasePax_A]    Script Date: 11/15/2013 15:16:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Da Silva Marcela
-- Create date: 14/11/2013
-- Description:	EMailLog relacionados con precompra de dias  add
-- =============================================
Create PROCEDURE [dbo].[EmailLog_R_PrepurchasePax_A]
	@IdEmailLog				INT,
	@CodigoPaxBox			INT,
	@CodigoVerif			VARCHAR(255) = NULL,
	@VoucherGroup			VARCHAR(255) = NULL,
	@CountryCode			INT = NULL
AS
BEGIN
	INSERT INTO
		EmailLog_R_PrepurchasePax
	(
		IdEmailLog,
		CodigoPaxBox,
		CodigoVerif,
		VoucherGroup,
		CountryCode
	)
	VALUES
	(
		@IdEmailLog,
		@CodigoPaxBox,
		@CodigoVerif,
		@VoucherGroup,
		@CountryCode
	)
	
	RETURN @@IDENTITY
END

