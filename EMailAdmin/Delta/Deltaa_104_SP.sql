USE [EMailAdmin]
GO
/****** Object:  StoredProcedure [dbo].[EMailLogLegal_Tx_Filters]    Script Date: 09/08/2014 08:58:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--exec EMailLogLegal_Tx_Filters 0,'','','Mail de Casos'
ALTER PROCEDURE [dbo].[EMailLogLegal_Tx_Filters]
@CountryCode		INT = -1,
@Vouchercode		VARCHAR(500)='',
@Email				VARCHAR(500)='',
@TemplateName		NVARCHAR(500)=''
AS
SET NOCOUNT ON
BEGIN
	SELECT IdEMailLog, l.CountryCode, l.VoucherCode, l.PaxName + ' ' + l.PaxSurname AS PaxName, 
		l.IssuanceDate AS EmissionDate, StartDate as SentDate,
	MailTo as EMail, ProcessStatus as Process, ErrorMessage, StartDate as ErrorDate, l.TemplateName
	FROM EMailLog l
	LEFT JOIN EMailLogMessage m on l.CountryCode = m.CountryCode and l.VoucherCode = m.VoucherCode and l.ModuleCode = m.ModuleCode
	WHERE 
	(@CountryCode = -1 OR l.CountryCode = @CountryCode)
	AND (@Vouchercode = '' OR l.VoucherCode = @Vouchercode)
	AND (@Email = '' OR MailTo = @Email)
	AND (@TemplateName = '' OR l.TemplateName = @TemplateName)
/*DECLARE @TSQL VARCHAR(MAX), @ACNetQuery VARCHAR(MAX), @VAR char(2)

SET @ACNetQuery = 'SELECT V.*, C.NOMBRE, C.APELLIDO FROM VOUCHER V, CLIENTES C WHERE V.PAIS = C.PAIS AND V.CLIENTE = C.CODIGO AND V.PAIS = ' + CONVERT(VARCHAR, @CountryCode)

IF (@Vouchercode <> '')
BEGIN
	SET @ACNetQuery = @ACNetQuery + N' AND V.CODIGO = ' + @Vouchercode
END
ELSE
BEGIN
	SET @ACNetQuery = @ACNetQuery + ' AND FEC_BAJA IS NULL AND V.FECHA_EMISION >= SYSDATE - 365'
END

SET  @TSQL = 'SELECT IdEMailLog, CountryCode, VoucherCode, Net.Apellido + '' '' + Net.Nombre as PaxName, Net.fecha_emision, StartDate as SentDate, ' +
 'MailTo as EMail, ProcessStatus as Process, ErrorMessage, StartDate as ErrorDate ' +
 'FROM EMailLog el INNER JOIN (SELECT * FROM OPENQUERY(ACNET, ''' + @ACNetQuery + ''')) Net ON Net.CODIGO = el.VoucherCode AND Net.PAIS = el.CountryCode ' +
 'WHERE el.CountryCode = ' + CONVERT(VARCHAR, @CountryCode)

IF (@Vouchercode <> '')
BEGIN
	SET @TSQL = @TSQL + ' AND el.VoucherCode = ' + @Vouchercode
END

IF (@Email <> '')
BEGIN
	SET @TSQL = @TSQL + ' AND el.MailTo = ' + @Email
END

--print(@TSQL)
EXEC (@TSQL)*/

/*
select
	IdEMailLog,
	CountryCode,
	VoucherCode,
	'' as PaxName,
	net.FECHA_EMISION as EmissionDate,
	StartDate as sentDate,
	MailTo as Email,
	ProcessStatus as process,
	ErrorMessage,
	StartDate as errorDate
from 
	EMailLog el
INNER JOIN (SELECT * FROM OPENQUERY(ACNET, @Query)) net 
ON
	net.Codigo = el.VoucherCode and
	net.pais = el.CountryCode
Where
	CountryCode = @CountryCode and
	((@vouchercode = '') or (@vouchercode <> '' and VoucherCode = @voucherCode))and
	((@Email = '') or (@Email <> '' and MailTo = @Email))
order by
	countrycode, VoucherCode, StartDate
	*/
END
