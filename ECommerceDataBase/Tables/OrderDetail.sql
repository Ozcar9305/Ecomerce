CREATE TABLE [dbo].[OrderDetail]
(
	OrderDetailId BIGINT IDENTITY(1,1),
	OrderId BIGINT NOT NULL,
	ProductCategoryId BIGINT NOT NULL,
	ProductCatalogId BIGINT NOT NULL,
	ProductPrice DECIMAL(16,4) NOT NULL,
	SizeId INT NOT NULL,
	Quantity INT NOT NULL,
	TotalAmount DECIMAL(16,4) NOT NULL,
	OrderDetailDate DATETIME DEFAULT GETUTCDATE(),
	CONSTRAINT PK_OrderDetail_OrderDetailId PRIMARY KEY (OrderDetailId),
	CONSTRAINT FK_OrderDetail_OrderId FOREIGN KEY (OrderId) REFERENCES [dbo].[Orders] (OrderId)
)
GO