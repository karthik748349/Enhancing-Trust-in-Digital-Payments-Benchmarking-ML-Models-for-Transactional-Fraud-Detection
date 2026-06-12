using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bank.user
{
	public partial class manage : System.Web.UI.Page
	{
        string connStr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadWalletTransactions();

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
                            // Label3.Text = name;
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
                            //bal.Text = balance;
                        }
                        con.Close();
                    }
                }
            }
        }

        private void LoadWalletTransactions()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = "SELECT tid,SenderAccNumber, ReceiverAccNumber, Amount, TransactionDate, status, ipads FROM WalletTransactions where UserId='"+ Session["UserId"].ToString() + "' ORDER BY TransactionDate DESC";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    rptTransactions.DataSource = reader;
                    rptTransactions.DataBind();
                }
            }
        }
    }
}