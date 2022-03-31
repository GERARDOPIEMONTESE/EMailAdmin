-- =============================================
-- Author:	Gustavo Suarez
-- Create date: 26/08/2014
-- Description: Adjunto Certificado para regulación de Brazil
-- =============================================
IF NOT EXISTS ( SELECT IdEstrategy from EMailAdmin.dbo.Estrategy WHERE Code = 'BENEFITSBRCERTIF' )
	INSERT INTO EMailAdmin.dbo.Estrategy
		([Code], 
		[Description], 
		[Class])
	VALUES
		('BENEFITSBRCERTIF', 
		'Main Benefits Brazil Certified Attach Pdf', 
		'EMailAdmin.BackEnd.Strategies.Attachment.MainBenefitsBrazilCertifiedAttachStrategy')
GO
  