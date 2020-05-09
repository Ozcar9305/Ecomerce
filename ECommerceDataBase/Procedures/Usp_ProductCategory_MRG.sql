CREATE PROCEDURE [dbo].[Usp_ProductCategory_MRG]
@ProductCategoryId bigint,
@ProductCategoryName varchar(50),
@ProductCategoryDescription varchar(200)
AS
BEGIN
	BEGIN TRY
	BEGIN TRANSACTION ProductCategoryTransaction

		MERGE INTO [dbo].[ProductCategory] AS Target
		USING(VALUES (@ProductCategoryId)) AS Source ([ProductCategoryId]) ON Target.ProductCategoryId = Source.ProductCategoryId
		WHEN MATCHED THEN
			UPDATE
			SET ProductCategoryName = @ProductCategoryName,
				ProductCategoryDescription = @ProductCategoryDescription
		WHEN NOT MATCHED BY TARGET THEN
			INSERT (ProductCategoryName, ProductCategoryDescription)
			VALUES (@ProductCategoryName, @ProductCategoryDescription);

	COMMIT TRANSACTION ProductCategoryTransaction
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION ProductCategoryTransaction
	END CATCH
END
GO