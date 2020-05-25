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
        public static ResponseListDTO<ProductCategoryDTO> CategoryGetList()
        {
            return new ProductCategoryLogic().CategoryGetList();
        }

        [WebMethod]
        public static ResponseListDTO<ProductCatalogDTO> ProductCatalogGetList(int productId, string wordFilter, int pageNumber, int pageSize, bool all)
        {
            var response = new ProductCatalogLogic().ProductCatalogGetFilteredList(new RequestDTO<ProductCatalogDTO>
            {
                Item = new ProductCatalogDTO { Identifier = productId },
                WordFilter = wordFilter,
                Paging = new PagingDTO { PageNumber = pageNumber, PageSize = pageSize, All = all }
            });
            return response;
        }
    }
}