using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection.Emit;

namespace bank.user
{
	public partial class Statement : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

            
            if (!IsPostBack)
            {
                LoadUserAccounts();
                if (Session["UserId"] != null)
                {
                    string userId = Session["UserId"].ToString();
                    string constr = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;

                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        string query = "SELECT Name FROM Users WHERE UserId = @UserId";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@UserId", userId);

                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            string name = reader["Name"].ToString();
                            Label2.Text = name;
                            //Label3.Text = name;
                        }
                        con.Close();
                    }

                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        string query = "SELECT SUM(Balance) FROM UserWallets WHERE UserId = @UserId";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@UserId", userId);

                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            string balance = reader[0].ToString();
                           // bal.Text = balance;
                        }
                        con.Close();
                    }
                }
                else
                {
                    // Session expired or user not logged in
                    Response.Redirect("~/login.aspx");
                }
            }
        }

        private void LoadUserAccounts()
        {
            string connStr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            string query = "SELECT * FROM UserAccounts where UserId='"+ Session["UserId"].ToString() + "'";

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                rptUserAccounts.DataSource = dt;
                rptUserAccounts.DataBind();
            }
        }
    }
}