use EMailAdmin

alter table EMailProcessType add [IdUser] [int] NULL
alter table EMailProcessType add [CreationDate] [datetime] NULL
alter table EMailProcessType add [ModifiedDate] [datetime] NULL
alter table EMailProcessType add [IdStatus] [int] NULL
go
update EMailProcessType set iduser = -3, [CreationDate] = GETDATE(), [ModifiedDate] = GETDATE(), [IdStatus]=25000
go
alter table EMailProcessType alter column [IdUser] [int] not NULL
alter table EMailProcessType alter column [CreationDate] [datetime] not NULL
alter table EMailProcessType alter column [ModifiedDate] [datetime] not NULL
alter table EMailProcessType alter column [IdStatus] [int] not NULL
go
alter table EMailProcessType add PeriodHours varchar(5000) null
go
alter table EMailProcessType add CheckLote bit null
go
update EMailProcessType set CheckLote = 0
go
alter table EMailProcessType alter column CheckLote bit
go
alter table EMailProcessLog add IdLote int null
go
alter table Emaillog add IdLote int null
go
alter table Emaillog add IdClienteUnico int null

GO
CREATE TABLE [dbo].[ConfigurationValue](
	[IdConfigurationValue] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](150) NOT NULL,
	[Value] [nvarchar](255) NULL,
	[ModificationDate] [datetime] NULL,
	[IdUser] [int] NOT NULL,
	[IdStatus] [int] NOT NULL,
 CONSTRAINT [PK_ConfigurationValue] PRIMARY KEY CLUSTERED 
(
	[IdConfigurationValue] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[Pixel]    Script Date: 14/3/2018 14:55:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Pixel](
	[IdPixel] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[jsonContenido] [varchar](max) NULL,
	[IdUser] [int] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[IdStatus] [int] NOT NULL,
 CONSTRAINT [PK_Pixel] PRIMARY KEY CLUSTERED 
(
	[IdPixel] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

CREATE TABLE [dbo].[Pixel_L](
	[IdPixelLog] [int] IDENTITY(1,1) NOT NULL,
	[IdPixel] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[jsonContenido] [varchar](max) NULL,
	[IdUser] [int] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[IdStatus] [int] NOT NULL,
 CONSTRAINT [PK_PixelLog] PRIMARY KEY CLUSTERED 
(
	[IdPixelLog] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO
