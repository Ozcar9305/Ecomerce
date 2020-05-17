using ECommerce;
using ECommerceDataModel;
using ECommerceDataModel.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        public static ResponseListDTO<ProductCatalogDTO> GetList(RequestDTO<ProductCatalogDTO> request)
        {
            var response = new ProductCatalogLogic().ProductCatalogGetFilteredList(request);
            return response;
        }

        [WebMethod]
        public static ResponseDTO<ProductCatalogDTO> Merge(ProductCatalogDTO product)
        {
            var path = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["ProductImagesDirectoryPath"]);
            var response = new ProductCatalogLogic().ProductCatalogExecute(new RequestDTO<ProductCatalogDTO>
            {
                OperationType = OperationType.Merge,
                Item = product,
                ServerPath = path
            });
            return response;
        }

        [WebMethod]
        public static ResponseDTO<ProductCatalogDTO> GetItem(int productIdentifier)
        {
            var response = new ProductCatalogLogic().ProductCatalogGetItem(productIdentifier);
            return response;
        }
    }
}