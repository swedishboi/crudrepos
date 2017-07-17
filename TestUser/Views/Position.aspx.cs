using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TestUser.DTO;
using TestUser.DAL;
using TestUser.Models;

namespace TestUser.Views
{
    public partial class WebForm2 : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GridViewDataBinding();
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int _id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values["id"].ToString());
            bool flag = new Position().Remove(_id);
            if (flag)
            {
                GridView1.EditIndex = -1;
                GridViewDataBinding();
            }
            else
            {
                Response.Write(@"<script language='javascript'>alert('Что-то не так')</script>");
            }
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            TextBox _name = GridView1.Rows[e.RowIndex].FindControl("tbPosName") as TextBox;
            int _id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values["id"].ToString());
            bool flag = new Position().Edit(_id, _name.Text.Trim());
            if (flag)
            {
                GridView1.EditIndex = -1;
                GridViewDataBinding();
            }
            else
            {
                Response.Write(@"<script language='javascript'>alert('Что-то не так')</script>");
            }
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GridViewDataBinding();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            GridViewDataBinding();
        }
        public void GridViewDataBinding()
        {
            List<Position> posList = new Position().GetAll();
            GridView1.DataSource = posList;
            GridView1.DataBind();
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Position asd = new Position().Add(tbNewPos.Text.Trim());
            if (asd == null)
            {
                Response.Write(@"<script language='javascript'>alert('Что-то не так')</script>");
            }
            else
            {
                tbNewPos.Text = "";
                GridViewDataBinding();
            }
        }
    }
}