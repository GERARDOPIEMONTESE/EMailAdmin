USE [EMailAdmin]
GO
/****** Object:  StoredProcedure [dbo].[Group_R_Template_Tx_TemplateType_Filters]    Script Date: 12/13/2013 15:31:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Lorena Cominotti
-- Create date: 03/05/2012
-- Description:	Looks for groups by parameters and template type
-- Modif: Da Silva Marcela 
-- Modif. Date: 16/12/2013
-- Description:	Excluir los template que tienen algun grupo en tildado no recive.
-- =============================================

-- dbo.Group_R_Template_Tx_TemplateType_Filters 1, 1, '1', '9055','27045','813075', '0', '2012-06-18',22
--dbo.Group_R_Template_Tx_TemplateType_Filters 1, 1, '1', '29198','27025','812972', '0', '2012-08-14',22
--dbo.Group_R_Template_Tx_TemplateType_Filters 1, 1, '1', '29198','0','0', '0', '2012-08-14',22
--dbo.Group_R_Template_Tx_TemplateType_Filters 1,1,'1','45570','27200','0','1','2012-08-24',22
ALTER PROCEDURE [dbo].[Group_R_Template_Tx_TemplateType_Filters]
	@IdTemplateType				INT,
	@IdGroupType				INT,
	@IdLocation					VARCHAR(10) = 0,
	@IdAccount					VARCHAR(10) = '0',
	@IdProduct					VARCHAR(10) = '0',
	@IdRate						VARCHAR(10) = '0',
	@IdDistributionType			VARCHAR(10) = '0',
	@EffectiveDate				DATETIME,
	@IdModule					INT = 0,
	@IdInactiveStatus			INT = 25002
AS
BEGIN

DECLARE

@IdCountryType			INT,
@IdAccountType			INT,
@IdProductType			INT,
@IdRateType				INT,
@IdDistributionTypeCond	INT

SELECT @IdCountryType = IdConditionType FROM ConditionType WHERE Code = 'CTR'

SELECT @IdAccountType = IdConditionType FROM ConditionType WHERE Code = 'ACC'

SELECT @IdProductType = IdConditionType FROM ConditionType WHERE Code = 'PRO'

SELECT @IdRateType = IdConditionType FROM ConditionType WHERE Code = 'RTE'

SELECT @IdDistributionTypeCond = IdConditionType FROM ConditionType WHERE Code = 'DIS'

/*IF (@IdDistributionType = '0')
BEGIN

SELECT Group_R_Template.*
FROM [Group], Group_R_Template, Template
WHERE
	[Group].IdGroup = Group_R_Template.IdGroup
AND
	Group_R_Template.IdTemplate = Template.IdTemplate
AND
	Template.IdTemplateType = @IdTemplateType
AND
	[Group].IdGroupType = @IdGroupType
AND
	Template.EffectiveStartDate <= @EffectiveDate
AND
	Template.EffectiveEndDate >= @EffectiveDate
AND
	(@IdModule = 0 OR Group_R_Template.IdModule = @IdModule)
AND
	[Group].IdStatus = @IdActiveStatus
AND
	Group_R_Template.IdStatus = @IdActiveStatus
AND
	Template.IdStatus = @IdActiveStatus
AND
(@IdLocation = '0'
OR NOT EXISTS (
	select 1
	from GroupCondition Country
	where Country.IdConditionType = @IdCountryType
	and Country.IdGroup = [Group].IdGroup
	and Country.IdStatus = @IdActiveStatus)
OR EXISTS (
	select 1
	from GroupCondition Country
	where Country.IdConditionType = @IdCountryType
	and Country.IdGroup = [Group].IdGroup
	and Country.IdStatus = @IdActiveStatus
	and (Country.Value = @IdLocation OR Country.Value = '-1')
))
AND
(@IdAccount = '0'
OR NOT EXISTS (
	select 1
	from GroupCondition Account
	where Account.IdConditionType = @IdAccountType
	and Account.IdGroup = [Group].IdGroup
	and Account.IdStatus = @IdActiveStatus
)
OR EXISTS (
	select 1
	from GroupCondition Account
	where Account.IdConditionType = @IdAccountType
	and Account.IdGroup = [Group].IdGroup
	and Account.IdStatus = @IdActiveStatus
	and (Account.Value = @IdAccount OR Account.Value = '-1')
))
AND
(@IdProduct = '0'
OR NOT EXISTS (
	select 1
	from GroupCondition Product
	where Product.IdConditionType = @IdProductType
	and Product.IdGroup = [Group].IdGroup
	and Product.IdStatus = @IdActiveStatus
)
OR EXISTS (
	select 1
	from GroupCondition Product
	where Product.IdConditionType = @IdProductType
	and Product.IdGroup = [Group].IdGroup
	and Product.IdStatus = @IdActiveStatus
	and (Product.Value = @IdProduct OR Product.Value = '-1')
))
AND
(@IdRate = '0'
OR NOT EXISTS (
	select 1
	from GroupCondition Rate
	where Rate.IdConditionType = @IdRateType
	and Rate.IdGroup = [Group].IdGroup
	and Rate.IdStatus = @IdActiveStatus
)
OR EXISTS (
	select 1
	from GroupCondition Rate
	where Rate.IdConditionType = @IdProductType
	and Rate.IdGroup = [Group].IdGroup
	and Rate.IdStatus = @IdActiveStatus
	and (Rate.Value = @IdRate OR Rate.Value = '-1')
))
END
ELSE
BEGIN*/

declare  @gt table ([IdGroup_R_Template] int,[IdGroup] int,[IdTemplate] int,[IdModule] int,
	[Receive] bit,[CreationDate] datetime,[ModifiedDate] datetime,[IdUser] int,
    [IdStatus] int)
insert into @gt
SELECT Group_R_Template.*
FROM [Group], Group_R_Template, Template
WHERE
	[Group].IdGroup = Group_R_Template.IdGroup
AND
	Group_R_Template.IdTemplate = Template.IdTemplate
AND
	Template.IdTemplateType = @IdTemplateType
AND
	[Group].IdGroupType = @IdGroupType
AND
	Template.EffectiveStartDate <= @EffectiveDate
AND
	Template.EffectiveEndDate >= @EffectiveDate
AND
	(@IdModule = 0 OR Group_R_Template.IdModule = @IdModule)
AND
	[Group].IdStatus <> @IdInactiveStatus
AND
	Group_R_Template.IdStatus <> @IdInactiveStatus
AND
	Template.IdStatus <> @IdInactiveStatus
AND
(--@IdLocation = '0'
--OR 
NOT EXISTS (
	select 1
	from GroupCondition Country
	where Country.IdConditionType = @IdCountryType
	and Country.IdGroup = [Group].IdGroup
	and Country.IdStatus <> @IdInactiveStatus)
OR EXISTS (
	select 1
	from GroupCondition Country
	where Country.IdConditionType = @IdCountryType
	and Country.IdGroup = [Group].IdGroup
	and Country.IdStatus <> @IdInactiveStatus
	and (Country.Value = @IdLocation OR Country.Value = '-1')
))
AND
(--@IdAccount = '0'
--OR 
NOT EXISTS (
	select 1
	from GroupCondition Account
	where Account.IdConditionType = @IdAccountType
	and Account.IdGroup = [Group].IdGroup
	and Account.IdStatus <> @IdInactiveStatus
)
OR EXISTS (
	select 1
	from GroupCondition Account
	where Account.IdConditionType = @IdAccountType
	and Account.IdGroup = [Group].IdGroup
	and Account.IdStatus <> @IdInactiveStatus
	and (Account.Value = @IdAccount OR Account.Value = '-1')
))
AND
(--@IdProduct = '0'
--OR 
NOT EXISTS (
	select 1
	from GroupCondition Product
	where Product.IdConditionType = @IdProductType
	and Product.IdGroup = [Group].IdGroup
	and Product.IdStatus <> @IdInactiveStatus
)
OR EXISTS (
	select 1
	from GroupCondition Product
	where Product.IdConditionType = @IdProductType
	and Product.IdGroup = [Group].IdGroup
	and Product.IdStatus <> @IdInactiveStatus
	and (Product.Value = @IdProduct OR Product.Value = '-1')
))
AND
(--@IdRate = '0'
--OR 
NOT EXISTS (
	select 1
	from GroupCondition Rate
	where Rate.IdConditionType = @IdRateType
	and Rate.IdGroup = [Group].IdGroup
	and Rate.IdStatus <> @IdInactiveStatus
)
OR EXISTS (
	select 1
	from GroupCondition Rate
	where Rate.IdConditionType = @IdRateType
	and Rate.IdGroup = [Group].IdGroup
	and Rate.IdStatus <> @IdInactiveStatus
	and (Rate.Value = @IdRate OR Rate.Value = '-1')
))
AND
(--@IdDistributionType = '0'
--OR 
NOT EXISTS (
	select 1
	from GroupCondition Distribution
	where Distribution.IdConditionType = @IdDistributionTypeCond
	and Distribution.IdGroup = [Group].IdGroup
	and Distribution.IdStatus <> @IdInactiveStatus
)
OR EXISTS (
	select 1
	from GroupCondition Distribution
	where Distribution.IdConditionType = @IdDistributionTypeCond
	and Distribution.IdGroup = [Group].IdGroup
	and Distribution.IdStatus <> @IdInactiveStatus
	and (Distribution.Value = @IdDistributionType OR Distribution.Value = '-1')
))
--END
	
	select * from @gt 
	where not IdTemplate in (select distinct IdTemplate from @gt where [Receive] = 0)	
	
	
END
