using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeOpenXml;

namespace bank
{
    public partial class index : System.Web.UI.Page
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

                // Count for chart
                if (amount > 5000) (isFraud ? fraudFeatures : genuineFeatures)[0]++;
                if (ipFlag == "1") (isFraud ? fraudFeatures : genuineFeatures)[1]++;
                if (device == "mobile") (isFraud ? fraudFeatures : genuineFeatures)[2]++;
                if (pastFraud == "1") (isFraud ? fraudFeatures : genuineFeatures)[3]++;
                if (failCount > 2) (isFraud ? fraudFeatures : genuineFeatures)[4]++;
                if (riskScore > 70) (isFraud ? fraudFeatures : genuineFeatures)[5]++;

                // Add to Grid
                result.Rows.Add(amount, ipFlag, device, pastFraud, failCount, riskScore, isFraud ? "Fraud" : "Genuine");
            }

            GridPredicted.DataSource = result;
            GridPredicted.DataBind();

            // Pass data to chart
            string jsFraud = "[" + string.Join(",", fraudFeatures) + "]";
            string jsGenuine = "[" + string.Join(",", genuineFeatures) + "]";

            ScriptManager.RegisterStartupScript(this, GetType(), "featureChart", $@"
                google.charts.setOnLoadCallback(function() {{
                    drawFeatureFraudChart({jsFraud}, {jsGenuine});
                }});", true);
        }
    }
}