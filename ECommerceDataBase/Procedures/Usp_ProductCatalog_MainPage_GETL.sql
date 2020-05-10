CREATE PROCEDURE [dbo].[Usp_ProductCatalog_MainPage_GETL]
@CategoryId bigint,
@PageSize int
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
	product.ProductImage
	FROM [dbo].ProductCatalog product
	INNER JOIN [dbo].ProductCategory category
	ON product.ProductCategoryId = category.ProductCategoryId
	WHERE product.ProductCategoryId = @CategoryId
	ORDER BY product.ProductCatalogId
	OFFSET 0 * @PageSize ROWS
	FETCH NEXT @PageSize ROWS ONLY

	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	SET NOCOUNT OFF

END
GO