
namespace ECommerceDataLayer
{
    using ECommerceDataLayer.Extensions;
    using ECommerceDataModel;
    using ECommerceDataModel.Shared;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    public class ProductCatalogDataLayer
    {
        public ResponseListDTO<ProductCatalogDTO> ProductCatalogGetFilteredList(RequestDTO<ProductCatalogDTO> product)
        {
            var response = new ResponseListDTO<ProductCatalogDTO> { Paging = new PagingDTO() };
            using (SqlCommand command = new SqlCommand("Usp_ProductCatalogFiltered_GETL"))
            {
                command.Parameters.Add("@ProductCatalogId", SqlDbType.VarChar).Value = product.Item.Identifier;
                command.Parameters.Add("@WordFilter", SqlDbType.VarChar).Value = product.WordFilter;
                command.Parameters.Add("@PageSize", SqlDbType.Int).Value = product.Paging.PageSize;
                command.Parameters.Add("@PageNumber", SqlDbType.Int).Value = product.Paging.PageNumber;
                command.Parameters.Add("@All", SqlDbType.Bit).Value = product.Paging.All;
                response.Result = command.Select(reader => reader.ToProductCatalog());
                response.Paging.TotalRecords = command.Select(reader => reader.ToTotalRecords()).FirstOrDefault();
                response.Paging.PageNumber = product.Paging.PageNumber;
                response.Paging.PageSize = product.Paging.PageSize;
            }
            return response;
        }

        public ResponseListDTO<ProductCatalogDTO> ProductCatalogGetListByCategory(RequestDTO<ProductCatalogDTO> product)
        {
            var productListResponse = new ResponseListDTO<ProductCatalogDTO>();
            using(SqlCommand command = new SqlCommand("Usp_ProductCatalogByCategory_GETL"))
            {
                command.Parameters.Add("@ProductCategoryId", SqlDbType.BigInt).Value = product.Item.ProductCategoryIdentifier;
                command.Parameters.Add("PageSize", SqlDbType.Int).Value = product.Paging.PageSize;
                command.Parameters.Add("PageNumber", SqlDbType.Int).Value = product.Paging.PageNumber;
                productListResponse.Result = command.Select(reader => reader.ToProductCatalog());
                productListResponse.Paging = new PagingDTO();
                productListResponse.Paging.TotalRecords = command.Select(reader => reader.ToTotalRecords()).FirstOrDefault();
            }
            return productListResponse;
        }

        public ResponseListDTO<ProductCatalogDTO> ProductCatalogForMainPage(long productCategoryIdentifier, PagingDTO paging)
        {
            var response = new ResponseListDTO<ProductCatalogDTO> { Paging = new PagingDTO() };
            using (SqlCommand command = new SqlCommand("Usp_ProductCatalog_MainPage_GETL"))
            {
                command.Parameters.AddWithValue("@CategoryId", productCategoryIdentifier);
                command.Parameters.AddWithValue("@PageSize", paging.PageSize);
                response.Result = command.Select(reader => reader.ToProductCatalog());
                response.Paging.TotalRecords = command.Select(reader => reader.ToTotalRecords()).FirstOrDefault();
            }
            return response;
        }

        public ProductCatalogDTO ProductCatalogGetItem(long productIdentifier)
        {
            var response = new ProductCatalogDTO();
            using (SqlCommand command = new SqlCommand("Usp_ProductCatalogById_GETI"))
            {
                command.Parameters.Add("@ProductCatalogId", SqlDbType.BigInt).Value = productIdentifier;
                response = command.Select(reader => reader.ToProductCatalog())?.FirstOrDefault();
            }
            return response;
        }

        public ProductCatalogDTO ProductCatalogMerge(ProductCatalogDTO productCatalog)
        {
            var product = new ProductCatalogDTO();
            using (SqlCommand command = new SqlCommand("Usp_ProductCatalog_MRG"))
            {
                command.Parameters.Add("@ProductCatalogId", SqlDbType.BigInt).Value = productCatalog.Identifier;
                command.Parameters.Add("@ProductCategoryId", SqlDbType.BigInt).Value = productCatalog.ProductCategoryIdentifier;
                command.Parameters.Add("@ProductShortName", SqlDbType.VarChar).Value = productCatalog.ShortName;
                command.Parameters.Add("@ProductDescription", SqlDbType.VarChar).Value = productCatalog.Description;
                command.Parameters.Add("@ProductDescriptionAditional", SqlDbType.VarChar).Value = productCatalog.AditionalDescription;
                command.Parameters.Add("@ProductPrice", SqlDbType.Decimal).Value = productCatalog.Price;
                command.Parameters.Add("@ProductImage", SqlDbType.VarChar).Value = productCatalog.ImageName;

                string execCommand = $"exec {command.CommandText}";
                foreach(SqlParameter parameter in command.Parameters)
                {
                    if(parameter.DbType == DbType.AnsiString)
                    {
                        execCommand += $"{parameter.ParameterName} = '{parameter.Value}',\n";
                    }
                    else
                    {
                        execCommand += $"{parameter.ParameterName} = {parameter.Value},\n";
                    }
                }
                product = command.Select(reader => reader.ToProductCatalog()).FirstOrDefault();
            }
            return product;
        }

        public bool ProductCatalogChangeStatus(long productCatalogIdentifier)
        {
            bool isUpdateComplete = default(bool);
            using (SqlCommand command = new SqlCommand("Usp_ProductCatalogChangeStatus_UPD"))
            {
                command.Parameters.Add("@ProductCatalogId", SqlDbType.BigInt).Value = productCatalogIdentifier;
                command.Parameters.Add("@StatusId", SqlDbType.Bit).Value = 0;
                isUpdateComplete = command.ExecuteQuery();
            }
            return isUpdateComplete;
        }
    }
}
