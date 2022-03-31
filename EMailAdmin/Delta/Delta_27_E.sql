USE [EMailAdmin]
GO

/****** Object:  Table [dbo].[TableVariableTextContent]    Script Date: 06/05/2013 09:58:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TableVariableTextContent](
	[IdTableVariableTextContent] [int] IDENTITY(1,1) NOT NULL,
	[IdTableVariableText] [int] NOT NULL,
	[IdLanguage] [int] NOT NULL,
	[ContentText] [varchar](max) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[IdUser] [int] NOT NULL,
	[IdStatus] [int] NOT NULL,
 CONSTRAINT [PK_TableVariableTextContent] PRIMARY KEY CLUSTERED 
(
	[IdTableVariableTextContent] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[TableVariableTextContent]  WITH CHECK ADD  CONSTRAINT [FK_TableVariableTextContent_TableVariableText] FOREIGN KEY([IdTableVariableText])
REFERENCES [dbo].[TableVariableText] ([IdTableVariableText])
GO

ALTER TABLE [dbo].[TableVariableTextContent] CHECK CONSTRAINT [FK_TableVariableTextContent_TableVariableText]
GO


