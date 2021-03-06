USE [EMailAdmin]
GO
/****** Object:  StoredProcedure [dbo].[Attachment_E]    Script Date: 10/7/2017 16:27:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Da Silva Marcela
-- Create date: 10/07/2017
-- va limpiando cuando envia el mail de nicetrip
-- =============================================
create PROCEDURE [dbo].[acnet_email.base_envio_E]
	@CountryCode		INT,
	@VoucherCode		INT,
	@AgencyCode			varchar(5),
	@BranchNumber		INT
AS
BEGIN
	
	delete 
	from acnet_email.base_envio
	where PAIS = @CountryCode
	and CODIGO = @VoucherCode
	and AGENCIA = @AgencyCode
	and SUC_AGENCIA = @BranchNumber

END
