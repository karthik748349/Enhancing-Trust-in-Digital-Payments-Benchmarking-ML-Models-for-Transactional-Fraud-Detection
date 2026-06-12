using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

namespace bank.Admin
{
	public partial class ManageBank : System.Web.UI.Page
	{
        string connStr = ConfigurationManager.ConnectionStrings["YourConnectionStringName"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadBanks();
            }
        }

        private void LoadBanks()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Bank", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                Repeater1.DataSource = dt;
                Repeater1.DataBind();
            }
        }

        [WebMethod]
        public static void DeleteBank(int bankId)
        {
            string connStr = ConfigurationManager.ConnectionStrings["YourConnectionStringName"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Bank WHERE BankID = @BankID", con);
                cmd.Parameters.AddWithValue("@BankID", bankId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}