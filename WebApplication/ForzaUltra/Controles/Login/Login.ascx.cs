
namespace WebApplication.ForzaUltra.Controles.Login
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Net.Mail;
    using System.Web;
    using System.Web.Configuration;
    using System.Web.Services;
    using ECommerce;
    using ECommerce.Helpers;
    using ECommerceDataModel;
    using ECommerceDataModel.Shared;
    using Microsoft.Ajax.Utilities;

    public partial class Login : System.Web.UI.UserControl
    {
        /// <summary>
        /// Acceso a logica de logueo
        /// </summary>
        private readonly LoginLogic loginLogic = new LoginLogic();

        /// <summary>
        /// Evento page load de la pagina
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //Establecer las cookies de usuario y contraseña
                if (Request.Cookies["forzaUltraUser"] != null && Request.Cookies["forzaUltraPwd"] != null)
                {
                    usrname.Text = Request.Cookies["forzaUltraUser"].Value;
                    psw.Text = Request.Cookies["forzaUltraPwd"].Value;
                    chkRememberMe.Checked = true;
                }
            }
        }

        /// <summary>
        /// Permite validar la constraseña o enviarle al usuario una si la ha olvidado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnValidateLogin_Click(object sender, EventArgs e)
        {
            switch (hdnLoginButtonAction.Value)
            {
                case "Login":
                    logUser();
                    break;
                case "ForgotPassword":
                    sendForgotPasswordEmail();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Envia al usuario la contraseña olvidada
        /// </summary>
        private void sendForgotPasswordEmail()
        {
            try
            {
                //Validar la existencia del usuario
                var customerResponse = loginLogic.CustomerGetItem(new RequestDTO<CustomerDTO>
                {
                    Item = new CustomerDTO
                    {
                        Email = usrname.Text.Trim()
                    }
                });

                //Si el usuario existe
                if (customerResponse.Success)
                {
                    var newPasswordUrl = string.Format("{0}?tk={1}", ToAbsoluteUrl("~/ForzaUltra/NewPassword.aspx"), customerResponse.Result.EncryptedPassword);

                    //Obtener la ruta para el archivo - cuerpo del correo
                    var emailBodyPath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["ForgotPasswordEmailBodyPath"]);
                    if (File.Exists(emailBodyPath))
                    {
                        string content;
                        using (StreamReader reader = new StreamReader(emailBodyPath))
                        {
                            content = reader.ReadToEnd();
                        }

                        //Validar el contenido del archivo email body
                        if (!string.IsNullOrEmpty(content))
                        {
                            string emailBody = string.Format(content, newPasswordUrl);
                            new EmailHelper().SendEmail(new MailDTO
                            {
                                EmailSubject = "Recuperación de contraseña ForzaUltra",
                                EmailBody = emailBody,
                                EmailTo = customerResponse.Result.Email
                            });
                        }
                    }
                }
                else
                {
                    //TODO: Usuario no valido
                }
            }
            catch (Exception ex)
            {
                ex.LogException();
            }
        }

        /// <summary>
        /// Inicia la variable de sesion del usuario logueado
        /// </summary>
        private void logUser()
        {
            //Validar la existencia del usuario
            var customerResponse = loginLogic.CustomerGetItem(new RequestDTO<CustomerDTO>
            {
                Item = new CustomerDTO
                {
                    Email = usrname.Text.Trim()
                }
            });

            //Si el usuario existe
            if (customerResponse.Success)
            {
                //Validar la contraseña del usuario
                if (loginLogic.ValidatePassword(psw.Text.Trim(), customerResponse.Result.EncryptedPassword))
                {
                    Session["SessionInit"] = true;
                    Session["SessionEmail"] = customerResponse.Result.Email;
                    Session["SessionCustomerIdentifier"] = customerResponse.Result.Identifier;
                    Session["SessionCustomerRole"] = customerResponse.Result.Role;
                    Session["SeccionFullName"] = string.Format("{0} {1}", customerResponse.Result.FirstName, customerResponse.Result.LastName);

                    var cartItemResponse = new CartLogic().CartGetFilteredList(new RequestDTO<CartDTO>
                    {
                        Item = new CartDTO
                        {
                            Identifier = string.Empty,
                            Customer = new CustomerDTO { Identifier = customerResponse.Result.Identifier }
                        }
                    });


                    Session["SessionCartIdentifier"] = (cartItemResponse.Success && cartItemResponse.Result.Count > 0) ? cartItemResponse.Result.FirstOrDefault().Identifier : string.Empty;

                    if (chkRememberMe.Checked)
                    {
                        //Establecer las cookies de usuario y contraseña
                        Response.Cookies["forzaUltraUser"].Value = usrname.Text;
                        Response.Cookies["forzaUltraPwd"].Value = psw.Text;

                        //Establecer el periodo de expiracion de las cookies
                        Response.Cookies["forzaUltraUser"].Expires = DateTime.Now.AddDays(15);
                        Response.Cookies["forzaUltraPwd"].Expires = DateTime.Now.AddDays(15);
                    }
                    else
                    {
                        Response.Cookies["forzaUltraUser"].Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies["forzaUltraPwd"].Expires = DateTime.Now.AddDays(-1);
                    }

                    //Establecer el time out de la sesion
                    Configuration config = WebConfigurationManager.OpenWebConfiguration("~/Web.Config");
                    SessionStateSection section = (SessionStateSection)config.GetSection("system.web/sessionState");
                    int timeout = (int)section.Timeout.TotalMinutes * 1000 * 60;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "SessionAlert", "SessionExpireAlert(" + timeout + ");", true);

                    //Redirigir al usuario a la pantalla de categoriaas
                    if (customerResponse.Result.Role == ECommerceDataModel.Enum.CustomerRole.Admin)
                    {
                        Response.Redirect("~/ForzaUltra/Admin/Categories.aspx", false);
                    }
                }
                else
                {
                    //TODO: Contraseña invalida
                }
            }
            else
            {
                //TODO: "Usuario no existente"
            }
        }

        /// <summary>
        /// Converts the provided app-relative path into an absolute Url containing the 
        /// full host name
        /// </summary>
        /// <param name="relativeUrl">App-Relative path</param>
        /// <returns>Provided relativeUrl parameter as fully qualified Url</returns>
        /// <example>~/path/to/foo to http://www.web.com/path/to/foo</example>
        public string ToAbsoluteUrl(string relativeUrl)
        {
            if (string.IsNullOrEmpty(relativeUrl))
                return relativeUrl;

            if (HttpContext.Current == null)
                return relativeUrl;

            if (relativeUrl.StartsWith("/"))
                relativeUrl = relativeUrl.Insert(0, "~");

            if (!relativeUrl.StartsWith("~/"))
                relativeUrl = relativeUrl.Insert(0, "~/");

            var url = HttpContext.Current.Request.Url;
            var port = url.Port != 80 ? (":" + url.Port) : String.Empty;

            return String.Format("{0}://{1}{2}{3}",
                url.Scheme, url.Host, port, VirtualPathUtility.ToAbsolute(relativeUrl));
        }

        
    }
}