
namespace ECommerceDataLayer
{
    using ECommerceDataLayer.Extensions;
    using ECommerceDataModel;
    using System.Linq;
    using System.Data;
    using System.Data.SqlClient;
    using System;

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


        public CustomerDTO GetCustomerByEmail(CustomerDTO item)
        {
            var customer = new CustomerDTO();
            using(SqlCommand command = new SqlCommand("Usp_CustomersByEmail_GETI"))
            {
                command.Parameters.Add("@Email", SqlDbType.VarChar).Value = item.Email;
                customer = command.Select(reader => reader.ToCustomer())?.FirstOrDefault();
            }
            return customer;
        }

        public bool CustomerChangePassword(CustomerDTO item)
        {
            bool isPasswordUpdated = default(bool);
            using (SqlCommand command = new SqlCommand("Usp_CustomerPassword_UPD"))
            {
                command.Parameters.Add("@CustomerId", SqlDbType.Int).Value = item.Identifier;
                command.Parameters.Add("@Email", SqlDbType.VarChar).Value = item.Email;
                command.Parameters.Add("@EncryptedPassword", SqlDbType.VarChar).Value = item.EncryptedPassword;
                isPasswordUpdated = command.ExecuteQuery();
            }
            return isPasswordUpdated;
        }
    }
}
