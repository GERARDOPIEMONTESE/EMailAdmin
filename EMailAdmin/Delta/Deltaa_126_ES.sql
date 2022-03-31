use EMailAdmin
go
alter table Estrategy add UrlDowload varchar(5000) null
go
insert into  Estrategy (code, Description, UrlDowload, Class, AttachName, AttachType) values
('BRBILHETE',
'Main Benefits Brazil Bihete Attach Pdf',
'https://www.assist-card.net/servlet/app?handler=PrintPolizaHandler&accion=printCertificadoByVoucher&pais={CountryCode}&codigo={VoucherCode}' ,
'EMailAdmin.BackEnd.Strategies.Attachment.MainDownloadAttachStrategy',
'BraBilhetePDF.pdf',
'application/pdf')