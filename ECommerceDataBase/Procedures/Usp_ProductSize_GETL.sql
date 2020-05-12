CREATE PROCEDURE [dbo].[Usp_ProductSize_GETL]
@ProductCategoryId BIGINT,
@ProductCatalogId BIGINT
AS
BEGIN

	SET NOCOUNT ON
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

	SELECT 
	pz.ProductSizesId,
	pz.CategoryId,
	pz.ProductId,
	s.SizeId,
	s.SizeName,
	s.SizeAbreviature,
	s.StatusId
	FROM dbo.ProductSizes pz
	INNER JOIN dbo.Sizes s
	ON pz.SizeId = s.SizeId
	WHERE pz.CategoryId = @ProductCategoryId
	AND pz.ProductId = @ProductCatalogId

	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	SET NOCOUNT OFF

END
GO