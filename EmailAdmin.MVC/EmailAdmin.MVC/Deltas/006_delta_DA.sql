/**
 * Nombre: 006_delta_DA.sql
 * Autor: Sebastian Semeraro
 * Accion: ACI9932 - ACTUALIZACION VERSION ACWAN
 * Fecha Creacion: 18/12/2019
 */

INSERT INTO Portal.dbo.CodigoActivador (Codigo, Valor) VALUES ('wan.version.v2', 'false')
INSERT INTO Workflow.DynamicWsSetup.WsSetup (Code, [Description], Environment, Url, [User], [Password], IdUser, IdStatus, Alias)
VALUES('WANServiceV2', 'WANServiceV2 Default', 'DEFAULT', 'http://172.17.1.140/appserver/services/intwsserver?wsdl', 'AC_CUENTAS', '1', 1, 25000, 'WAN')