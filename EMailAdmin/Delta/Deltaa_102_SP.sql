USE [EMailAdmin]
GO
/****** Object:  StoredProcedure [dbo].[Chat_Count_NotAssigned_Alert]    Script Date: 07/25/2014 08:19:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		@mdasilva
-- Create date: jul.2014
-- =============================================
-- exec [Chat_Count_NotAssigned_Alert] '2014-07-18 15:40:00'
create PROCEDURE [dbo].[Chat_Count_NotAssigned_Alert]
	@FechaUTC	datetime
AS
BEGIN
	SELECT 
		Client.Description negocio, 
		OperatorType.Description OperatorType,
		Chat.LatitudeLocation, Chat.LongitudeLocation,
		Chat.IsFallBack,
		COUNT(Chat.IdChat) Cantidad		
	FROM 
		iMessenger.dbo.Chat
		inner join iMessenger.dbo.Client on Client.IdClient = Chat.IdClient
		inner join iMessenger.dbo.OperatorType on OperatorType.IdOperatorType = Chat.IdOperatorType 
		LEFT JOIN iMessenger.dbo.Chat_PrimerRespuestaOperador AS pr ON pr.IdChat = Chat.IdChat 
	WHERE
		(IdOperator IS NULL OR IdOperator = 0 )
		AND IdChatStatus = 1
		AND DATEDIFF(MINUTE,(ISNULL(pr.firstRst, chat.StartDate)), @FechaUTC)> 10
	group by Client.Description, OperatorType.Description, 
	Chat.LatitudeLocation, Chat.LongitudeLocation,IsFallBack
END

GO
