
namespace ECommerceDataModel
{
    using System.Collections.Generic;

    public class ProductCatalogDTO
    {
        public long Identifier { get; set; }

        public long ProductCategoryIdentifier { get; set; }

        public string ShortName { get; set; }

        public string Description { get; set; }

        public string AditionalDescription { get; set; }

        public decimal Price { get; set; }

        public string ImageName { get; set; }

        public List<SizesDTO> Sizes { get; set; }

        public bool ApplyDiscount { get; set; }

        public int DiscountAmount { get; set; }

        public bool Status { get; set; }
        public string ImageBase64 { get; set; }
    }
}
