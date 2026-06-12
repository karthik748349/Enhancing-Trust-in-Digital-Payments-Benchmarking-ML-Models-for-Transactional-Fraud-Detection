using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bank
{
	public partial class register : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string name = txtName.Value.Trim();
            string email = txtEmail.Value.Trim();
            string phone = txtPhone.Value.Trim();
            string address = txtAddress.Value.Trim();
            string password = txtPassword.Value.Trim();

            string conStr = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;

            using (SqlConnection con = new SqlConnection(conStr))
            {
                string query = "INSERT INTO Users (Name, Email, Phone, Address, Password) VALUES (@Name, @Email, @Phone, @Address, @Password)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Phone", phone);
                    cmd.Parameters.AddWithValue("@Address", address);
                    cmd.Parameters.AddWithValue("@Password", password); // (Note: Store hashed password in real apps)

                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        // Show alert and redirect
                        string script = "alert('Registration successful!'); window.location='Default.aspx';";
                        ClientScript.RegisterStartupScript(this.GetType(), "SuccessAlert", script, true);
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = "Error: " + ex.Message;
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }

    }
}