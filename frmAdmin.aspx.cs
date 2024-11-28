using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_form : System.Web.UI.Page
{
    public string conStr = ConfigurationManager.ConnectionStrings["DbDoc"].ToString();
    SqlConnection con;
    string vUser, getUId, getRoleId, getStaffId, getDepId;
    DataTable tdShow = new DataTable("tdShow");
    string DocIdS;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user_name"] == null)
        {
            Response.Redirect("~/Default.aspx");
        }
        else
        {
            vUser = Session["user_name"].ToString();
            if (!string.IsNullOrEmpty(vUser))
            {
                ((Label)Master.FindControl("lblLoginName")).Text = vUser;
                ((LinkButton)Master.FindControl("loginStatus")).Text = "Logout";
                getUserInfo();
                if (Convert.ToInt32(getRoleId) != 0)
                {
                    Response.Redirect("~/Default.aspx");
                }
            }
        }
    }

    private void getUserInfo()
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            string query = "SELECT UserId, RoleId, StaffId, DepId FROM tblUsers WHERE UName = @UNames";
            using (SqlCommand cmdGetUser = new SqlCommand(query, con))
            {
                cmdGetUser.Parameters.Add(new SqlParameter("@UNames", vUser));
                con.Open();

                using (SqlDataReader rdUName = cmdGetUser.ExecuteReader())
                {
                    if (rdUName.Read())
                    {
                        getUId = rdUName["UserId"].ToString();
                        getRoleId = rdUName["RoleId"].ToString();
                        getStaffId = rdUName["StaffId"].ToString();
                        getDepId = rdUName["DepId"].ToString();
                    }
                }
            }
        }
    }

    protected void btnAddrEditU_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/frmAddNEditeUsers.aspx");
    }


    protected void btnAddrEditDep_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/frmAddNEditDep.aspx");

    }
}