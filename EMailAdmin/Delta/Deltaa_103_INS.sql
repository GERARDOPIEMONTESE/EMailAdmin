use EMailAdmin

BEGIN TRY
    BEGIN TRANSACTION

	insert into TemplateType (Code, [Description]) values ('XAM','Email de XAM')
	insert into EMailProcessType(Code,Description,Period) values ('MXAMCASES','XAM Cases',10)

	insert into EMailAdmin.dbo.TableVariableText (Name, IdUser, CreationDate, ModifiedDate, IdStatus)
	values ('XamCases',125,GETDATE(), GETDATE(), 25000)

	declare @idtable int
	set @idtable = (select IdTableVariableText from TableVariableText where Name = 'XamCases')

	insert into EMailAdmin.dbo.TableVariableTextContent (IdTableVariableText,IdLanguage, ContentText, IdUser, CreationDate, ModifiedDate, IdStatus)
	values ( @idtable ,1,'<table border="1"><tr><th>CaseID</th><th>Last Coordinator</th><th>Country</th><th>Delay</th><th>Region</th></tr><tbody>$body$</tbody></table>',125,GETDATE(), GETDATE(), 25000)

	insert into EMailAdmin.dbo.TableVariableTextContent (IdTableVariableText,IdLanguage, ContentText, IdUser, CreationDate, ModifiedDate, IdStatus)
	values ( @idtable ,2,'<table border="1"><tr><th>CaseId</th><th>Last Coordinator</th><th>Country</th><th>Delay</th><th>Region</th></tr><tbody>$body$</tbody></table>',125,GETDATE(), GETDATE(), 25000)

    COMMIT
END TRY
BEGIN CATCH
	ROLLBACK
END CATCH