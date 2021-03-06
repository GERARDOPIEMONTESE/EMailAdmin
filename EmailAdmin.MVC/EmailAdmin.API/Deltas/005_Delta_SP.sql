
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[AsignacionPregunta_Tx_IdUsuario] 
	@IdUsuario		INT = NULL,
	@IdPregunta		INT = NULL
AS
BEGIN

	select * 
	from AsignacionPregunta
	Where ISNULL(@IdUsuario, IdUsuario) = IdUsuario
		AND ISNULL(@IdPregunta, IdPregunta) = IdPregunta
		AND IdEstado <> 25002
END
GO
