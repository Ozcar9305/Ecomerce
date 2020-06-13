
namespace ECommerceDataLayer
{
    using System.Collections.Generic;
    using ECommerceDataLayer.Extensions;
    using ECommerceDataModel;
    using ECommerceDataModel.Shared;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    public class OrderDataLayer
    {
        public OrderDTO OrderInsert(OrderDTO orderItem)
        {
            var order = new OrderDTO();
            using(SqlCommand command = new SqlCommand("Usp_Order_INS"))
            {
                command.Parameters.Add("@CustomerId", SqlDbType.Int).Value = orderItem.Customer.Identifier;
                command.Parameters.Add("@CartItemId", SqlDbType.VarChar).Value = orderItem.CartItems.FirstOrDefault().Identifier;
                order = command.Select(reader => reader.ToOrder()).FirstOrDefault();
            }
            return order;
        }

        public OrderDTO OrderGetFilteredList(RequestDTO<OrderDTO> orderItem)
        {
            var order = new OrderDTO { CartItems = new List<CartDTO>() };
            using(SqlCommand command = new SqlCommand("Usp_Order_GETL"))
            {
                command.Parameters.Add("@OrderId", SqlDbType.BigInt).Value = orderItem.Item.Identifier;
                order = command.Select(reader => reader.ToOrder()).FirstOrDefault();
                order.CartItems = command.Select(reader => reader.ToCart());
                order.CartItems.ForEach(cartItem => 
                {
                    cartItem.ProductCatalog.Sizes = command.Select(reader => reader.ToSize());
                });
            }
            return order;
        }
    }
}
