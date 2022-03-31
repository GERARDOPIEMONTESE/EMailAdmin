alter table Estrategy add AttachName varchar(255) null
alter table Estrategy add AttachType varchar(255) null

update Estrategy set AttachName='CONDICIONES PARTICULARES.pdf', AttachType='application/pdf' where Code='BNF1P'