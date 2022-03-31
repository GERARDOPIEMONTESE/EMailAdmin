USE [EMailAdmin]
GO

/****** Object:  StoredProcedure [dbo].[EmailLog_R_PrepurchasePax_A]    Script Date: 06/23/2014 15:36:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Da Silva Marcela
-- Create date: 23/06/2014
-- Description:	EMailLog relacionados con capitas
-- =============================================
create PROCEDURE [dbo].[EmailLog_R_Capita_A]
	@IdEmailLog int,
	@Nombre  varchar(255),
	@Apellido varchar(255),
	@CodigoTipoDocumento varchar(25) = NULL,
	@Documento  varchar(50)  = NULL,
	@CodigoPais int = NULL,
	@CapitaCode varchar(50) = NULL,
	@Capita  varchar(255) = NULL,
	@PlanCode varchar(50) = NULL,
	@Plan  varchar(255) = NULL,
	@EnvioLinks bit = 0
AS
BEGIN
	INSERT INTO
		EmailLog_R_Capita
	(
		IdEmailLog,
		Nombre,
		Apellido,
		CodigoTipoDocumento,
		Documento,
		CodigoPais,
		CapitaCode,
		Capita,
		PlanCode,
		[Plan],
		EnvioLinks
	)
	VALUES
	(
		@IdEmailLog,
		@Nombre,
		@Apellido,
		@CodigoTipoDocumento,
		@Documento,
		@CodigoPais,
		@CapitaCode,
		@Capita,
		@PlanCode,
		@Plan,
		@EnvioLinks
	)
	
	RETURN @@IDENTITY
END


GO


