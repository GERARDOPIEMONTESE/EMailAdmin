USE [EMailAdmin]
GO
/****** Object:  StoredProcedure [dbo].[ACCOM_not_issue]    Script Date: 10/7/2017 15:52:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Marcela Da Silva
-- Create date: 10/07/2017
-- Description:	Nice trip envio mails
-- =============================================

--exec [NiceTrip_emails]
create PROCEDURE [dbo].[NiceTrip_emails]
AS
BEGIN

	declare @filtro varchar(max) = (select Valor from Portal.dbo.CodigoActivador where Codigo = 'emailadmin.Trip.AGV')
	
	declare @mailTo varchar(max) = (select Valor from Portal.dbo.CodigoActivador where Codigo = 'emailadmin.Trip.emailTo')
	
	if @mailTo<>''
		select top 1
			PAIS, CODIGO, AGENCIA,SUC_AGENCIA, TARIFA_IMPRESA, PRODUCTO, COD_TARIFA, TIPO_PAX_VOUCHER, FEC_VIG_INIC,
			FEC_VIF_FIN, CANT_DIAS, AREA, NOMBRE, APELLIDO, PASAPORTE, FEC_NACIMIENTO, TEL_PARTICULAR, DOMICILIO,
			@mailTo EMAIL, 
			EMERG_CONTACTO, EMERG_DOMICILIO, EMERG_TEL1, EDAD,
			0 Eliminar
			from acnet_email.base_envio v
	ELSE
		BEGIN
			if LEN(@filtro)>0
				select top 100 
				PAIS, CODIGO, AGENCIA,SUC_AGENCIA, TARIFA_IMPRESA, PRODUCTO, COD_TARIFA, TIPO_PAX_VOUCHER, FEC_VIG_INIC,
				FEC_VIF_FIN, CANT_DIAS, AREA, NOMBRE, APELLIDO, PASAPORTE, FEC_NACIMIENTO, TEL_PARTICULAR, DOMICILIO,
				EMAIL, 
				EMERG_CONTACTO, EMERG_DOMICILIO, EMERG_TEL1, EDAD,
				1 Eliminar
				from acnet_email.base_envio v
				inner join (select Item codigoAGV from EMailAdmin.dbo.SplitString(@filtro, ';')) x
				on x.codigoAGV = v.AGENCIA
			ELSE
				select top 100 
				PAIS, CODIGO, AGENCIA,SUC_AGENCIA, TARIFA_IMPRESA, PRODUCTO, COD_TARIFA, TIPO_PAX_VOUCHER, FEC_VIG_INIC,
				FEC_VIF_FIN, CANT_DIAS, AREA, NOMBRE, APELLIDO, PASAPORTE, FEC_NACIMIENTO, TEL_PARTICULAR, DOMICILIO,
				EMAIL, 
				EMERG_CONTACTO, EMERG_DOMICILIO, EMERG_TEL1, EDAD,
				1 Eliminar
				from acnet_email.base_envio v
		END
	
END