use EMailAdmin
alter table content add Color varchar(20) null
alter table content_L add Color varchar(20) null

alter table template add MergeAttachsWithEKit bit null
alter table template add IdTemplatePDF int null
alter table template_L add MergeAttachsWithEKit bit null
alter table template_L add IdTemplatePDF int null

update template set MergeAttachsWithEKit = 0

alter table estrategy add AttachName_EN varchar(255) null
alter table estrategy add AttachName_PT varchar(255) null

update estrategy set attachname_EN = 'Policy Schedule of Benefits.pdf' where code = 'BNF1P'

alter table link alter column url varchar(max)