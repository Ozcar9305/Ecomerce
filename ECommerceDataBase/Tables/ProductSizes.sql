CREATE TABLE [dbo].[ProductSizes](
	[ProductSizesId] [bigint] IDENTITY(1,1) NOT NULL,
	[CategoryId] [bigint] NOT NULL,
	[ProductId] [bigint] NOT NULL,
	[SizeId] [int] NOT NULL,
    CONSTRAINT [PK_ProductSizes_ProductSizesId] PRIMARY KEY CLUSTERED ([ProductSizesId] ASC),
	CONSTRAINT [FK_ProductSizes_CategoryId] FOREIGN KEY([CategoryId]) REFERENCES [dbo].[ProductCategory] ([ProductCategoryId]),
	CONSTRAINT [FK_ProductSizes_ProductId] FOREIGN KEY([ProductId]) REFERENCES [dbo].[ProductCatalog] ([ProductCatalogId]),
	CONSTRAINT [FK_ProductSizes_SizeId] FOREIGN KEY([SizeId]) REFERENCES [dbo].[Sizes] ([SizeId])
)



