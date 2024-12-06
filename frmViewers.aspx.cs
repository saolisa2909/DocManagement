using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Page2 : System.Web.UI.Page
{
    public string conStr = ConfigurationManager.ConnectionStrings["DbDoc"].ToString();
    SqlConnection con;
    string vUser, getUId, getRoleId, getStaffId, getDepId, getDepName;
    DataTable tdShow = new DataTable("tdShow");
    string DocIdS;
    DateTime startDate, endDate;
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
                if (Convert.ToInt32(getRoleId) != 1 && Convert.ToInt32(getRoleId) != 3)
                {
                    Response.Redirect("~/Default.aspx");
                }
            }
            if (!IsPostBack)
            {
                lblSType.Text = "លេខឯកសារ";
                fromDate.Enabled = false;
                toDate.Enabled = false;
                txtSearch.Enabled = false;
                if (Convert.ToInt32(getRoleId) == 1)
                {
                    drpDep.Enabled = true;
                    getDepNames();
                    ShowData();
                }
                else
                {
                    getDepNames();
                    drpDep.SelectedValue = getDepId.ToString();
                    ShowDataByDep();
                    drpDep.Enabled = false;
                }
            }

        }
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
    public void getDepNames()
    {
        drpDep.Items.Clear();
        con = new SqlConnection(conStr);
        SqlCommand cmdGetDeps = new SqlCommand("Select DepId ,DepName From  tblDepartment", con);
        DataTable dt = new DataTable();
        con.Open();
        dt.Load(cmdGetDeps.ExecuteReader());
        drpDep.DataTextField = "DepName";
        drpDep.DataValueField = "DepID";
        drpDep.DataSource = dt;
        drpDep.DataBind();
        con.Close();
    }
    protected void btnOpen_Click(object sender, EventArgs e)
    {
        DocIdS = gridSearch.SelectedRow.Cells[6].Text;
        Response.Redirect("~/frmopenfile.aspx?docId=" + DocIdS);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {

    }
    private void ShowDataByDep()
    {
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
    private void ShowData()
    {
        getUserInfo();
        con = new SqlConnection(conStr);
        SqlCommand cmdGetInfo = new SqlCommand("Select FileNumber, FileName, FileNote, DepName, DocType, DocId From tblDocument td Inner Join tblDocType tdt ON td.DocTypeId=tdt.DocTypeId Inner Join tblDepartment tdp ON td.DepId=tdp.DepId", con);
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmdGetInfo);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gridSearch.DataSource = dt;
        gridSearch.DataBind();
        con.Close();
        title();
    }
    public void SearchAllDocByDepAllType(int DepIdS) //Done 2
    {
        con = new SqlConnection(conStr);
        SqlCommand cmdGetData = new SqlCommand("Select FileNumber, FileName, FileNote, DepName, DocType, DocId From tblDocument td Inner Join tblDocType tdt ON td.DocTypeId=tdt.DocTypeId Inner Join tblDepartment tdp ON td.DepId=tdp.DepId where td.DepId =@DepIdS", con);
        cmdGetData.Parameters.AddWithValue("@DepIdS", DepIdS);
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmdGetData);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gridSearch.DataSource = dt;
        gridSearch.DataBind();
        con.Close();
        title();
    }
    public void SearchAllDocAllDepByType(int TypeIdS) //Done 3
    {
        con = new SqlConnection(conStr);
        SqlCommand cmdGetData = new SqlCommand("Select FileNumber, FileName, FileNote, DepName, DocType, DocId From tblDocument td Inner Join tblDocType tdt ON td.DocTypeId=tdt.DocTypeId Inner Join tblDepartment tdp ON td.DepId=tdp.DepId where td.DocTypeId =@TypeIdS", con);
        cmdGetData.Parameters.AddWithValue("@TypeIdS", TypeIdS);
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmdGetData);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gridSearch.DataSource = dt;
        gridSearch.DataBind();
        con.Close();
        title();
    }
    public void SearchAllDocByDepByType(int DepIdS, int TypeIdS)//Done 4
    {
        con = new SqlConnection(conStr);
        SqlCommand cmdGetData = new SqlCommand("Select FileNumber, FileName, FileNote, DepName, DocType, DocId From tblDocument td Inner Join tblDocType tdt ON td.DocTypeId=tdt.DocTypeId Inner Join tblDepartment tdp ON td.DepId=tdp.DepId Where td.DepId =@DepIdS AND td.DocTypeId=@TypeIdS", con);
        cmdGetData.Parameters.AddWithValue("@DepIdS", DepIdS);
        cmdGetData.Parameters.AddWithValue("@TypeIdS", TypeIdS);
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmdGetData);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gridSearch.DataSource = dt;
        gridSearch.DataBind();
        con.Close();
        title();
    }
    public void SearchByFileNumberAll() //Done 5
    {
        if (txtSearch.Text != "")
        {
            con = new SqlConnection(conStr);
            SqlCommand cmdGetInfo = new SqlCommand("Select FileNumber, FileName, FileNote, DepName, DocType, DocId From tblDocument td Inner Join tblDocType tdt ON td.DocTypeId=tdt.DocTypeId Inner Join tblDepartment tdp ON td.DepId=tdp.DepId where FileNumber like N'%" + txtSearch.Text + "%'", con);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmdGetInfo);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridSearch.DataSource = dt;
            gridSearch.DataBind();
            con.Close();
            title();
        }
    }
    public void SearchByFileNumberByDepAllType(int DepIdS)//Done 6
    {
        con = new SqlConnection(conStr);
        SqlCommand cmdGetInfo = new SqlCommand("Select FileNumber, FileName, FileNote, DepName, DocType, DocId From tblDocument td Inner Join tblDocType tdt ON td.DocTypeId=tdt.DocTypeId Inner Join tblDepartment tdp ON td.DepId=tdp.DepId where FileNumber like N'%" + txtSearch.Text + "%' and td.DepId=@DepIdS", con);
        cmdGetInfo.Parameters.AddWithValue("@DepIdS", DepIdS);
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmdGetInfo);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gridSearch.DataSource = dt;
        gridSearch.DataBind();
        con.Close();
        title();
    }
    public void SearchByFileNumberAllDepByType(int TypeIdS)//Done 7
    {
        con = new SqlConnection(conStr);
        SqlCommand cmdGetInfo = new SqlCommand("Select FileNumber, FileName, FileNote, DepName, DocType, DocId From tblDocument td Inner Join tblDocType tdt ON td.DocTypeId=tdt.DocTypeId Inner Join tblDepartment tdp ON td.DepId=tdp.DepId where FileNumber like N'%" + txtSearch.Text + "%' and td.DocTypeId=@TypeIdS", con);
        cmdGetInfo.Parameters.AddWithValue("@TypeIdS", TypeIdS);
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmdGetInfo);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gridSearch.DataSource = dt;
        gridSearch.DataBind();
        con.Close();
        title();
    }
    public void SearchByFileNumberByDepByType(int DepIdS, int TypeIdS)//Done 8
    {
        con = new SqlConnection(conStr);
        SqlCommand cmdGetInfo = new SqlCommand("Select FileNumber, FileName, FileNote, DepName, DocType, DocId From tblDocument td Inner Join tblDocType tdt ON td.DocTypeId=tdt.DocTypeId Inner Join tblDepartment tdp ON td.DepId=tdp.DepId where FileNumber like N'%" + txtSearch.Text + "%' and td.DepId=@DocIdS and td.DocTypeId=@TypeIdS", con);
        cmdGetInfo.Parameters.AddWithValue("@DocIdS", DepIdS);
        cmdGetInfo.Parameters.AddWithValue("@TypeIdS", TypeIdS);
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmdGetInfo);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gridSearch.DataSource = dt;
        gridSearch.DataBind();
        con.Close();
        title();
    }
    public void SearchByFileNameAll() //Done 9
    {
        con = new SqlConnection(conStr);
        SqlCommand cmdGetInfo = new SqlCommand("Select FileNumber, FileName, FileNote, DepName, DocType, DocId From tblDocument td Inner Join tblDocType tdt ON td.DocTypeId=tdt.DocTypeId Inner Join tblDepartment tdp ON td.DepId=tdp.DepId where FileName like N'%" + txtSearch.Text + "%'", con);
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmdGetInfo);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gridSearch.DataSource = dt;
        gridSearch.DataBind();
        con.Close();
        title();
    }
    public void SearchByFileNameByDepAllType(int DepIdS)//Done 10
    {
        con = new SqlConnection(conStr);
        SqlCommand cmdGetInfo = new SqlCommand("Select FileNumber, FileName, FileNote, DepName, DocType, DocId From tblDocument td Inner Join tblDocType tdt ON td.DocTypeId=tdt.DocTypeId Inner Join tblDepartment tdp ON td.DepId=tdp.DepId where FileName like N'%" + txtSearch.Text + "%' and td.DepId=@DepIdS", con);
        cmdGetInfo.Parameters.AddWithValue("@DepIdS", DepIdS);
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmdGetInfo);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gridSearch.DataSource = dt;
        gridSearch.DataBind();
        con.Close();
        title();
    }
    public void SearchByFileNameAllDepByType(int TypeIdS)//Done 11
    {
        con = new SqlConnection(conStr);
        SqlCommand cmdGetInfo = new SqlCommand("Select FileNumber, FileName, FileNote, DepName, DocType, DocId From tblDocument td Inner Join tblDocType tdt ON td.DocTypeId=tdt.DocTypeId Inner Join tblDepartment tdp ON td.DepId=tdp.DepId where FileName like N'%" + txtSearch.Text + "%' and td.DocTypeId=@TypeIdS", con);
        cmdGetInfo.Parameters.AddWithValue("@TypeIdS", TypeIdS);
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmdGetInfo);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gridSearch.DataSource = dt;
        gridSearch.DataBind();
        con.Close();
        title();
    }
    public void SearchByFileNameberByDepByType(int DepIdS, int TypeIdS)//Done 12
    {
        con = new SqlConnection(conStr);
        SqlCommand cmdGetInfo = new SqlCommand("Select FileNumber, FileName, FileNote, DepName, DocType, DocId From tblDocument td Inner Join tblDocType tdt ON td.DocTypeId=tdt.DocTypeId Inner Join tblDepartment tdp ON td.DepId=tdp.DepId where FileName like N'%" + txtSearch.Text + "%' and td.DepId=@DocIdS and td.DocTypeId=@TypeIdS", con);
        cmdGetInfo.Parameters.AddWithValue("@DocIdS", DepIdS);
        cmdGetInfo.Parameters.AddWithValue("@TypeIdS", TypeIdS);
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmdGetInfo);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gridSearch.DataSource = dt;
        gridSearch.DataBind();
        con.Close();
        title();
    }
    public void SearchByDateAll(DateTime stDate, DateTime enDate) //Done 13
    {
        con = new SqlConnection(conStr);
        SqlCommand cmdGetInfo = new SqlCommand("Select FileNumber, FileName, FileNote, DepName, DocType, DocId From tblDocument td Inner Join tblDocType tdt ON td.DocTypeId=tdt.DocTypeId Inner Join tblDepartment tdp ON td.DepId=tdp.DepId where DateIn>=@stD and DateIn<=@enD", con);
        cmdGetInfo.Parameters.AddWithValue("@stD", stDate);
        cmdGetInfo.Parameters.AddWithValue("@enD", enDate);
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmdGetInfo);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gridSearch.DataSource = dt;
        gridSearch.DataBind();
        con.Close();
        title();
    }
    public void SearchByDateByDepAllType(int DepIdS, DateTime stDate, DateTime enDate)//Done 14
    {
        con = new SqlConnection(conStr);
        SqlCommand cmdGetInfo = new SqlCommand("Select FileNumber, FileName, FileNote, DepName, DocType, DocId From tblDocument td Inner Join tblDocType tdt ON td.DocTypeId=tdt.DocTypeId Inner Join tblDepartment tdp ON td.DepId=tdp.DepId where DateIn>=@stD and DateIn<=@enD and td.DepId=@DepIdS", con);
        cmdGetInfo.Parameters.AddWithValue("@stD", stDate);
        cmdGetInfo.Parameters.AddWithValue("@enD", enDate);
        cmdGetInfo.Parameters.AddWithValue("@DepIdS", DepIdS);
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmdGetInfo);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gridSearch.DataSource = dt;
        gridSearch.DataBind();
        con.Close();
        title();
    }
    public void SearchByDateAllDepByType(int TypeIdS, DateTime stDate, DateTime enDate)//Done 15
    {
        con = new SqlConnection(conStr);
        SqlCommand cmdGetInfo = new SqlCommand("Select FileNumber, FileName, FileNote, DepName, DocType, DocId From tblDocument td Inner Join tblDocType tdt ON td.DocTypeId=tdt.DocTypeId Inner Join tblDepartment tdp ON td.DepId=tdp.DepId where DateIn>=@stD and DateIn<=@enD and td.DocTypeId=@TypeIdS", con);
        cmdGetInfo.Parameters.AddWithValue("@stD", stDate);
        cmdGetInfo.Parameters.AddWithValue("@enD", enDate);
        cmdGetInfo.Parameters.AddWithValue("@TypeIdS", TypeIdS);
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmdGetInfo);
        DataTable dt = new DataTable();
        da.Fill(dt);
        gridSearch.DataSource = dt;
        gridSearch.DataBind();
        con.Close();
        title();
    }
    public void SearchByDateByDepByType(int DepIdS, int TypeIdS, DateTime stDate, DateTime enDate)//Done 16
    {
        con = new SqlConnection(conStr);
        SqlCommand cmdGetInfo = new SqlCommand("Select FileNumber, FileName, FileNote, DepName, DocType, DocId From tblDocument td Inner Join tblDocType tdt ON td.DocTypeId=tdt.DocTypeId Inner Join tblDepartment tdp ON td.DepId=tdp.DepId where DateIn>=@stD and DateIn<=@enD and td.DepId=@DocIdS and td.DocTypeId=@TypeIdS", con);
        cmdGetInfo.Parameters.AddWithValue("@stD", stDate);
        cmdGetInfo.Parameters.AddWithValue("@enD", enDate);
        cmdGetInfo.Parameters.AddWithValue("@DocIdS", DepIdS);
        cmdGetInfo.Parameters.AddWithValue("@TypeIdS", TypeIdS);
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

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        drpDocType.SelectedIndex = 0;
        drpSearchType.SelectedIndex = 0;
        txtSearch.Text = "";
        fromDate.SelectedDate = DateTime.Now;
        toDate.SelectedDate = DateTime.Now;
        if (getRoleId == "2")
            drpDep.SelectedIndex = 0;
    }
    protected void drpSearchType_SelectedIndexChanged(object sender, EventArgs e)
    {
        int drpS = drpSearchType.SelectedIndex;
        lblSType.Text = drpSearchType.SelectedItem.Text.ToString();
        if (getRoleId == "1")
        {
            if (drpS == 0)
            {
                txtSearch.Text = "";
                txtSearch.Enabled = false;
                fromDate.Enabled = false;
                toDate.Enabled = false;
                drpDep.Enabled = true;
                drpDocType.Enabled = true;
                ShowData();
            }
            else if (drpS == 1 || drpS == 2)
            {
                txtSearch.Text = "";
                txtSearch.Enabled = true;
                fromDate.Enabled = false;
                toDate.Enabled = false;
                drpDep.Enabled = true;
                drpDocType.Enabled = true;
            }
            else
            {
                txtSearch.Text = "";
                txtSearch.Enabled = false;
                fromDate.Enabled = true;
                toDate.Enabled = true;
                drpDep.Enabled = true;
                drpDocType.Enabled = true;
            }
        }
        else // RoleId=3
        {
            if (drpS == 0)
            {
                txtSearch.Text = "";
                txtSearch.Enabled = false;
                fromDate.Enabled = false;
                toDate.Enabled = false;
                drpDep.Enabled = false;
                drpDocType.Enabled = true;
                ShowDataByDep();
            }
            else if (drpS == 1 || drpS == 2)
            {
                txtSearch.Text = "";
                txtSearch.Enabled = true;
                fromDate.Enabled = false;
                toDate.Enabled = false;
                drpDep.Enabled = false;
                drpDocType.Enabled = true;
            }
            else
            {
                txtSearch.Text = "";
                txtSearch.Enabled = false;
                fromDate.Enabled = true;
                toDate.Enabled = true;
                drpDep.Enabled = false;
                drpDocType.Enabled = true;
            }


        }
    }


    protected void drpDep_SelectedIndexChanged(object sender, EventArgs e)
    {
        int searchTypeIdS = drpSearchType.SelectedIndex;
        int DepIdS = drpDep.SelectedIndex;
        int docTypeIdS = drpDocType.SelectedIndex;
        // In Case All TypeSearch
        if (searchTypeIdS == 0)
        {
            txtSearch.Text = "";
            txtSearch.Enabled = false;
            fromDate.Enabled = false;
            toDate.Enabled = false;
            if (DepIdS == 0 && docTypeIdS == 0)
            {
                //Search By All TypeSearch, All Department and All DocType
                ShowData();
            }
            else if (DepIdS != 0 && docTypeIdS == 0)
            {
                //Search By All TypeSearch, By Department and All DocType
                SearchAllDocByDepAllType(DepIdS);
            }
            else if (DepIdS == 0 && docTypeIdS != 0)
            {
                //Search By All TypeSearch, All Department and By DocType
                SearchAllDocAllDepByType(docTypeIdS);
            }
            else //ok
            {
                //Search By All TypeSearch, By Department and By DocType
                SearchAllDocByDepByType(DepIdS, docTypeIdS);
            }
        }
        // In Case Search By FileNumber
        else if (searchTypeIdS == 1)
        {
            // txtSearch.Text = "";
            txtSearch.Enabled = true;
            fromDate.Enabled = false;
            toDate.Enabled = false;
            if (DepIdS == 0 && docTypeIdS == 0)
            {
                //Search By FileNumber, All Department and All DocType
                SearchByFileNumberAll();
            }
            else if (DepIdS != 0 && docTypeIdS == 0)
            {
                //Search By FileNumber, By Department and All DocType
                SearchByFileNumberByDepAllType(DepIdS);
            }
            else if (DepIdS == 0 && docTypeIdS != 0)
            {
                //Search By FileNumber, All Department and By DocType
                SearchByFileNumberAllDepByType(docTypeIdS);
            }
            else
            {
                //Search By FileNumber, By Department and By DocType
                SearchByFileNumberByDepByType(DepIdS, docTypeIdS);
            }
        }
        // In Case Search By FileName
        else if (searchTypeIdS == 2)
        {
            // txtSearch.Text = "";
            txtSearch.Enabled = true;
            fromDate.Enabled = false;
            toDate.Enabled = false;
            if (DepIdS == 0 && docTypeIdS == 0)
            {
                //Search By FileName, All Department and All DocType
                SearchByFileNameAll();
            }
            else if (DepIdS != 0 && docTypeIdS == 0)
            {
                //Search By FileName, By Department and All DocType
                SearchByFileNameByDepAllType(DepIdS);
            }
            else if (DepIdS == 0 && docTypeIdS != 0)
            {
                //Search By FileName, All Department and By DocType
                SearchByFileNameAllDepByType(docTypeIdS);
            }
            else
            {
                //Search By FileName, By Department and By DocType
                SearchByFileNameberByDepByType(DepIdS, docTypeIdS);
            }
        }
        // In Case Search By Date
        else
        {
            startDate = fromDate.SelectedDate.Date;
            endDate = toDate.SelectedDate.Date;
            txtSearch.Text = "";
            txtSearch.Enabled = false;
            fromDate.Enabled = true;
            toDate.Enabled = true;
            if (DepIdS == 0 && docTypeIdS == 0) //done
            {
                //Search By Date, All Department and All DocType
                SearchByDateAll(startDate, endDate);
            }
            else if (DepIdS != 0 && docTypeIdS == 0) //done
            {
                //Search By Date, By Department and All DocType
                SearchByDateByDepAllType(DepIdS, startDate, endDate);

            }
            else if (DepIdS == 0 && docTypeIdS != 0) //done
            {
                //Search By Date, All Department and By DocType
                SearchByDateAllDepByType(docTypeIdS, startDate, endDate);

            }

        }
    }

    protected void drpDocType_SelectedIndexChanged(object sender, EventArgs e)
    {
        int depIdS1 = drpDep.SelectedIndex;
        int docTypeIdS1 = drpDocType.SelectedIndex;
        int sType1 = drpSearchType.SelectedIndex;

        if (getRoleId == "1")
        {
            // Enable department selection for roleId 2
            drpDep.Enabled = true;

            if (sType1 == 0) // Search by All TypeSearch
            {
                txtSearch.Text = "";
                txtSearch.Enabled = false;
                fromDate.Enabled = false;
                toDate.Enabled = false;

                if (depIdS1 == 0 && docTypeIdS1 == 0)
                {
                    ShowData(); // All TypeSearch, All Department, All DocType
                }
                else if (depIdS1 != 0 && docTypeIdS1 == 0)
                {
                    SearchAllDocByDepAllType(depIdS1); // By Department
                }
                else if (depIdS1 == 0 && docTypeIdS1 != 0)
                {
                    SearchAllDocAllDepByType(docTypeIdS1); // By DocType
                }
                else
                {
                    SearchAllDocByDepByType(depIdS1, docTypeIdS1); // By Department and By DocType
                }
            }
            else if (sType1 == 1) // Search by FileNumber
            {
                txtSearch.Enabled = true;
                fromDate.Enabled = false;
                toDate.Enabled = false;

                if (depIdS1 == 0 && docTypeIdS1 == 0)
                {
                    SearchByFileNumberAll(); // All
                }
                else if (depIdS1 != 0 && docTypeIdS1 == 0)
                {
                    SearchByFileNumberByDepAllType(depIdS1); // By Department
                }
                else if (depIdS1 == 0 && docTypeIdS1 != 0)
                {
                    SearchByFileNumberAllDepByType(docTypeIdS1); // By DocType
                }
                else
                {
                    SearchByFileNumberByDepByType(depIdS1, docTypeIdS1); // Both
                }
            }
            else if (sType1 == 2) // Search by FileName
            {
                txtSearch.Enabled = true;
                fromDate.Enabled = false;
                toDate.Enabled = false;

                if (depIdS1 == 0 && docTypeIdS1 == 0)
                {
                    SearchByFileNameAll(); // All
                }
                else if (depIdS1 != 0 && docTypeIdS1 == 0)
                {
                    SearchByFileNameByDepAllType(depIdS1); // By Department
                }
                else if (depIdS1 == 0 && docTypeIdS1 != 0)
                {
                    SearchByFileNameAllDepByType(docTypeIdS1); // By DocType
                }
                else
                {
                    SearchByFileNameberByDepByType(depIdS1, docTypeIdS1); // Both
                }
            }
            else // Search by Date
            {
                DateTime startDate = fromDate.SelectedDate.Date;
                DateTime endDate = toDate.SelectedDate.Date;
                txtSearch.Text = "";
                txtSearch.Enabled = false;
                fromDate.Enabled = true;
                toDate.Enabled = true;

                if (depIdS1 == 0 && docTypeIdS1 == 0)
                {
                    SearchByDateAll(startDate, endDate); // All Departments, All DocTypes
                }
                else if (depIdS1 != 0 && docTypeIdS1 == 0)
                {
                    SearchByDateByDepAllType(depIdS1, startDate, endDate); // By Department
                }
                else if (depIdS1 == 0 && docTypeIdS1 != 0)
                {
                    SearchByDateAllDepByType(docTypeIdS1, startDate, endDate); // By DocType
                }
                else
                {
                    SearchByDateByDepByType(depIdS1, docTypeIdS1, startDate, endDate); // Both
                }
            }
        }
        else 
        {
            // Disable department selection for roleId 3
            drpDep.Enabled = false;

            if (sType1 == 0) // Search by All TypeSearch
            {
                txtSearch.Text = "";
                txtSearch.Enabled = false;
                fromDate.Enabled = false;
                toDate.Enabled = false;

                if (docTypeIdS1 == 0)
                {
                    ShowDataByDep(); // All TypeSearch, All DocType
                }
                else
                {
                    SearchAllDocAllDepByType(docTypeIdS1); // By DocType
                }
            }
            else if (sType1 == 1) // Search by FileNumber
            {
                txtSearch.Enabled = true;
                fromDate.Enabled = false;
                toDate.Enabled = false;

                if (docTypeIdS1 == 0)
                {
                    SearchByFileNumberAll(); // All
                }
                else
                {
                    SearchByFileNumberAllDepByType(docTypeIdS1); // By DocType
                }
            }
            else if (sType1 == 2) // Search by FileName
            {
                txtSearch.Enabled = true;
                fromDate.Enabled = false;
                toDate.Enabled = false;

                if (docTypeIdS1 == 0)
                {
                    SearchByFileNameAll(); // All
                }
                else
                {
                    SearchByFileNameAllDepByType(docTypeIdS1); // By DocType
                }
            }
            else // Search by Date
            {
                DateTime startDate = fromDate.SelectedDate.Date;
                DateTime endDate = toDate.SelectedDate.Date;
                txtSearch.Text = "";
                txtSearch.Enabled = false;
                fromDate.Enabled = true;
                toDate.Enabled = true;
                if (docTypeIdS1 == 0)
                {
                    SearchByDateAll(startDate, endDate); // All Departments, All DocTypes
                }
                else
                {
                    SearchByDateAllDepByType(docTypeIdS1, startDate, endDate); // By DocType
                }
            }
        }
    }

    protected void toDate_SelectionChanged(object sender, EventArgs e)
    {

    }

    protected void fromDate_SelectionChanged(object sender, EventArgs e)
    {

    }
}
