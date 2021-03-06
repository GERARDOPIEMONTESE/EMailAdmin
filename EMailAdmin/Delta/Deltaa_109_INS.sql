
-- =============================================
-- Author:		Gustavo Suarez
-- Create date: 26/02/2015
-- Description:	ACI1889
-- =============================================

declare @idtable int
set @idtable = (select IdTableVariableText from EMailAdmin.dbo.TableVariableText where Name = 'XamCases')

update EMailAdmin.dbo.TableVariableTextContent
set ContentText = '<table border="1"><tr><th>CaseID</th><th>Last Coordinator</th><th>Assistance Country</th><th>Coordinator Country</th><th>Delay</th><th>Region</th></tr><tbody>$body$</tbody></table>'
where IdTableVariableText = @idtable 
	and IdLanguage in (1,2)
