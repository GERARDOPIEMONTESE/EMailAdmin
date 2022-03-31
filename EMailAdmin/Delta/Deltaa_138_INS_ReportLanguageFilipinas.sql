
insert into ReportLanguage
select IdLanguage, 
(select IdEstrategy from Estrategy where code = 'FILIPINAS') IdEstrategy,
[key], Value 
from ReportLanguage where IdStrategy = (select IdEstrategy from Estrategy where code = 'BNF1P')