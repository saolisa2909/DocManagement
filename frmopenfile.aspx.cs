using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmopenfile : System.Web.UI.Page
{
    public string conStr = ConfigurationManager.ConnectionStrings["DbDoc"].ToString();
    SqlConnection con;
    protected void Page_Load(object sender, EventArgs e)
    {
        String getDocId = Request.QueryString.Get("docId");

        if (getDocId != null)
        {

            con = new SqlConnection(conStr);
            SqlCommand cmdGetFile = new SqlCommand("Select FileUrl From tblDocument where DocId=@getDocId", con);
            cmdGetFile.Parameters.AddWithValue("@getDocId", getDocId);
            con.Open();
            SqlDataReader rdFile = cmdGetFile.ExecuteReader();
            if (rdFile.Read())
            {
                Response.Clear();

                //Reading PDF File
                string path = Server.MapPath(rdFile[0].ToString().Trim());
                WebClient client = new WebClient();
                Byte[] buffer = client.DownloadData(path);
                if (buffer != null)
                {
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-length", buffer.Length.ToString());
                    Response.BinaryWrite(buffer);
                }

            }
            con.Close();
        }
        else
        {
            Response.Redirect("~/frmUsers.aspx");
        }

    }
}