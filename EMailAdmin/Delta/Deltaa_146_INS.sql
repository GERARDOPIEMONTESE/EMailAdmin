insert EMailProcessType (code, [Description], Period, PeriodHours)
values ('KEEPBUY', 'Continua tu compra',0,'16:00')

insert  [EMailAdmin].[dbo].[TemplateType] (Code, [Description])
values ('KEEPBUY', 'Continua tu compra')

GO

INSERT [dbo].[ConfigurationValue] ( [Code], [Description], [Value], [ModificationDate], [IdUser], [IdStatus]) VALUES ( N'MainBenefitsDocumentLinks', N'URL descarga de condiciones', N'http://condiciones.assist-card.com/Documento/DescargaDeDocumento.aspx?IdDocumento=',null, 1, 25000)
INSERT [dbo].[ConfigurationValue] ( [Code], [Description], [Value], [ModificationDate], [IdUser], [IdStatus]) VALUES ( N'ApplicationUrl', N'URL de emailadmin', N'http://mailadmin.dev.assist-card.com/', null, 1, 25000)
INSERT [dbo].[ConfigurationValue] ( [Code], [Description], [Value], [ModificationDate], [IdUser], [IdStatus]) VALUES ( N'ReportPath', N'Path de archivos de reportes', N'C:\Websites\EMailAdmin\Reports\', null, 1, 25000)
INSERT [dbo].[ConfigurationValue] ( [Code], [Description], [Value], [ModificationDate], [IdUser], [IdStatus]) VALUES ( N'ClauseSeparator', N'Separador de clausulas', N'S', NULL, 1, 25000)
INSERT [dbo].[ConfigurationValue] ( [Code], [Description], [Value], [ModificationDate], [IdUser], [IdStatus]) VALUES ( N'OrClause', N'Caracter or en clausulas', N'||', NULL, 1, 25000)
INSERT [dbo].[ConfigurationValue] ( [Code], [Description], [Value], [ModificationDate], [IdUser], [IdStatus]) VALUES ( N'AndClause', N'Caracter and en clausulas', N'&&', NULL, 1, 25000)
INSERT [dbo].[ConfigurationValue] ( [Code], [Description], [Value], [ModificationDate], [IdUser], [IdStatus]) VALUES ( N'MaxTimeAlert', N'tiempo de alertas', N'60', NULL, 1, 25000)
INSERT [dbo].[ConfigurationValue] ( [Code], [Description], [Value], [ModificationDate], [IdUser], [IdStatus]) VALUES ( N'ContentImageUrl', N'URL imagenes', N'http://mailadmin.dev.assist-card.com/Image/ContentImageHandler.ashx',null, 1, 25000)
INSERT [dbo].[ConfigurationValue] ( [Code], [Description], [Value], [ModificationDate], [IdUser], [IdStatus]) VALUES ( N'HandlerQR', N'URL handler QR', N'http://mailadmin.dev.assist-card.com/Image/handlerQR.ashx',null, 1, 25000)
INSERT [dbo].[ConfigurationValue] ( [Code], [Description], [Value], [ModificationDate], [IdUser], [IdStatus]) VALUES ( N'HB_Email', N'Email de debug ', N'Gabriel.Lammenda@assistcard.com',null, 1, 25000)
INSERT [dbo].[ConfigurationValue] ( [Code], [Description], [Value], [ModificationDate], [IdUser], [IdStatus]) VALUES ( N'PixelTID', N'Id de seguimiento de Universal Analytics', N'UA-112410385-1',null, 1, 25000)
INSERT [dbo].[ConfigurationValue] ( [Code], [Description], [Value], [ModificationDate], [IdUser], [IdStatus]) VALUES ( N'PixelURL', N'Url de analisis del pixel', N'http://www.google-analytics.com/collect',null, 1, 25000)
INSERT [dbo].[ConfigurationValue] ( [Code], [Description], [Value], [ModificationDate], [IdUser], [IdStatus]) VALUES ( N'HabilitarContinuaTuCompra', N'habilita proceso continua tu compra', N'true',null, 1, 25000)
INSERT [dbo].[ConfigurationValue] ( [Code], [Description], [Value], [ModificationDate], [IdUser], [IdStatus]) VALUES ( N'KEEPBUY_Email', N'Email debug continua tu compra1', N'Gabriel.Lammenda@assistcard.com',null, 1, 25000)

