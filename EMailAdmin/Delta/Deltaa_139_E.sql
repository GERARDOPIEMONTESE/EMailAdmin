USE [EMailAdmin]
GO
/****** Object:  Table [dbo].[ContentAttachment]    Script Date: 08/05/2017 12:30:35 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
USE [EMailAdmin]
GO

SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ContentAttachment](
	[IdContentAttachment] [int] IDENTITY(1,1) NOT NULL,
	[IdTemplate] [int] NOT NULL,
	[IdAttachment] [int] NOT NULL,
	[IdLanguage] [int] NOT NULL,
	[CodeRPT] [varchar](20) NOT NULL,
	[Body] [varchar](max) NOT NULL,
	[IdUser] [int] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[IdStatus] [int] NOT NULL,
 CONSTRAINT [PK_IdContentAttachment] PRIMARY KEY CLUSTERED 
(
	[IdContentAttachment] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[ContentAttachment]  WITH CHECK ADD  CONSTRAINT [FK_ContentAttachment_Attachment] FOREIGN KEY([IdAttachment])
REFERENCES [dbo].[Attachment] ([IdAttachment])
GO
ALTER TABLE [dbo].[ContentAttachment] CHECK CONSTRAINT [FK_ContentAttachment_Attachment]
GO
ALTER TABLE [dbo].[ContentAttachment]  WITH CHECK ADD  CONSTRAINT [FK_ContentAttachment_Template] FOREIGN KEY([IdTemplate])
REFERENCES [dbo].[Template] ([IdTemplate])
GO
ALTER TABLE [dbo].[ContentAttachment] CHECK CONSTRAINT [FK_ContentAttachment_Template]
GO
