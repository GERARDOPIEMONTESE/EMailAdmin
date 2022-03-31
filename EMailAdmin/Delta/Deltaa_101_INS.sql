use EMailAdmin

insert into TemplateType (Code, [Description]) values ('IMESSENGER','Email de IMESSENGER')
insert into EMailProcessType(Code,Description,Period) values ('ALERTCHATS','Alarma de chats en espera',10)

exec VariableText_A 'Total',1,125,25000

insert into EMailAdmin.dbo.TableVariableText (Name, IdUser, CreationDate, ModifiedDate, IdStatus)
values ('AlertChatsUnattended',125,GETDATE(), GETDATE(), 25000)

declare @idtable int
set @idtable = (select IdTableVariableText from TableVariableText where Name = 'AlertChatsUnattended')

insert into EMailAdmin.dbo.TableVariableTextContent (IdTableVariableText,IdLanguage, ContentText, IdUser, CreationDate, ModifiedDate, IdStatus)
values ( @idtable ,1,'<table border="1"><tr><th>Negocio</th><th>Tipo de operador</th><th>Latitud</th><th>Longitud</th><th>Mobile</th><th>Cantidad</th></tr><tbody>$body$</tbody></table>',125,GETDATE(), GETDATE(), 25000)

insert into EMailAdmin.dbo.TableVariableTextContent (IdTableVariableText,IdLanguage, ContentText, IdUser, CreationDate, ModifiedDate, IdStatus)
values ( @idtable ,2,'<table border="1"><tr><th>Business</th><th>Operator Type</th><th>Latitud</th><th>Longitud</th><th>Mobile</th><th>Count</th></tr><tbody>$body$</tbody></table>',125,GETDATE(), GETDATE(), 25000)