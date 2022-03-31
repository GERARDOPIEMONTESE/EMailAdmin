insert into TemplateType (Code, [Description]) values ('ACCOMNOTISSUE', 'ACCOM not issue');

insert into VariableText (Name, IdVariableTextType, IdUser, CreationDate,ModifiedDate, IdStatus)
values ('LastIssue', 1, -3, getdate(), GETDATE(), 25000)

insert into VariableText (Name, IdVariableTextType, IdUser, CreationDate,ModifiedDate, IdStatus)
values ('GatewayName', 1, -3, getdate(), GETDATE(), 25000)

insert into VariableText (Name, IdVariableTextType, IdUser, CreationDate,ModifiedDate, IdStatus)
values ('PurchaseProcessTypeDesc', 1, -3, getdate(), GETDATE(), 25000)

insert into EMailProcessType (Code, Description, Period) values ('ACCOMISSUE', 'ACCOM not issue', 720)