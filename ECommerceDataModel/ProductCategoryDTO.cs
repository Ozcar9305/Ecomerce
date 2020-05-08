
namespace ECommerceDataModel
{
    using System.Collections.Generic;

    public class ProductCategoryDTO
    {
        public long Identifier { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Status { get; set; }

        public List<ProductCatalogDTO> ProductList { get; set; }
    }
}
