USE [EMailAdmin]
GO

/****** Object:  Index [LogLegalFilters]    Script Date: 09/08/2014 09:07:48 ******/
CREATE NONCLUSTERED INDEX [LogLegalFilters] ON [dbo].[EMailLog] 
(
	[CountryCode] ASC,
	[VoucherCode] ASC,
	[MailTo] ASC,
	[TemplateName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO


