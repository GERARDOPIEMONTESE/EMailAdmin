USE [EMailAdmin]
GO

/****** Object:  Table [dbo].[EMailListType_L]    Script Date: 03/06/2013 16:50:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[EMailListType_L](
	[IdEMailListType_L] [int] IDENTITY(1,1) NOT NULL,
	[IdEMailListType] [int] NOT NULL,
	[Code] [varchar](10) NOT NULL,
	[Description] [varchar](100) NOT NULL,
	[IdUser] [int] NULL,
	[CreationDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[IdStatus] [int] NULL,
 CONSTRAINT [PK_EMailListType_L] PRIMARY KEY CLUSTERED 
(
	[IdEMailListType_L] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]


GO

SET ANSI_PADDING OFF
GO

