USE [EMailAdmin]
GO

CREATE TABLE dbo.EmailLog_R_PrepurchasePax
	(
	[IdEmailLog_R_PrepurchasePax] [int] IDENTITY(1,1) NOT NULL,
	[IdEmailLog] [int] NOT NULL,
	[CodigoPaxBox] [int] NOT NULL,
	[CodigoVerif] [varchar](255) NULL,
	[VoucherGroup] [varchar](255) NULL,
	[CountryCode] [int] NULL,
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.EmailLog_R_PrepurchasePax ADD CONSTRAINT
	PK_EmailLog_R_PrepurchasePax PRIMARY KEY CLUSTERED 
	(
	IdEmailLog_R_PrepurchasePax
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO

