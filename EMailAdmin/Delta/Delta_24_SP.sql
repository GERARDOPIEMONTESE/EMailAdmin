USE [EMailAdmin]
GO

/****** Object:  StoredProcedure [dbo].[Group_R_Template_Tx_TemplateType_FiltersCodes]    Script Date: 04/16/2013 09:33:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Marcela Da Silva
-- Create date: 07/06/2012
-- =============================================
ALTER PROCEDURE [dbo].[Group_R_Template_Tx_TemplateType_FiltersCodes]
	@IdTemplateType				INT,
	@IdGroupType				INT,
	@CountryCode				VARCHAR(10) = '',
	@AccountCode				VARCHAR(10) = '',
	@IdProduct					VARCHAR(10) = '',
	@RateCode					VARCHAR(10) = '',
	@EffectiveDate				DATETIME
AS
BEGIN
	DECLARE @IdNotActiveStatus	INT = 25002
	DECLARE @IdCountryType	INT
	DECLARE @IdAccountType	INT
	DECLARE @IdProductType	INT
	DECLARE @IdRateType		INT

	SELECT @IdCountryType = IdConditionType FROM ConditionType WHERE Code = 'CTR'
	SELECT @IdAccountType = IdConditionType FROM ConditionType WHERE Code = 'ACC'
	SELECT @IdProductType = IdConditionType FROM ConditionType WHERE Code = 'PRO'
	SELECT @IdRateType = IdConditionType FROM ConditionType WHERE Code = 'RTE'

	DECLARE @IdLocation		INT
	
	
	DECLARE @tIdsGC TABLE(idGC INT)

	IF (@IdProduct<>'')
	BEGIN
		INSERT INTO @tIdsGC 
		SELECT IdGroupCondition
		from GroupCondition gc
		where gc.IdStatus<>@IdNotActiveStatus 
			AND gc.IdConditionType = @IdProductType
			AND gc.Value=@IdProduct;
	END

	IF (@CountryCode<>'')
	BEGIN
		SELECT @IdLocation = IdLocacion from portal.Locacion.pais WHERE Codigo = @CountryCode 

		insert into @tIdsGC 
		select IdGroupCondition 
		from GroupCondition 
		where IdStatus<>@IdNotActiveStatus 
			AND IdConditionType = @IdCountryType
			AND Value = @IdLocation;
	END
	
	insert into @tIdsGC 
	select IdGroupCondition
	from Condiciones.dbo.Tarifa t
	inner join GroupCondition gc
		on gc.Value = t.IdTarifa
	where 
		gc.IdStatus<>@IdNotActiveStatus 
		AND gc.IdConditionType = @IdRateType
		AND (@RateCode ='' OR (@RateCode <>'' AND t.Codigo = @RateCode))
		AND (@CountryCode = '' OR (@CountryCode<>'' AND t.CodigoPais=@CountryCode))
		AND (@IdProduct = '' OR (@IdProduct<>'' AND t.IdProducto=@IdProduct));


	insert into @tIdsGC 	
	select IdGroupCondition
	from Portal.Cuenta.Cuenta c 
	inner join Portal.Cuenta.Sucursal s
		on c.IdCuenta = s.IdCuenta
	inner join GroupCondition gc
		on gc.Value = s.IdSucursal
	where gc.IdConditionType = @IdAccountType
		and gc.IdStatus<>@IdNotActiveStatus
		and s.NumeroSucursal = 0 
		and c.IdEstado <>@IdNotActiveStatus
		and  (@AccountCode<>'' AND c.Codigo = @AccountCode) 

	select 
	t.Name TemplateName,
	gcv.Value HierarchyDescription,
	g.Name GroupDescription,
	gcv.VisibleCountryOfValue CountryDescription,
	gcv.VisibleValue AccountDescription,
	gcv.VisibleProductOfValue ProductDescription,
	gcv.VisibleValue RateDescription
	from dbo.GroupCondition_Tx_IdGroupComplete_view gcv
	inner join (select distinct idGC from @tIdsGC) temp on temp.idGC = gcv.IdGroupCondition 
	inner join Group_R_Template grt on grt.IdGroup = gcv.IdGroup
	inner join Template t on t.IdTemplate = grt.IdTemplate
	inner join [Group] g on g.IdGroup = grt.IdGroup
	WHERE grt.IdStatus<>@IdNotActiveStatus

END


GO


