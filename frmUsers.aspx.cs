using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.ServiceModel.Activities.Configuration;

public partial class Page1 : System.Web.UI.Page
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
            if (vUser != "")
            {
                ((Label)Master.FindControl("lblLoginName")).Text = vUser;
                ((LinkButton)Master.FindControl("loginStatus")).Text = "Logout";
                getUserInfo();
                if (Convert.ToInt32(getRoleId) != 2)
                    Response.Redirect("~/Default.aspx");
            }
        }

        if (!IsPostBack)
        {
            BtnEdit.Enabled = false;
            btnInput.Enabled = false;
            rdAdd.Checked = true;
            drpSearch.Enabled = false;
           txtSearch.Enabled = false;
            btnSearch.Enabled = false;
            ShowData();
        }
    }
    protected void ChkA_CheckedChanged(object sender, EventArgs e)
    {
        if (chkA.Checked == true)
        {
            if (txtFileNum.Text == "" || txtFileTitle.Text == "" || txtFileNote.Text == "" || drpDocTypeId.SelectedItem.ToString() == "")
            {
                lblBlank.Text = "សូមបំពេញព័ត៌មានឱ្យបានគ្រប់គ្រាន់!";
                chkA.Checked = false;

            }
            else
            {
                if (rdAdd.Checked == true)
                {
                    btnInput.Enabled = true;
                    BtnEdit.Enabled = false;
                }
                else
                {
                    btnInput.Enabled = false;
                    BtnEdit.Enabled = true;
                }
                lblBlank.Text = "";
            }
        }
        else
        {
            btnInput.Enabled = false;
            BtnEdit.Enabled = false;
        }
    }
    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        txtFileNum.Text = "";
        txtFileTitle.Text = "";
        txtFileNote.Text = "";
        drpDocTypeId.SelectedIndex = 0;
        lblBlank.Text = "";
        btnInput.Enabled = true;
        chkA.Checked = false;
        BtnEdit.Enabled = false;
    }

    protected void btnInput_Click(object sender, EventArgs e)
    {
        if (txtFileNum.Text == "" || txtFileTitle.Text == "" || txtFileNote.Text == "" || drpDocTypeId.SelectedItem.ToString() == "")
        {
            lblBlank.Text = "សូមបំពេញព័ត៌មានឱ្យបានគ្រប់គ្រាន់!";
            chkA.Checked = false;
            
        }
        else
        {
            if (FUpload.HasFile)
            {
                //Get User
                 getUserInfo();
                //Add Data
                String DocName = FUpload.FileName;
                FUpload.SaveAs(Server.MapPath("~\\Files") + "/" + FUpload.FileName);
                String FileUrlS = "Files/" + DocName;
                con = new SqlConnection(conStr);
                SqlCommand cmdAdd = new SqlCommand("Insert into tblDocument (FileNumber, FileName, FileNote, " +
                    "DocTypeId, UserId, StaffId, DepId, FileUrl, DateIn) Values (@FileNumber, @FileName, " +
                    "@FileNote, @DocTypeId, @UserId, @StaffId, @DepId, @FileUrl, @DateIn)", con);
                cmdAdd.Parameters.AddWithValue("@FileNumber", txtFileNum.Text);
                cmdAdd.Parameters.AddWithValue("@FileName", txtFileTitle.Text);
                cmdAdd.Parameters.AddWithValue("@FileNote", txtFileNote.Text);
                cmdAdd.Parameters.AddWithValue("@DocTypeId", drpDocTypeId.SelectedValue);
                cmdAdd.Parameters.AddWithValue("@UserId", getUId);
                cmdAdd.Parameters.AddWithValue("@StaffId", getStaffId);
                cmdAdd.Parameters.AddWithValue("@DepId", getDepId);
                cmdAdd.Parameters.AddWithValue("@FileUrl", FileUrlS);
                cmdAdd.Parameters.AddWithValue("@DateIn", DateTime.Now);
                con.Open();
                cmdAdd.ExecuteNonQuery();
                con.Close();
                BtnDelete_Click(sender, e);
                ShowData();
                
            }
            else
            {
                lblBlank.Text = "សូមជ្រើសរើសឯកសារភ្ជាប់!";
            }
        }
    }

    protected void rdAdd_CheckedChanged(object sender, EventArgs e)
    {
        drpSearch.Enabled = false;
        txtSearch.Enabled = false;
        btnSearch.Enabled = false;
        chkA.Checked = false;
        BtnEdit.Enabled = false;
       
    }

    protected void rdUpdate_CheckedChanged(object sender, EventArgs e)
    {
        drpSearch.Enabled = true;
        txtSearch.Enabled = true;
        btnSearch.Enabled = true;
        BtnEdit.Enabled = true;
        chkA.Checked = false;
        btnInput.Enabled = false;
        
    }

    public void getUserInfo()
    {
        con = new SqlConnection(conStr);
        SqlCommand cmdGetUser = new SqlCommand("Select UserId, RoleId, StaffId, DepId From tblUsers where UName=@UNames", con);
        cmdGetUser.Parameters.AddWithValue("@UNames", ((Label)Master.FindControl("lblLoginName")).Text);
        con.Open();
        SqlDataReader rdUName = cmdGetUser.ExecuteReader();
        if (rdUName.Read())
        {
            getUId = rdUName[0].ToString();
            getRoleId = rdUName[1].ToString();
            getStaffId = rdUName[2].ToString();
            getDepId = rdUName[3].ToString();
        }
        con.Close();
    }
    public void ShowData()
    {
        getUserInfo();
        con = new SqlConnection(conStr);
        SqlCommand cmdGetInfo = new SqlCommand("Select FileNumber, FileName, FileNote, DepName, DocType, DocId From tblDocument td Inner Join tblDocType tdt ON td.DocTypeId=tdt.DocTypeId Inner Join tblDepartment tdp ON td.DepId=tdp.DepId where td.DepId=@DepId", con);
        cmdGetInfo.Parameters.AddWithValue("@DepId", getDepId);
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmdGetInfo);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gridSearch.DataSource = dt;
        gridSearch.DataBind();
        con.Close();
        title();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        getUserInfo();
        if (drpSearch.SelectedIndex == 0)
            SearcByFileNumber();
        else
            SearcByFileName();
    }

    private void SearcByFileName()
    {

        con = new SqlConnection(conStr);
        SqlCommand cmdGetInfo = new SqlCommand("Select FileNumber, FileName, FileNote, DepName, DocType, DocId From tblDocument td Inner Join tblDocType tdt ON td.DocTypeId=tdt.DocTypeId Inner Join tblDepartment tdp ON td.DepId=tdp.DepId where FileName like N'%" + txtSearch.Text + "%' and td.DepId=@DepId", con);
        cmdGetInfo.Parameters.AddWithValue("@DepId", getDepId);
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmdGetInfo);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gridSearch.DataSource = dt;
        gridSearch.DataBind();
        con.Close();
        title();
    }

    private void SearcByFileNumber()
    {
        con = new SqlConnection(conStr);
        SqlCommand cmdGetInfo = new SqlCommand("Select FileNumber, FileName, FileNote, DepName, DocType, DocId From tblDocument td Inner Join tblDocType tdt ON td.DocTypeId=tdt.DocTypeId Inner Join tblDepartment tdp ON td.DepId=tdp.DepId where FileNumber like N'%" + txtSearch.Text + "%' and td.DepId=@DepId", con);
        cmdGetInfo.Parameters.AddWithValue("@DepId", getDepId);
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmdGetInfo);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gridSearch.DataSource = dt;
        gridSearch.DataBind();
        con.Close();
        title();
    }

    private void title()
    {
        if (gridSearch != null && gridSearch.HeaderRow != null)
        {
            gridSearch.HeaderRow.Cells[0].Text = "";
            gridSearch.HeaderRow.Cells[1].Text = "លេខឯកសារ"; 
            gridSearch.HeaderRow.Cells[2].Text = "ឈ្មោះឯកសារ"; 
            gridSearch.HeaderRow.Cells[3].Text = "កំណត់សម្គាល់";
            gridSearch.HeaderRow.Cells[4].Text = "នាយកដ្ឋាន"; 
            gridSearch.HeaderRow.Cells[5].Text = "ប្រភេទ"; 
            gridSearch.HeaderRow.Cells[6].Text = "";

           
            gridSearch.HeaderRow.Cells[0].Width = 100;
            gridSearch.HeaderRow.Cells[1].Width = 200;
            gridSearch.HeaderRow.Cells[2].Width = 300;
            gridSearch.HeaderRow.Cells[3].Width = 300;
            gridSearch.HeaderRow.Cells[4].Width = 300;
            gridSearch.HeaderRow.Cells[5].Width = 80;

        }
    }

    protected void gridSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtFileNum.Text = gridSearch.SelectedRow.Cells[1].Text;
        txtFileTitle.Text = gridSearch.SelectedRow.Cells[2].Text;
        txtFileNote.Text = gridSearch.SelectedRow.Cells[3].Text;
        drpDocTypeId.SelectedItem.Text = gridSearch.SelectedRow.Cells[5].Text;
    }

    protected void BtnEdit_Click(object sender, EventArgs e)
    {
        DocIdS = gridSearch.SelectedRow.Cells[6].Text;
        if (FUpload.HasFile)
        {
            UpdateWithFile();
        }
        else
        {
            UpdateWithoutFile();
        }

        BtnDelete_Click(sender, e);
         ShowData();
    }

    private void UpdateWithFile()
    {
        if (txtFileNum.Text == "" || txtFileTitle.Text == "" || txtFileNote.Text == "" || drpDocTypeId.SelectedItem.ToString() == "")
        {
            lblBlank.Text = "សូមបំពេញព័ត៌មានឱ្យបានគ្រប់គ្រាន់!";
            chkA.Checked = false;
            btnInput.Enabled = false;
        }
        else
        {
            getUserInfo();
            //Update Data
            String DocName = FUpload.FileName;
            FUpload.SaveAs(Server.MapPath("~\\File") + "/" + FUpload.FileName);
            String FileUrlS = "Files/" + DocName;
            con = new SqlConnection(conStr);
            SqlCommand cmdUpdate = new SqlCommand("Update tblDocument Set FileNumber=@FileNumber, " +
            "FileName=@FileName, FileNote=@FileNote, DocTypeId=@DocTypeId, UserId=@UserId, " +
            "StaffId=@StaffId, FileUrl=@FileUrl, DateModified=@DateModified where DocId=@DocIdS", con);
            cmdUpdate.Parameters.AddWithValue("@FileNumber", txtFileNum.Text);
            cmdUpdate.Parameters.AddWithValue("@FileName", txtFileTitle.Text);
            cmdUpdate.Parameters.AddWithValue("@FileNote", txtFileNote.Text);
            cmdUpdate.Parameters.AddWithValue("@DocTypeId", drpDocTypeId.SelectedValue);
            cmdUpdate.Parameters.AddWithValue("@UserId", getUId);
            cmdUpdate.Parameters.AddWithValue("@StaffId", getStaffId);
            cmdUpdate.Parameters.AddWithValue("@FileUrl", FileUrlS);
            cmdUpdate.Parameters.AddWithValue("@DateModified", DateTime.Now);
            cmdUpdate.Parameters.AddWithValue("@DocIdS", DocIdS);
            con.Open();
            cmdUpdate.ExecuteNonQuery();
            con.Close();
        }
    }

    private void UpdateWithoutFile()
    {
        if (txtFileNum.Text == "" || txtFileTitle.Text == "" || txtFileNote.Text == "" || drpDocTypeId.SelectedItem.ToString() == "")
        {
            lblBlank.Text = "សូមបំពេញព័ត៌មានឱ្យបានគ្រប់គ្រាន់!";
            chkA.Checked = false;
            btnInput.Enabled = false;
        }
        else
        {
            getUserInfo();
            //Update Data
            con = new SqlConnection(conStr);
            //SqlCommand cmdUpdate1 = new SqlCommand("Update tblDocument Set FileNumber=@FileNumber where DocId=@DocIdS1", con);
            SqlCommand cmdUpdate = new SqlCommand("Update tblDocument Set FileNumber=@FileNumber, " +
            "FileName=@FileName, FileNote=@FileNote, DocTypeId=@DocTypeId, UserId=@UserId, " +
            "StaffId=@StaffId, DateModified=@DateModified where DocId=@DocIdS", con);
            cmdUpdate.Parameters.AddWithValue("@FileNumber", txtFileNum.Text);
            cmdUpdate.Parameters.AddWithValue("@FileName", txtFileTitle.Text);
            cmdUpdate.Parameters.AddWithValue("@FileNote", txtFileNote.Text);
            cmdUpdate.Parameters.AddWithValue("@DocTypeId", drpDocTypeId.SelectedValue);
            cmdUpdate.Parameters.AddWithValue("@UserId", getUId);
            cmdUpdate.Parameters.AddWithValue("@StaffId", getStaffId);
            cmdUpdate.Parameters.AddWithValue("@DateModified", DateTime.Now);
            cmdUpdate.Parameters.AddWithValue("@DocIdS", DocIdS);
            con.Open();
            cmdUpdate.ExecuteNonQuery();
            con.Close();
        }
    }


    protected void BtnOpen_Click(object sender, EventArgs e)
    {
        DocIdS = gridSearch.SelectedRow.Cells[6].Text;
        Response.Redirect("~/frmopenfile.aspx?docId=" + DocIdS);
    }
}

