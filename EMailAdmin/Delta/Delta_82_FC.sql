
USE [EMailAdmin]
GO

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
CREATE FUNCTION VoucherPoints
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
		@IdBranch		INT,
		@IdRate			INT,
		@voucherPoints	INT
	
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
		
	SET @BranchRate = (SELECT TOP 1 ISNULL(p.ModificationDate, p.CreationDate)
						FROM ACCOM_Prod.Points.BranchRatePoint p
						WHERE 
							 p.CountryCode = @voucherCountryCode AND
							 (p.IdBranch = @IdBranch OR p.IdBranch IS NULL) AND
							 (p.IdRate = @IdRate OR p.IdRate IS NULL) AND
							 p.IdStatus <> 25002
						ORDER BY p.IdBranch DESC, p.IdRate DESC, p.CountryCode)
							 
	IF (@Date > @BranchRate) -- uso la current
	BEGIN
		SELECT @voucherPoints = p.Points
		FROM ACCOM_Prod.Points.BranchRatePoint p
		WHERE 
			 p.CountryCode = @voucherCountryCode AND
			 (p.IdBranch = @IdBranch OR p.IdBranch IS NULL) AND
			 (p.IdRate = @IdRate OR p.IdRate IS NULL) AND
			 p.IdStatus <> 25002
		ORDER BY p.IdBranch DESC, p.IdRate DESC, p.CountryCode
	END
	ELSE -- uso la historica
	BEGIN
		SELECT @voucherPoints = l.Points
		FROM ACCOM_Prod.Points.BranchRatePoint_L l
			INNER JOIN ACCOM_Prod.Points.BranchRatePoint p 
			ON l.IdBranchRatePoint = p.IdBranchRatePoint
		WHERE 
			 p.CountryCode = @voucherCountryCode AND
			 (p.IdBranch = @IdBranch OR p.IdBranch IS NULL) AND
			 (p.IdRate = @IdRate OR p.IdRate IS NULL) AND						
			 (@Date > l.DateFrom AND @Date < L.DateTo) AND
			 p.IdStatus <> 25002
		 ORDER BY 
			p.IdBranch DESC, p.IdRate DESC, p.CountryCode
	END	
	
	RETURN ISNULL(@voucherPoints, 0)
END
GO
