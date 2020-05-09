CREATE TABLE [dbo].[ProductCategory]
(
	[ProductCategoryId] [bigint] IDENTITY(1,1) NOT NULL,
	[ProductCategoryName] [varchar](50) NOT NULL,
	[ProductCategoryDescription] [varchar](200) NULL,
	[ProductCategoryStatus] [bit] NULL,
	[ProductCategoryDateCreated] [datetime] NULL,
    CONSTRAINT [PK_ProductCategory_ProductCategoryId] PRIMARY KEY CLUSTERED ([ProductCategoryId])
)
GO