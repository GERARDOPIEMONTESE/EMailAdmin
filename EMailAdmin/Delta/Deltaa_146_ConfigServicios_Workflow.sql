use Workflow

INSERT INTO DynamicWsSetup.WsSetup(Code,[Description],Environment,Url,[User],[Password],IdUser,IdStatus) 
VALUES('ServicioClausulasWS', 'ServicioClausulasWS Producción', 'DEFAULT', 'http://serviciocondiciones.assist-card.com/ServicioClausulasWS.asmx', 
'Juan', 'Juan', 1, 25000)

INSERT INTO DynamicWsSetup.WsSetup(Code,[Description],Environment,Url,[User],[Password],IdUser,IdStatus) 
VALUES('AssistCardService', 'AssistCardService Producción', 'DEFAULT', 'https://www.assist-card.net/ws/services/AssistCardService', 
'ACNET', 'ACNET', 1, 25000)

INSERT INTO DynamicWsSetup.WsSetup(Code,[Description],Environment,Url,[User],[Password],IdUser,IdStatus) 
VALUES('XAMService', 'XAMService Producción', 'DEFAULT', 'http://acixamweb03.assist-card.com.ar/eAAvs/WebServices/WsMail/WSMail.svc/soap', 
'', '', 1, 25000)

INSERT INTO DynamicWsSetup.WsSetup(Code,[Description],Environment,Url,[User],[Password],IdUser,IdStatus) 
VALUES('AssistCardDaysAcquisitionService', 'AssistCardDaysAcquisitionService Producción', 'DEFAULT', 'https://www.assist-card.net/ws/services/AssistCardDaysAcquisitionService', 
'ACNET', 'ACNET', 1, 25000)