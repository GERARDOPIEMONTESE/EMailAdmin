USE [EMailAdmin]
GO

/****** Object:  Table [dbo].[Estrategy]    Script Date: 05/12/2014 08:54:23 a.m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Clausula_R_Estrategy](
	[IdClausula_R_Estrategy] [int] IDENTITY(1,1) NOT NULL, 
	[CodigoPais] int NOT NULL,
	[IdEstrategy] int NOT NULL,
	[IdTipoClausula] int NOT NULL,
	[ClausulaCode] [varchar](15) NOT NULL,
	[Excluye] bit
 CONSTRAINT [PK_Clausula_R_Estrategy] PRIMARY KEY CLUSTERED 
(
	[IdClausula_R_Estrategy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO