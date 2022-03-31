use EMailAdmin
go
alter table Link alter column Url varchar(300)
go
insert into LinkType (Code, Description) values ('QR','QR Link Image')
go