
namespace ECommerceDataLayer.Extensions
{
    using ECommerceDataModel;
    using ECommerceDataModel.Enum;
    using System.Data;

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
                Identifier = reader.Get<long>("ProductCatalogId"),
                ProductCategoryIdentifier = reader.Get<long>("ProductCategoryId"),
                ProductCategory = reader.Get<string>("ProductCategoryName"),
                ShortName = reader.Get<string>("ProductShortName"),
                Description = reader.Get<string>("ProductDescription"),
                AditionalDescription = reader.Get<string>("ProductDescriptionAditional"),
                Price = reader.Get<decimal>("ProductPrice"),
                Status = reader.Get<bool>("ProductStatus"),
                ImageName = reader.Get<string>("ProductImage")
            };
        }

        public static CustomerDTO ToCustomer(this IDataReader reader)
        {
            return new CustomerDTO
            {
                Identifier = reader.Get<int>("CustomerId"),
                FirstName = reader.Get<string>("FirstName"),
                LastName = reader.Get<string>("LastName"),
                Email = reader.Get<string>("Email"),
                EncryptedPassword = reader.Get<string>("EncryptedPassword"),
                ShippingAddress = reader.Get<string>("ShippingAddress"),
                Role = (CustomerRole)reader.Get<int>("CustomerRoleId"),
                BillingInformation = new BillingInformation
                {
                    RFC = reader.Get<string>("Rfc")
                },
                Status = reader.Get<bool>("StatusId")
            };
        }

        public static CartDTO ToCart(this IDataReader reader)
        {
            return new CartDTO
            {
                Identifier = reader.Get<string>("CartItemId"),
                Quantity = reader.Get<int>("Quantity"),
                TotalAmount = reader.Get<decimal>("TotalAmount"),
                ProductCatalog = ToProductCatalog(reader),
                ProductCategory = ToProductCategory(reader),
                Customer = ToCustomer(reader),
                SizeId = reader.Get<int>("SizeId")
            };
        }

        public static SizesDTO ToSize(this IDataReader reader)
        {
            return new SizesDTO
            {
                Identifier = reader.Get<int>("SizeId"),
                Name = reader.Get<string>("SizeName"),
                Abreviature = reader.Get<string>("SizeAbreviature"),
                Status = reader.Get<bool>("StatusId")
            };
        }

        public static OrderDTO ToOrder(this IDataReader reader)
        {
            return new OrderDTO 
            {
                Identifier = reader.Get<long>("OrderId"),
                Count = reader.Get<int>("ItemsCount"),
                TotalAmount = reader.Get<decimal>("ItemsTotalAmount"),
                Customer = reader.ToCustomer()
            };
        }

        public static int ToTotalRecords(this IDataReader reader)
        {
            return reader.Get<int>("TotalCount");
        }
    }
}
