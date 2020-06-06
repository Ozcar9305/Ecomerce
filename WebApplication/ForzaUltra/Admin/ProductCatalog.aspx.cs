using ECommerce;
using ECommerceDataModel;
using ECommerceDataModel.Enum;
using ECommerceDataModel.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing.Imaging;
using System.IO;
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
            if (Session["SessionCustomerRole"] == null || Convert.ToInt32(Session["SessionCustomerRole"]) != (int)CustomerRole.Admin)
            {
                Response.Redirect("/ForzaUltra/Store.aspx");
            }
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

            //Guardar la imagen del producto
            if (response.Success && !string.IsNullOrEmpty(path) && !string.IsNullOrEmpty(product.ImageBase64))
            {
                System.Drawing.Image image;
                byte[] imageBytes = Convert.FromBase64String(product.ImageBase64);
                using (var ms = new MemoryStream(imageBytes))
                {
                    image = System.Drawing.Image.FromStream(ms);
                }
                File.WriteAllBytes(Path.Combine(path, product.ImageName), imageBytes);
            }
            return response;
        }

        [WebMethod]
        public static ResponseDTO<ProductCatalogDTO> GetItem(int productIdentifier)
        {
            var response = new ProductCatalogLogic().ProductCatalogGetItem(productIdentifier);
            return response;
        }

        [WebMethod]
        public static ResponseDTO<ProductCatalogDTO> Delete(int productIdentifier)
        {
            var response = new ProductCatalogLogic().ProductCatalogExecute(new RequestDTO<ProductCatalogDTO>
            {
                OperationType = OperationType.Delete,
                Item = new ProductCatalogDTO
                {
                    Identifier = productIdentifier
                }
            });
            return response;
        }
    }
}