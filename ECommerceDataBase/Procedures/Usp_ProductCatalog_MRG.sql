CREATE PROCEDURE [dbo].[Usp_ProductCatalog_MRG]
@ProductCatalogId bigint,
@ProductCategoryId bigint,
@ProductShortName varchar(20),
@ProductDescription varchar(200),
@ProductDescriptionAditional varchar(500),
@ProductPrice decimal(16,4),
@ProductImage varchar(250)
AS
BEGIN
	BEGIN TRY
	BEGIN TRANSACTION ProductCatalogTransaction

		MERGE INTO [dbo].[ProductCatalog] AS Target
		USING(VALUES (@ProductCatalogId)) AS Source ([ProductCatalogId]) ON Target.ProductCatalogId = Source.ProductCatalogId
		WHEN MATCHED THEN
			UPDATE
			SET ProductCategoryId = @ProductCategoryId,
				ProductShortName = @ProductShortName,
				ProductDescription = @ProductDescription,
				ProductDescriptionAditional = @ProductDescriptionAditional,
				ProductPrice = @ProductPrice,
				ProductImage = @ProductImage
		WHEN NOT MATCHED BY TARGET THEN
			INSERT (ProductCategoryId, ProductShortName, ProductDescription, ProductDescriptionAditional, ProductPrice, ProductImage)
			VALUES (@ProductCategoryId, @ProductShortName, @ProductDescription, @ProductDescriptionAditional, @ProductPrice, @ProductImage);
			
	IF @ProductCatalogId > 0
	BEGIN
		SELECT @ProductCatalogId AS ProductCatalogId
	END
	ELSE
	BEGIN
		SELECT SCOPE_IDENTITY() AS ProductCatalogId
	END

	COMMIT TRANSACTION ProductCatalogTransaction
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION ProductCatalogTransaction
	END CATCH
END
GO