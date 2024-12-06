using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CreateUser : System.Web.UI.Page
{
    public string conStr = ConfigurationManager.ConnectionStrings["DbDoc"].ToString();
    SqlConnection con;
    string vUser, getUId, getRoleId, getStaffId, getDepId;
    DataTable tdShow = new DataTable("tdShow");
    string uIdS;
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
        if (!IsPostBack)
        {
            BtnEdit.Enabled = false;
            BtnInput.Enabled = false;
            rdAdd.Checked = true;
            drpSearch.Enabled = false;
            txtSearch.Enabled = false;
            BtnSearch.Enabled = false;
            ShowData();
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
    private void ShowData()
    {
        getUserInfo();
        using (SqlConnection con = new SqlConnection(conStr))
        {
            string query = "SELECT StaffId, UName, pw, DepName , Role, Status, UserId FROM tblUsers td INNER JOIN tblRoles tdt ON td.RoleId = tdt.RoleId INNER JOIN tblDepartment tdp ON td.DepId = tdp.DepId";

            using (SqlCommand cmdGetInfo = new SqlCommand(query, con))
            {
                try
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmdGetInfo);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gridSearch.DataSource = dt;
                    gridSearch.DataBind();
                }
                catch (Exception ex)
                {
                    // Handle the exception (log it, show a message, etc.)
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }
        title();
    }

    private void title()
         {
            if (gridSearch != null && gridSearch.HeaderRow != null)
            {
                gridSearch.HeaderRow.Cells[0].Text = "";
                gridSearch.HeaderRow.Cells[1].Text = "អត្តលេខ";
                gridSearch.HeaderRow.Cells[2].Text = "ឈ្មោះអ្នកប្រើ";
                gridSearch.HeaderRow.Cells[3].Text = "ពាក្យសម្ងាត់";
                gridSearch.HeaderRow.Cells[4].Text = "អង្គភាព";
                gridSearch.HeaderRow.Cells[5].Text = "តួនាទី​";
                gridSearch.HeaderRow.Cells[6].Text = "ដំណើការ";
                gridSearch.HeaderRow.Cells[7].Text = "";

                gridSearch.HeaderRow.Cells[0].Width = 100;
                gridSearch.HeaderRow.Cells[1].Width = 100;
                gridSearch.HeaderRow.Cells[2].Width = 300;
                gridSearch.HeaderRow.Cells[3].Width = 200;
                gridSearch.HeaderRow.Cells[4].Width = 300;
                gridSearch.HeaderRow.Cells[5].Width = 150;
                gridSearch.HeaderRow.Cells[6].Width = 100;

        }
    }


    protected void rdAdd_CheckedChanged(object sender, EventArgs e)
    {
        drpSearch.Enabled = false;
        txtSearch.Enabled = false;
        BtnSearch.Enabled = false;
        chkA.Checked = false;
        BtnEdit.Enabled = false;

    }

    protected void rdUpdate_CheckedChanged(object sender, EventArgs e)
    {
        drpSearch.Enabled = true;
        txtSearch.Enabled = true;
        BtnSearch.Enabled = true;
        BtnEdit.Enabled = true;
        chkA.Checked = false;
        BtnInput.Enabled = false;

    }

    protected void chkA_CheckedChanged(object sender, EventArgs e)
    {
        if (chkA.Checked == true)
        {
            if ( txtNum.Text == "" || txtName.Text == "" || txtPw.Text == "" || drpDep.SelectedItem.ToString() == "" || drpRole.SelectedItem.ToString() == "" || drpStatus.SelectedItem.ToString() == "")
            {
                lblBlank.Text = "សូមបំពេញព័ត៌មានឱ្យបានគ្រប់គ្រាន់!";
                chkA.Checked = false;

            }
            else
            {
                if (rdAdd.Checked == true)
                {
                    BtnInput.Enabled = true;
                    BtnEdit.Enabled = false;
                }
                else
                {
                    BtnInput.Enabled = false;
                    BtnEdit.Enabled = true;
                }
                lblBlank.Text = "";
            }
        }
        else
        {
            BtnInput.Enabled = false;
             BtnDelete.Enabled = false;
        }
    }

    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        txtNum.Text = "";
        txtName.Text = "";
        txtPw.Text = "";
        drpDep.SelectedIndex = 0;
        drpStatus.SelectedIndex = 0;
        drpRole.SelectedIndex = 0;
        lblBlank.Text = "";
        BtnInput.Enabled = true;
        chkA.Checked = false;
        BtnEdit.Enabled = false;
    }

    protected void BtnInput_Click(object sender, EventArgs e)
    {
        if (txtNum.Text == "" || txtName.Text == "" || txtPw.Text == "" || drpDep.SelectedItem.ToString() == "" || drpRole.SelectedItem.ToString() == "" || drpStatus.SelectedItem.ToString() == "")
        {
            lblBlank.Text = "សូមបំពេញព័ត៌មានឱ្យបានគ្រប់គ្រាន់!";
            chkA.Checked = false;

        }
        else
        {
           
                //Get User
                getUserInfo();
                //Add Data
                con = new SqlConnection(conStr);
                SqlCommand cmdAdd = new SqlCommand("Insert into tblUsers (StaffId, UName, pw, " +
                    "DepId, RoleId, Status, CreateDate) Values (@StaffId, @UName, " +
                    "@pw, @DepId, @RoleId, @Status, @CreateDate)", con);
                cmdAdd.Parameters.AddWithValue("@StaffId", txtNum.Text);
                cmdAdd.Parameters.AddWithValue("@UName", txtName.Text);
                cmdAdd.Parameters.AddWithValue("@pw", txtPw.Text);
                cmdAdd.Parameters.AddWithValue("@DepId", drpDep.SelectedValue);
                cmdAdd.Parameters.AddWithValue("@RoleId", drpRole.SelectedValue);
                cmdAdd.Parameters.AddWithValue("@Status", drpStatus.SelectedValue);
                cmdAdd.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                con.Open();
                cmdAdd.ExecuteNonQuery();
                con.Close();
                BtnDelete_Click(sender, e);
                ShowData();

            }
        }

    protected void BtnEdit_Click(object sender, EventArgs e)
    {
        uIdS = gridSearch.SelectedRow.Cells[7].Text;
        Update_Users();
        BtnDelete_Click(sender, e);
        ShowData();
    }

    private void Update_Users()
    {
        if (string.IsNullOrEmpty(txtNum.Text) || string.IsNullOrEmpty(txtName.Text) ||
            string.IsNullOrEmpty(txtPw.Text) || drpDep.SelectedItem == null ||
            drpRole.SelectedItem == null || drpStatus.SelectedItem == null)
        {
            lblBlank.Text = "សូមបំពេញព័ត៌មានឱ្យបានគ្រប់គ្រាន់!";
            chkA.Checked = false;
            BtnInput.Enabled = false;
            return; // Exit the method if validation fails
        }

        getUserInfo(); // Retrieve current user info

        using (SqlConnection con = new SqlConnection(conStr))
        {
            string query = "UPDATE tblUsers SET StaffId = @StaffId, UName = @UName, pw = @pw, " +
                           "DepId = @DepId, RoleId = @RoleId, Status = @Status, DateModified=@DateModified  " +
                           "WHERE UserId = @UserId";

            using (SqlCommand cmdUpdate = new SqlCommand(query, con))
            {
                cmdUpdate.Parameters.AddWithValue("@StaffId", txtNum.Text);
                cmdUpdate.Parameters.AddWithValue("@UName", txtName.Text);
                cmdUpdate.Parameters.AddWithValue("@pw", txtPw.Text);
                cmdUpdate.Parameters.AddWithValue("@DepId", drpDep.SelectedValue);
                cmdUpdate.Parameters.AddWithValue("@RoleId", drpRole.SelectedValue);
                cmdUpdate.Parameters.AddWithValue("@Status", drpStatus.SelectedValue);
                cmdUpdate.Parameters.AddWithValue("@DateModified", DateTime.Now);
                cmdUpdate.Parameters.AddWithValue("@UserId", uIdS); // Assuming DocIdS holds the UserId of the user to update

                try
                {
                    con.Open();
                    cmdUpdate.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Handle the exception (log it, show a message, etc.)
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }
    }

    protected void gridSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtNum.Text = gridSearch.SelectedRow.Cells[1].Text;
        txtName.Text = gridSearch.SelectedRow.Cells[2].Text;
        txtPw.Text = gridSearch.SelectedRow.Cells[3].Text;
        drpDep.SelectedItem.Text = gridSearch.SelectedRow.Cells[4].Text;
        drpRole.SelectedItem.Text = gridSearch.SelectedRow.Cells[5].Text;
        drpStatus.SelectedItem.Text = gridSearch.SelectedRow.Cells[6].Text;
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        getUserInfo();
        if (drpSearch.SelectedIndex == 0)
            SearcById_Staff();
        else
            SearcByName_Staff();
    }

    private void SearcByName_Staff()
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            // Adjusted the query to search by UName instead of StaffName
            string query = "SELECT StaffId, UName, pw, DepName, Role, Status, UserId " +
                           "FROM tblUsers td " +
                           "INNER JOIN tblRoles tdt ON td.RoleId = tdt.RoleId " +
                           "INNER JOIN tblDepartment tdp ON td.DepId = tdp.DepId " +
                           "WHERE UName LIKE @UserName"; // Search by UName

            using (SqlCommand cmdSearch = new SqlCommand(query, con))
            {
                // Use parameterized query for safe execution
                cmdSearch.Parameters.AddWithValue("@UserName", "%" + txtSearch.Text.Trim() + "%");

                try
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmdSearch);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gridSearch.DataSource = dt;
                    gridSearch.DataBind();
                }
                catch (Exception ex)
                {
                    // Handle the exception (log it, show a message, etc.)
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }
        title(); // Call to update the grid headers if necessary
    }


    private void SearcById_Staff()
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            string query = "SELECT StaffId, UName, pw, DepName, Role, Status, UserId " +
                           "FROM tblUsers td " +
                           "INNER JOIN tblRoles tdt ON td.RoleId = tdt.RoleId " +
                           "INNER JOIN tblDepartment tdp ON td.DepId = tdp.DepId " +
                           "WHERE StaffId = @StaffId";

            using (SqlCommand cmdSearch = new SqlCommand(query, con))
            {
                cmdSearch.Parameters.AddWithValue("@StaffId", txtSearch.Text.Trim());
                try
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmdSearch);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gridSearch.DataSource = dt;
                    gridSearch.DataBind();
                }
                catch (Exception ex)
                {
                    // Handle the exception (log it, show a message, etc.)
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }
        title(); // Call to update the grid headers if necessary
    }
}


