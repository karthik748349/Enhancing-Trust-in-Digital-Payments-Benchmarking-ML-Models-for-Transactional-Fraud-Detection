using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bank
{
	public partial class test : System.Web.UI.Page
	{
        private const string ViewStateKey = "CleanExcelData";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GridView1.AllowPaging = true;
            }
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            lblMessage.Text = lblRowCount.Text = lblColRemoved.Text = lblBlankRowRemoved.Text = lblUnwantedRowRemoved.Text = "";

            if (!FileUpload1.HasFile || Path.GetExtension(FileUpload1.FileName).ToLower() != ".xlsx")
            {
                lblMessage.Text = "Please upload a valid Excel (.xlsx) file.";
                return;
            }

            try
            {
                string filePath = Server.MapPath("~/Uploads/" + Guid.NewGuid() + ".xlsx");
                FileUpload1.SaveAs(filePath);

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

                // ❌ Remove 'job_description' column if it exists
                if (dt.Columns.Contains("job_description"))
                {
                    dt.Columns.Remove("job_description");
                }

                // Clean data
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

                int unwantedRowCount = removeRows.Count;
                foreach (var row in removeRows) dt.Rows.Remove(row);

                List<DataRow> blankRows = new List<DataRow>();
                foreach (DataRow row in dt.Rows)
                {
                    if (row.ItemArray.All(val => string.IsNullOrWhiteSpace(val.ToString())))
                        blankRows.Add(row);
                }

                int blankRowCount = blankRows.Count;
                foreach (var row in blankRows) dt.Rows.Remove(row);

                List<string> blankCols = new List<string>();
                foreach (DataColumn col in dt.Columns)
                {
                    if (dt.AsEnumerable().All(row => string.IsNullOrWhiteSpace(row[col].ToString())))
                        blankCols.Add(col.ColumnName);
                }

                foreach (var col in blankCols) dt.Columns.Remove(col);

                // ✅ Store cleaned data
                ViewState[ViewStateKey] = dt;
                BindGrid();
                ShowLabels(dt, blankCols.Count, blankRowCount, unwantedRowCount);

                // ✅ Analyze for graphs
                ShowFraudGraphs(dt);
                PredictByRules(dt);

                File.Delete(filePath);
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
            }
        }


        private void ShowLabels(DataTable dt, int colRemoved, int blankRows, int unwantedRows)
        {
            lblRowCount.Text = "Total Rows After Cleanup: " + dt.Rows.Count;
            lblColRemoved.Text = "Blank Columns Removed: " + colRemoved;
            lblBlankRowRemoved.Text = "Blank Rows Removed: " + blankRows;
            lblUnwantedRowRemoved.Text = "Unwanted Rows Removed: " + unwantedRows;
        }

        private void BindGrid()
        {
            if (ViewState[ViewStateKey] != null)
            {
                GridView1.DataSource = (DataTable)ViewState[ViewStateKey];
                GridView1.DataBind();
            }
        }

        protected void GridView1_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        private void ShowFraudGraphs(DataTable dt)
        {
            // Ensure 'category' column exists
            if (!dt.Columns.Contains("category"))
                return;

            // Count occurrences of each distinct category
            var categoryCounts = dt.AsEnumerable()
                .Where(r => !string.IsNullOrWhiteSpace(r["category"]?.ToString()))
                .GroupBy(r => r["category"].ToString())
                .Select(g => new { Category = g.Key, Count = g.Count() })
                .ToList();

            // Build JS data array
            string jsCategoryData = "[['Category', 'Count'], " +
                string.Join(",", categoryCounts.Select(c => $"['{c.Category}', {c.Count}]")) +
                "]";

            // Dummy ML Accuracy
            int svm = 87, rf = 91, lstm = 93;

            ScriptManager.RegisterStartupScript(this, GetType(), "fraudChart", $@"
        google.charts.setOnLoadCallback(function() {{
            drawCategoryPieChart({jsCategoryData});
            drawAccuracyChart({svm}, {rf}, {lstm});
        }});", true);
        }


        private void PredictByRules(DataTable dt)
        {
            DataTable result = new DataTable();
            result.Columns.Add("job_id");
            result.Columns.Add("category");
            result.Columns.Add("job_title");
            result.Columns.Add("job_description");
            result.Columns.Add("job_skill_set");

            foreach (DataRow row in dt.Rows)
            {
                string jobId = row["job_id"]?.ToString();
                string category = row["category"]?.ToString();
                string jobTitle = row["job_title"]?.ToString();
                string jobDesc = row["job_description"]?.ToString();
                string skillSet = row["job_skill_set"]?.ToString();

                result.Rows.Add(jobId, category, jobTitle, jobDesc, skillSet);
            }

            GridPredicted.DataSource = result;
            GridPredicted.DataBind();
        }

    }
}