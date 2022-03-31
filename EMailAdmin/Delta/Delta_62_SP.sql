USE [EMailAdmin]
GO
/****** Object:  StoredProcedure [dbo].[GroupCondition_Tx_IdGroupValuesComplete]    Script Date: 11/11/2013 11:26:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ===============================================================
-- Author:		Da Silva Marcela
-- Create date: 05/11/2013
-- Description:	Looks for GroupConditions by group id with values
-- ===============================================================
ALTER PROCEDURE [dbo].[GroupCondition_Tx_IdGroupValuesComplete]
	@IdGroup			INT
AS
BEGIN
declare @IdInactiveStatus INT = 25002
DECLARE @IdCountryType	INT
DECLARE @IdAccountType	INT
DECLARE @IdProductType	INT
DECLARE @IdRateType		INT
DECLARE @IdAccount		INT

SELECT @IdCountryType = IdConditionType FROM ConditionType WHERE Code = 'CTR'

SELECT @IdAccountType = IdConditionType FROM ConditionType WHERE Code = 'ACC'

SELECT @IdProductType = IdConditionType FROM ConditionType WHERE Code = 'PRO'

SELECT @IdRateType = IdConditionType FROM ConditionType WHERE Code = 'RTE'

declare @condiciones table (IdGroupCondition int , IdGroup int , IdConditionType int, Value varchar(255), VisibleValue varchar(255), IdUser int , IdStatus int)
insert into @condiciones 
select IdGroupCondition, IdGroup, IdConditionType, gc.Value, l.Nombre, IdUser, IdStatus from GroupCondition gc
inner join Portal.Locacion.Locacion l on l.IdLocacion = gc.Value
where IdConditionType = @IdCountryType and IdGroup = @IdGroup and gc.IdStatus<> @IdInactiveStatus
union
select IdGroupCondition, IdGroup, IdConditionType, gc.Value, p.Nombre, gc.IdUser, gc.IdStatus from GroupCondition gc
inner JOIN Condiciones..Producto p ON p.IdProducto = gc.Value	
where IdConditionType = @IdProductType and IdGroup = @IdGroup and gc.IdStatus<> @IdInactiveStatus
union
select IdGroupCondition, IdGroup, IdConditionType,gc.Value, t.Nombre, gc.IdUser, gc.IdStatus from GroupCondition gc
inner JOIN Condiciones..Tarifa t ON t.IdTarifa = gc.Value
where IdConditionType = @IdRateType  and IdGroup = @IdGroup and gc.IdStatus<> @IdInactiveStatus
union
select IdGroupCondition, IdGroup, IdConditionType,gc.Value, pj.RazonSocial, gc.IdUser, gc.IdStatus  from GroupCondition gc
inner join Portal.Cuenta.Sucursal s ON s.IdSucursal = gc.Value
inner JOIN Portal.Cuenta.PersonaJuridica pj ON pj.IdPersona = s.IdPersona
where IdConditionType = @IdAccountType and IdGroup = @IdGroup and gc.IdStatus<> @IdInactiveStatus

select * from @condiciones c

END
