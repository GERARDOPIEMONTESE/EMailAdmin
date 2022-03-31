USE EMAILADMIN

INSERT INTO Estrategy (Code, Description, Class, AttachName)
VALUES ('EVOUCHER', 'Ekit Attach Pdf', 'EMailAdmin.BackEnd.Strategies.Attachment.EKitAttachStrategy', 'EVOUCHER.pdf')

insert into [dbo].[VariableText] (name, IdVariableTextType, IdUser, CreationDate, ModifiedDate, IdStatus)
values ( 'PrimaTotal', 1, 125, getdate(), getdate(), 25000)

insert into [dbo].[VariableText] (name, IdVariableTextType, IdUser, CreationDate, ModifiedDate, IdStatus)
values ( 'PrimaNeta', 1, 125, getdate(), getdate(), 25000)


declare @IdStrategy INT = (select IDESTRATEGY from Estrategy WHERE CODE = 'BNF1P')
update ReportLanguage set value = 'Resumen de Garantías' where [key]='ConditionsReportLabel' and idlanguage=1 and IdStrategy = @IdStrategy
update ReportLanguage set value = 'Warranty Summary' where [key]='ConditionsReportLabel' and idlanguage=2 and IdStrategy = @IdStrategy
update ReportLanguage set value = 'Resumo de Garantias' where [key]='ConditionsReportLabel' and idlanguage=3 and IdStrategy = @IdStrategy