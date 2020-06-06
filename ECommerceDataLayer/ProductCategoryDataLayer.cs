/// <summary>
/// namespace ECommerceDataLayer
/// </summary>
namespace ECommerceDataLayer
{
    using ECommerceDataLayer.Extensions;
    using ECommerceDataModel;
    using ECommerceDataModel.Shared;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Collections.Generic;
    using System;

    public class ProductCategoryDataLayer
    {
        public ResponseListDTO<ProductCategoryDTO> ProductCategoryGetList(RequestDTO<ProductCategoryDTO> category)
        {
            var response = new ResponseListDTO<ProductCategoryDTO> { Result = new List<ProductCategoryDTO>(),  Paging = new PagingDTO() };
            using (SqlCommand command = new SqlCommand("Usp_ProductCategory_GETL"))
            {
                command.Parameters.Add("@WordFilter", SqlDbType.VarChar).Value = category.WordFilter; 
                command.Parameters.Add("@PageSize", SqlDbType.Int).Value = category.Paging.PageSize;
                command.Parameters.Add("@PageNumber", SqlDbType.Int).Value = category.Paging.PageNumber;
                command.Parameters.Add("@All", SqlDbType.Bit).Value = category.Paging.All;
                response.Result = command.Select(reader => reader.ToProductCategory());
                response.Paging.TotalRecords = command.Select(reader => reader.ToTotalRecords()).FirstOrDefault();
                response.Paging.PageNumber = category.Paging.PageNumber;
                response.Paging.PageSize = category.Paging.PageSize;
            }
            return response;
        }

        public ProductCategoryDTO ProductCategoryGetItem(long categoryIdentifier)
        {
            var productCategory = new ProductCategoryDTO();
            using(SqlCommand command = new SqlCommand("Usp_ProductCategory_GETI"))
            {
                command.Parameters.AddWithValue("@ProductCategoryId", categoryIdentifier);
                productCategory = command.Select(reader => reader.ToProductCategory())?.FirstOrDefault();
            }
            return productCategory;
        }

        public bool ProductCategoryMerge(ProductCategoryDTO productCategory)
        {
            bool isMergeComplete = default(bool);
            using (SqlCommand command = new SqlCommand("Usp_ProductCategory_MRG"))
            {
                command.Parameters.Add("@ProductCategoryId", SqlDbType.BigInt).Value = productCategory.Identifier;
                command.Parameters.Add("@ProductCategoryName", SqlDbType.VarChar).Value = productCategory.Name;
                command.Parameters.Add("@ProductCategoryDescription", SqlDbType.VarChar).Value = productCategory.Description;
                isMergeComplete = command.ExecuteQuery();
            }
            return isMergeComplete;
        }

        public bool ProductCategoryChangeStatus(long productCategoryIdentifier)
        {
            bool isUpdateComplete = default(bool);
            using (SqlCommand command = new SqlCommand("Usp_ProductCategoryChangeStatus_UPD"))
            {
                command.Parameters.Add("@ProductCategoryId", SqlDbType.BigInt).Value = productCategoryIdentifier;
                command.Parameters.Add("@StatusId", SqlDbType.Bit).Value = 0;
                isUpdateComplete = command.ExecuteQuery();
            }
            return isUpdateComplete;
        }
    }
}
