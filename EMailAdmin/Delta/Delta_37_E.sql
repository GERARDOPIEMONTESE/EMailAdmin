USE [EMailAdmin]
GO

/****** Object:  Table [dbo].[ConditionVariableText_R_VariableText_L]    Script Date: 07/18/2013 17:46:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ConditionVariableText_R_VariableText_L](
	[IdConditionVariableText_R_VariableTextLog] [int] IDENTITY(1,1) NOT NULL,
	[IdConditionVariableText_R_VariableText] [int] NOT NULL,
	[IdConditionVariableText] [int] NOT NULL,
	[IdVariableText] [int] NOT NULL,
	[Condition] [varchar](1000) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[IdUser] [int] NOT NULL,
	[IdStatus] [int] NOT NULL,
 CONSTRAINT [PK_ConditionVariableText_R_VariableTextLog] PRIMARY KEY CLUSTERED 
(
	[IdConditionVariableText_R_VariableTextLog] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


