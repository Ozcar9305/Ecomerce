
namespace ECommerce
{
    using ECommerce.Helpers;
    using ECommerceDataLayer;
    using ECommerceDataModel;
    using ECommerceDataModel.Shared;
    using System;
    using System.Linq;
    using Newtonsoft.Json;

    public class ProductSizeLogic
    {
        private readonly ProductSizeDataLayer dataLayer = new ProductSizeDataLayer();

        /// <summary>
        /// Obtiene el listado de tallas disponibles por categoria y producto
        /// </summary>
        /// <param name="productCategoryIdentifier"></param>
        /// <param name="productCatalogIdentifier"></param>
        /// <returns></returns>
        public ResponseListDTO<SizesDTO> ProductSizeGetFilteredList(long productCategoryIdentifier, long productCatalogIdentifier)
        {
            var responseList = new ResponseListDTO<SizesDTO>();
            try
            {
                responseList.Result  = dataLayer.ProductSizeGetFilteredList(productCategoryIdentifier, productCatalogIdentifier);
                responseList.Success = responseList.Result.Any();
            }
            catch (Exception exception)
            {
                exception.LogException();
            }
            return responseList;
        }

        /// <summary>
        /// Pemite actualizar las tallas de un producto especifico
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public ResponseDTO<SizesDTO> ProductSizeMerge(RequestDTO<ProductCatalogDTO> product)
        {
            var productSizeResponse = new ResponseDTO<SizesDTO>();
            try
            {
                var json = JsonConvert.SerializeObject(product);
                if (!string.IsNullOrEmpty(json))
                {
                    productSizeResponse.Success = dataLayer.ProductSizeMerge(product, json);
                }
            }
            catch (Exception exception)
            {
                exception.LogException();
            }
            return productSizeResponse;
        }
    }
}
