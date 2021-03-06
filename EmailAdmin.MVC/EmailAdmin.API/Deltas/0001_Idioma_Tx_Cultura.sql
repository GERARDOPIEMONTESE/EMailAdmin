USE [Portal]
GO
/****** Object:  StoredProcedure [dbo].[Idioma_Tx_Cultura]    Script Date: 24/8/2018 12:52:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Marcelo De Luca
-- Create date: 26/03/2009
-- Description:	Busqueda idioma por cultura
-- =============================================
--Idioma_Tx_Cultura 'es'
ALTER PROCEDURE [dbo].[Idioma_Tx_Cultura]
	@Cultura			VARCHAR(20)
AS
BEGIN
	SELECT *
	FROM
		IDIOMA
	WHERE
		UPPER(Cultura) like UPPER(@Cultura) + '%'
		and IdIdioma>-1
END


