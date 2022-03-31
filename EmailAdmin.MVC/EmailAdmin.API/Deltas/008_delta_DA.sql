/**
 * Nombre: 008_delta_DA.sql
 * Autor: Sebastian Semeraro
 * Accion: ACI9971 - ESTABILIZACIÓN - CUENTA DUPLICADA
 * Fecha Creacion: 23/12/2019
 */
DECLARE @IdCuenta INT
SET @IdCuenta = (
	SELECT
		PCC.IdCuenta
	FROM
		Portal.Cuenta.Cuenta PCC
		INNER JOIN Portal.Cuenta.Sucursal PCS
			ON PCC.IdCuenta = PCS.IdCuenta
	WHERE 
		PCC.Codigo = '218' 
			AND PCC.IdLocacion = 22
				AND PCS.IdEstadoAprobacion = 9
)
UPDATE 
	CC
SET 
	IdEstado = 25002
FROM 
	Cuenta.Cuenta AS CC
	INNER JOIN Cuenta.Sucursal AS CS
		ON CC.IdCuenta = CS.IdCuenta
WHERE 
	CC.IdCuenta = @IdCuenta

UPDATE
	CS
SET
	IdEstado = 25002
FROM
	Cuenta.Sucursal CS WHERE IdCuenta = @IdCuenta