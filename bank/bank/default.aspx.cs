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
	public partial class _default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Admin login
            if (email == "admin@gmail.com" && password == "admin")
            {
                Response.Redirect("~/admin/index.aspx");
            }
            else
            {
                string constr = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string query = "SELECT UserId FROM Users WHERE Email = @Email AND Password = @Password";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);  // Note: Plain text password is not recommended

                    con.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        Session["UserId"] = result.ToString();  // Store the UserId in session
                        Response.Redirect("~/user/index.aspx");
                    }
                    else
                    {
                        lblMessage.Text = "Invalid email or password.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }


    }
}