--======================================================================
--Recibe un json de tipo producto para actualizar la tabla de ProductSizes
--=======================================================================

CREATE PROCEDURE [dbo].[Usp_ProductSizes_INS]
@ProductCategoryId BIGINT,
@ProductCatalogId BIGINT,
@ProductSizesJson NVARCHAR(MAX)
AS
BEGIN

	DELETE dbo.ProductSizes
	WHERE CategoryId = @ProductCategoryId 
	AND ProductId = @ProductCatalogId

	INSERT INTO dbo.ProductSizes
	(
		CategoryId,
		ProductId,
		SizeId
	)
	SELECT
	ProductCategoryId,
	ProductCatalogId,
	SizeId
	FROM OPENJSON(@ProductSizesJson)  
		WITH (
		ProductCategoryId BIGINT '$.Item.ProductCategoryIdentifier',
		ProductCatalogId BIGINT '$.Item.Identifier',
		Sizes NVARCHAR(MAX) '$.Item.Sizes' AS JSON
		)
	OUTER APPLY OPENJSON(Sizes)
		WITH (SizeId INT '$.Identifier');
   

END
GO
