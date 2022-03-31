begin tran
GO

set xact_abort on
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

USE [EMailAdmin]
GO

ALTER PROCEDURE [dbo].[EMailLog_A]
	@CountryCode				INT,
	@ModuleCode					VARCHAR(50),
	@VoucherCode				VARCHAR(50) = '-',
	@InvokeInformation			VARCHAR(MAX),
	@StartDate					DATETIME,
	@ProcessStatus				INT,
	@IdStatus					INT,
	@EndDate					DATETIME = NULL,
	@ErrorMessage				VARCHAR(MAX) = NULL
AS
BEGIN
	INSERT INTO
		EMailLog
	(
		CountryCode,
		ModuleCode,
		VoucherCode,
		InvokeInformation,
		StartDate,
		ProcessStatus,
		IdStatus,
		EndDate,
		ErrorMessage,
		[Receive]
	)
	VALUES
	(
		@CountryCode,
		@ModuleCode,
		@VoucherCode,
		@InvokeInformation,
		@StartDate,
		@ProcessStatus,
		@IdStatus,
		@EndDate,
		@ErrorMessage,
		0
	)
	
	RETURN @@IDENTITY
END

GO

ALTER PROCEDURE [dbo].[EMailLog_M]
	@IdEMailLog				INT,
	@CountryCode			INT,
	@ModuleCode				VARCHAR(50),
	@VoucherCode			VARCHAR(50) = '-',
	@InvokeInformation		VARCHAR(MAX),
	@MailTo					VARCHAR(250),
	@StartDate				DATETIME,
	@EndDate				DATETIME,
	@ErrorMessage			VARCHAR(MAX),
	@ProcessStatus			INT,
	@IdStatus				INT,
	@Receive				BIT = 0,
	@ReceiveDate			DATETIME = NULL
AS
BEGIN
	UPDATE
		EMailLog
	SET
		CountryCode = @CountryCode,
		ModuleCode = @ModuleCode,
		VoucherCode = @VoucherCode,
		InvokeInformation = @InvokeInformation,
		MailTo = @MailTo,
		StartDate = @StartDate,
		EndDate = @EndDate,
		ErrorMessage = @ErrorMessage,
		ProcessStatus = @ProcessStatus,
		IdStatus = @IdStatus,
		Receive = @Receive,
		ReceiveDate = @ReceiveDate
	WHERE
		IdEMailLog = @IdEMailLog
END

GO

ALTER PROCEDURE [dbo].[EMailLog_Tx_Filters]
	@CountryCode			INT,
	@VoucherCode			VARCHAR(50),
	@IdTemplateType			INT,
	@ProcessStatus			INT = 100 -- Only OK status
AS
BEGIN
	SELECT *
	FROM
		EMailLog l
	LEFT JOIN EMailLogMessage m ON l.CountryCode = m.CountryCode and l.VoucherCode = m.VoucherCode and l.ModuleCode = m.ModuleCode
	WHERE
		l.CountryCode = @CountryCode
	AND
		l.VoucherCode = @VoucherCode
	AND
		IdTemplateType = @IdTemplateType
	AND
		ProcessStatus = @ProcessStatus
END

GO

ALTER PROCEDURE [dbo].[EMailLog_Tx_Zip]
AS
BEGIN
	SELECT TOP 2500 *
	FROM EMailLogMessage
	WHERE 
		ContextInformation IS NOT NULL 
	AND  
		ContextInformation.exist('/VOUCHERALL') = 1
	AND 
		ZipContextInformation IS NULL
END

GO

ALTER PROCEDURE [dbo].[EMailLogLegal_Tx_Filters]
@CountryCode		INT,
@Vouchercode		VARCHAR(500)='',
@Email				VARCHAR(500)=''
AS
SET NOCOUNT ON
BEGIN
	SELECT IdEMailLog, l.CountryCode, l.VoucherCode, PaxName + ' ' + PaxSurname AS PaxName, 
		IssuanceDate AS EmissionDate, StartDate as SentDate,
	MailTo as EMail, ProcessStatus as Process, ErrorMessage, StartDate as ErrorDate, TemplateName
	FROM EMailLog l
	LEFT JOIN EMailLogMessage m on l.CountryCode = m.CountryCode and l.VoucherCode = m.VoucherCode and l.ModuleCode = m.ModuleCode
	WHERE l.CountryCode = @CountryCode
	AND (@Vouchercode = '' OR l.VoucherCode = @Vouchercode)
	AND (@Email = '' OR MailTo = @Email)
END

GO

ALTER PROCEDURE [dbo].[EMailLogMessage_A]	
	@CountryCode			INT,
	@ModuleCode				VARCHAR(50),
	@VoucherCode			VARCHAR(50) = '-',
	@IdTemplate				INT,
	@IdTemplateType			INT,
	@TemplateName			VARCHAR(150),	
	@MailFrom				VARCHAR(250),
	@Subject				VARCHAR(50),
	@Body					VARCHAR(MAX),
	@AttachmentIds			VARCHAR(50),
	@ZipContextInformation	IMAGE=null,
	@PaxName				VARCHAR(250) = '',
	@PaxSurname				VARCHAR(250) = '',
	@IssuanceDate			VARCHAR(50) = ''
AS
BEGIN
	INSERT INTO
		EMailLogMessage
	(
		CountryCode,
		ModuleCode,
		VoucherCode,
		IdTemplate, 
		IdTemplateType,
		TemplateName,
		MailFrom,
		Subject,
		Body,
		AttachmentIds,
		ZipContextInformation,
		PaxName,
		PaxSurname,
		IssuanceDate
	)
	VALUES
	(
		@CountryCode,
		@ModuleCode,
		@VoucherCode,
		@IdTemplate,
		@IdTemplateType,
		@TemplateName,
		@MailFrom,
		@Subject,
		@Body,
		@AttachmentIds,
		@ZipContextInformation,
		@PaxName,
		@PaxSurname,
		@IssuanceDate
	)
	
	RETURN @@IDENTITY
END
GO

CREATE PROCEDURE [dbo].[EMailLogMessage_M]
	@IdEmailLogMessage		INT,
	@IdTemplate				INT,
	@IdTemplateType			INT,
	@TemplateName			VARCHAR(150),
	@CountryCode			INT,
	@ModuleCode				VARCHAR(50),
	@VoucherCode			VARCHAR(50) = '-',
	@ContextInformation		XML,
	@ZipContextInformation	IMAGE=null,
	@MailFrom				VARCHAR(250),
	@Subject				VARCHAR(50),
	@Body					VARCHAR(MAX),
	@AttachmentIds			VARCHAR(50),
	@PaxName				VARCHAR(250) = '',
	@PaxSurname				VARCHAR(250) = '',
	@IssuanceDate			VARCHAR(50) = ''
AS
BEGIN
	UPDATE
		EMailLogMessage
	SET
		IdTemplate = @IdTemplate,
		IdTemplateType = @IdTemplateType,
		TemplateName = @TemplateName,
		CountryCode = @CountryCode,
		ModuleCode = @ModuleCode,
		VoucherCode = @VoucherCode,
		ContextInformation = @ContextInformation,
		ZipContextInformation = @ZipContextInformation,
		MailFrom = @MailFrom,
		Subject = @Subject,
		Body = @Body,
		AttachmentIds = @AttachmentIds,
		PaxName = @PaxName,
		PaxSurname = @PaxSurname,
		IssuanceDate = @IssuanceDate
	WHERE
		IdEMailLogMessage = @IdEMailLogMessage
END

GO

CREATE PROCEDURE [dbo].[EMailLogMessage_Tx_Filters]
	@CountryCode			INT,
	@VoucherCode			VARCHAR(50),
	@IdTemplateType			INT
AS
BEGIN
	SELECT *
	FROM
		EMailLogMessage
	WHERE
		CountryCode = @CountryCode
	AND
		VoucherCode = @VoucherCode
	AND
		IdTemplateType = @IdTemplateType
END

GO

CREATE PROCEDURE [dbo].[EMailLogMessage_Tx_FiltersModule]
	@CountryCode			INT,
	@VoucherCode			VARCHAR(50),
	@ModuleCode				VARCHAR(50)
AS
BEGIN
	SELECT *
	FROM
		EMailLogMessage
	WHERE
		CountryCode = @CountryCode
	AND
		VoucherCode = @VoucherCode
	AND
		ModuleCode = @ModuleCode
END
GO

CREATE PROCEDURE [dbo].[EMailLogMessage_Tx_IdEMailLogMessage]
	@IdEMailLogMessage			INT
AS
BEGIN
	SELECT *
	FROM
		EMailLogMessage
	WHERE
		IdEMailLogMessage = @IdEMailLogMessage
END

GO

CREATE PROCEDURE [dbo].[EMailLogMessage_Tx_Zip]
AS
BEGIN
	SELECT TOP 2500 *
	FROM EMailLogMessage
	WHERE 
		ContextInformation IS NOT NULL 
	AND  
		ContextInformation.exist('/VOUCHERALL') = 1
	AND 
		ZipContextInformation IS NULL
END
GO

COMMIT TRAN