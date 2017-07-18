using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestUser.Views
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lbProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Main.aspx");
        }

        protected void lbHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Main.aspx");
        }

        protected void lbPosition_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Position.aspx");
        }

        protected void lbItemType_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/ItemType.aspx");
        }
    }
}