CREATE TABLE [dbo].[Product]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(200) NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    [Weight] INT NULL, 
    [Height] INT NULL, 
    [Width] INT NULL, 
    [Length] INT NULL
)