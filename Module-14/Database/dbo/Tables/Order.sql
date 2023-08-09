CREATE TABLE [dbo].[Order]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [Status] NVARCHAR(20) NULL, 
    [CreatedDate] DATETIME2 NULL, 
    [UpdatedDate] DATETIME2 NULL, 
    [ProductId] INT NOT NULL, 
    CONSTRAINT [FK_Order_ToProduct] FOREIGN KEY ([ProductId]) REFERENCES [Product]([Id]) ON DELETE CASCADE, 
    CONSTRAINT [CK_Order_Status] CHECK (Status IN ('NotStarted', 'Loading', 'InProgress', 'Arrived', 'Unloading', 'Cancelled', 'Done')) 
)