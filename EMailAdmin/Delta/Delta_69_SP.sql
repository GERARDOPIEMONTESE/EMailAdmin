USE [EMailAdmin]
GO
/****** Object:  StoredProcedure [dbo].[EmailLog_R_PrepurchasePax_Tx_CodigoVerif]    Script Date: 11/15/2013 16:02:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Da Silva Marcela
-- Create date: 14/11/2013
-- Description:	Lista EMailLog relacionados con precompra de dias
-- =============================================
create PROCEDURE [dbo].[EmailLog_R_PrepurchasePax_Tx_CodigoVerif]
	@CodigoVerif	varchar(255) = '',
	@CodigoPaxBox	INT = -1,
	@VoucherGroup	varchar(255) = '',
	@CountryCode	INT = -1	
AS
BEGIN
	IF (@CodigoPaxBox<>-1)
		select distinct top 1 codigoPaxBox, codigoverif, vouchergroup, countrycode
			from EmailLog_R_PrepurchasePax epp
			WHERE
				epp.CodigoPaxBox = @CodigoPaxBox
			order by codigoverif, countrycode, vouchergroup desc			
	ELSE
		BEGIN
			select distinct top 1 codigoPaxBox, codigoverif, vouchergroup, countrycode
			from EmailLog_R_PrepurchasePax epp
			WHERE
				(@CodigoVerif='' OR (@CodigoVerif<>'' and epp.CodigoVerif = @CodigoVerif))
				AND (@VoucherGroup='' OR (@VoucherGroup<>'' and epp.VoucherGroup = @VoucherGroup))
				AND (@CountryCode=-1 OR (@CountryCode<>-1 and epp.CountryCode = @CountryCode))
			order by codigoverif, countrycode, vouchergroup desc
		END
END
