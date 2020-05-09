
namespace ECommerceDataModel
{
    using ECommerceDataModel.Enum;

    public class CustomerDTO
    {
        public int Identifier { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string EncryptedPassword { get; set; }

        public string ShippingAddress { get; set; }

        public string PhoneNumber { get; set; }

        public CustomerRole Role { get; set; }
    }
}
