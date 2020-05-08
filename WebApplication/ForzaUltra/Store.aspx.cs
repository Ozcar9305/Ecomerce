using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication.ForzaUltra
{
    public partial class Store : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                hfdMainPageProductCount.Value = System.Configuration.ConfigurationManager.AppSettings["MainPageProductCount"] ?? "3";
            }
        }
    }
}