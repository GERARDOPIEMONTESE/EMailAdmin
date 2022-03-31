insert into EMailAdmin.dbo.VariableText (Name, IdVariableTextType, IdUser, CreationDate, ModifiedDate, IdStatus)
values ( 'EffectiveStartDate',1,125,GETDATE(), GETDATE(), 25000)

insert into EMailAdmin.dbo.TableVariableText (Name, IdUser, CreationDate, ModifiedDate, IdStatus)
values ( 'EffectiveEndDate',125,GETDATE(), GETDATE(), 25000)

insert into EMailAdmin.dbo.TableVariableText (Name, IdUser, CreationDate, ModifiedDate, IdStatus)
values ( 'issuanceDate',125,GETDATE(), GETDATE(), 25000)