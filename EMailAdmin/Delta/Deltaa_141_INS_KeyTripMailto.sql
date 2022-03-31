insert into Portal.dbo.CodigoActivador (Codigo, Valor) values ('emailadmin.Trip.emailTo', 'marcela.dasilva@assistcard.com')
insert into Portal.dbo.CodigoActivador (Codigo, Valor) values ('emailadmin.Trip.Activado', 'false')
update emailadmin.dbo.EMailProcessType set Period = 15 where code = 'Trip'