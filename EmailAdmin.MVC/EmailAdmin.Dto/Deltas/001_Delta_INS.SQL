
USE [Workflow]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [DynamicWsSetup].[WsSetup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](50) NOT NULL,
	[Description] [varchar](255) NOT NULL,
	[Environment] [varchar](50) NOT NULL,
	[Url] [varchar](255) NOT NULL,
	[User] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[IdUser] [int] NOT NULL,
	[IdStatus] [int] NOT NULL,
 CONSTRAINT [PK_WsSetup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

INSERT INTO DynamicWsSetup.WsSetup(Code,[Description],Environment,Url,[User],[Password],IdUser,IdStatus) VALUES('EMailSenderService', 'EMailSenderService Producción', 'PROD', 'http://mailservice.assist-card.com/EMailSenderService.asmx?wsdl', 'assist-card.net@assist-card.com.ar', '5555-2000', 1, 25000)
INSERT INTO DynamicWsSetup.WsSetup(Code,[Description],Environment,Url,[User],[Password],IdUser,IdStatus) VALUES('EMailSenderService', 'EMailSenderService Testing', 'TEST', 'http://mailservice.dev.assist-card.com/EMailSenderService.asmx?wsdl', 'assist-card.net@assist-card.com.ar', '5555-2000', 1, 25000)

INSERT INTO DynamicWsSetup.WsSetup(Code,[Description],Environment,Url,[User],[Password],IdUser,IdStatus) VALUES('CuentasService', 'CuentasService Producción', 'PROD', 'https://www.assist-card.net/ws/services/CuentasService', 'assist-card.net@assist-card.com.ar', '5555-2000', 1, 25000)
INSERT INTO DynamicWsSetup.WsSetup(Code,[Description],Environment,Url,[User],[Password],IdUser,IdStatus) VALUES('CuentasService', 'CuentasService Testing', 'TEST', 'http://172.17.1.40:8080/ws/services/CuentasService', 'assist-card.net@assist-card.com.ar', '5555-2000', 1, 25000)

INSERT INTO DynamicWsSetup.WsSetup(Code,[Description],Environment,Url,[User],[Password],IdUser,IdStatus) VALUES('WANService', 'WANService Producción', 'PROD', 'http://172.16.1.33/appserversite/IntWsServer.asmx', 'assist-card.net@assist-card.com.ar', '5555-2000', 1, 25000)
INSERT INTO DynamicWsSetup.WsSetup(Code,[Description],Environment,Url,[User],[Password],IdUser,IdStatus) VALUES('WANService', 'WANService Testing', 'TEST', 'http://172.17.1.75/appserversite/IntWsServer.asmx', 'assist-card.net@assist-card.com.ar', '5555-2000', 1, 25000)

INSERT INTO DynamicWsSetup.WsSetup(Code,[Description],Environment,Url,[User],[Password],IdUser,IdStatus) VALUES('ACNETService', 'ACNETService Producción', 'PROD', 'https://www.assist-card.net/ws/services/GenericACNETService', 'assist-card.net@assist-card.com.ar', '5555-2000', 1, 25000)
INSERT INTO DynamicWsSetup.WsSetup(Code,[Description],Environment,Url,[User],[Password],IdUser,IdStatus) VALUES('ACNETService', 'ACNETService Testing', 'TEST', 'http://172.17.1.40:8080/ws/services/GenericACNETService', 'assist-card.net@assist-card.com.ar', '5555-2000', 1, 25000)

INSERT INTO DynamicWsSetup.WsSetup(Code,[Description],Environment,Url,[User],[Password],IdUser,IdStatus) VALUES('AgencyACNET', 'AgencyACNET Producción', 'PROD', 'https://www.assist-card.net/ws/services/AgencyService', 'assist-card.net@assist-card.com.ar', '5555-2000', 1, 25000)
INSERT INTO DynamicWsSetup.WsSetup(Code,[Description],Environment,Url,[User],[Password],IdUser,IdStatus) VALUES('AgencyACNET', 'AgencyACNET Testing', 'TEST', 'http://172.17.1.40:8080/ws/services/AgencyService', 'assist-card.net@assist-card.com.ar', '5555-2000', 1, 25000)

SET ANSI_PADDING OFF
GO








