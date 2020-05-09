CREATE PROCEDURE [dbo].[Usp_ProductCatalogById_GETI]
@ProductId bigint
AS
BEGIN

	SET NOCOUNT ON
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

	SELECT
	product.ProductCatalogId,
	product.ProductShortName,
	product.ProductDescription,
	product.ProductDescriptionAditional,
	product.ProductPrice,
	product.ProductStatus,
	product.ProductImage,
	product.ProductCategoryId
	FROM [dbo].ProductCatalog product
	INNER JOIN [dbo].ProductCategory category
	ON product.ProductCategoryId = category.ProductCategoryId
	WHERE product.ProductCatalogId = @ProductId

	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	SET NOCOUNT OFF

END
GO
