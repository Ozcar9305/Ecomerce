CREATE TABLE [dbo].[Orders]
(
	OrderId BIGINT IDENTITY(1,1),
	CustomerId INT,
	CartItemId VARCHAR(MAX),
	ItemsCount INT,
	ItemsTotalAmount DECIMAL(16,4),
	OrderDate DATETIME DEFAULT GETUTCDATE(),
	CONSTRAINT PK_Orders_OrderId PRIMARY KEY (OrderId),
	CONSTRAINT FK_Orders_CustomerId FOREIGN KEY(CustomerId) REFERENCES [dbo].[Customers] (CustomerId)  
)
GO