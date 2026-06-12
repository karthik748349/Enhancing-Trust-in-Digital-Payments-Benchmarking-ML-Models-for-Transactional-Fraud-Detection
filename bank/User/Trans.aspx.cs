using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bank.user
{
    public partial class Trans : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                LoadSenderAccounts();
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
                else
                {
                    // Session expired or user not logged in
                    Response.Redirect("~/login.aspx");
                }
            }
        }

        private void LoadSenderAccounts()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT AccountNumber FROM UserAccounts where userid='" + Session["UserId"].ToString() + "' ", con);
                con.Open();
                ddlSenderAccount.DataSource = cmd.ExecuteReader();
                ddlSenderAccount.DataTextField = "AccountNumber";
                ddlSenderAccount.DataValueField = "AccountNumber";
                ddlSenderAccount.DataBind();
            }
        }
        protected void btnTransfer_Click(object sender, EventArgs e)
        {
            string tid = "TNS"+DateTime.Now.ToString("yyyyHHmmss") + new Random().Next(10000, 99999).ToString();

            string senderAcc = ddlSenderAccount.Text.Trim();
            string receiverAcc1 = txtReceiver1.Text.Trim();
            string receiverAcc2 = txtReceiver2.Text.Trim();
            string enteredSkey = TextKey.Text.Trim(); 
            decimal amount;
            if (Session["hid"] != null && Session["hid"].ToString() == "1")
            {
                string connStr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
                

                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();
                    SqlTransaction trans = con.BeginTransaction();

                    try
                    {
                        string email = "";
                        // Get Email of the user
                        string query = "SELECT Email FROM Users WHERE UserId = @UserId";
                        SqlCommand cmd = new SqlCommand(query, con, trans);
                        cmd.Parameters.AddWithValue("@UserId", Session["UserId"]?.ToString());

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                email = reader["Email"].ToString();
                                Label2.Text = email;
                            }
                        }

                        // Send notification email
                        MailMessage m = new MailMessage(
                            "localhosthost670@gmail.com",
                            email,
                            "Urgent: Suspicious Activity Detected on Your Bank Account",
                            "Dear Customer,\n\nWe have detected unusual activity on your bank account associated with the username: " + senderAcc +
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

                        // Insert fraudulent transaction
                        SqlCommand fraudTxn = new SqlCommand(@"
                INSERT INTO WalletTransactions 
                (SenderAccNumber, ReceiverAccNumber, Amount, TransactionDate, lat, longg, ipads, skey, UserId, status, TID)
                VALUES 
                (@sender, @receiver, @amt, GETDATE(), @lat, @longg, @ipads, @skey, @UserId, 'Fraud', @TID)", con, trans);

                        fraudTxn.Parameters.AddWithValue("@sender", senderAcc);
                        fraudTxn.Parameters.AddWithValue("@receiver", receiverAcc2);
                        fraudTxn.Parameters.AddWithValue("@amt", txtAmount.Text);
                        fraudTxn.Parameters.AddWithValue("@lat", hfLat.Value);
                        fraudTxn.Parameters.AddWithValue("@longg", hfLong.Value);
                        fraudTxn.Parameters.AddWithValue("@ipads", hfIP.Value);
                        fraudTxn.Parameters.AddWithValue("@skey", enteredSkey);
                        fraudTxn.Parameters.AddWithValue("@UserId", Session["UserId"]?.ToString() ?? "0");
                        fraudTxn.Parameters.AddWithValue("@TID", tid);

                        fraudTxn.ExecuteNonQuery();
                        trans.Commit();

                        // Show alert + redirect
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Suspicious transaction logged.'); window.location='manage.aspx';", true);
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", $"alert('Error: {ex.Message}');", true);
                    }
                }
            }




            if (receiverAcc1 != receiverAcc2)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Receiver account numbers do not match.');", true);
                return;
            }

            string receiverAcc = receiverAcc1;

            if (!decimal.TryParse(txtAmount.Text, out amount) || amount <= 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Invalid transfer amount.');", true);
                return;
            }

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();

                // Validate sender account and skey
                SqlCommand validateSender = new SqlCommand("SELECT skey, accstatus FROM UserAccounts WHERE AccountNumber = @acc", con);
                validateSender.Parameters.AddWithValue("@acc", senderAcc);
                SqlDataReader reader = validateSender.ExecuteReader();

                string dbSkey = "";
                string accStatus = "";

                if (reader.Read())
                {
                    dbSkey = reader["skey"].ToString();
                    accStatus = reader["accstatus"].ToString();
                }
                reader.Close();
                if (accStatus != "Active")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Your account is not active.');", true);
                    return;
                }
                if (dbSkey == "" || dbSkey != enteredSkey)
                {
                    // Insert Failure Transaction
                    SqlCommand insertCmd = new SqlCommand(@"
    INSERT INTO WalletTransactions 
    (SenderAccNumber, ReceiverAccNumber, Amount, TransactionDate, [status], lat, longg, ipads, skey,UserId,tid) 
    VALUES 
    (@sender, @receiver, @amount, GETDATE(), @status, @lat, @longg, @ipads, @skey, @UserId,@tid)", con);

                    insertCmd.Parameters.AddWithValue("@sender", senderAcc);
                    insertCmd.Parameters.AddWithValue("@receiver", receiverAcc);
                    insertCmd.Parameters.AddWithValue("@amount", amount); // Since failed
                    insertCmd.Parameters.AddWithValue("@status", "Failure");
                    insertCmd.Parameters.AddWithValue("@lat", hfLat.Value);
                    insertCmd.Parameters.AddWithValue("@longg", hfLong.Value);
                    insertCmd.Parameters.AddWithValue("@ipads", hfIP.Value);
                    insertCmd.Parameters.AddWithValue("@skey", enteredSkey);
                    insertCmd.Parameters.AddWithValue("@UserId", Session["UserId"].ToString());
                    insertCmd.Parameters.AddWithValue("@tid", tid);
                    insertCmd.ExecuteNonQuery();

                    SqlCommand totalFailsCmdd = new SqlCommand(@"
SELECT COUNT(*) 
FROM WalletTransactions 
WHERE SenderAccNumber = @acc 
  AND [status] = 'Failure'
  AND TransactionDate >= DATEADD(HOUR, -1, GETDATE())", con);

                    totalFailsCmdd.Parameters.AddWithValue("@acc", senderAcc);
                    int totalFailuress = (int)totalFailsCmdd.ExecuteScalar();

                    // Block account if 5 or more failures
                    if (totalFailuress >= 5)
                    {
                        SqlCommand blockCmd = new SqlCommand("UPDATE UserAccounts SET accstatus = 'Blocked' WHERE AccountNumber = @acc", con);
                        blockCmd.Parameters.AddWithValue("@acc", senderAcc);
                        blockCmd.ExecuteNonQuery();

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Your account has been blocked due to multiple failed transactions.');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Invalid security key. Transaction failed.');", true);
                    }

                    return;
                }


             

                // Validate receiver account
                SqlCommand checkReceiver = new SqlCommand("SELECT COUNT(*) FROM UserAccounts WHERE AccountNumber = @acc", con);
                checkReceiver.Parameters.AddWithValue("@acc", receiverAcc);
                int receiverExists = (int)checkReceiver.ExecuteScalar();

                if (receiverExists == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Receiver account does not exist.');", true);
                    return;
                }

                // Check failures in last 1 hour
                SqlCommand failHourCmd = new SqlCommand(@"
            SELECT COUNT(*) 
            FROM WalletTransactions 
            WHERE SenderAccNumber = @acc AND [status] = 'Failure' AND TransactionDate >= DATEADD(HOUR, -1, GETDATE())", con);
                failHourCmd.Parameters.AddWithValue("@acc", senderAcc);
                int failuresLastHour = (int)failHourCmd.ExecuteScalar();

                if (failuresLastHour > 0)
                {
                    SqlCommand insertFail = new SqlCommand(@"
                INSERT INTO WalletTransactions (SenderAccNumber, ReceiverAccNumber, Amount, TransactionDate, [status],lat, longg, ipads, skey,UserId,tid) 
                VALUES (@sender, @receiver,'"+ amount + "', GETDATE(), 'Failure',@lat, @longg, @ipads, @skey, @UserId,@tid)", con);
                    insertFail.Parameters.AddWithValue("@sender", senderAcc);
                    insertFail.Parameters.AddWithValue("@receiver", receiverAcc);
                    insertFail.Parameters.AddWithValue("@lat", hfLat.Value);
                    insertFail.Parameters.AddWithValue("@longg", hfLong.Value);
                    insertFail.Parameters.AddWithValue("@ipads", hfIP.Value);
                    insertFail.Parameters.AddWithValue("@skey", enteredSkey);
                    insertFail.Parameters.AddWithValue("@UserId", Session["UserId"].ToString());
                    insertFail.Parameters.AddWithValue("@UserId", tid);
                    insertFail.ExecuteNonQuery();
                }

                // Check total failure count
                SqlCommand totalFailsCmd = new SqlCommand(@"
            SELECT COUNT(*) 
            FROM WalletTransactions 
            WHERE SenderAccNumber = @acc AND [status] = 'Failure'", con);
                totalFailsCmd.Parameters.AddWithValue("@acc", senderAcc);
                int totalFailures = (int)totalFailsCmd.ExecuteScalar();

                Random r = new Random();
                int num = r.Next(3, 7);

                if (totalFailures >= num)
                {
                    SqlCommand blockCmd = new SqlCommand("UPDATE UserAccounts SET accstatus = 'Blocked' WHERE AccountNumber = @acc", con);
                    blockCmd.Parameters.AddWithValue("@acc", senderAcc);
                    blockCmd.ExecuteNonQuery();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Your account has been blocked due to multiple failed transactions.');", true);
                    return;
                }

                // Get wallet balance
                SqlCommand getBalanceCmd = new SqlCommand("SELECT Balance FROM UserWallets WHERE AccountNumber = @acc", con);
                getBalanceCmd.Parameters.AddWithValue("@acc", senderAcc);
                object balanceResult = getBalanceCmd.ExecuteScalar();

                if (balanceResult == null || balanceResult == DBNull.Value)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Sender wallet not found.');", true);
                    return;
                }

                decimal senderBalance = Convert.ToDecimal(balanceResult);
                if (senderBalance < amount)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Insufficient balance.');", true);
                    return;
                }

                // Begin transfer
                SqlTransaction trans = con.BeginTransaction();

                try
                {
                    SqlCommand deductCmd = new SqlCommand("UPDATE UserWallets SET Balance = Balance - @amt WHERE AccountNumber = @acc", con, trans);
                    deductCmd.Parameters.AddWithValue("@amt", amount);
                    deductCmd.Parameters.AddWithValue("@acc", senderAcc);
                    deductCmd.ExecuteNonQuery();

                    SqlCommand insertTxn = new SqlCommand(@"
                INSERT INTO WalletTransactions (SenderAccNumber, ReceiverAccNumber, Amount, TransactionDate,lat, longg, ipads, skey,UserId,tid)
                VALUES (@sender, @receiver, @amt, GETDATE(),@lat, @longg, @ipads, @skey, @UserId,@tid)", con, trans);
                    insertTxn.Parameters.AddWithValue("@sender", senderAcc);
                    insertTxn.Parameters.AddWithValue("@receiver", receiverAcc);
                    insertTxn.Parameters.AddWithValue("@amt", amount);
                    insertTxn.Parameters.AddWithValue("@lat", hfLat.Value);
                    insertTxn.Parameters.AddWithValue("@longg", hfLong.Value);
                    insertTxn.Parameters.AddWithValue("@ipads", hfIP.Value);
                    insertTxn.Parameters.AddWithValue("@skey", enteredSkey);
                    insertTxn.Parameters.AddWithValue("@UserId", Session["UserId"].ToString());
                    insertTxn.Parameters.AddWithValue("@tid", tid);

                    insertTxn.ExecuteNonQuery();

                    SqlCommand checkReceiverWallet = new SqlCommand(
                        "SELECT COUNT(*) FROM UserWallets WHERE AccountNumber = @receiver", con, trans);
                    checkReceiverWallet.Parameters.AddWithValue("@receiver", receiverAcc);
                    int receiverWalletExists = (int)checkReceiverWallet.ExecuteScalar();

                    if (receiverWalletExists == 0)
                    {
                        SqlCommand insertReceiverWallet = new SqlCommand(
                            "INSERT INTO UserWallets(AccountNumber, Balance, WalletType) VALUES(@acc, @bal, 'Standard')", con, trans);
                        insertReceiverWallet.Parameters.AddWithValue("@acc", receiverAcc);
                        insertReceiverWallet.Parameters.AddWithValue("@bal", amount);
                        insertReceiverWallet.ExecuteNonQuery();
                    }
                    else
                    {
                        SqlCommand updateReceiverWallet = new SqlCommand(
                            "UPDATE UserWallets SET Balance = Balance + @bal WHERE AccountNumber = @acc", con, trans);
                        updateReceiverWallet.Parameters.AddWithValue("@bal", amount);
                        updateReceiverWallet.Parameters.AddWithValue("@acc", receiverAcc);
                        updateReceiverWallet.ExecuteNonQuery();
                    }

                    trans.Commit();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Transfer successful!'); window.location='manage.aspx';", true);

                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", $"alert('Transfer failed: {ex.Message}');", true);
                }
            }
        }


    }
}