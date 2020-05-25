using ECommerce;
using ECommerceDataModel;
using ECommerceDataModel.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication.ForzaUltra
{
    public partial class Cart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static ResponseListDTO<CartDTO> CartItemGetList()
        {
            var response = new CartLogic().CartGetFilteredList(new RequestDTO<CartDTO>
            {
                Item = new CartDTO
                {
                    Customer = new CustomerDTO
                    {
                        Identifier = 1
                    },
                    Identifier = "D107605D-23A9-4204-81D0-1A7E6825679A"
                }
            });

            return response;
        }


        [WebMethod]
        public static ResponseDTO<CartDTO> DeleteCartItem(string cartId, int customerId, int productId, int categoryId)
        {
            var response = new CartLogic().CartItemExecute(new RequestDTO<CartDTO>
            {
                OperationType = OperationType.Delete,
                Item = new CartDTO
                {
                    Identifier = cartId,
                    Customer = new CustomerDTO { Identifier = customerId },
                    ProductCatalog = new ProductCatalogDTO { Identifier = productId },
                    ProductCategory = new ProductCategoryDTO { Identifier = categoryId }
                }
            });
            return response;
        }

        [WebMethod]
        public static ResponseDTO<CartDTO> UpdateCartItem(string cartId, int customerId, int productId, int categoryId, int quantity, int size)
        {
            var response = new CartLogic().CartItemExecute(new RequestDTO<CartDTO>
            {
                OperationType = OperationType.Update,
                Item = new CartDTO
                {
                    Identifier = cartId,
                    Customer = new CustomerDTO { Identifier = customerId },
                    ProductCatalog = new ProductCatalogDTO { Identifier = productId ,Sizes = new List<SizesDTO> { new SizesDTO { Identifier = size} } },
                    ProductCategory = new ProductCategoryDTO { Identifier = categoryId},
                    Quantity = quantity
                }
            });

            return response;
        }
    }
}