USE [EMailAdmin]
GO
/****** Object:  StoredProcedure [dbo].[ACCOM_not_issue]    Script Date: 03/11/2015 02:41:45 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Marcela Da Silva
-- Create date: 27/10/2015
-- Description:	Busca si hubo emisiones exitosas dentro del periodo configurado, y la ultima cotizacion hasta que paso llego
-- =============================================
-- ACCOM_not_issue 540
CREATE PROCEDURE [dbo].[ACCOM_not_issue]
AS
BEGIN

 	declare @IdPurchaseProcessType int  = (select IdPurchaseProcessType from ACCOM.dbo.PurchaseProcessType where  Description ='Confirmation' ) 

	declare @tACCOMIssue table (countryCode int, AlertPeriod int, confirmation int, lastQuoteLog int null)

	insert into @tACCOMIssue
	select CountryCode, AlertPeriod, 0,  null from ACCOM.dbo.CountryConfiguration where not AlertPeriod is null and AlertPeriod>0

	update @tACCOMIssue set lastQuoteLog = q.IdQuoteLog
	from @tACCOMIssue x
	inner join (select ql.CountryCode, max( ql.IdQuoteLog) IdQuoteLog
	from ACCOM.dbo.QuoteLog ql
	inner join @tACCOMIssue x on x.countryCode = ql.CountryCode
	group by ql.CountryCode
	) q
	on x.countryCode = q.CountryCode


	update @tACCOMIssue set confirmation = c.issue
	from 
	@tACCOMIssue x
	inner join 
	(select ql.CountryCode, count(1) over (partition by ql.CountryCode order by ql.IdQuoteLog desc) issue
		from @tACCOMIssue cc 
		inner join ACCOM.dbo.QuoteLog ql on ql.CountryCode = cc.CountryCode 
		inner join ACCOM.dbo.PurchaseProcessType ppt on ppt.IdPurchaseProcessType = ql.IdPurchaseProcessType
		inner join ACCOM.dbo.VoucherLog v on v.IdQuoteLog = ql.IdQuoteLog
		where ql.IdPurchaseProcessType = @IdPurchaseProcessType
		and ql.CreationDate > (select DATEADD(MINUTE, (cc.AlertPeriod*-1), GETDATE()))) c
	on c.CountryCode = x.countryCode
	
	select cc.countryCode, cc.CountryDescription CountryName, ql.CreationDate QuoteLogDate, cc.Gateway, ppt.Description PurchaseProcessTypeDesc
	from @tACCOMIssue i
		inner join ACCOM.dbo.CountryConfiguration cc on cc.countryCode = i.countryCode
		left join ACCOM.dbo.QuoteLog ql on ql.CountryCode = cc.CountryCode 
		left join ACCOM.dbo.PurchaseProcessType ppt on ppt.IdPurchaseProcessType = ql.IdPurchaseProcessType
		left join ACCOM.dbo.VoucherLog v on v.IdQuoteLog = ql.IdQuoteLog
	where i.confirmation = 0 and ql.IdQuoteLog = i.lastQuoteLog

END