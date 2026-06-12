using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bank.Admin
{
    public partial class graph : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindChart();
            imghacker();
            GetStatusCounts();
        }





        //------------------------------------------------------------------------
        private void GetStatusCounts()
        {
            DataTable dt = new DataTable();
            string connStr = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = @"SELECT status, COUNT(*) AS count FROM WalletTransactions GROUP BY status";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            // Convert to JSON-like format for Google Charts
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("[['Status', 'Count'],");
            foreach (DataRow row in dt.Rows)
            {
                sb.Append("['" + row["status"].ToString() + "', " + row["count"].ToString() + "],");
            }
            sb.Length--; // Remove last comma
            sb.Append("]");

            // Register script with page
            ClientScript.RegisterStartupScript(this.GetType(), "drawChart", $@"
            <script type='text/javascript'>
                google.charts.load('current', {{ packages: ['corechart'] }});
                google.charts.setOnLoadCallback(drawChart);
                function drawChart() {{
                    var data = google.visualization.arrayToDataTable({sb.ToString()});
                    var options = {{
                        title: 'Transaction Status Report',
                        chartArea: {{ width: '50%' }},
                        hAxis: {{
                            title: 'Total Transactions',
                            minValue: 0
                        }},
                        vAxis: {{
                            title: 'Status'
                        }}
                    }};
                    var chart = new google.visualization.BarChart(document.getElementById('chart_divvv'));
                    chart.draw(data, options);
                }}
            </script>", false);
        }
        //------------------------------------------------------------------------
        private void BindChart()
        {
            DataTable dsChartData = new DataTable();
            StringBuilder strScript = new StringBuilder();

            try
            {
                dsChartData = GetChartData();

                strScript.Append(@"<script type='text/javascript'>  
                    google.load('visualization', '1', {packages: ['corechart']});</script>  
  
                    <script type='text/javascript'>  
                    function drawVisualization() {         
                    var data = google.visualization.arrayToDataTable([  
                    ['ipAddress', 'Count'],");

                foreach (DataRow row in dsChartData.Rows)
                {
                    strScript.Append("['" + row["userName"] + "'," + row["Counter"] + "],");
                }
                strScript.Remove(strScript.Length - 1, 1);
                strScript.Append("]);");

                strScript.Append("var options = { backgroundColor: 'transparent',title : 'Visual Chart for Email', seriesType: 'bars', series: {3: {type: 'area'}} };");
                strScript.Append(" var chart = new google.visualization.PieChart(document.getElementById('chart_div'));  chart.draw(data, options); } google.setOnLoadCallback(drawVisualization);");
                strScript.Append(" </script>");

                ltScripts.Text = strScript.ToString();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                dsChartData.Dispose();
            }
        }




        private void imghacker()
        {
            DataTable dsChartData = new DataTable();
            StringBuilder strScript = new StringBuilder();

            try
            {
                dsChartData = GetChartData1();

                strScript.Append(@"<script type='text/javascript'>  
                    google.load('visualization', '1', {packages: ['corechart']});</script>  
  
                    <script type='text/javascript'>  
                    function drawVisualization() {         
                    var data = google.visualization.arrayToDataTable([  
                    ['ipAddress', 'Count'],");

                foreach (DataRow row in dsChartData.Rows)
                {
                    strScript.Append("['" + row["SenderAccNumber"] + "'," + row["Counter"] + "],");
                }
                strScript.Remove(strScript.Length - 1, 1);
                strScript.Append("]);");

                strScript.Append("var options = { backgroundColor: 'transparent',title : 'Visual Chart for Account seriesType: 'bars', series: {3: {type: 'area'}} };");
                strScript.Append(" var chart = new google.visualization.PieChart(document.getElementById('chart_div1'));  chart.draw(data, options); } google.setOnLoadCallback(drawVisualization);");
                strScript.Append(" </script>");

                Literal1.Text = strScript.ToString();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                dsChartData.Dispose();
            }
        }


        private DataTable GetChartData()
        {
            DataSet dsData = new DataSet();
            try
            {
                SqlConnection sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
                //SqlDataAdapter sqlCmd = new SqlDataAdapter("GetData", sqlCon);
                //sqlCmd.SelectCommand.CommandType = CommandType.StoredProcedure;
                //string queryString = "Select distinct ipAddress, Count(ipAddress) as Counter from request group by ipAddress";
                string queryString = "Select Count(userName) as Counter,userName from hacker group by userName";
                SqlDataAdapter adapter = new SqlDataAdapter(queryString, sqlCon);
                sqlCon.Open();

                adapter.Fill(dsData);

                sqlCon.Close();
            }
            catch
            {
                throw;
            }
            return dsData.Tables[0];
        }

        private DataTable GetChartData1()
        {
            DataSet dsData = new DataSet();
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString);
                //SqlDataAdapter sqlCmd = new SqlDataAdapter("GetData", sqlCon);
                //sqlCmd.SelectCommand.CommandType = CommandType.StoredProcedure;
                //string queryString = "Select distinct ipAddress, Count(ipAddress) as Counter from request group by ipAddress";
                string queryString = "Select Count(TransactionId) as Counter,SenderAccNumber from WalletTransactions group by SenderAccNumber";
                SqlDataAdapter adapter = new SqlDataAdapter(queryString, con);
                con.Open();

                adapter.Fill(dsData);

                con.Close();
            }
            catch
            {
                throw;
            }
            return dsData.Tables[0];
        }
    }
}