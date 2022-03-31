USE [EMailAdmin]
GO

/****** Object:  Table [dbo].[EMailList]    Script Date: 03/06/2013 16:50:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EMailList](
	[IdEMailList] [int] IDENTITY(1,1) NOT NULL,
	[IdEMailListType] [int] NOT NULL,
	[IdLocacion] [int] NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[IdUser] [int] NULL,
	[CreationDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[IdStatus] [int] NULL,
 CONSTRAINT [PK_EMailList] PRIMARY KEY CLUSTERED 
(
	[IdEMailList] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[EMailList]  WITH CHECK ADD  CONSTRAINT [FK_EMailList_EMailListType] FOREIGN KEY([IdEMailListType])
REFERENCES [dbo].[EMailListType] ([IdEMailListType])
GO

ALTER TABLE [dbo].[EMailList] CHECK CONSTRAINT [FK_EMailList_EMailListType]
GO

