USE [EMailAdmin]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EMailListExclude](
	[IdEMailListExclude] [int] IDENTITY(1,1) NOT NULL,
	[CountryCode] [int] NOT NULL,
	[AgencyCode] [varchar](5) NOT NULL,
	[BranchNumber] [int] NOT NULL,
	[IdUser] [int] NULL,
	[CreationDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[IdStatus] [int] NULL,
 CONSTRAINT [PK_EMailListExclude] PRIMARY KEY CLUSTERED 
(
	[IdEMailListExclude] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Index [IX_Agency]    Script Date: 09/11/2015 02:48:13 p.m. ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Agency] ON [dbo].[EMailListExclude]
(
	[CountryCode] ASC,
	[AgencyCode] ASC,
	[BranchNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE TABLE [dbo].[EMailListExclude_L](
	[IdLogEMailListExclude] [int] IDENTITY(1,1) NOT NULL,
	[IdEMailListExclude] [int] NOT NULL,
	[CountryCode] [int] NOT NULL,
	[AgencyCode] [varchar](5) NOT NULL,
	[BranchNumber] [int] NOT NULL,
	[IdUser] [int] NULL,
	[CreationDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[IdStatus] [int] NULL,
 CONSTRAINT [PK_EMailListExclude_L] PRIMARY KEY CLUSTERED 
(
	[IdLogEMailListExclude] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
