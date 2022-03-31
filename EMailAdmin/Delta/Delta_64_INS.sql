insert into EMailAdmin.dbo.VariableText (Name, IdVariableTextType, IdUser, CreationDate, ModifiedDate, IdStatus)
values ( 'BoxPaxCodeVerifier',1,125,GETDATE(), GETDATE(), 25000)

insert into EMailAdmin.dbo.TableVariableText (Name, IdUser, CreationDate, ModifiedDate, IdStatus)
values ( 'BoxPaxPassengers',125,GETDATE(), GETDATE(), 25000)

declare @idtable int
set @idtable = (select IdTableVariableText from TableVariableText where Name = 'BoxPaxPassengers')

insert into EMailAdmin.dbo.TableVariableTextContent (IdTableVariableText,IdLanguage, ContentText, IdUser, CreationDate, ModifiedDate, IdStatus)
values ( @idtable ,1,'<table border="1"><tbody>$body$</tbody></table><templateBody><tr><td><ul><li>PASAJERO:{0}</li><li>NUMERO DE TARJETA ASSIST-CARD:{1}</li><li>VIGENCIA: {2} al {3}</li></ul></td></tr><templateBody>',125,GETDATE(), GETDATE(), 25000)
