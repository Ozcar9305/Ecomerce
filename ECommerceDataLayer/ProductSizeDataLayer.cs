
namespace ECommerceDataLayer
{
    using ECommerceDataLayer.Extensions;
    using ECommerceDataModel;
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
    }
}
