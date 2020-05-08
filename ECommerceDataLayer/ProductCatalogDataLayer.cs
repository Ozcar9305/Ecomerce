
namespace ECommerceDataLayer
{
    using ECommerceDataLayer.Extensions;
    using ECommerceDataModel;
    using ECommerceDataModel.Shared;
    using System.Data.SqlClient;
    using System.Linq;

    public class ProductCatalogDataLayer
    {
        public ResponseListDTO<ProductCatalogDTO> ProductCatalogByCategoryId(long productCategoryIdentifier)
        {
            var response = new ResponseListDTO<ProductCatalogDTO>();
            using (SqlCommand command = new SqlCommand("Usp_ProductCatalogByCategoryId_GETL"))
            {
                command.Parameters.AddWithValue("@CategoryId", productCategoryIdentifier);
                response.Result = command.Select(reader => reader.ToProductCatalog());
                response.Paging.TotalRecords = command.Select(reader => reader.ToTotalRecords()).FirstOrDefault();
            }
            return response;
        }

        public ResponseListDTO<ProductCatalogDTO> ProductCatalogForMainPage(long productCategoryIdentifier, PagingDTO paging)
        {
            var response = new ResponseListDTO<ProductCatalogDTO>();
            using (SqlCommand command = new SqlCommand("Usp_ProductCatalog_MainPage_GETL"))
            {
                command.Parameters.AddWithValue("@CategoryId", productCategoryIdentifier);
                command.Parameters.AddWithValue("@PageSize", paging.PageSize);
                response.Result = command.Select(reader => reader.ToProductCatalog());
                response.Paging.TotalRecords = command.Select(reader => reader.ToTotalRecords()).FirstOrDefault();
            }
            return response;
        }

        public ResponseDTO<ProductCatalogDTO> ProductCatalogGetItem(long productIdentifier)
        {
            var response = new ResponseDTO<ProductCatalogDTO>();
            using (SqlCommand command = new SqlCommand("Usp_ProductCatalogById_GETI"))
            {
                command.Parameters.AddWithValue("@ProductId", productIdentifier);
                response.Result = command.Select(reader => reader.ToProductCatalog()).FirstOrDefault();
            }
            return response;
        }
    }
}
