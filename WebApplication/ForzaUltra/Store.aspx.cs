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
            var response = new ProductCategoryLogic().CategoryListForMainPage(4);
            return response;
        }

        [WebMethod(EnableSession=true)]
        public static ResponseDTO<CartDTO> CartItemExecute(CartDTO item)
        {
            if(HttpContext.Current.Session["CURRENT_CART_GUID"] != null)
            {
                if (!string.IsNullOrEmpty(HttpContext.Current.Session["CURRENT_CART_GUID"].ToString()) &&
                    !string.IsNullOrWhiteSpace(HttpContext.Current.Session["CURRENT_CART_GUID"].ToString())) 
                {
                    item.Identifier = HttpContext.Current.Session["CURRENT_CART_GUID"].ToString();
                }
            }

            item.Customer = new CustomerDTO
            {
                Identifier = int.Parse(HttpContext.Current.Session["SessionCustomerIdentifier"].ToString())
            };

            var request = new RequestDTO<CartDTO>
            {
                Item = item,
                OperationType = OperationType.Insert
            };

            var response = new CartLogic().CartItemExecute(request);
            if (response.Success)
            {
                HttpContext.Current.Session["SessionCartIdentifier"] = response.Result.Identifier;
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
                        Identifier = int.Parse(HttpContext.Current.Session["SessionCustomerIdentifier"].ToString())
                    },
                    Identifier = HttpContext.Current.Session["SessionCartIdentifier"].ToString() ?? string.Empty
                }
            });
            return response;
        }
    }
}