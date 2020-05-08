using ECommerceDataLayer;
using ECommerceDataModel;
using ECommerceDataModel.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce
{
    public class ProductCategoryLogic
    {
        /// <summary>
        /// Obtiene un listado de categorias existentes
        /// </summary>
        /// <returns></returns>
        public ResponseListDTO<ProductCategoryDTO> CategoryGetList()
        {
            var categoryResponse = new ResponseListDTO<ProductCategoryDTO> { Result = new List<ProductCategoryDTO>(), Paging = new PagingDTO() };
            try
            {
                var categoryDataLayer = new ProductCategoryDataLayer();
                var dataLayerResponse = categoryDataLayer.ProductCategoryGetList();
                categoryResponse.Result = dataLayerResponse?.Result;
                categoryResponse.Paging = dataLayerResponse?.Paging;
                categoryResponse.Success = categoryResponse.Result != null && categoryResponse.Result.Any();
            }
            catch (Exception exception)
            {
                categoryResponse = null;
                throw exception;
            }
            return categoryResponse;
        }

        /// <summary>
        /// Obtiene una categoria por identificador
        /// </summary>
        /// <param name="categoryIdentifier">Identificador de la categoria</param>
        /// <returns></returns>
        public ResponseDTO<ProductCategoryDTO> CategoryGetItem(long categoryIdentifier)
        {
            var categoryResponse = new ResponseDTO<ProductCategoryDTO>();
            try
            {
                var categoryDataLayer = new ProductCategoryDataLayer();
                categoryResponse.Result = categoryDataLayer.ProductCategoryGetItem(categoryIdentifier);
                categoryResponse.Success = categoryResponse.Result != null;
            }
            catch (Exception exception)
            {
                categoryResponse = null;
                throw exception;
            }
            return categoryResponse;
        }
    }
}
