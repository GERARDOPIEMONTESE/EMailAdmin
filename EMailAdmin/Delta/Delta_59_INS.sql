insert into TemplateType (Code, [Description]) values ('BoxPaxBuy', 'BoxPax buy');
insert into TemplateType (Code, [Description]) values ('BoxPaxBalance', 'BoxPax balance');
insert into TemplateType (Code, [Description]) values ('BoxPaxCancel', 'BoxPax cancel');

insert into Portal.dbo.CodigoActivador (Codigo, Valor) values ('habilitar.boxpax.mail','false');

insert into EMailAdmin.dbo.VariableText (Name, IdVariableTextType, IdUser, CreationDate, ModifiedDate, IdStatus)
values ( 'BoxPaxCode',1,125,GETDATE(), GETDATE(), 25000)

insert into EMailAdmin.dbo.VariableText (Name, IdVariableTextType, IdUser, CreationDate, ModifiedDate, IdStatus)
values ( 'BoxPaxPricePaid',1,125,GETDATE(), GETDATE(), 25000)


