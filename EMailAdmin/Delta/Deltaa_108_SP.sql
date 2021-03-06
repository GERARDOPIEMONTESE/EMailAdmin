USE [EMailAdmin]
GO
/****** Object:  StoredProcedure [dbo].[Content_Tx_IdContent]    Script Date: 05/12/2014 09:44:37 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Da Silva Marcela
-- Create date: 05/12/2014
-- Description:	busca excepciones por estrategia
-- =============================================
create PROCEDURE [dbo].[Clausula_R_Estrategy_ByIdEstrategy]
	@CodigoPais INT,
	@IdEstrategy INT	
AS
BEGIN
	SELECT *
	FROM
		CLAUSULA_R_ESTRATEGY
	WHERE
		CodigoPais = @CodigoPais 
		and IdEstrategy = @IdEstrategy		
END
