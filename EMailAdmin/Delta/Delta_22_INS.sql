USE [EMailAdmin]
GO

INSERT INTO [VariableText] ([Name]
      ,[IdVariableTextType]
      ,[IdUser]
      ,[CreationDate]
      ,[ModifiedDate]
      ,[IdStatus])
      VALUES
      (
      'PrepurchaseSaldo',
      1,
      125,
      GETDATE(),
      GETDATE(),
      25000      
      );
INSERT INTO [VariableText] ([Name]
      ,[IdVariableTextType]
      ,[IdUser]
      ,[CreationDate]
      ,[ModifiedDate]
      ,[IdStatus])
      VALUES
      (
      'PrepurchaseTipoSaldo',
      1,
      125,
      GETDATE(),
      GETDATE(),
      25000      
      );