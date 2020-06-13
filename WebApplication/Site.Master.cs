namespace WebApplication
{
    using System;
    using System.Configuration;
    using System.Web;
    using System.Web.Configuration;
    using System.Web.UI;

    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["SessionInit"] != null && Convert.ToBoolean(Session["SessionInit"]))
                {
                    Configuration config = WebConfigurationManager.OpenWebConfiguration("~/Web.Config");
                    SessionStateSection section = (SessionStateSection)config.GetSection("system.web/sessionState");
                    int timeout = (int)section.Timeout.TotalMinutes * 1000 * 60;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "SessionAlert", "SessionExpireAlert(" + timeout + ");", true);
                }
            }
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.Response.Redirect("/ForzaUltra/Store.aspx");
        }
    }
}