namespace WebApplication.ForzaUltra
{
    using ECommerce;
    using ECommerceDataModel;
    using ECommerceDataModel.Shared;
    using System;
    using System.Web.Services;
    using System.Web.UI;

    public partial class RegisterForm : System.Web.UI.Page
    {
        private static LoginLogic loginLogic = new LoginLogic();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

            }
        }

        /// <summary>
        /// Permite registrar un nuevo usuario
        /// </summary>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        public static ResponseDTO<CustomerDTO> RegisterUser(CustomerDTO customer)
        {
            return loginLogic.RegisterUser(new RequestDTO<CustomerDTO>
            {
                Item = customer
            });
        } 
    }
}