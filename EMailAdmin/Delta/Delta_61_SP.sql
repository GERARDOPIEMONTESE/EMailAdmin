USE [EMailAdmin]
GO
/****** Object:  StoredProcedure [dbo].[Group_R_Template_Tx_TemplateType_FiltersCodes]    Script Date: 11/11/2013 11:26:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Da Silva Marcela
-- Create date: 05/11/2013
-- =============================================
ALTER PROCEDURE [dbo].[Group_R_Template_Tx_TemplateType_FiltersCodes]
	@IdTemplateType				INT = -1,
	@GroupName					varchar(255) = '',
	@IdGroupType				INT,
	@IdLocacion					VARCHAR(10) = '-1',
	@AccountCode				VARCHAR(10) = '',
	@IdProduct					VARCHAR(10) = '-1',
	@RateCode					VARCHAR(10) = '',
	@EffectiveDate				DATETIME,
	@Asociados					int = -1,
	@IdInactiveStatus			INT = 25002
AS
BEGIN
DECLARE @IdCountryType	INT
DECLARE @IdAccountType	INT
DECLARE @IdProductType	INT
DECLARE @IdRateType		INT
DECLARE @IdAccount		INT

SELECT @IdCountryType = IdConditionType FROM ConditionType WHERE Code = 'CTR'

SELECT @IdAccountType = IdConditionType FROM ConditionType WHERE Code = 'ACC'

SELECT @IdProductType = IdConditionType FROM ConditionType WHERE Code = 'PRO'

SELECT @IdRateType = IdConditionType FROM ConditionType WHERE Code = 'RTE'


declare @groupsTable table(idgroup int)

insert into @groupsTable
select IdGroup from GroupCondition 
where (@idLocacion=-1 or (IdConditionType = @IdCountryType and @idLocacion<>-1 and value = @idLocacion and IdStatus<> @IdInactiveStatus))
INTERSECT 
select IdGroup from GroupCondition 
where (@IdProduct=-1 or (IdConditionType = @IdProductType and @IdProduct<>-1 and value = @IdProduct and IdStatus<> @IdInactiveStatus))
INTERSECT 
select IdGroup from GroupCondition 
where (@RateCode='' or (IdConditionType = @IdRateType  and @RateCode<>'' and value = @RateCode and IdStatus<> @IdInactiveStatus))

if (@AccountCode <>'')
BEGIN
	select @IdAccount = idsucursal from portal.cuenta.Sucursal s 
		inner join portal.cuenta.cuenta c on c.IdCuenta = s.IdCuenta 
		where c.Codigo = @AccountCode and s.IdEstado <> @IdInactiveStatus and c.IdEstado <> @IdInactiveStatus and
		((@idLocacion = -1) or (@idLocacion <> -1 and c.IdLocacion = @idLocacion)) and NumeroSucursal = 0

	insert into @groupsTable
	select IdGroup from GroupCondition 
	where (@idLocacion=-1 or (IdConditionType = @IdCountryType and @idLocacion<>-1 and value = @idLocacion and IdStatus<> @IdInactiveStatus))
	INTERSECT 
	select IdGroup from GroupCondition 
	where (@IdAccount=-1 or (IdConditionType = @IdAccountType and @IdAccount<>-1 and value = @IdAccount and IdStatus<> @IdInactiveStatus))
END

SELECT  distinct 
t.IdTemplate, 
t.Name TemplateName, 
g.Name GroupDescription,
tt.[Description] TemplateType,
t.EffectiveStartDate,
t.EffectiveEndDate
FROM @groupsTable r
inner join [Group] g on g.IdGroup = r.idgroup
left join (select * from Group_R_Template where IdStatus<> @IdInactiveStatus) gt on gt.IdGroup = r.IdGroup
left join Template t on t.IdTemplate = gt.IdTemplate
left join TemplateType tt on tt.IdTemplateType = t.IdTemplateType
where 
(@IdTemplateType =-1 or (@IdTemplateType<>-1 AND t.IdTemplateType = @IdTemplateType))
--and (t.EffectiveStartDate<=@EffectiveDate and t.EffectiveEndDate>=@EffectiveDate)
AND (@GroupName ='' or (@GroupName<>'' AND g.Name like '%'+@GroupName+'%'))
and (@Asociados=-1 or ((@Asociados=0 AND t.IdTemplate is null) OR (@Asociados=1 AND not t.IdTemplate is null)))

END
