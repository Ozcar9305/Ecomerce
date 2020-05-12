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
    public partial class ProductCatalog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static ResponseListDTO<ProductCatalogDTO> GetList()
        {
            var response = new ProductCatalogLogic().ProductCatalogGetFilteredList(new RequestDTO<ProductCatalogDTO>
            {
                WordFilter = string.Empty,
                Item = new ProductCatalogDTO
                {
                    Identifier = default
                }
            });
            return response;
        }

        [WebMethod]
        public static ResponseDTO<ProductCatalogDTO> Merge(ProductCatalogDTO product)
        {
            var response = new ProductCatalogLogic().ProductCatalogExecute(new RequestDTO<ProductCatalogDTO>
            {
                OperationType = OperationType.Merge,
                Item = product
            });
            return response;
        }
    }
}