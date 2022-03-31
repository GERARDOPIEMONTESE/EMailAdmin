USE [EMailAdmin]
GO
/****** Object:  StoredProcedure [dbo].[Attachment_Tx_Filters]    Script Date: 06/19/2014 10:52:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Da Silva Marcela
-- Create date: 19/06/2014
-- =============================================
Create PROCEDURE [dbo].[CondicionesTipoDocumento_Tx_All]	
AS
BEGIN
	SELECT
		*
	FROM
		Condiciones.dbo.TipoDocumento 
	FOR XML AUTO, TYPE, ROOT('TiposDocumento')
END
