using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bank.Admin
{
	public partial class AddBank : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

        protected void Unnamed_ServerClick(object sender, EventArgs e)
        {
            // Retrieve values from HTML controls using .Value
            string bankNamee = bankName.Value;
            string branchh = branch.Value;
            string ifscc = ifsc.Value;
            string cityy = city.Value;
            string statee = state.Value;
            string contactt = contact.Value;

            // Connection string from Web.config
            string connStr = ConfigurationManager.ConnectionStrings["YourConnectionStringName"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = "INSERT INTO Bank (BankName, Branch, IFSC, City, State, Contact) " +
                               "VALUES (@BankName, @Branch, @IFSC, @City, @State, @Contact)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@BankName", bankNamee);
                    cmd.Parameters.AddWithValue("@Branch", branchh);
                    cmd.Parameters.AddWithValue("@IFSC", ifscc);
                    cmd.Parameters.AddWithValue("@City", cityy);
                    cmd.Parameters.AddWithValue("@State", statee);
                    cmd.Parameters.AddWithValue("@Contact", contactt);

                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        // Optional: Show success message
                        Response.Write("<script>alert('Bank record added successfully!');</script>");
                    }
                    catch (Exception ex)
                    {
                        // Log or show error
                        Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
                    }
                }
            }
        }
    }
}