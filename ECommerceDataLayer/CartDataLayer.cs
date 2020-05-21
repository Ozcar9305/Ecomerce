namespace ECommerceDataLayer
{
    using ECommerceDataLayer.Extensions;
    using ECommerceDataModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    public class CartDataLayer
    {
        public List<CartDTO> CartGeFilteredtList(CartDTO cart)
        {
            var cartList = new List<CartDTO>();
            using(SqlCommand command = new SqlCommand("Usp_CartItems_GETL"))
            {
                command.Parameters.Add("@CustomerId", SqlDbType.Int).Value = cart.Customer?.Identifier;
                command.Parameters.Add("@CartItemId", SqlDbType.VarChar).Value = cart.Identifier;
                cartList = command.Select(reader => reader.ToCart());
            }
            return cartList;
        }
        
        public CartDTO CartAddItem(CartDTO cart)
        {
            var cartResult = new CartDTO();
            using (SqlCommand command = new SqlCommand("Usp_CartItems_INS"))
            {
                command.Parameters.Add("@CartItemId", SqlDbType.NVarChar).Value = cart.Identifier;
                command.Parameters.Add("@CustomerId", SqlDbType.Int).Value = cart.Customer.Identifier;
                command.Parameters.Add("@ProductCategoryId", SqlDbType.BigInt).Value = cart.ProductCategory.Identifier;
                command.Parameters.Add("@ProductCatalogId", SqlDbType.BigInt).Value = cart.ProductCatalog.Identifier;
                command.Parameters.Add("@ProductPrice", SqlDbType.Decimal).Value = cart.ProductCatalog.Price;
                command.Parameters.Add("@Quantity", SqlDbType.Int).Value = cart.Quantity;
                command.Parameters.Add("@SizeId", SqlDbType.Int).Value = cart.ProductCatalog.Sizes[0].Identifier;

                string execCommand = $"exec {command.CommandText}";
                foreach (SqlParameter parameter in command.Parameters)
                {
                    if (parameter.DbType == DbType.AnsiString)
                    {
                        execCommand += $"{parameter.ParameterName} = '{parameter.Value}',\n";
                    }
                    else
                    {
                        execCommand += $"{parameter.ParameterName} = {parameter.Value},\n";
                    }
                }

                cartResult.Identifier = command.Escalar<string>();                
            }
            return cartResult;
        }

        public bool CartUpdateItem(CartDTO item)
        {
            bool isCartUpdated = default(bool);
            using(SqlCommand command = new SqlCommand("Usp_CartItems_UPD"))
            {
                command.Parameters.Add("@CartItemId", SqlDbType.NVarChar).Value = item.Identifier;
                command.Parameters.Add("@CustomerId", SqlDbType.Int).Value = item.Customer.Identifier;
                command.Parameters.Add("@ProductCategoryId", SqlDbType.BigInt).Value = item.ProductCategory.Identifier;
                command.Parameters.Add("@ProductCatalogId", SqlDbType.BigInt).Value = item.ProductCatalog.Identifier;
                command.Parameters.Add("@Quantity", SqlDbType.Int).Value = item.Quantity;
                isCartUpdated = command.ExecuteQuery();
            }
            return isCartUpdated;
        }

        public bool CartDeleteItem(CartDTO item)
        {
            bool isCartDeleted = default(bool);
            using (SqlCommand command = new SqlCommand("Usp_CartItems_DEL"))
            {
                command.Parameters.Add("@CartItemId", SqlDbType.NVarChar).Value = item.Identifier;
                command.Parameters.Add("@CustomerId", SqlDbType.Int).Value = item.Customer.Identifier;
                command.Parameters.Add("@ProductCategoryId", SqlDbType.BigInt).Value = item.ProductCategory.Identifier;
                command.Parameters.Add("@ProductCatalogId", SqlDbType.BigInt).Value = item.ProductCatalog.Identifier;
                isCartDeleted = command.ExecuteQuery();
            }
            return isCartDeleted;
        }
    }
}
