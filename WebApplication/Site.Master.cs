namespace WebApplication
{
    using System;
    using System.Web;
    using System.Web.UI;

    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                
            } 
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.Response.Redirect("/ForzaUltra/Store.aspx");
        }
    }
}