/**
 * Nombre: 007_delta_DA ROLLBACK.sql
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
		PCC.Codigo = '89412' 
			AND PCC.IdLocacion = 1 
				AND PCS.IdEstadoAprobacion = 9
					AND PCC.IdEstado = 25002
)
UPDATE 
	CC
SET 
	IdEstado = 25000
FROM 
	Cuenta.Cuenta AS CC
	INNER JOIN Cuenta.Sucursal AS CS
		ON CC.IdCuenta = CS.IdCuenta
WHERE 
	CC.IdCuenta = @IdCuenta

UPDATE
	CS
SET
	IdEstado = 25000
FROM
	Cuenta.Sucursal CS WHERE IdCuenta = @IdCuenta