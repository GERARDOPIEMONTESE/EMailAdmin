USE [Portal_Prod_Last]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[PreguntaIdioma_Tx_IdIdioma] 
	@IdIdioma		INT
AS
BEGIN

	SELECT PrId.IdPregunta_R_Idioma, 
		P.IdPregunta, 
		PrId.IdIdioma, 
		P.Codigo, 
		PrId.Descripcion, 
		PrId.IdEstado 
	FROM Pregunta P
		inner join Pregunta_R_Idioma PrId ON P.IdPregunta = PrId.IdPregunta
	WHERE PrId.IdIdioma = @IdIdioma
		and P.IdEstado <> 25002
		AND PrId.IdEstado <> 25002

END
GO


CREATE PROC [dbo].[PreguntaIdioma_Tx_IdUsuarioIdIdioma] 
	@IdUsuario		INT,
	@IdIdioma		INT
AS
BEGIN

	SELECT PrId.IdPregunta_R_Idioma, 
		P.IdPregunta, 
		PrId.IdIdioma, 
		P.Codigo, 
		PrId.Descripcion, 
		PrId.IdEstado 
	FROM Pregunta P
		inner join Pregunta_R_Idioma PrId ON P.IdPregunta = PrId.IdPregunta
		inner join AsignacionPregunta AP ON AP.IdPregunta = P.IdPregunta
	WHERE AP.IdUsuario = @IdUsuario
		and PrId.IdIdioma = @IdIdioma
		and AP.IdEstado <> 25002
		and P.IdEstado <> 25002
		AND PrId.IdEstado <> 25002

END
GO

