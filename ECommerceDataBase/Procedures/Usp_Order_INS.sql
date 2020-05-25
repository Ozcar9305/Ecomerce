
CREATE PROCEDURE [dbo].[Usp_Order_INS] 
@CustomerId  INT,
@CartItemId VARCHAR(MAX)
AS
SET NOCOUNT ON;
BEGIN TRY
BEGIN TRANSACTION

	DECLARE @NewOrderId BIGINT

	--OBTENER EL TOTAL DE PRODUCTOS EN EL CARRITO
	DECLARE @TotalItems int = (SELECT COUNT(CartItemId) 
	                           FROM dbo.CartItems
						       WHERE CustomerId = @CustomerId
						       AND CartItemId = @CartItemId)
	
	--OBTENER EL IMPORTE TOTAL DEL CARRITO
	DECLARE @TotalAmount DECIMAL(16,4) = (SELECT SUM(TotalAmount) 
										  FROM dbo.CartItems
										  WHERE CustomerId = @CustomerId
										  AND CartItemId = @CartItemId)

	--INSERTAR EL HEADER DE LA ORDEN										
	INSERT INTO dbo.Orders(CustomerId,CartItemId,ItemsCount,ItemsTotalAmount)
	VALUES (@CustomerId, @CartItemId, @TotalItems, @TotalAmount)
    SET @NewOrderId = SCOPE_IDENTITY() 

	--INSERTAR LOS DETALLES DE LA ORDEN
	INSERT INTO dbo.OrderDetail
	(
		OrderId,
		ProductCategoryId,
		ProductCatalogId,
		ProductPrice,
		SizeId,
		Quantity,
		TotalAmount
	)
	SELECT
	@NewOrderId,
	cart.ProductCategoryId,
	cart.ProductCatalogId,
	cart.ProductPrice,
	cart.SizeId,
	Quantity,
	TotalAmount
	FROM dbo.CartItems cart
	INNER JOIN dbo.Customers customers
	ON cart.CustomerId = customers.CustomerId
	INNER JOIN ProductCategory category
	ON category.ProductCategoryId = cart.ProductCategoryId
	INNER JOIN dbo.ProductCatalog products
	ON products.ProductCategoryId = category.ProductCategoryId
	AND products.ProductCatalogId = cart.ProductCatalogId
	INNER JOIN dbo.Sizes sizes
	ON sizes.SizeId = cart.SizeId
	WHERE cart.CustomerId = @CustomerId
	AND cart.CartItemId = @CartItemId

	--ELIMINAR REGISTROS DEL CARRITO DE COMPRAS
	DELETE CartItems
	WHERE CustomerId = @CustomerId
	AND CartItemId = @CartItemId

	SELECT @NewOrderId as 'OrderId'

COMMIT TRANSACTION
END TRY
BEGIN CATCH

    IF @@TRANCOUNT > 0 
	ROLLBACK

	 DECLARE 
	 @ErrorMessage NVARCHAR(2048),
     @ErrorSeverity INT,
     @ErrorState INT
	 
	 SELECT
	 @ErrorMessage =ERROR_MESSAGE(),
	 @ErrorSeverity =ERROR_SEVERITY(),
	 @ErrorState =ERROR_STATE()
 
	 RAISERROR (@ErrorMessage, @ErrorSeverity,@ErrorState)
END CATCH