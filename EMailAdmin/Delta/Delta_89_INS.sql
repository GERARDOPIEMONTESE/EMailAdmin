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
		   ('CAT'
		   ,'Condition Alert')
GO

INSERT INTO [EMailAdmin].[dbo].[EMailProcessType]
			(Code, 
			[Description], 
			Period)
	VALUES ('CndAlt', 
			'Condition Alert', 
			100)
GO

GO