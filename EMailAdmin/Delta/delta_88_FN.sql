USE [EMailAdmin]
GO
/****** Object: [dbo].[VoucherPoints] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Gustavo Suarez
-- Create date: 10/03/2014
-- Description:	
-- SELECT dbo.VoucherPoints('598','11203',0,'27','7212',0,getdate())
-- =============================================
ALTER FUNCTION [dbo].[VoucherPoints]
	(@voucherCountryCode CHAR(3),
	@voucherAgencyCode VARCHAR(5),
	@voucherBranchNumber INT,
	@voucherProductCode VARCHAR(120),
	@voucherRateCode VARCHAR(120),
	@voucherModality INT,
	@Date DATETIME)
RETURNS INT
AS 
BEGIN

	DECLARE 
		@BranchRate		DATETIME,
		@IdPais			INT,
		@IdBranch		INT,
		@IdProducto		INT,
		@IdRate			INT,
		@IdException	INT,
		@voucherPoints	INT

	SELECT @IdPais = L.IdPais
	FROM
		Portal.Locacion.Pais L
	WHERE
		L.Codigo = @voucherCountryCode
	
	SELECT @IdProducto = Prd.IdProducto
	FROM
		Condiciones.dbo.Producto Prd
	WHERE
		Prd.CodigoPais = @voucherCountryCode
		AND Prd.Codigo = @voucherProductCode
	
	SELECT @IdBranch = S.IdSucursal
	FROM
		Portal.Cuenta.Cuenta C,
		Portal.Cuenta.Sucursal S,
		Portal.Cuenta.Persona Per,
		Portal.Cuenta.PersonaJuridica PJ,
		Portal.Locacion.Pais L
	WHERE
		C.IdCuenta = S.IdCuenta
		AND S.IdPersona = Per.IdPersona
		AND Per.IdPersona = PJ.IdPersona
		AND S.IdLocacion = L.IdLocacion
		AND S.NumeroSucursal = @voucherBranchNumber
		AND L.Codigo = @voucherCountryCode
		AND C.Codigo = @voucherAgencyCode

	SELECT @IdRate = T.IdTarifa
	FROM
		Condiciones.dbo.Tarifa T, 
		Condiciones.dbo.Producto Prd
	WHERE
		T.IdProducto = Prd.IdProducto
		AND Prd.CodigoPais = @voucherCountryCode
		AND Prd.Codigo = @voucherProductCode
		AND T.CodigoPais = @voucherCountryCode
		AND T.Codigo = @voucherRateCode
		AND T.Anual = (CASE @voucherModality WHEN 5 THEN 1 ELSE 0 END)

	--
	-- Obtener los puntos del Voucher
	--
	
	SELECT @voucherPoints = BRP.Points
		FROM ACCOM_Prod.Points.BranchRatePoint BRP
		WHERE	@date >= BRP.StartDate AND @date <= BRP.EndDate 
				AND
				(EXISTS(SELECT 1
						FROM ACCOM_Prod.Points.BranchRatePointCondition PC
							JOIN ACCOM_Prod.Points.BranchRatePointConditionType PCT 
							ON PCT.IdBranchRatePointConditionType = PC.IdBranchRatePointConditionType
						WHERE PCT.Code = 'COUNTRY'
							AND PC.IdObject = @IdPais
							AND PC.IdBranchRatePoint = BRP.IdBranchRatePoint)
				AND EXISTS(SELECT 1
							FROM ACCOM_Prod.Points.BranchRatePointCondition PC
								JOIN ACCOM_Prod.Points.BranchRatePointConditionType PCT 
								ON PCT.IdBranchRatePointConditionType = PC.IdBranchRatePointConditionType
							WHERE PCT.Code = 'AGENCY'
								AND PC.IdObject = @IdBranch
								AND PC.IdBranchRatePoint = BRP.IdBranchRatePoint)
				AND EXISTS(SELECT 1
							FROM ACCOM_Prod.Points.BranchRatePointCondition PC
								JOIN ACCOM_Prod.Points.BranchRatePointConditionType PCT 
								ON PCT.IdBranchRatePointConditionType = PC.IdBranchRatePointConditionType
							WHERE PCT.Code = 'PRODUCT'
								AND PC.IdObject = @IdProducto
								AND PC.IdBranchRatePoint = BRP.IdBranchRatePoint)
				AND EXISTS(SELECT 1
							FROM ACCOM_Prod.Points.BranchRatePointCondition PC
								JOIN ACCOM_Prod.Points.BranchRatePointConditionType PCT 
								ON PCT.IdBranchRatePointConditionType = PC.IdBranchRatePointConditionType
							WHERE PCT.Code = 'RATE'
								AND PC.IdObject = @IdRate
								AND PC.IdBranchRatePoint = BRP.IdBranchRatePoint))		
	
	SELECT @voucherPoints = ISNULL(@voucherPoints, 0)
	
	--
	-- Obtener excepcion aplicable al Voucher (segun vigencias y otras condiciones)
	--

	SELECT @IdException = E.IdException
		FROM ACCOM_Prod.Exceptions.Exception E
		WHERE	@date >= E.StartDate AND @date <= E.EndDate 
			AND
				((E.ExceptionType = 2 -- Asignacion de puntos a un numero de Voucher  especifico
					AND E.Voucher = @voucherProductCode)
				OR 
				(E.ExceptionType <> 2 AND -- La asignacion de puntos no es a un numero de Voucher especifico
					(EXISTS(SELECT 1
							FROM ACCOM_Prod.Exceptions.ExceptionCondition EC
								JOIN ACCOM_Prod.Exceptions.ExceptionConditionType ECT 
								ON ECT.IdExceptionConditionType = EC.IdExceptionConditionType
							WHERE ECT.Code = 'COUNTRY'
								AND EC.IdObject = @IdPais
								AND EC.IdException = E.IdException)
					AND EXISTS(SELECT 1
								FROM ACCOM_Prod.Exceptions.ExceptionCondition EC
									JOIN ACCOM_Prod.Exceptions.ExceptionConditionType ECT 
									ON ECT.IdExceptionConditionType = EC.IdExceptionConditionType
								WHERE ECT.Code = 'AGENCY'
									AND EC.IdObject = @IdBranch
									AND EC.IdException = E.IdException)
					AND EXISTS(SELECT 1
								FROM ACCOM_Prod.Exceptions.ExceptionCondition EC
									JOIN ACCOM_Prod.Exceptions.ExceptionConditionType ECT 
									ON ECT.IdExceptionConditionType = EC.IdExceptionConditionType
								WHERE ECT.Code = 'PRODUCT'
									AND EC.IdObject = @IdProducto
									AND EC.IdException = E.IdException)
					AND EXISTS(SELECT 1
								FROM ACCOM_Prod.Exceptions.ExceptionCondition EC
									JOIN ACCOM_Prod.Exceptions.ExceptionConditionType ECT 
									ON ECT.IdExceptionConditionType = EC.IdExceptionConditionType
								WHERE ECT.Code = 'RATE'
									AND EC.IdObject = @IdRate
									AND EC.IdException = E.IdException)
					)))
	
	--
	-- Si existe una excepcion aplicar el criterio y modificar los puntos obtenidos
	--

	IF (@IdException IS NOT NULL) 
	BEGIN
		SELECT 
			@voucherPoints = 
				CASE
					WHEN E.ExceptionType = 1 THEN @voucherPoints * E.Value
					WHEN E.ExceptionType = 2 THEN @voucherPoints + E.Value
					WHEN E.ExceptionType = 3 THEN @voucherPoints
					ELSE @voucherPoints
				END
		FROM ACCOM_Prod.Exceptions.Exception E
		WHERE E.IdException = @IdException
	END	
	
	RETURN @voucherPoints
END

