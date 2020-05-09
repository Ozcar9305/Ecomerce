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
        private static readonly ProductCategoryLogic categoryLogic = new ProductCategoryLogic();

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
        public static ResponseListDTO<ProductCategoryDTO> CategoryGetList()
        {
            return categoryLogic.CategoryGetList();
        }

        /// <summary>
        /// Permite obtener los datos de la categoria apartir del identificador
        /// </summary>
        /// <param name="categoryIdentifier"></param>
        /// <returns></returns>
        [WebMethod()]
        public static ResponseDTO<ProductCategoryDTO> CategoryGetItem(long categoryIdentifier)
        {
            return categoryLogic.CategoryGetItem(categoryIdentifier);
        }

        /// <summary>
        /// Permite actualizar, insertar o eliminar una categoria
        /// </summary>
        /// <param name="categoryItem"></param>
        /// <returns></returns>
        [WebMethod()]
        public static ResponseDTO<ProductCategoryDTO> CategoryMerge(ProductCategoryDTO category)
        {
            var productCategoryRequest = new RequestDTO<ProductCategoryDTO>
            {
                Item = category,
                OperationType = OperationType.Merge
            };
            return categoryLogic.CategoryExecute(productCategoryRequest);
        }

        /// <summary>
        /// Permite actualizar el status de una categoria
        /// </summary>
        /// <param name="categoryIdentifier"></param>
        /// <returns></returns>
        [WebMethod()]
        public static ResponseDTO<ProductCategoryDTO> CategoryChangeStatus(long categoryIdentifier)
        {
            return categoryLogic.CategoryExecute(new RequestDTO<ProductCategoryDTO>
            {
                Item = new ProductCategoryDTO
                {
                    Identifier = categoryIdentifier
                },
                OperationType = OperationType.Delete
            });
        }
    }
}