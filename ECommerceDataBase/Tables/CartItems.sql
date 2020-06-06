CREATE TABLE [dbo].[CartItems]
(
	[CartItemId] [nvarchar](max) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[ProductCategoryId] [bigint] NOT NULL,
	[ProductCatalogId] [bigint] NOT NULL,
	[ProductPrice] [decimal](16, 4) NOT NULL,
	[SizeId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[TotalAmount]  AS ([ProductPrice]*[Quantity]),
	[PreOrderDateCreated] [datetime] NOT NULL DEFAULT (GETUTCDATE()),
	CONSTRAINT [FK_CartItems_CustomerId] FOREIGN KEY([CustomerId]) REFERENCES [dbo].[Customers] ([CustomerId]),
	CONSTRAINT [FK_CartItems_ProductCatalog] FOREIGN KEY([ProductCatalogId]) REFERENCES [dbo].[ProductCatalog] ([ProductCatalogId]),
	CONSTRAINT [FK_CartItems_ProductCategoryId] FOREIGN KEY([ProductCategoryId]) REFERENCES [dbo].[ProductCategory] ([ProductCategoryId]),
	CONSTRAINT [FK_CartItems_SizeId] FOREIGN KEY([SizeId]) REFERENCES [dbo].[Sizes] ([SizeId])
)
GO