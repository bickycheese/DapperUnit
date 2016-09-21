CREATE TABLE [dbo].[Ship]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(255) NOT NULL, 
    [CountryId] INT NOT NULL, 
    CONSTRAINT [FK_Ship_Country] FOREIGN KEY ([CountryId]) REFERENCES [Country]([Id])
)
