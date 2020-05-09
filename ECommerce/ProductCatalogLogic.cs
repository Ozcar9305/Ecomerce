namespace ECommerce
{
    using ECommerceDataLayer;
    using ECommerceDataModel;
    using ECommerceDataModel.Shared;
    using System;
    using System.Linq;

    public class ProductCatalogLogic
    {
        /// <summary>
        /// Acceso a la capa de datos
        /// </summary>
        private readonly ProductCatalogDataLayer dataLayer = new ProductCatalogDataLayer();

        /// <summary>
        /// Obtiene un producto apartir de su identificador
        /// </summary>
        /// <param name="productIdentifier"></param>
        /// <returns></returns>
        public ResponseDTO<ProductCatalogDTO> ProductCatalogGetItem(long productIdentifier)
        {
            var productCatalogItem = new ResponseDTO<ProductCatalogDTO>();
            try
            {
                productCatalogItem.Result = dataLayer.ProductCatalogGetItem(productIdentifier);
                productCatalogItem.Success = productCatalogItem.Result.Identifier > default(long);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return productCatalogItem;
        }

        /// <summary>
        /// Permite recuperar un listado de productos por identificador, nombre del producto,
        /// nombre de la categoria o descripcion de la categoria
        /// EXEC [dbo].[Usp_ProductCatalogFiltered_GETL] 0, ''
        /// </summary>
        /// <param name="product">Filtro de tipo producto</param>
        /// <returns></returns>
        public ResponseListDTO<ProductCatalogDTO> ProductCatalogGetFilteredList(RequestDTO<ProductCatalogDTO> product)
        {
            var productList = new ResponseListDTO<ProductCatalogDTO>();
            try
            {
                productList = dataLayer.ProductCatalogGetFilteredList(product);
                productList.Success = productList.Result.Any();
            }
            catch (Exception exception)
            {
                throw;
            }
            return productList;
        }

        /// <summary>
        /// Permite insertar o actualizar la informacion de un producto
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public ResponseDTO<ProductCatalogDTO> ProductCatalogExecute(RequestDTO<ProductCatalogDTO> product)
        {
            var response = new ResponseDTO<ProductCatalogDTO>();
            try
            {
                if (product != null)
                {
                    switch (product.OperationType)
                    {
                        case OperationType.Merge:
                            response.Success = dataLayer.ProductCatalogMerge(product.Item);
                            break;
                        case OperationType.Delete:
                            response.Success = dataLayer.ProductCatalogChangeStatus(product.Item.Identifier);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    throw new Exception("El request no puede ser nulo");
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return response;
        }
    }
}