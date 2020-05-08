using ECommerceDataModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceDataLayer.Extensions
{
    public static class MapExtensions
    {
        public static ProductCategoryDTO ToProductCategory(this IDataReader reader)
        {
            return new ProductCategoryDTO
            {
                Identifier = reader.Get<long>("ProductCategoryId"),
                Name = reader.Get<string>("ProductCategoryName"),
                Description = reader.Get<string>("ProductCategoryDescription"),
                Status = reader.Get<bool>("ProductCategoryStatus")
            };
        }

        public static ProductCatalogDTO ToProductCatalog(this IDataReader reader)
        {
            return new ProductCatalogDTO
            {
                Identifier = reader.Get<long>("ProductCatalogIdentifier"),
                ProductCategoryIdentifier = reader.Get<long>("ProductCategoryId"),
                ShortName = reader.Get<string>("ProductShortName"),
                Description = reader.Get<string>("ProductDescription"),
                AditionalDescription = reader.Get<string>("ProductDescriptionAditional"),
                Price = reader.Get<decimal>("ProductPrice"),
                Status = reader.Get<bool>("ProductStatus")
            };
        }

        public static int ToTotalRecords(this IDataReader reader)
        {
            return reader.Get<int>("TotalCount");
        }
    }
}
