using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    
    public string conStr = ConfigurationManager.ConnectionStrings["DbDoc"].ToString();
    SqlConnection con;
    protected void Page_Load(object sender, EventArgs e)
    {
         lblBlank.Text= "";  
        //Session.RemoveAll();
         Session.Clear();
    }
    protected void btnAdd_Click1(object sender, EventArgs e)
    {
        if (txtUName.Text == "" || txtPw.Text == "")
        {
            lblBlank.Text = "សូមបំពេញព័ត៌មានឱ្យបានគ្រប់គ្រាន់!";
        }
        else
        {
            con = new SqlConnection(conStr);
            SqlCommand cmdCheck = new SqlCommand("Select * From tblUsers where UName=@UNameS and Pw=@PwS", con);
            cmdCheck.Parameters.AddWithValue("@UNameS", txtUName.Text);
            cmdCheck.Parameters.AddWithValue("@PwS", txtPw.Text);
            con.Open();
            SqlDataReader rdUName = cmdCheck.ExecuteReader();
            if (rdUName.Read())
            {
                if (rdUName[6].ToString() == "True")
                {
                    Session["user_name"] = txtUName.Text;
                    if (rdUName[5].ToString() == "0")
                        Response.Redirect("~/frmAdmin.aspx");
                    else if (rdUName[5].ToString() == "2")
                        Response.Redirect("~/frmUsers.aspx");
                    else
                        Response.Redirect("~/frmViewers.aspx");
                 }
                else
                {
                    txtUName.Text = "";
                    txtPw.Text = "";
                }
            }
            else
            {
                lblBlank.Text = "សូមអភ័យទោស គណនីនេះមិនត្រឹមត្រូវទេ!";
            }
        }
    }

    protected void btnClear_Click1(object sender, EventArgs e)
    {
        txtUName.Text = "";
        txtPw.Text = "";
        lblBlank.Text = "";
    }
}
