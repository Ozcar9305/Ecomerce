namespace WebApplication
{
    using System;
    using System.Web.UI;
    using System.Web.Services;
    using System.IO;
    using System.Web;

    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Button1.Click += Button1_Click;
            if (!Page.IsPostBack)
            {
                
            } 

            
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Response.Redirect("/ForzaUltra/Store.aspx");
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            
        }
    }
}