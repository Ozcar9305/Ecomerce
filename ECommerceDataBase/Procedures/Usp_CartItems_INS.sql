CREATE PROCEDURE [dbo].[Usp_CartItems_INS]
@CartItemId NVARCHAR(MAX),
@CustomerId INT,
@ProductCategoryId BIGINT,
@ProductCatalogId BIGINT,
@ProductPrice DECIMAL(16,4),
@Quantity INT,
@SizeId INT
AS
BEGIN
	BEGIN TRY
			
		IF @CartItemId = ''
		BEGIN
			SET @CartItemId = (SELECT NEWID())
		END

		DECLARE @PreOrderDate DATETIME
		SET @PreOrderDate = GETUTCDATE()

		INSERT INTO [dbo].CartItems
		(
			CartItemId,
			CustomerId,
			ProductCategoryId,
			ProductCatalogId,
			ProductPrice,
			Quantity,
			SizeId,
			PreOrderDateCreated
		)
		OUTPUT Inserted.CartItemId
		VALUES
		(
			@CartItemId,
			@CustomerId,
			@ProductCategoryId,
			@ProductCatalogId,
			@ProductPrice,
			@Quantity,
			@SizeId,
			@PreOrderDate
		)	

	END TRY
	BEGIN CATCH
			
	END CATCH
END
GO