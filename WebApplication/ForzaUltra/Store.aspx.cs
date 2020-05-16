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
    }
}