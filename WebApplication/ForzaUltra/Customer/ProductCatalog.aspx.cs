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

namespace WebApplication.ForzaUltra.Customer
{
    public partial class ProductCatalog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod()]
        public static ResponseListDTO<ProductCategoryDTO> CategoryGetList(string wordFilter)
        {            
            return new ProductCategoryLogic().CategoryGetList(new RequestDTO<ProductCategoryDTO>
            {
                WordFilter = wordFilter,
                Paging = new PagingDTO
                {
                    All = true,
                    PageNumber = 0,
                    PageSize = 0
                }
            });
        }

        [WebMethod]
        public static ResponseListDTO<ProductCatalogDTO> ProductCatalogGetList(int productId, int categoryId, string wordFilter, int pageNumber, int pageSize, bool all)
        {
            var response = new ProductCatalogLogic().ProductCatalogGetListByCategory(new RequestDTO<ProductCatalogDTO>
            {
                Item = new ProductCatalogDTO { Identifier = productId, ProductCategoryIdentifier = categoryId },
                WordFilter = wordFilter,
                Paging = new PagingDTO { PageNumber = pageNumber, PageSize = pageSize, All = all }
            });
            return response;
        }

        [WebMethod]
        public static ResponseDTO<CartDTO> CartItemExecute(CartDTO item)
        {
            var response = new ResponseDTO<CartDTO>();
            if (HttpContext.Current.Session["SessionInit"] != null && bool.Parse(HttpContext.Current.Session["SessionInit"].ToString()))
            {
                item.Identifier = (HttpContext.Current.Session["SessionCartIdentifier"] == null) ? string.Empty : HttpContext.Current.Session["SessionCartIdentifier"].ToString();

                item.Customer = new CustomerDTO
                {
                    Identifier = int.Parse(HttpContext.Current.Session["SessionCustomerIdentifier"].ToString())
                };

                var request = new RequestDTO<CartDTO>
                {
                    Item = item,
                    OperationType = OperationType.Insert
                };

                if (HttpContext.Current.Session["SessionCartIdentifier"] != null)
                {
                    var currentCartItems = new CartLogic().CartGetFilteredList(new RequestDTO<CartDTO>
                    {
                        Item = new CartDTO
                        {
                            Customer = new CustomerDTO
                            {
                                Identifier = int.Parse(HttpContext.Current.Session["SessionCustomerIdentifier"].ToString())
                            },
                            Identifier = HttpContext.Current.Session["SessionCartIdentifier"].ToString()
                        }
                    });

                    var product = currentCartItems.Result.FirstOrDefault(p => (p.ProductCatalog.Identifier == item.ProductCatalog.Identifier
                   && p.ProductCategory.Identifier == item.ProductCategory.Identifier));

                    if (product != null)
                    {
                        request.Item.Quantity += 1;
                        request.OperationType = OperationType.Update;
                    }
                }

                response = new CartLogic().CartItemExecute(request);

                if (response.Success && request.OperationType == OperationType.Insert)
                {
                    HttpContext.Current.Session["SessionCartIdentifier"] = response.Result.Identifier;
                }

                response.SessionInit = true;
            }
            return response;
        }
    }
}