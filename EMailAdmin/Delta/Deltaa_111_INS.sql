insert into TemplateType (Code, [Description]) values ('POLIZAVOID', 'Poliza Cancelacion');
insert into TemplateType (Code, [Description]) values ('POLIZAUPDATE', 'Poliza Modificacion');

insert into VariableText (Name,IdVariableTextType, IdUser, CreationDate, ModifiedDate, IdStatus )
values ('PolizaCertificate', 2, -3, getdate(), GETDATE(), 25000)
insert into VariableText (Name,IdVariableTextType, IdUser, CreationDate, ModifiedDate, IdStatus )
values ('PolizaVoidCertificate', 2, -3, getdate(), GETDATE(), 25000)
insert into VariableText (Name,IdVariableTextType, IdUser, CreationDate, ModifiedDate, IdStatus )
values ('PolizaVoucherCertificate', 2, -3, getdate(), GETDATE(), 25000)
insert into VariableText (Name,IdVariableTextType, IdUser, CreationDate, ModifiedDate, IdStatus )
values ('CodigoPoliza', 1, -3, getdate(), GETDATE(), 25000)
