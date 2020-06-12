namespace ECommerceDataModel
{
    public class CartDTO
    {
        public string Identifier { get; set; }

        public CustomerDTO Customer { get; set;}

        public ProductCategoryDTO ProductCategory { get; set; }

        public ProductCatalogDTO ProductCatalog { get; set; }
        
        public int Quantity { get; set; }

        public decimal TotalAmount { get; set; }

        public int SizeId { get; set; }
    }
}
