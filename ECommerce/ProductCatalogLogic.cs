namespace ECommerce
{
    using ECommerce.Helpers;
    using ECommerceDataLayer;
    using ECommerceDataModel;
    using ECommerceDataModel.Shared;
    using System;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Drawing;
    using System.Drawing.Imaging;

    public class ProductCatalogLogic
    {
        /// <summary>
        /// Acceso a la capa de datos
        /// </summary>
        private readonly ProductCatalogDataLayer dataLayer = new ProductCatalogDataLayer();

        /// <summary>
        /// Acceso a capa logica de categorias
        /// </summary>
        private readonly ProductSizeLogic productSizeLogic = new ProductSizeLogic();

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
                if (productCatalogItem.Success)
                {
                    productCatalogItem.Result.Sizes = productSizeLogic.ProductSizeGetFilteredList
                    (
                        productCatalogItem.Result.ProductCategoryIdentifier,
                        productCatalogItem.Result.Identifier
                    )?.Result;
                }
            }
            catch (Exception exception)
            {
                exception.LogException();
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

                if (productList.Success)
                {
                    var categoryDataLayer = new ProductCategoryDataLayer();
                    foreach(var p in productList.Result)
                    {
                        var response = categoryDataLayer.ProductCategoryGetItem(p.ProductCategoryIdentifier);
                        p.ProductCategory = response.Name ?? string.Empty;
                    }
                }
            }
            catch (Exception exception)
            {
                exception.LogException();
            }
            return productList;
        }

        /// <summary>
        /// Permite obtener un listado de productos filtrados por categoria
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public ResponseListDTO<ProductCatalogDTO> ProductCatalogGetListByCategory(RequestDTO<ProductCatalogDTO> product)
        {
            var productListResponse = new ResponseListDTO<ProductCatalogDTO>();
            try
            {
                productListResponse = dataLayer.ProductCatalogGetListByCategory(product);
                productListResponse.Success = productListResponse.Result.Any();
            }
            catch (Exception exception)
            {
                exception.LogException();
            }
            return productListResponse;
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
                            response = mergeProduct(product);                                                   
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
                exception.LogException();
            }
            return response;
        }

        /// <summary>
        /// Permite realizar la operacion merge sobre un producto
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        private ResponseDTO<ProductCatalogDTO> mergeProduct(RequestDTO<ProductCatalogDTO> product)
        {
            var response = new ResponseDTO<ProductCatalogDTO>();
            response.Result = dataLayer.ProductCatalogMerge(product.Item);
            if (product.Item.Identifier == default(long))
            {
                product.Item.Identifier = response.Result.Identifier;
            }

            response.Success = product.Item.Identifier > 0 && productSizeLogic.ProductSizeMerge(product).Success;
            return response;
        }
    }
}