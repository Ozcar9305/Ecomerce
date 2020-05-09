CREATE PROCEDURE [dbo].[Usp_ProductCatalogChangeStatus_UPD]
@ProductCatalogId BIGINT,
@StatusId BIT
AS
BEGIN

	UPDATE dbo.ProductCatalog
	SET ProductStatus = @StatusId 
	WHERE ProductCatalogId = @ProductCatalogId

END
GO