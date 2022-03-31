USE [EMailAdmin]
GO

/****** Object:  Table [dbo].[ConditionVariableText_L]    Script Date: 07/18/2013 17:45:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ConditionVariableText_L](
	[IdConditionVariableTextLog] [int] IDENTITY(1,1) NOT NULL,
	[IdConditionVariableText] [int] NOT NULL,
	[Name] [varchar](30) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[IdUser] [int] NOT NULL,
	[IdStatus] [int] NOT NULL,
 CONSTRAINT [PK_ConditionVariableTextLog] PRIMARY KEY CLUSTERED 
(
	[IdConditionVariableTextLog] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


