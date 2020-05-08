namespace WebApplication.ForzaUltra
{
    using ECommerce;
    using ECommerceDataModel;
    using ECommerceDataModel.Shared;
    using System;
    using System.Web;
    using System.Web.Services;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class Categories : Page
    {
        /// <summary>
        /// Evento page load del formulario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                hdnCurrentPage.Value = HttpContext.Current.Request.Url.AbsoluteUri;
                hdnCategoryIdentifier.Value = "0";
            }
        }

        /// <summary>
        /// Carga el gridview de categorias
        /// </summary>
        [WebMethod()]
        public static ResponseListDTO<ProductCategoryDTO> LoadCategoryList()
        {
            var categoryLogic = new ProductCategoryLogic();
            return categoryLogic.CategoryGetList();
        }
    }
}