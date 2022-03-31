
USE [EMailAdmin]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE PointsReportHistory (
	[IdHistory] [int] IDENTITY(1,1) NOT NULL,
	[ReportDate] [datetime] NOT NULL,
	[IdUser] [int] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
	[IdStatus] [int] NOT NULL
)
GO

