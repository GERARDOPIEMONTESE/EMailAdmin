USE [EMailAdmin]
GO
/****** Object:  StoredProcedure [dbo].[ConfigurationValue_A]    Script Date: 14/3/2018 11:17:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ConfigurationValue_A]
	@Code				NVARCHAR(50),
	@Description		NVARCHAR(150),
	@Value				NVARCHAR(255),
	@IdUser				INT,
	@IdStatus			INT	
AS
BEGIN
	INSERT INTO dbo.ConfigurationValue
	(Code, [Description], Value, IdUser, IdStatus)
	VALUES
	(@Code, @Description, @Value, @IdUser, @IdStatus)
	
	RETURN SCOPE_IDENTITY()
END


GO
/****** Object:  StoredProcedure [dbo].[ConfigurationValue_E]    Script Date: 14/3/2018 11:17:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ConfigurationValue_E]
	@IdConfigurationValue	INT,
	@ModificationDate		DATETIME,
	@IdStatus				INT = 25002,
	@IdUser					INT
AS
BEGIN
	UPDATE dbo.ConfigurationValue
		SET
			ModificationDate = @ModificationDate,
			IdUser = @IdUser,
			IdStatus = @IdStatus
		WHERE
			IdConfigurationValue = @IdConfigurationValue

END

GO
/****** Object:  StoredProcedure [dbo].[ConfigurationValue_M]    Script Date: 14/3/2018 11:17:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ConfigurationValue_M]
	@IdConfigurationValue	INT,
	@Code					NVARCHAR(50),
	@Description			NVARCHAR(150),
	@Value					NVARCHAR(255),
	@IdUser					INT,  
	@ModificationDate		DATETIME,
	@IdStatus				INT	
AS
BEGIN
	
	UPDATE dbo.ConfigurationValue		
			SET 
				Code = @Code,
				[Description] = @Description,
				Value = @Value,
				IdStatus = @IdStatus,
				IdUser = @IdUser,
				ModificationDate = @ModificationDate
	 WHERE 
			IdConfigurationValue = @IdConfigurationValue
			
END


GO
/****** Object:  StoredProcedure [dbo].[ConfigurationValue_TT]    Script Date: 14/3/2018 11:17:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ConfigurationValue_TT]
AS
BEGIN
	SELECT * 
	FROM
		dbo.ConfigurationValue
	WHERE
		IdStatus NOT IN (25002)
END

GO
/****** Object:  StoredProcedure [dbo].[ConfigurationValue_Tx_Code]    Script Date: 14/3/2018 11:17:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ConfigurationValue_Tx_Code]
	@Code		NVARCHAR(50)
AS
BEGIN
	SELECT * 
	FROM
		dbo.ConfigurationValue
	WHERE
		Code = @Code AND IdStatus NOT IN (25002)
END


GO
/****** Object:  StoredProcedure [dbo].[ConfigurationValue_Tx_IdConfigurationValue]    Script Date: 14/3/2018 11:17:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ConfigurationValue_Tx_IdConfigurationValue]
	@IdConfigurationValue		INT
AS
BEGIN
	SELECT * 
	FROM
		dbo.ConfigurationValue
	WHERE
		IdConfigurationValue = @IdConfigurationValue
END


GO
/****** Object:  StoredProcedure [dbo].[EMailLog_A]    Script Date: 14/3/2018 11:17:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Lorena Cominotti
-- Mod by:		Yoel Dan Etbul
-- Create date: 04/06/2012
-- Mod date:	04/10/2012
-- Description:	EMailLog add
-- =============================================
ALTER PROCEDURE [dbo].[EMailLog_A]
	@CountryCode				INT,
	@ModuleCode					VARCHAR(50),
	@VoucherCode				VARCHAR(50) = '-',
	@InvokeInformation			VARCHAR(MAX),
	@ZipContextInformation		IMAGE=null,
	@StartDate					DATETIME,
	@ProcessStatus				INT,
	@IdStatus					INT,
	@EndDate					DATETIME = NULL,
	@ErrorMessage				VARCHAR(MAX) = NULL,
	@PaxName					VARCHAR(250) = '',
	@PaxSurname					VARCHAR(250) = '',
	@IssuanceDate				VARCHAR(50) = '',
	@IdLote						INT = NULL
AS
BEGIN
	INSERT INTO
		EMailLog
	(
		CountryCode,
		ModuleCode,
		VoucherCode,
		InvokeInformation,
		ZipContextInformation,
		StartDate,
		ProcessStatus,
		IdStatus,
		EndDate,
		ErrorMessage,
		PaxName,
		PaxSurname,
		IssuanceDate,
		IdLote
	)
	VALUES
	(
		@CountryCode,
		@ModuleCode,
		@VoucherCode,
		@InvokeInformation,
		@ZipContextInformation,
		@StartDate,
		@ProcessStatus,
		@IdStatus,
		@EndDate,
		@ErrorMessage,
		@PaxName,
		@PaxSurname,
		@IssuanceDate,
		@IdLote
	)
	
	RETURN @@IDENTITY
END


GO
/****** Object:  StoredProcedure [dbo].[EMailLog_Tx_Exist]    Script Date: 14/3/2018 11:17:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Marcela Da Silva
-- Create date: 23/02/2018
-- Description:	Chequea si fue enviado el mail
-- =============================================
--exec [EMailLog_Tx_Exist] 5, null,null, 'marcela.dasilva@assistcard.com', null
CREATE PROCEDURE [dbo].[EMailLog_Tx_Exist]
	@IdTemplateType			INT,	
	@CountryCode			INT = null,
	@VoucherCode			VARCHAR(50) = null,
	@Email					VARCHAR(50) = null,
	@ProcessStatus			INT = null, -- Only OK status
	@EndDate					datetime = NULL,
	@PaxName			VARCHAR(50) = null,
	@PaxSurname			VARCHAR(50) = null
AS
BEGIN

declare @sql nvarchar(max),	@params nvarchar(max)

set @sql ='SELECT *	FROM EMailLog l 
	WHERE l.IdTemplateType = @IdTemplateType '

	if not @Email is null
		set @sql = @sql + '  AND l.mailto = @Email '

	if not @CountryCode is null
		set @sql = @sql + '  AND l.CountryCode = @CountryCode '

	if not @VoucherCode is null
		set @sql = @sql + '  AND l.VoucherCode = @VoucherCode '

	if not @ProcessStatus is null
		set @sql = @sql + '  AND l.ProcessStatus = @ProcessStatus '

	if not @EndDate is null
		set @sql = @sql + '  AND l.EndDate >= @EndDate '

	if not @PaxName is null
		set @sql = @sql + '  AND l.PaxName = @PaxName '

	if not @PaxSurname is null
		set @sql = @sql + '  AND l.PaxSurname = @PaxSurname '

	print @sql

	set @params = '@IdTemplateType			INT,	
	@CountryCode			INT = null,
	@VoucherCode			VARCHAR(50) = null,
	@Email					VARCHAR(50) = null,
	@ProcessStatus			INT = null,
	@EndDate					datetime = NULL,
	@PaxName			VARCHAR(50) = null,
	@PaxSurname			VARCHAR(50) = null'

	exec sp_executesql @sql, 
		@params,		
		@IdTemplateType=@IdTemplateType,
		@CountryCode=@CountryCode,
		@VoucherCode=@VoucherCode,
		@Email = @Email,
		@ProcessStatus=@ProcessStatus,
		@EndDate = @EndDate,
		@PaxName = @PaxName,
		@PaxSurname = @PaxSurname
END

GO
/****** Object:  StoredProcedure [dbo].[EMailLogLegal_Tx_Filters]    Script Date: 14/3/2018 11:17:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[EMailLogLegal_Tx_Filters]
	@CountryCode		INT = -1,
	@Vouchercode		VARCHAR(500)='',
	@Email				VARCHAR(500)='',
	@TemplateName		NVARCHAR(500)=''
AS
SET NOCOUNT ON
BEGIN
	SELECT TOP 10000 IdEMailLog, l.CountryCode, l.VoucherCode, l.PaxName + ' ' + l.PaxSurname AS PaxName, 
		l.IssuanceDate AS EmissionDate, StartDate as SentDate,
	MailTo as EMail, ProcessStatus as Process, ErrorMessage, StartDate as ErrorDate, l.TemplateName,
	l.IdLote
	FROM EMailLog l
	WHERE 
		(@CountryCode = -1 OR l.CountryCode = @CountryCode)
		AND (@Vouchercode = '' OR l.VoucherCode = @Vouchercode)
		AND (@Email = '' OR MailTo = @Email)
		AND (@TemplateName = '' OR l.TemplateName = @TemplateName)
	order by l.StartDate desc
END

GO
/****** Object:  StoredProcedure [dbo].[EMailProcessLog_M]    Script Date: 14/3/2018 11:17:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Lorena Cominoti
-- Create date: 07/06/2012
-- Description:	EMailProcessLog
-- =============================================
ALTER PROCEDURE [dbo].[EMailProcessLog_M]
	@IdEMailProcessLog			INT,
	@IdEMailProcessType			INT,
	@StartDate					DATETIME,
	@EndDate					DATETIME,
	@IdLote						INT = NULL
AS
BEGIN
	UPDATE
		EMailProcessLog
	SET
		IdEmailProcessType = @IdEMailProcessType,
		StartDate = @StartDate,
		EndDate = @EndDate,
		IdLote = @IdLote
	WHERE
		IdEMailProcessLog = @IdEMailProcessLog
END

GO
/****** Object:  StoredProcedure [dbo].[EMailProcessLog_Tx_Dates]    Script Date: 14/3/2018 11:17:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Lorena Cominotti
-- Create date: 15/11/2012
-- Description:	EMailProcessLog
-- =============================================
ALTER PROCEDURE [dbo].[EMailProcessLog_Tx_Dates]
	@FromDate			DATETIME,
	@ToDate				DATETIME
AS
BEGIN
	SELECT *
	FROM EMailProcessLog
	WHERE StartDate >= @FromDate
	AND StartDate <= @ToDate
	order by IdEMailProcessLog desc
END

GO
/****** Object:  StoredProcedure [dbo].[EMailProcessLog_Tx_Last_IdEMailProcessType]    Script Date: 14/3/2018 11:17:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Lorena Cominoti
-- Create date: 07/06/2012
-- Description:	EMailProcessLog
-- =============================================
ALTER PROCEDURE [dbo].[EMailProcessLog_Tx_Last_IdEMailProcessType]
	@IdEMailProcessType			INT
AS
BEGIN
	SELECT TOP 1 *
	FROM
		EMailProcessLog
	WHERE
		IdEmailProcessType = @IdEMailProcessType
	ORDER BY
		StartDate DESC
END

GO
/****** Object:  StoredProcedure [dbo].[EMailProcessType_A]    Script Date: 14/3/2018 11:17:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Da Silva Marcela
-- Create date: 09/03/2018
-- Description:	EMailProcessType
-- =============================================
Create PROCEDURE [dbo].[EMailProcessType_A]
	@Code						varchar(10),
	@Description				varchar(50),
	@Period						INT = 0,
	@PeriodHours				varchar(5000) = null,
	@CheckLote					BIT = 0,
	@IdUser						INT,
	@IdStatus					INT
AS
BEGIN
	INSERT INTO
		EMailProcessType
	(
		Code,
		[Description],
		Period,
		PeriodHours,
		CheckLote,
		IdUser,
		IdStatus,
		CreationDate
	)
	VALUES
	(
		@Code,
		@Description,
		@Period,
		@PeriodHours,
		@CheckLote,
		@IdUser,
		@IdStatus,
		GETDATE()
	)
	
	RETURN @@IDENTITY
END

GO
/****** Object:  StoredProcedure [dbo].[EMailProcessType_M]    Script Date: 14/3/2018 11:17:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Da Silva Marcela
-- Create date: 09/03/2018
-- Description:	EMailProcessType
-- =============================================
CREATE PROCEDURE [dbo].[EMailProcessType_M]
	@IdEmailProcessType			INT,
	@Code						varchar(10),
	@Description				varchar(50),
	@Period						INT = 0,
	@PeriodHours				varchar(5000) = null,
	@CheckLote					BIT = 0,
	@IdUser						INT,
	@IdStatus					INT
AS
BEGIN
	UPDATE dbo.EMailProcessType		
			SET 
				Code = @Code,
				[Description] = @Description,
				Period = @Period,
				PeriodHours = @PeriodHours,
				CheckLote = @CheckLote,
				IdUser = @IdUser,
				IdStatus = @IdStatus,
				ModifiedDate = GETDATE()
	 WHERE 
			IdEmailProcessType = @IdEmailProcessType
END

GO
/****** Object:  StoredProcedure [dbo].[Link_Tx_NameUrl]    Script Date: 14/3/2018 11:17:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Lorena Cominotti
-- Create date: 17/04/2012
-- Description:	Link type by id
-- EXEC [dbo].[Link_Tx_NameUrl] 'visa','', 25002
-- =============================================
ALTER PROCEDURE [dbo].[Link_Tx_NameUrl]
	@Name					VARCHAR(50) = NULL,
	@Url					VARCHAR(150) = NULL,
	@IdDeleteStatus			INT = 25002
AS
BEGIN
	SELECT 
		 [IdLink]
		,Link.IdLinkType
		,[Name]
		,[Url]
		,[IdUser]
		,[CreationDate]
		,[ModifiedDate]
		,[IdStatus]
	FROM
		Link
	WHERE
		((@name is null or Name LIKE '%'+@Name+'%')
	AND
		(@url is null or  Url LIKE '%'+@Url+'%'))
	AND
		IdStatus <> @IdDeleteStatus
	AND 
		IdLinkType <> 2
END



GO
/****** Object:  StoredProcedure [dbo].[Pixel_A]    Script Date: 14/3/2018 11:17:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Marcela Da Silva
-- Create date: 7/03/2018
-- =============================================
create PROCEDURE [dbo].[Pixel_A]
	@Name					VARCHAR(50),
	@jsonContenido			varchar(max),
	@IdUser					INT,
	@IdStatus				INT
AS
BEGIN
	INSERT INTO
		Pixel
	(
		Name,
		jsonContenido,
		IdUser,
		CreationDate,
		ModifiedDate,
		IdStatus
	)
	VALUES
	(
		@Name,
		@jsonContenido,
		@IdUser,
		GETDATE(),
		GETDATE(),
		@IdStatus
	)
	
	RETURN @@IDENTITY
END

GO
/****** Object:  StoredProcedure [dbo].[Pixel_E]    Script Date: 14/3/2018 11:17:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Marcela Da Silva
-- Create date: 7/03/2018
-- =============================================
create PROCEDURE [dbo].[Pixel_E]
	@IdPixel				INT,
	@IdUser					INT,
	@IdStatus				INT
AS
BEGIN
	UPDATE
		Pixel
	SET
		IdUser = @IdUser,
		ModifiedDate = GETDATE(),
		IdStatus = @IdStatus
	WHERE
		IdPixel = @IdPixel
END

GO
/****** Object:  StoredProcedure [dbo].[Pixel_M]    Script Date: 14/3/2018 11:17:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Marcela Da Silva
-- Create date: 7/03/2018
-- =============================================
create PROCEDURE [dbo].[Pixel_M]
	@IdPixel				INT,
	@Name					VARCHAR(50),
	@jsonContenido			varchar(max),
	@IdUser					INT,
	@IdStatus				INT
AS
BEGIN
	UPDATE
		Pixel
	SET
		Name = @Name,
		jsonContenido = @jsonContenido,
		IdUser = @IdUser,
		ModifiedDate = GETDATE(),
		IdStatus = @IdStatus
	WHERE
		IdPixel = @IdPixel
END

GO
/****** Object:  StoredProcedure [dbo].[Pixel_Tx_IdPixel]    Script Date: 14/3/2018 11:17:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Marcela Da Silva
-- Create date: 7/03/2018
-- =============================================
create PROCEDURE [dbo].[Pixel_Tx_IdPixel]
	@IdPixel				INT
AS
BEGIN
	SELECT *
	FROM
		Pixel
	WHERE
		IdPixel = @IdPixel
END

GO
/****** Object:  StoredProcedure [dbo].[Pixel_Tx_Name]    Script Date: 14/3/2018 11:17:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Marcela Da Silva
-- Create date: 7/03/2018
-- =============================================
create PROCEDURE [dbo].[Pixel_Tx_Name]
	@Name					VARCHAR(50) = NULL,
	@IdDeleteStatus			INT = 25002
AS
BEGIN
	SELECT *
	FROM
		Pixel
	WHERE
		(@Name IS NULL OR Name = @Name)
	AND
		IdStatus <> @IdDeleteStatus
END

GO
