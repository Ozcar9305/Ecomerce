using System.Collections.Generic;

namespace ECommerceDataModel
{
    public class OrderDTO
    {
        public long Identifier { get; set; }

        public CustomerDTO Customer { get; set; }

        public List<CartDTO> CartItems { get; set; }

        public decimal TotalAmount { get; set; }

        public int Count { get; set; }
    }
}
