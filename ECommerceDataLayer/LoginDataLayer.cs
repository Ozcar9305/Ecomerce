
namespace ECommerceDataLayer
{
    using ECommerceDataLayer.Extensions;
    using ECommerceDataModel;
    using System.Data;
    using System.Data.SqlClient;

    public class LoginDataLayer
    {
        public bool RegisterUser(CustomerDTO customer)
        {
            bool isCustomerRegistered = default(bool);
            using(SqlCommand command = new SqlCommand("Usp_Customers_INS"))
            {
                command.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = customer.FirstName;
                command.Parameters.Add("@LastName", SqlDbType.VarChar).Value = customer.LastName;
                command.Parameters.Add("@Email", SqlDbType.VarChar).Value = customer.Email;
                command.Parameters.Add("@EncryptedPassword", SqlDbType.VarChar).Value = customer.EncryptedPassword;
                command.Parameters.Add("@ShippingAddress", SqlDbType.VarChar).Value = customer.ShippingAddress;
                command.Parameters.Add("@CustomerRoleId", SqlDbType.Int).Value = (int)customer.Role;
                isCustomerRegistered = command.ExecuteQuery();
            }
            return isCustomerRegistered;
        }
    }
}
