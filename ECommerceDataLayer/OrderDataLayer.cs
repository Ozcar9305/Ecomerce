
namespace ECommerceDataLayer
{
    using ECommerceDataLayer.Extensions;
    using ECommerceDataModel;
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
                command.Parameters.Add("@CustomerId", SqlDbType.Int).Value = order.Customer.Identifier;
                command.Parameters.Add("@CartItemId", SqlDbType.VarChar).Value = order.Identifier;
                order = command.Select(reader => reader.ToOrder()).FirstOrDefault();
            }
            return order;
        } 
    }
}
