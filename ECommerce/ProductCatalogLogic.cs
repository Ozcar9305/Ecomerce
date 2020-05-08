namespace ECommerce
{
    using ECommerceDataLayer;
    using ECommerceDataModel;
    using ECommerceDataModel.Shared;
    using System;
    using System.Linq;

    public class ProductCatalogLogic
    {
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
                    for (int i = 0; i < categoryList.Count; i++)
                    {
                        categoryList[i].ProductList = productDataLayer.ProductCatalogForMainPage
                        (
                            categoryList[i].Identifier, new PagingDTO
                            {
                                PageSize = productCount
                            }
                        )?.Result;
                    }
                    dataResponse.Result = categoryList;
                    dataResponse.Success = categoryList.Any() && categoryList.Any(p => p.ProductList.Any());
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return dataResponse;
        }
    }
}
