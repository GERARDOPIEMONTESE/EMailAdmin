USE [EMailAdmin]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Da Silva Marcela
-- Create date: 04/11/2013
-- =============================================
CREATE PROCEDURE [dbo].[Group_M]
	@IdGroup			INT,
	@Name				VARCHAR(50) = null,
	@IdGroupType		INT = 1,
	@IdUser				INT = null,
	@IdStatus			INT = null
AS
BEGIN
	update [Group] set	
	Name = @name,
	IdGroupType = @IdGroupType,
	IdUser = @IdUser,
	IdStatus = @IdStatus
	where IdGroup = @IdGroup
	
	
END
