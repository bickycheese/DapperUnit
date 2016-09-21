﻿CREATE TABLE [dbo].[Pirate]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [CountryId] INT NOT NULL, 
    CONSTRAINT [FK_Pirate_Country] FOREIGN KEY ([CountryId]) REFERENCES [Country]([Id])
)
