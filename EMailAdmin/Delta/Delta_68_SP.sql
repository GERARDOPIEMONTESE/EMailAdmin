USE [EMailAdmin]
GO
/****** Object:  StoredProcedure [dbo].[EmailLog_R_PrepurchasePax_Tx_Filters]    Script Date: 11/15/2013 14:53:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Da Silva Marcela
-- Create date: 14/11/2013
-- Description:	Lista EMailLog relacionados con precompra de dias
-- =============================================
create PROCEDURE [dbo].[EmailLog_R_PrepurchasePax_Tx_Filters]
	@CodigoVerif	varchar(255) = '',
	@CodigoPaxBox	INT = -1,
	@VoucherGroup	varchar(255) = '',
	@CountryCode	INT = -1	
AS
BEGIN
	select * 
	from EmailLog_R_PrepurchasePax epp
	left join EMailAdmin.dbo.EMailLog l on l.IdEMailLog = epp.IdEmailLog
	WHERE
		(@CodigoVerif='' OR (@CodigoVerif<>'' and epp.CodigoVerif = @CodigoVerif))
		AND (@CodigoPaxBox=-1 OR (@CodigoPaxBox<>-1 and epp.CodigoPaxBox = @CodigoPaxBox))
		AND (@VoucherGroup='' OR (@VoucherGroup<>'' and epp.VoucherGroup = @VoucherGroup))
		AND (@CountryCode=-1 OR (@CountryCode<>-1 and epp.CountryCode = @CountryCode))
END
