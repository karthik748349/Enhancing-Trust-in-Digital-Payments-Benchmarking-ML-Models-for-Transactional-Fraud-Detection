using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bank.user
{
    public partial class CreateAcount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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



        protected void Text1_ServerClick(object sender, EventArgs e)
        {

            Random R = new Random();
            int sky = R.Next(123456, 999999);
            string fullName = uname.Value;
            string dobb = dob.Value;
            string gender = gen.Value;
            string emaill = email.Value;
            string phone = phn.Value;
            string aadhar = aad.Value;
            string pan = pannum.Value;
            string address = ads.Value;
            string pincode = pin.Value;
            string nominee = nname.Value;
            string bankName = ddlBank.Value;
            string accountType = ddlAccountType.Value;
            DateTime createdDate = DateTime.Now;

            // Generate Account Number: BankName + Year + 5-digit random
            string accNum = bankName.Substring(0, 3).ToUpper() + DateTime.Now.Year + new Random().Next(10000, 99999).ToString();

            string connStr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = @"INSERT INTO UserAccounts 
                            (AccountNumber, FullName, DOB, Gender, Email, Phone, AadharNumber, PanNumber, Address, Pincode, NomineeName, BankName, AccountType, CreatedDate,UserID,skey)
                            VALUES
                            (@AccountNumber, @FullName, @DOB, @Gender, @Email, @Phone, @AadharNumber, @PanNumber, @Address, @Pincode, @NomineeName, @BankName, @AccountType, @CreatedDate,@UserID,@skey)";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@AccountNumber", accNum);
                cmd.Parameters.AddWithValue("@FullName", fullName);
                cmd.Parameters.AddWithValue("@DOB", DateTime.Parse(dobb));
                cmd.Parameters.AddWithValue("@Gender", gender);
                cmd.Parameters.AddWithValue("@Email", emaill);
                cmd.Parameters.AddWithValue("@Phone", phone);
                cmd.Parameters.AddWithValue("@AadharNumber", aadhar);
                cmd.Parameters.AddWithValue("@PanNumber", pan);
                cmd.Parameters.AddWithValue("@Address", address);
                cmd.Parameters.AddWithValue("@Pincode", pincode);
                cmd.Parameters.AddWithValue("@NomineeName", nominee);
                cmd.Parameters.AddWithValue("@BankName", bankName);
                cmd.Parameters.AddWithValue("@AccountType", accountType);
                cmd.Parameters.AddWithValue("@CreatedDate", createdDate);
                cmd.Parameters.AddWithValue("@UserId", Session["UserId"].ToString());
                cmd.Parameters.AddWithValue("@skey", sky);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            // Optional: Show confirmation (you can replace with redirect or literal)
            Response.Write("<script>alert('Account Created Successfully. Account Number: " + accNum + "');</script>");
            Response.Write("<script>Window.location.href='statement.aspx');</script>");
        }
    }
}
