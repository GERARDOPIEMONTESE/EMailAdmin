USE [EMailAdmin]
GO

/****** Object:  Table [dbo].[ConditionVariableTextContent]    Script Date: 07/18/2013 17:46:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ConditionVariableTextContent](
	[IdConditionVariableTextContent] [int] IDENTITY(1,1) NOT NULL,
	[IdConditionVariableText] [int] NOT NULL,
	[IdLanguage] [int] NOT NULL,
	[ContentText] [varchar](max) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[IdUser] [int] NOT NULL,
	[IdStatus] [int] NOT NULL,
 CONSTRAINT [PK_ConditionVariableTextContent] PRIMARY KEY CLUSTERED 
(
	[IdConditionVariableTextContent] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[ConditionVariableTextContent]  WITH CHECK ADD  CONSTRAINT [FK_ConditionVariableTextContent_ConditionVariableText] FOREIGN KEY([IdConditionVariableText])
REFERENCES [dbo].[ConditionVariableText] ([IdConditionVariableText])
GO

ALTER TABLE [dbo].[ConditionVariableTextContent] CHECK CONSTRAINT [FK_ConditionVariableTextContent_ConditionVariableText]
GO


