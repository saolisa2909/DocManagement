using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmAddNEditDep : System.Web.UI.Page
{
    public string conStr = ConfigurationManager.ConnectionStrings["DbDoc"].ToString();
    SqlConnection con;
    string vUser, getUId, getRoleId, getStaffId, getDepId;
    DataTable tdShow = new DataTable("tdShow");
    string DepIdS;

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
            btnEdit.Enabled = false;
            btnInput.Enabled = false;
            rdAdd.Checked = true;
            drpSearch.Enabled = false;
            txtSearch.Enabled = false;
            btnSearch.Enabled = false;
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
            // Corrected the SQL query and table name
            string query = "SELECT  DepName, DepShortName, DepId FROM tblDepartment"; // Ensure the table name is correct

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
                    lblBlank.Text = "Error: " + ex.Message; // Display error message to the user
                }
                finally
                {
                    con.Close();
                }
            }
        }
        title(); // Call to set the grid headers if necessary
    }

    private void title()
    {
        if (gridSearch != null && gridSearch.HeaderRow != null)
        {
            gridSearch.HeaderRow.Cells[0].Text = "";
            gridSearch.HeaderRow.Cells[1].Text = "ឈ្មោះដេប៉ាតេម៉ង";
            gridSearch.HeaderRow.Cells[2].Text = "ឈ្មោះកាត់ដេប៉ាតេម៉ង";
            gridSearch.HeaderRow.Cells[3].Text = "";

            gridSearch.HeaderRow.Cells[0].Width = 100;
            gridSearch.HeaderRow.Cells[1].Width = 500;
            gridSearch.HeaderRow.Cells[2].Width = 300;
           

        }
    }

    protected void rdAdd_CheckedChanged(object sender, EventArgs e)
    {
        drpSearch.Enabled = false;
        txtSearch.Enabled = false;
        btnSearch.Enabled = false;
        chkA.Checked = false;
        btnEdit.Enabled = false;
    }

    protected void rdUpdate_CheckedChanged(object sender, EventArgs e)
    {
        drpSearch.Enabled = true;
        txtSearch.Enabled = true;
        btnSearch.Enabled = true;
        btnEdit.Enabled = true;
        chkA.Checked = false;
        btnInput.Enabled = false;
    }

    protected void chkA_CheckedChanged(object sender, EventArgs e)
    {
        if (chkA.Checked == true)
        {
            if (txtDep.Text == "" || txtTDep.Text == "" )
            {
                lblBlank.Text = "សូមបំពេញព័ត៌មានឱ្យបានគ្រប់គ្រាន់!";
                chkA.Checked = false;

            }
            else
            {
                if (rdAdd.Checked == true)
                {
                    btnInput.Enabled = true;
                    btnEdit.Enabled = false;
                }
                else
                {
                    btnInput.Enabled = false;
                    btnEdit.Enabled = true;
                }
                lblBlank.Text = "";
            }
        }
        else
        {
            btnInput.Enabled = false;
            btnEdit.Enabled = false;
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        txtDep.Text = "";
        txtTDep.Text = "";
        lblBlank.Text = "";
        btnInput.Enabled = true;
        chkA.Checked = false;
        btnEdit.Enabled = false;
    }

    protected void btnInput_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtDep.Text) || string.IsNullOrWhiteSpace(txtTDep.Text))
        {
            lblBlank.Text = "សូមបំពេញព័ត៌មានឱ្យបានគ្រប់គ្រាន់!";
            chkA.Checked = false;
        }
        else
        {
            // Get User
            getUserInfo();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                // Corrected the SQL command and added missing parentheses
                SqlCommand cmdAdd = new SqlCommand("INSERT INTO tblDepartment (DepName, DepShortName) VALUES (@DepName, @DepShortName)", con);
                cmdAdd.Parameters.AddWithValue("@DepName", txtDep.Text.Trim());
                cmdAdd.Parameters.AddWithValue("@DepShortName", txtTDep.Text.Trim());

                try
                {
                    con.Open();
                    cmdAdd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Handle the exception (log it, show a message, etc.)
                    lblBlank.Text = "Error: " + ex.Message; // Display error message to the user
                }
                finally
                {
                    con.Close();
                }

                // Clear input fields and refresh data
                btnDelete_Click(sender, e);
                ShowData();
            }
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
            DepIdS = gridSearch.SelectedRow.Cells[3].Text.Trim();
            UpdateDep();
            btnDelete_Click(sender, e);
            ShowData();
    }

    private void UpdateDep()
    {
        if (string.IsNullOrWhiteSpace(txtDep.Text) || string.IsNullOrWhiteSpace(txtTDep.Text))
        {
            lblBlank.Text = "សូមបំពេញព័ត៌មានឱ្យបានគ្រប់គ្រាន់!";
            return;
        }

        using (SqlConnection con = new SqlConnection(conStr))
        {
            string query = "UPDATE tblDepartment SET DepName = @DepName, DepShortName = @DepShortName, CreateDate = @CreateDate " +
                           "WHERE DepId = @DepId";

            using (SqlCommand cmdUpdate = new SqlCommand(query, con))
            {
                cmdUpdate.Parameters.AddWithValue("@DepName", txtDep.Text.Trim());
                cmdUpdate.Parameters.AddWithValue("@DepShortName", txtTDep.Text.Trim());
                cmdUpdate.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                cmdUpdate.Parameters.AddWithValue("@DepId", DepIdS); // Use the selected department ID

                try
                {
                    con.Open();
                    int rowsAffected = cmdUpdate.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        lblBlank.Text = "Update successful!"; // User feedback on success
                    }
                    else
                    {
                        lblBlank.Text = "No record updated."; // User feedback if no rows were updated
                    }
                }
                catch (Exception ex)
                {
                    // Handle the exception (log it, show a message, etc.)
                    lblBlank.Text = "Error: " + ex.Message; // Display error message to the user
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
        txtDep.Text = gridSearch.SelectedRow.Cells[1].Text;
        txtTDep.Text = gridSearch.SelectedRow.Cells[2].Text;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtSearch.Text))
        {
            lblBlank.Text = "សូមបញ្ចូលព័ត៌មានស្វែងរក!";
            return;
        }

        getUserInfo();
        if (drpSearch.SelectedIndex == 0)
            SearcByDepId();
        else
            SearcByDepName();
    }

    private void SearcByDepName()
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            // Correct the query to search in tblDepartment
            string query = "SELECT DepName, DepShortName, DepId FROM tblDepartment WHERE DepName LIKE @DepName";

            using (SqlCommand cmdSearch = new SqlCommand(query, con))
            {
                cmdSearch.Parameters.AddWithValue("@DepName", "%" + txtSearch.Text.Trim() + "%");

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
                    lblBlank.Text = "Error: " + ex.Message; // Display error message to the user
                }
                finally
                {
                    con.Close();
                }
            }
        }
        title(); // Call to update the grid headers if necessary
    }

    private void SearcByDepId()
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            // Correct the query to search in tblDepartment
            string query = "SELECT DepName, DepShortName, DepId FROM tblDepartment WHERE DepId LIKE @DepId";

            using (SqlCommand cmdSearch = new SqlCommand(query, con))
            {
                cmdSearch.Parameters.AddWithValue("@DepId",  txtSearch.Text.Trim() + "%");

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
                    lblBlank.Text = "Error: " + ex.Message; // Display error message to the user
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
