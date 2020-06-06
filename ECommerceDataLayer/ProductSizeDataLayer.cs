
namespace ECommerceDataLayer
{
    using ECommerceDataLayer.Extensions;
    using ECommerceDataModel;
    using ECommerceDataModel.Shared;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    public class ProductSizeDataLayer
    {
        public List<SizesDTO> ProductSizeGetFilteredList(long productCategoryIdentifier, long productCatalogIdentifier)
        {
            var sizesList = new List<SizesDTO>();
            using(SqlCommand command = new SqlCommand("Usp_ProductSize_GETL"))
            {
                command.Parameters.Add("@ProductCategoryId", SqlDbType.BigInt).Value = productCategoryIdentifier;
                command.Parameters.Add("@ProductCatalogId", SqlDbType.BigInt).Value = productCatalogIdentifier;
                sizesList = command.Select(reader => reader.ToSize());
            }
            return sizesList;
        }

        public bool ProductSizeMerge(RequestDTO<ProductCatalogDTO> product, string json)
        {
            bool sizesUpdated = default(bool);
            using(SqlCommand command = new SqlCommand("Usp_ProductSizes_INS"))
            {
                command.Parameters.Add("@ProductCategoryId", SqlDbType.BigInt).Value = product.Item.ProductCategoryIdentifier;
                command.Parameters.Add("@ProductCatalogId", SqlDbType.BigInt).Value = product.Item.Identifier;
                command.Parameters.Add("@ProductSizesJson", SqlDbType.NVarChar).Value = json;
                sizesUpdated = command.ExecuteQuery();
            }
            return sizesUpdated;
        }
    }
}
