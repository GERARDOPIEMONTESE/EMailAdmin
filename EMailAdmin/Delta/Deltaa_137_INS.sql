/****** Script for SelectTopNRows command from SSMS  ******/
insert into [EMailAdmin].[dbo].[Estrategy] ([Code]
      ,[Description]
      ,[Class]
      ,[AttachName]
      ,[AttachType]
      ,[UrlDowload]
      ,[KeyError]
      ,[AttachName_EN]
      ,[AttachName_PT])
values
(
'FILIPINAS',
'Main Benefits PHILIPPINES PDF',
'EMailAdmin.BackEnd.Strategies.Attachment.MainBenefitsOnePageAttachStrategy',
'CONDICIONES PARTICULARES.pdf',
'application/pdf',
'http://172.17.1.40:8080/servlet/DescargaDocumentosPoliza?pais={CountryCode}&voucher={VoucherCode}&documento=philippines&lang=en',
'<html>',
'Policy Schedule of Benefits.pdf',
NULL
)