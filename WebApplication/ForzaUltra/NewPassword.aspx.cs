namespace WebApplication.ForzaUltra
{
    using ECommerce;
    using ECommerceDataModel;
    using ECommerceDataModel.Shared;
    using System;
    using System.Web.Services;

    public partial class NewPassword : System.Web.UI.Page
    {
        /// <summary>
        /// Acceso a logica de logueo
        /// </summary>
        private static readonly LoginLogic loginLogic = new LoginLogic();

        /// <summary>
        /// Evento page load de la pagina
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
        
        }

        [WebMethod()]
        public static ResponseDTO<CustomerDTO> UpdateCustomerPassword(CustomerDTO customer)
        {
            return loginLogic.CustomerExecute(new RequestDTO<CustomerDTO>
            {
                Item = customer,
                OperationType = OperationType.ChangePassword
            });
        }
    }
}