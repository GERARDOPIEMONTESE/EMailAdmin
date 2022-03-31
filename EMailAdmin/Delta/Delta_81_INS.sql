
USE [EMailAdmin]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

INSERT INTO [EMailAdmin].[dbo].[TemplateType]
           ([Code]
           ,[Description])
     VALUES
           ('VPR'
           ,'Vouchers Points Report')
GO

INSERT INTO [EMailAdmin].[dbo].[EMailProcessType]
			(Code, 
			[Description], 
			Period)
	VALUES ('PtsRpt', 
			'Vouchers points report', 
			42000)
GO

INSERT INTO [EMailAdmin].[dbo].[Estrategy]
           ([Code]
           ,[Description]
           ,[Class])
     VALUES
           ('PTSREPORT'
           ,'Vouchers Points Report'
           ,'EMailAdmin.BackEnd.Strategies.Attachment.ReportAttachStrategy')
GO