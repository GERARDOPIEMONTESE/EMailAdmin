USE [EMailAdmin]
GO

/****** Object:  Table [dbo].[EMailList_L]    Script Date: 03/06/2013 16:50:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EMailList_L](
	[IdEMailList_L] [int] IDENTITY(1,1) NOT NULL,
	[IdEMailList] [int] NOT NULL,
	[IdEMailListType] [int] NOT NULL,
	[IdLocacion] [int] NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[IdUser] [int] NULL,
	[CreationDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[IdStatus] [int] NULL,
 CONSTRAINT [PK_EMailList_L] PRIMARY KEY CLUSTERED 
(
	[IdEMailList_L] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO