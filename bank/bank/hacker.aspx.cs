using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bank
{
    public partial class hacker : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private string GetLocalIpAddress()
        {
            string ipAddress = string.Empty;

            // Check if the request is available
            if (HttpContext.Current != null && HttpContext.Current.Request != null)
            {
                // Get the user's IP address from the request
                string userIpAddress = HttpContext.Current.Request.UserHostAddress;

                // If userIpAddress is "::1", then it's localhost
                if (userIpAddress == "::1" || userIpAddress == "127.0.0.1")
                {
                    // Get the local IP address of the machine
                    System.Net.IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
                    System.Net.IPAddress[] addr = ipEntry.AddressList;
                    ipAddress = addr[addr.Length - 1].ToString();
                }
                else
                {
                    // If user is not localhost, then return the user's IP address
                    ipAddress = userIpAddress;
                }
            }

            return ipAddress;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
        
            try
            {
                con.Open();
                string query = "select * from Users where Email='" + txtUsername.Value + "' and Password='" + txtPassword.Value + "'; ";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        string un = dr.GetValue(0).ToString();
                        string pd = dr.GetValue(1).ToString();

                       // Session["Username"] = txtUsername.Value;
                        Session["UserId"] = dr.GetValue(0).ToString();
                        Session["hid"] = 1;
                        Response.Redirect("User/index.aspx");
                        dr.Close(); // Close the DataReader here
                        return; // Exit the method after successful authentication

                    }
                    dr.Close(); // Close the DataReader if no successful authentication
                }
                else
                {
                    dr.Close(); // Close the DataReader if no rows found
                    Response.Write("<script>alert('Invalid User!!!');</script>");
                    // Insert data into hacker table
                    string hackerQuery = "INSERT INTO hacker (userName, LogdinTime, Area, insertedOn, lat, lng, IpAddress) VALUES (@userName, @LogdinTime, @Area, @insertedOn, @lat, @lng, @IpAddress)";
                    SqlCommand hackerCmd = new SqlCommand(hackerQuery, con);
                    string ipAddress = GetLocalIpAddress();
                    hackerCmd.Parameters.AddWithValue("@userName", txtUsername.Value);
                    hackerCmd.Parameters.AddWithValue("@LogdinTime", DateTime.Now);
                    hackerCmd.Parameters.AddWithValue("@Area", txtPassword.Value); // You may replace DBNull.Value with actual area data
                    hackerCmd.Parameters.AddWithValue("@insertedOn", DateTime.Now);
                    hackerCmd.Parameters.AddWithValue("@lat", latitude.Value); // You may replace DBNull.Value with actual latitude data
                    hackerCmd.Parameters.AddWithValue("@lng", longitude.Value); // You may replace DBNull.Value with actual longitude data
                    hackerCmd.Parameters.AddWithValue("@IpAddress", ipAddress);
                    hackerCmd.ExecuteNonQuery();


                    Random r = new Random();
                    string num = r.Next(2, 7).ToString();
                    int number = Convert.ToInt32(num);
                    // Check count of username
                    string countQuery = "SELECT COUNT(*) FROM hacker WHERE Username = @username";
                    SqlCommand countCmd = new SqlCommand(countQuery, con);
                    countCmd.Parameters.AddWithValue("@username", txtUsername.Value);
                    int count = Convert.ToInt32(countCmd.ExecuteScalar());
                    if (count > number)
                    {
                        try
                        {
                            // Send notification email
                            MailMessage m = new MailMessage(
       "localhosthost670@gmail.com",
       txtUsername.Value,
       "Urgent: Suspicious Activity Detected on Your Bank Account",
       "Dear Customer,\n\nWe have detected unusual activity on your bank account associated with the username: " + txtUsername.Value +
       ". For your security, we recommend that you change your password immediately.\n\n" +
       "If you did not initiate this activity or continue to experience issues, please contact our support team as soon as possible.\n\n" +
       "Stay safe,\nYour Bank Security Team"
   );

                            SmtpClient s = new SmtpClient("smtp.gmail.com", 587);
                            s.EnableSsl = true;
                            s.UseDefaultCredentials = false;
                            s.Credentials = new System.Net.NetworkCredential("localhosthost670@gmail.com", "uykpqxbpbuaamkom");
                            s.Send(m);

                            Console.WriteLine("Notification Email Sent");

                        }
                        catch
                        {

                        }
                    }


                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
            }
            finally
            {
                con.Close();
            }
        }

    }
}