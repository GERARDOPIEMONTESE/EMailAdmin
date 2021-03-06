USE [EMailAdmin]
GO
/****** Object:  StoredProcedure [dbo].[EmailLog_R_PrepurchasePax_Tx_Filters]    Script Date: 06/23/2014 16:56:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Da Silva Marcela
-- Create date: 14/11/2013
-- Description:	Lista EMailLog relacionados con precompra de dias
-- =============================================
create PROCEDURE [dbo].[EmailLog_R_Capita_Tx_Filtros]
	@Apellido varchar(255) = '',
	@Nombre varchar(255) = '',
	@Documento  varchar(50)  = '',
	@CodigoPais int = NULL,
	@Capita  varchar(255)  = '',
	@Plan  varchar(255)  = '',
	@EnvioLinks int = null,
	@FechaDesde datetime = null,
	@FechaHasta datetime = null
AS
BEGIN
	select 
	*
	from EmailLog_R_Capita c
	left join EMailAdmin.dbo.EMailLog l on l.IdEMailLog = c.IdEmailLog
	WHERE
		(@Documento='' OR c.Documento = @Documento)
		AND (@Nombre='' OR c.Nombre like '%'+ @Nombre +'%')
		AND (@Apellido='' OR c.Apellido like '%'+ @Apellido +'%')
		AND (@Capita='' OR c.Capita like '%'+ @Capita +'%')
		AND (@Plan='' OR c.[Plan] like '%'+ @Plan +'%')
		AND (@CodigoPais IS NULL OR c.CodigoPais = @CodigoPais)
		AND (@EnvioLinks is null or c.EnvioLinks=@EnvioLinks)		
		AND ((@FechaDesde is null or @FechaHasta is null) OR
		 CONVERT(date, StartDate) between convert(date,@FechaDesde) and CONVERT(date, @FechaHasta))
END
