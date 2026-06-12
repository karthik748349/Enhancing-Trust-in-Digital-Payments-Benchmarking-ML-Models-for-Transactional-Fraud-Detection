using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bank.Admin
{
	public partial class index : System.Web.UI.Page
	{
        SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["Constr"]);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int count = GetExcelRecordCount();
                label1.Text = count.ToString();
                
                int kcnt = KarnatakaBank();
                Label2.Text = kcnt.ToString();

                int hcnt = HDFCBank();
                Label3.Text = hcnt.ToString();

                int scnt = SbiBank();
                Label4.Text = scnt.ToString();
            }
        }

        private int GetExcelRecordCount()
        {
            try
            {
                string folderPath = Server.MapPath("~/Uploads/");
                var latestFile = new DirectoryInfo(folderPath)
                                 .GetFiles("*.xlsx")
                                 .OrderByDescending(f => f.LastWriteTime)
                                 .FirstOrDefault();

                if (latestFile == null)
                    return 0;

                string filePath = latestFile.FullName;

                string conStr = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={filePath};
                        Extended Properties='Excel 12.0 Xml;HDR=YES;'";
                DataTable dt = new DataTable();

                using (OleDbConnection conn = new OleDbConnection(conStr))
                {
                    conn.Open();
                    var sheet = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                    new OleDbDataAdapter($"SELECT * FROM [{sheet}]", conn).Fill(dt);
                    conn.Close();
                }

                // Clean same as in btnUpload_Click
                string[] unwanted = { "test", "na", "null", "xyz" };
                List<DataRow> removeRows = new List<DataRow>();
                foreach (DataRow row in dt.Rows)
                {
                    foreach (DataColumn col in dt.Columns)
                    {
                        string val = row[col]?.ToString().Trim().ToLower();
                        if (unwanted.Contains(val))
                        {
                            removeRows.Add(row);
                            break;
                        }
                    }
                }

                foreach (var row in removeRows) dt.Rows.Remove(row);

                List<DataRow> blankRows = new List<DataRow>();
                foreach (DataRow row in dt.Rows)
                {
                    if (row.ItemArray.All(val => string.IsNullOrWhiteSpace(val.ToString())))
                        blankRows.Add(row);
                }

                foreach (var row in blankRows) dt.Rows.Remove(row);

                List<string> blankCols = new List<string>();
                foreach (DataColumn col in dt.Columns)
                {
                    if (dt.AsEnumerable().All(row => string.IsNullOrWhiteSpace(row[col].ToString())))
                        blankCols.Add(col.ColumnName);
                }

                foreach (var col in blankCols) dt.Columns.Remove(col);

                return dt.Rows.Count;
            }
            catch
            {
                return 0;
            }
        }
        private int KarnatakaBank()
        {
            try
            {
                string folderPath = Server.MapPath("~/karnataka/");
                var latestFile = new DirectoryInfo(folderPath)
                                 .GetFiles("*.xlsx")
                                 .OrderByDescending(f => f.LastWriteTime)
                                 .FirstOrDefault();

                if (latestFile == null)
                    return 0;

                string filePath = latestFile.FullName;

                string conStr = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={filePath};
                        Extended Properties='Excel 12.0 Xml;HDR=YES;'";
                DataTable dt = new DataTable();

                using (OleDbConnection conn = new OleDbConnection(conStr))
                {
                    conn.Open();
                    var sheet = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                    new OleDbDataAdapter($"SELECT * FROM [{sheet}]", conn).Fill(dt);
                    conn.Close();
                }

                // Clean same as in btnUpload_Click
                string[] unwanted = { "test", "na", "null", "xyz" };
                List<DataRow> removeRows = new List<DataRow>();
                foreach (DataRow row in dt.Rows)
                {
                    foreach (DataColumn col in dt.Columns)
                    {
                        string val = row[col]?.ToString().Trim().ToLower();
                        if (unwanted.Contains(val))
                        {
                            removeRows.Add(row);
                            break;
                        }
                    }
                }

                foreach (var row in removeRows) dt.Rows.Remove(row);

                List<DataRow> blankRows = new List<DataRow>();
                foreach (DataRow row in dt.Rows)
                {
                    if (row.ItemArray.All(val => string.IsNullOrWhiteSpace(val.ToString())))
                        blankRows.Add(row);
                }

                foreach (var row in blankRows) dt.Rows.Remove(row);

                List<string> blankCols = new List<string>();
                foreach (DataColumn col in dt.Columns)
                {
                    if (dt.AsEnumerable().All(row => string.IsNullOrWhiteSpace(row[col].ToString())))
                        blankCols.Add(col.ColumnName);
                }

                foreach (var col in blankCols) dt.Columns.Remove(col);

                return dt.Rows.Count;
            }
            catch
            {
                return 0;
            }
        }


        private int HDFCBank()
        {
            try
            {
                string folderPath = Server.MapPath("~/hdfc/");
                var latestFile = new DirectoryInfo(folderPath)
                                 .GetFiles("*.xlsx")
                                 .OrderByDescending(f => f.LastWriteTime)
                                 .FirstOrDefault();

                if (latestFile == null)
                    return 0;

                string filePath = latestFile.FullName;

                string conStr = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={filePath};
                        Extended Properties='Excel 12.0 Xml;HDR=YES;'";
                DataTable dt = new DataTable();

                using (OleDbConnection conn = new OleDbConnection(conStr))
                {
                    conn.Open();
                    var sheet = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                    new OleDbDataAdapter($"SELECT * FROM [{sheet}]", conn).Fill(dt);
                    conn.Close();
                }

                // Clean same as in btnUpload_Click
                string[] unwanted = { "test", "na", "null", "xyz" };
                List<DataRow> removeRows = new List<DataRow>();
                foreach (DataRow row in dt.Rows)
                {
                    foreach (DataColumn col in dt.Columns)
                    {
                        string val = row[col]?.ToString().Trim().ToLower();
                        if (unwanted.Contains(val))
                        {
                            removeRows.Add(row);
                            break;
                        }
                    }
                }

                foreach (var row in removeRows) dt.Rows.Remove(row);

                List<DataRow> blankRows = new List<DataRow>();
                foreach (DataRow row in dt.Rows)
                {
                    if (row.ItemArray.All(val => string.IsNullOrWhiteSpace(val.ToString())))
                        blankRows.Add(row);
                }

                foreach (var row in blankRows) dt.Rows.Remove(row);

                List<string> blankCols = new List<string>();
                foreach (DataColumn col in dt.Columns)
                {
                    if (dt.AsEnumerable().All(row => string.IsNullOrWhiteSpace(row[col].ToString())))
                        blankCols.Add(col.ColumnName);
                }

                foreach (var col in blankCols) dt.Columns.Remove(col);

                return dt.Rows.Count;
            }
            catch
            {
                return 0;
            }
        }

        private int SbiBank()
        {
            try
            {
                string folderPath = Server.MapPath("~/sbi/");
                var latestFile = new DirectoryInfo(folderPath)
                                 .GetFiles("*.xlsx")
                                 .OrderByDescending(f => f.LastWriteTime)
                                 .FirstOrDefault();

                if (latestFile == null)
                    return 0;

                string filePath = latestFile.FullName;

                string conStr = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={filePath};
                        Extended Properties='Excel 12.0 Xml;HDR=YES;'";
                DataTable dt = new DataTable();

                using (OleDbConnection conn = new OleDbConnection(conStr))
                {
                    conn.Open();
                    var sheet = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                    new OleDbDataAdapter($"SELECT * FROM [{sheet}]", conn).Fill(dt);
                    conn.Close();
                }

                // Clean same as in btnUpload_Click
                string[] unwanted = { "test", "na", "null", "xyz" };
                List<DataRow> removeRows = new List<DataRow>();
                foreach (DataRow row in dt.Rows)
                {
                    foreach (DataColumn col in dt.Columns)
                    {
                        string val = row[col]?.ToString().Trim().ToLower();
                        if (unwanted.Contains(val))
                        {
                            removeRows.Add(row);
                            break;
                        }
                    }
                }

                foreach (var row in removeRows) dt.Rows.Remove(row);

                List<DataRow> blankRows = new List<DataRow>();
                foreach (DataRow row in dt.Rows)
                {
                    if (row.ItemArray.All(val => string.IsNullOrWhiteSpace(val.ToString())))
                        blankRows.Add(row);
                }

                foreach (var row in blankRows) dt.Rows.Remove(row);

                List<string> blankCols = new List<string>();
                foreach (DataColumn col in dt.Columns)
                {
                    if (dt.AsEnumerable().All(row => string.IsNullOrWhiteSpace(row[col].ToString())))
                        blankCols.Add(col.ColumnName);
                }

                foreach (var col in blankCols) dt.Columns.Remove(col);

                return dt.Rows.Count;
            }
            catch
            {
                return 0;
            }
        }
        private void Loadcounts()
        {
            //string sql = "select count(id) from cr_login where utype='alumini' and status='approved'";


            //try
            //{
            //    con.open();
            //    sqlcommand cmd = new sqlcommand(sql, con);
            //    int32 count = convert.toint32(cmd.executescalar());
            //    label1.text = convert.tostring(count);
            //    cmd.dispose();
            //    con.close();

            //}
            //catch (exception ex)
            //{

            //}

            //string sql2 = "Select Count(id) from CR_Login where utype='Faculty' and status='Approved'";


            //try
            //{
            //    con.Open();
            //    SqlCommand cmd = new SqlCommand(sql2, con);
            //    Int32 count = Convert.ToInt32(cmd.ExecuteScalar());
            //    Label2.Text = Convert.ToString(count);
            //    cmd.Dispose();
            //    con.Close();

            //}
            //catch (Exception ex)
            //{

            //}


            //string sql3 = "Select Count(id) from CR_Login where utype='Current' and status='Approved'";


            //try
            //{
            //    con.Open();
            //    SqlCommand cmd = new SqlCommand(sql3, con);
            //    Int32 count = Convert.ToInt32(cmd.ExecuteScalar());
            //    Label3.Text = Convert.ToString(count);
            //    cmd.Dispose();
            //    con.Close();

            //}
            //catch (Exception ex)
            //{

            //}



            //string sql4 = "Select sum(cast(Amount as int) ) from CR_Donation ";


            //try
            //{
            //    con.Open();
            //    SqlCommand cmd = new SqlCommand(sql4, con);
            //    Int32 count = Convert.ToInt32(cmd.ExecuteScalar());
            //    Label4.Text = Convert.ToString(count);
            //    cmd.Dispose();
            //    con.Close();

            //}
            //catch (Exception ex)
            //{

            //}
        }
    }
}
