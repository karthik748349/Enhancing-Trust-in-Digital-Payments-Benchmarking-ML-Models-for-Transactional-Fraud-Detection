using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bank.user
{
    public partial class AddWallet : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
        {
         

            if (!IsPostBack)
            {
                    LoadAccountNumbers();
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
                          //  bal.Text = balance;
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

        private void LoadAccountNumbers()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = "SELECT AccountNumber FROM UserAccounts";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                ddlAccountNum.DataSource = cmd.ExecuteReader();
                ddlAccountNum.DataTextField = "AccountNumber";
                ddlAccountNum.DataValueField = "AccountNumber";
                ddlAccountNum.DataBind();
            }

            ddlAccountNum.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select --", ""));
        }

        protected void btnAddWallet_Click(object sender, EventArgs e)
        {
            string accNum = ddlAccountNum.SelectedValue;
            string walletType = ddlWalletType.SelectedValue;
            decimal balance;

            if (!decimal.TryParse(txtInitialBalance.Text, out balance))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Invalid amount entered');", true);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();

                    // Check if wallet already exists
                    SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM UserWallets WHERE AccountNumber=@acc", con);
                    checkCmd.Parameters.AddWithValue("@acc", accNum);
                    int count = (int)checkCmd.ExecuteScalar();

                    if (count > 0)
                    {
                        // Update existing
                        SqlCommand updateCmd = new SqlCommand("UPDATE UserWallets SET Balance = Balance + @bal WHERE AccountNumber = @acc", con);
                        updateCmd.Parameters.AddWithValue("@acc", accNum);
                        updateCmd.Parameters.AddWithValue("@bal", balance);
                        updateCmd.ExecuteNonQuery();
                    }
                    else
                    {
                        // Insert new
                        SqlCommand insertCmd = new SqlCommand("INSERT INTO UserWallets(AccountNumber, Balance, WalletType,UserId) VALUES(@acc, @bal, @type,@UserId)", con);
                        insertCmd.Parameters.AddWithValue("@acc", accNum);
                        insertCmd.Parameters.AddWithValue("@bal", balance);
                        insertCmd.Parameters.AddWithValue("@type", walletType);
                        insertCmd.Parameters.AddWithValue("@UserId", Session["UserId"].ToString());
                        insertCmd.ExecuteNonQuery();
                    }

                    // Show success alert
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Amount added to your wallet successfully');", true);
                }
            }
            catch (Exception ex)
            {
                // Optional: log error ex.Message
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('An error occurred while processing your request');", true);
            }
        }

    }
}