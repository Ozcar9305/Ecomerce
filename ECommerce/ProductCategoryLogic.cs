namespace ECommerce
{
    using ECommerce.Helpers;
    using ECommerceDataLayer;
    using ECommerceDataModel;
    using ECommerceDataModel.Shared;
    using System;
    using System.Collections.Generic;
    using System.Linq;

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
                exception.LogException();
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
                exception.LogException();
            }
            return categoryResponse;
        }

        /// <summary>
        /// Obtiene un listado de categorias y productos para la pagina principal
        /// </summary>
        /// <param name="productCount"></param>
        /// <returns></returns>
        public ResponseListDTO<ProductCategoryDTO> CategoryListForMainPage(int productCount)
        {
            ResponseListDTO<ProductCategoryDTO> dataResponse = null;
            try
            {
                var categoryDataLayer = new ProductCategoryDataLayer();
                var categoryList = categoryDataLayer.ProductCategoryGetList()?.Result;
                if (categoryList.Any())
                {
                    var productDataLayer = new ProductCatalogDataLayer();
                    var productSizeLogic = new ProductSizeLogic();

                    for (int i = 0; i < categoryList.Count; i++)
                    {
                        categoryList[i].ProductList = productDataLayer.ProductCatalogForMainPage
                        (
                            categoryList[i].Identifier, new PagingDTO { PageSize = productCount }
                        )?.Result;

                        for (int j = 0; j < categoryList[i].ProductList.Count; j++)
                        {
                            categoryList[i].ProductList[j].Sizes = productSizeLogic.ProductSizeGetFilteredList
                            (
                                categoryList[i].Identifier,
                                categoryList[i].ProductList[j].Identifier
                            ).Result;
                        }
                    }

                    dataResponse = new ResponseListDTO<ProductCategoryDTO>();
                    dataResponse.Result = categoryList;
                    dataResponse.Success = categoryList.Any() && categoryList.Any(p => p.ProductList.Any());
                }
            }
            catch (Exception exception)
            {
                exception.LogException();
            }
            return dataResponse;
        }

        /// <summary>
        /// Permite actualizar, eliminar o insertar 
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public ResponseDTO<ProductCategoryDTO> CategoryExecute(RequestDTO<ProductCategoryDTO> category)
        {
            var response = new ResponseDTO<ProductCategoryDTO>();
            try
            {
                if (category != null)
                {
                    var dataLayer = new ProductCategoryDataLayer();
                    switch (category.OperationType)
                    {
                        case OperationType.Merge:
                            response.Success = dataLayer.ProductCategoryMerge(category.Item);
                            break;
                        case OperationType.Delete:
                            response.Success = dataLayer.ProductCategoryChangeStatus(category.Item.Identifier);
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
    }
}
