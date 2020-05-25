namespace ECommerce
{
    using ECommerceDataModel.Shared;
    using ECommerceDataModel;

    public class MailLogic
    {
        LoginLogic loginLogic = new LoginLogic();

        public void SendForgottenPassword(string email)
        {
            var customerResponse = loginLogic.CustomerGetItem(new RequestDTO<ECommerceDataModel.CustomerDTO>
            {
                Item = new CustomerDTO
                {
                    Email = email
                }
            });

            if (customerResponse.Success)
            {
                //string customerPassword = loginLogic.ValidatePassword
            }
        }
    }
}
