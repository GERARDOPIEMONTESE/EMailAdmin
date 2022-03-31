USE [EMailAdmin]
GO

CREATE TABLE dbo.EmailLog_R_Capita
	(
	[IdEmailLog_R_Capita] [int] IDENTITY(1,1) NOT NULL,
	[IdEmailLog] [int] NOT NULL,
	Nombre  [varchar](255) NOT NULL,
	Apellido [varchar](255) NOT NULL,
	CodigoTipoDocumento [varchar](25) NULL,
	Documento  [varchar](50) NULL,
	CodigoPais [int] NULL,
	CapitaCode [varchar](50) NULL,
	Capita  [varchar](255) NULL,
	PlanCode [varchar](50) NULL,
	[Plan]  [varchar](255) NULL,
	EnvioLinks bit
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.EmailLog_R_Capita ADD CONSTRAINT
	PK_EmailLog_R_Capita PRIMARY KEY CLUSTERED 
	(
	IdEmailLog_R_Capita
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
