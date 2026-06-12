using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bank
{
	public partial class logout : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            Session.Clear();
            Session.Abandon();

            // Remove all cookies
            if (Request.Cookies != null)
            {
                foreach (string cookie in Request.Cookies.AllKeys)
                {
                    HttpCookie expiredCookie = new HttpCookie(cookie);
                    expiredCookie.Expires = DateTime.Now.AddDays(-1); // Expire immediately
                    Response.Cookies.Add(expiredCookie);
                }
            }

            // Redirect to Intrusion.aspx
            Response.Redirect("Intrusion.aspx");
        }
	}
}