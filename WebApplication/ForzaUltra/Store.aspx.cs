using ECommerce;
using ECommerceDataModel;
using ECommerceDataModel.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication.ForzaUltra
{
    public partial class Store : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //hfdMainPageProductCount.Value = System.Configuration.ConfigurationManager.AppSettings["MainPageProductCount"] ?? "3";
            }
        }

        [WebMethod]
        public static ResponseListDTO<ProductCategoryDTO> GetStoreGetList()
        {
            HttpContext.Current.Session["SessionInit"] = false;
            var response = new ProductCategoryLogic().CategoryListForMainPage(4);
            return response;
        }

        [WebMethod]
        public static ResponseDTO<CartDTO> CartItemExecute(CartDTO item)
        {
            item.Customer = new CustomerDTO
            {
                Identifier = 1
            };

            var request = new RequestDTO<CartDTO>
            {
                Item = item,
                OperationType = OperationType.Insert
            };


            if (HttpContext.Current.Session["CURRENT_CART_GUID"] != null)
            {
                var list = new CartLogic().CartGetFilteredList(new RequestDTO<CartDTO>
                {
                    Item = new CartDTO
                    {
                        Identifier = item.Identifier,
                        Customer = item.Customer
                    }
                });

                if (list.Success)
                {
                    var exist = list.Result.FirstOrDefault(x => x.ProductCatalog.Identifier == item.ProductCatalog.Identifier && x.ProductCategory.Identifier == item.ProductCategory.Identifier);
                    if (exist != null)
                    {
                        request.OperationType = OperationType.Update;
                        request.Item.Quantity += exist.Quantity;
                    }
                }

                request.Item.Identifier = HttpContext.Current.Session["CURRENT_CART_GUID"].ToString();
            }

            var response = new CartLogic().CartItemExecute(request);

            if (response.Success)
            {
                HttpContext.Current.Session["CURRENT_CART_GUID"] = response.Result.Identifier;
            }

            return response;
        }

        [WebMethod]
        public static ResponseListDTO<CartDTO> CartGetFilteredList()
        {
            var response = new CartLogic().CartGetFilteredList(new RequestDTO<CartDTO>
            {
                Item = new CartDTO
                {
                    Customer = new CustomerDTO
                    {
                        Identifier = 1
                    },
                    Identifier = HttpContext.Current.Session["CURRENT_CART_GUID"].ToString() ?? string.Empty
                }
            });
            return response;
        }
    }
}