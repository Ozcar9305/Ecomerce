CREATE PROCEDURE [dbo].[Usp_ProductCatalogFiltered_GETL]
@ProductCatalogId bigint,
@WordFilter varchar(20)
AS
BEGIN

	SET NOCOUNT ON
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

	IF @ProductCatalogId = 0
	BEGIN
		SET @ProductCatalogId = NULL
	END

	SET @WordFilter = (SELECT dbo.fnRemoveAccents(@WordFilter))
	
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
	WHERE product.ProductCatalogId = COALESCE(@ProductCatalogId, product.ProductCatalogId)
	AND PATINDEX('%' + @WordFilter + '%', dbo.fnRemoveAccents(CONCAT(product.[ProductShortName], product.[ProductDescription], category.[ProductCategoryName], category.[ProductCategoryDescription]))) > 0

	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	SET NOCOUNT OFF

END
GO
