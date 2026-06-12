using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bank.Admin
{
    public partial class karnataka : System.Web.UI.Page
	{
        private const string ViewStateKey = "CleanExcelData";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GridView1.AllowPaging = true;
                LoadLatestExcelFromFolder(); // Directly load on first load
            }
        }
        private void LoadLatestExcelFromFolder()
        {
            try
            {
                string folderPath = Server.MapPath("~/karnataka/");
                DirectoryInfo dir = new DirectoryInfo(folderPath);

                if (!dir.Exists)
                {
                    lblMessage.Text = "Uploads folder not found.";
                    return;
                }

                // Get the most recent .xlsx file
                FileInfo latestFile = dir.GetFiles("*.xlsx")
                                         .OrderByDescending(f => f.LastWriteTime)
                                         .FirstOrDefault();

                if (latestFile == null)
                {
                    lblMessage.Text = "No Excel file found in folder.";
                    return;
                }

                string conStr = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={latestFile.FullName};
                           Extended Properties='Excel 12.0 Xml;HDR=YES;'";
                DataTable dt = new DataTable();

                using (OleDbConnection conn = new OleDbConnection(conStr))
                {
                    conn.Open();
                    var sheet = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                    new OleDbDataAdapter($"SELECT * FROM [{sheet}]", conn).Fill(dt);
                    conn.Close();
                }

                // Clean the data (same logic as in your upload method)
                string[] unwanted = { "test", "na", "null", "xyz" };
                var removeRows = dt.AsEnumerable()
                                   .Where(row => dt.Columns.Cast<DataColumn>()
                                   .Any(col => unwanted.Contains(row[col]?.ToString().Trim().ToLower()))).ToList();

                int unwantedRowCount = removeRows.Count;
                foreach (var row in removeRows) dt.Rows.Remove(row);

                var blankRows = dt.AsEnumerable()
                                  .Where(row => row.ItemArray.All(val => string.IsNullOrWhiteSpace(val.ToString())))
                                  .ToList();

                int blankRowCount = blankRows.Count;
                foreach (var row in blankRows) dt.Rows.Remove(row);

                var blankCols = dt.Columns.Cast<DataColumn>()
                                  .Where(col => dt.AsEnumerable().All(row => string.IsNullOrWhiteSpace(row[col].ToString())))
                                  .Select(c => c.ColumnName).ToList();

                foreach (var col in blankCols) dt.Columns.Remove(col);

                // Bind and process
                ViewState[ViewStateKey] = dt;
                BindGrid();
                ShowLabels(dt, blankCols.Count, blankRowCount, unwantedRowCount);
                ShowFraudGraphs(dt);
                PredictByRules(dt);
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error while loading Excel: " + ex.Message;
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
            int fraud = dt.AsEnumerable().Count(r => r.Table.Columns.Contains("Fraud_Label") && r["Fraud_Label"].ToString() == "1");
            int genuine = dt.Rows.Count - fraud;

            // Dummy ML Accuracy
            int svm = 87, rf = 91, lstm = 93;

            ScriptManager.RegisterStartupScript(this, GetType(), "fraudChart", $@"
                google.charts.setOnLoadCallback(function() {{
                    drawFraudPieChart({fraud}, {genuine});
                    drawAccuracyChart({svm}, {rf}, {lstm});
                }});", true);
        }

        private void PredictByRules(DataTable dt)
        {
            int[] fraudFeatures = new int[6];
            int[] genuineFeatures = new int[6];

            DataTable result = new DataTable();
            result.Columns.Add("Transaction_Amount");
            result.Columns.Add("IP_Address_Flag");
            result.Columns.Add("Device_Type");
            result.Columns.Add("Previous_Fraudulent_Activity");
            result.Columns.Add("Failed_Transaction_Count_7d");
            result.Columns.Add("Risk_Score");
            result.Columns.Add("Prediction");

            foreach (DataRow row in dt.Rows)
            {
                double.TryParse(row["Transaction_Amount"].ToString(), out double amount);
                double.TryParse(row["Failed_Transaction_Count_7d"].ToString(), out double failCount);
                double.TryParse(row["Risk_Score"].ToString(), out double riskScore);

                string ipFlag = row["IP_Address_Flag"]?.ToString();
                string device = row["Device_Type"]?.ToString().ToLower();
                string pastFraud = row["Previous_Fraudulent_Activity"]?.ToString();

                bool isFraud = amount > 5000 || ipFlag == "1" || device == "mobile" || pastFraud == "1" || failCount > 2 || riskScore > 70;

                // Count features for charting
                if (amount > 5000) (isFraud ? fraudFeatures : genuineFeatures)[0]++;
                if (ipFlag == "1") (isFraud ? fraudFeatures : genuineFeatures)[1]++;
                if (device == "mobile") (isFraud ? fraudFeatures : genuineFeatures)[2]++;
                if (pastFraud == "1") (isFraud ? fraudFeatures : genuineFeatures)[3]++;
                if (failCount > 2) (isFraud ? fraudFeatures : genuineFeatures)[4]++;
                if (riskScore > 70) (isFraud ? fraudFeatures : genuineFeatures)[5]++;

                // Add result
                result.Rows.Add(amount, ipFlag, device, pastFraud, failCount, riskScore, isFraud ? "Fraud" : "Genuine");
            }

            // Save results to ViewState for further use
            ViewState["PredictedTable"] = result;

            // Optional: show all in one table
            //GridPredicted.DataSource = result;
            //GridPredicted.DataBind();

            // Show separated tables
            BindFraudAndGenuineGrids();

            // Render chart via JS
            string jsFraud = "[" + string.Join(",", fraudFeatures) + "]";
            string jsGenuine = "[" + string.Join(",", genuineFeatures) + "]";

            ScriptManager.RegisterStartupScript(this, GetType(), "featureChart", $@"
        google.charts.setOnLoadCallback(function() {{
            drawFeatureFraudChart({jsFraud}, {jsGenuine});
        }});", true);
        }

        private void BindFraudAndGenuineGrids()
        {
            DataTable result = ViewState["PredictedTable"] as DataTable;

            if (result != null)
            {
                // Filter for Fraud
                DataView dvFraud = new DataView(result);
                dvFraud.RowFilter = "Prediction = 'Fraud'";
                GridFraud.DataSource = dvFraud;
                GridFraud.DataBind();

                // Filter for Genuine
                DataView dvGenuine = new DataView(result);
                dvGenuine.RowFilter = "Prediction = 'Genuine'";
                GridGenuine.DataSource = dvGenuine;
                GridGenuine.DataBind();
            }
        }
    }
}
	