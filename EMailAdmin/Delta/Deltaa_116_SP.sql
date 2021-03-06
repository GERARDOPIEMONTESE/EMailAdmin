USE [EMailAdmin]
GO
/****** Object:  StoredProcedure [dbo].[EMailListExclude_Tx_Filters]    Script Date: 15/10/2015 02:47:37 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Da Silva Marcela
-- Create date: 15/10/2015
-- =============================================

--exec [EMailListExclude_Tx_Filters] 

CREATE PROCEDURE [dbo].[EMailListExclude_Tx_Filters]
	@CountryCode int = null,
	@AgencyCode varchar(5) = null,
	@BranchNumber int = null,
	@IdStatus int = null
AS 
BEGIN
	declare @sql nvarchar(max),	@params nvarchar(max)

	declare @Filters nvarchar(max)

	set @sql = 'SELECT 
					e.*,
					pais.IdLocacion,
					l.Nombre Pais,
					pj.RazonSocial,
					pj.Denominacion
				FROM 
					EMailListExclude e
				INNER JOIN
					Portal_Prod_Last.Locacion.Pais on pais.Codigo = e.CountryCode
				inner join Portal_Prod_Last.Locacion.Locacion l on l.IdLocacion = pais.IdLocacion 
				inner join Portal_Prod_Last.cuenta.Cuenta c on c.Codigo = e.AgencyCode 
				inner join Portal_Prod_Last.cuenta.Sucursal s on s.IdCuenta = c.IdCuenta and s.NumeroSucursal = e.branchNumber and s.IdLocacion = pais.IdLocacion	
				inner join Portal_Prod_Last.cuenta.PersonaJuridica pj on pj.IdPersona = s.IdPersona
				WHERE 1 = 1 '

	if (@IdStatus is not null)
		set @sql = @sql + ' AND e.IdStatus = @IdStatus '

	if @CountryCode is not null
		set @sql = @sql + ' AND e.CountryCode = @CountryCode '

	if @AgencyCode is not null
		set @sql = @sql + ' AND e.AgencyCode = @AgencyCode '

	if @BranchNumber is not null
		set @sql = @sql + ' AND e.BranchNumber = @BranchNumber '	

	--set @sql = @sql + ' order by FullName'

	print @sql

	set @params = '@CountryCode int = null,
		@AgencyCode varchar(5) = null,
		@BranchNumber int = null,
		@IdStatus int = null'

exec sp_executesql 
		@sql, 
		@params, 
		@CountryCode = @CountryCode,
		@AgencyCode = @AgencyCode,
		@BranchNumber = @BranchNumber,
		@IdStatus = @IdStatus

END
