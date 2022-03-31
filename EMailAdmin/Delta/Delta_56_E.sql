begin tran

set xact_abort on

USE [EMailAdmin]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

USE [EMailAdmin]
GO

CREATE TABLE [dbo].[EMailLog_New](
	[IdEMailLog] [int] IDENTITY(1,1) NOT NULL,
	[CountryCode] [int] NOT NULL,
	[ModuleCode] [varchar](50) NOT NULL,
	[VoucherCode] [varchar](50) NULL,
	[InvokeInformation] [varchar](max) NULL,
	[MailTo] [varchar](250) NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NULL,
	[ErrorMessage] [varchar](max) NULL,
	[ProcessStatus] [int] NOT NULL,
	[IdStatus] [int] NOT NULL,
	[Receive] [bit] NOT NULL,
	[ReceiveDate] [datetime] NULL,
 CONSTRAINT [PK_EMailLog_New] PRIMARY KEY CLUSTERED 
(
	[IdEMailLog] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [dbo].[EMailLogMessage](
	[IdEMailLogMessage] [int] IDENTITY(1,1) NOT NULL,
	[CountryCode] [int] NOT NULL,
	[ModuleCode] [varchar](50) NOT NULL,
	[VoucherCode] [varchar](50) NOT NULL,
	[IdTemplate] [int] NULL,
	[IdTemplateType] [int] NULL,
	[TemplateName] [varchar](150) NULL,
	[MailFrom] [varchar](250) NULL,
	[Subject] [varchar](50) NULL,
	[Body] [varchar](max) NULL,
	[AttachmentIds] [varchar](50) NULL,
	[ContextInformation] [xml] NULL,
	[ZipContextInformation] [image] NULL,
	[PaxName] [varchar](250) NOT NULL,
	[PaxSurname] [varchar](250) NOT NULL,
	[IssuanceDate] [varchar](50) NOT NULL,
 CONSTRAINT [PK_EMailLog_R_Message_1] PRIMARY KEY CLUSTERED 
(
	[IdEMailLogMessage] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


/****** Object:  Index [IX_EMailLog_R_Message_Ctr_Mod_Voucher]    Script Date: 08/28/2013 14:35:15 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_EMailLog_R_Message_Ctr_Mod_Voucher] ON [dbo].[EMailLogMessage] 
(
	[CountryCode] ASC,
	[ModuleCode] ASC,
	[VoucherCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

SET IDENTITY_INSERT [dbo].[EMailLog_New] ON
GO

INSERT INTO [dbo].[EMailLog_New]
        ([IdEMailLog], [CountryCode],[ModuleCode],[VoucherCode],[InvokeInformation],[MailTo],
         [StartDate],[EndDate],[ErrorMessage],[ProcessStatus],[IdStatus],
         [Receive],[ReceiveDate])
select e.IdEMailLog, e.CountryCode, e.ModuleCode, e.VoucherCode, e.InvokeInformation, e.MailTo,
	   e.StartDate, e.EndDate, e.ErrorMessage, e.ProcessStatus, e.IdStatus, 
	   e.Receive, e.ReceiveDate
from EMailLog e -- 28650
GO

SET IDENTITY_INSERT [dbo].[EMailLog_New] OFF
GO

INSERT INTO [dbo].[EMailLogMessage]
           ([CountryCode]
           ,[ModuleCode]
           ,[VoucherCode]
           ,[IdTemplate]
           ,[IdTemplateType]
           ,[TemplateName]
           ,[MailFrom]
           ,[Subject]
           ,[Body]
           ,[AttachmentIds]
           ,[ContextInformation]
           ,[ZipContextInformation]
           ,[PaxName]
           ,[PaxSurname]
           ,[IssuanceDate])     
select e.CountryCode, e.ModuleCode, e.VoucherCode, MAX(e.IdTemplate) as IdTemplate, MAX(e.IdTemplateType) as IdTemplateType,
	   MAX(e.TemplateName) as TemplateName, MAX(e.MailFrom) as MailFrom, MAX(e.Subject) as [Subject], MAX(e.Body) as Body, 
	   MAX(e.AttachmentIds) as AttachmentIds, (SELECT TOP 1 c.ContextInformation FROM EMailLog c WHERE MAX(e.IdEMailLog) = c.IdEMailLog) as ContextInformation,
	   (SELECT TOP 1 c.ZipContextInformation FROM EMailLog c WHERE MAX(e.IdEMailLog) = c.IdEMailLog) as ZipContextInformation,
	   MAX(e.PaxName) as PaxName, MAX(e.PaxSurname) as PaxSurname,  MAX(e.IssuanceDate)	   
from EMailLog e
group by e.CountryCode, e.VoucherCode, e.ModuleCode -- 24627
GO

sp_rename 'EMailLog','EMailLog_Old';
GO

sp_rename 'EMailLog_New','EMailLog';
GO

commit tran
