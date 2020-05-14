--================================================
--EXEC [dbo].[Usp_ProductCatalogFiltered_GETL]
--@ProductCatalogId = 0,
--@WordFilter = '',
--@PageSize = 3,
--@PageNumber = 2
--================================================

CREATE PROCEDURE [dbo].[Usp_ProductCatalogFiltered_GETL]
@ProductCatalogId BIGINT,
@WordFilter VARCHAR(20),
@PageSize INT,
@PageNumber INT
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
	product.ProductCategoryId,
	COUNT(product.ProductCatalogId) OVER() AS TotalCount
	FROM [dbo].ProductCatalog product
	INNER JOIN [dbo].ProductCategory category
	ON product.ProductCategoryId = category.ProductCategoryId
	WHERE product.ProductCatalogId = COALESCE(@ProductCatalogId, product.ProductCatalogId)
	AND PATINDEX('%' + @WordFilter + '%', dbo.fnRemoveAccents(CONCAT(product.[ProductShortName], product.[ProductDescription], category.[ProductCategoryName], category.[ProductCategoryDescription]))) > 0
	ORDER BY product.ProductCatalogId ASC
	OFFSET (@PageNumber-1) * @PageSize ROWS
	FETCH NEXT @PageSize ROWS ONLY


	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	SET NOCOUNT OFF

END
GO
