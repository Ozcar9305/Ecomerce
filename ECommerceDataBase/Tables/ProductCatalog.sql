CREATE TABLE [dbo].[ProductCatalog]
(
	[ProductCatalogId] [bigint] IDENTITY(1,1) NOT NULL,
	[ProductCategoryId] [bigint] NOT NULL,
	[ProductShortName] [varchar](20) NOT NULL,
	[ProductDescription] [varchar](200) NULL,
	[ProductDescriptionAditional] [varchar](500) NULL,
	[ProductPrice] [decimal](16, 4) NOT NULL,
	[ProductStatus] [bit] NULL,
	[ProductDateCreated] [datetime] NULL,
	[ProductImage] [varchar](250) NOT NULL,
	CONSTRAINT [PK_ProductCatalog_ProductCatalogId] PRIMARY KEY CLUSTERED ([ProductCatalogId])
)