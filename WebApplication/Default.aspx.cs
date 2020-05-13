using ECommerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //Prueba de CategoryListForMainPage
                //var categoryResponse = new ProductCategoryLogic().CategoryListForMainPage(4);

                //var productItem = new ProductCatalogLogic().ProductCatalogGetItem(1);

                //var x = new ProductCatalogLogic().ProductCatalogGetFilteredList
                //(
                //    new ECommerceDataModel.Shared.RequestDTO<ECommerceDataModel.ProductCatalogDTO>
                //    {
                //        Item = new ECommerceDataModel.ProductCatalogDTO
                //        {
                //            Identifier = 1
                //        },
                //        WordFilter = ""
                //    }
                // );

            }
        }
    }
}