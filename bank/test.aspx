<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="bank.test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>Fraud Detection Dashboard</title>

    <!-- Bootstrap & Google Charts -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>

    <style>
        .highlighted-page a {
            background-color: #0d6efd !important;
            color: #fff !important;
        }
        .section-title {
            font-weight: bold;
            font-size: 1.25rem;
            margin-bottom: 10px;
            color: #343a40;
        }
    </style>

    <script type="text/javascript">
        google.charts.load("current", { packages: ["corechart", "bar"] });

        function drawCategoryPieChart(dataArray) {
            var data = google.visualization.arrayToDataTable(dataArray);

            var options = {
                title: 'Job Category',
                is3D: true,
                pieHole: 0.3
            };

            var chart = new google.visualization.PieChart(document.getElementById('piechart'));
            chart.draw(data, options);
        }

        function drawAccuracyChart(svm, rf, lstm) {
            var data = google.visualization.arrayToDataTable([
                ['Algorithm', 'Accuracy (%)', { role: 'style' }],
                ['NLP', svm, 'color: #007bff'],
                ['KNN', rf, 'color: #28a745'],
                ['Logistic Regression', lstm, 'color: #ffc107']
            ]);
            var options = {
                title: 'Algorithm Accuracy Comparison',
                hAxis: { title: 'Algorithm' },
                vAxis: { title: 'Accuracy (%)', minValue: 0, maxValue: 100 },
                legend: 'none'
            };
            var chart = new google.visualization.ColumnChart(document.getElementById('accuracychart'));
            chart.draw(data, options);
        }

        function drawFeatureFraudChart(fraudData, genuineData) {
            var data = google.visualization.arrayToDataTable([
                ['Feature', 'Fraud Count', 'Genuine Count'],
                ['Transaction > ₹5000', fraudData[0], genuineData[0]],
                ['IP Address Flag', fraudData[1], genuineData[1]],
                ['Device: Mobile', fraudData[2], genuineData[2]],
                ['Previous Fraud', fraudData[3], genuineData[3]],
                ['Failed Tx > 2', fraudData[4], genuineData[4]],
                ['Risk > 70', fraudData[5], genuineData[5]]
            ]);
            var options = {
                title: 'Feature-based Risk Detection',
                chartArea: { width: '60%' },
                isStacked: true,
                hAxis: {
                    title: 'Number of Transactions',
                    minValue: 0
                },
                vAxis: {
                    title: 'Risk Feature'
                }
            };
            var chart = new google.visualization.BarChart(document.getElementById('featurechart'));
            chart.draw(data, options);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" class="container mt-4">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />

        <!-- Introduction -->
   

        <!-- Upload Section -->
        <div class="card shadow mb-4">
            <div class="card-header bg-dark text-white">
                <h5 class="mb-0">Step 1: Upload and Clean Excel Data</h5>
            </div>
            <div class="card-body">
                <div class="mb-3">
                    <label class="form-label">Choose .xlsx File:</label>
                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" />
                </div>
                <asp:Button ID="btnUpload" runat="server" Text="Upload & Clean"
                    CssClass="btn btn-success mb-3" OnClick="btnUpload_Click" />

                <asp:Label ID="lblMessage" runat="server" CssClass="text-danger fw-bold d-block"></asp:Label>
                <asp:Label ID="lblRowCount" runat="server" CssClass="fw-bold d-block text-info"></asp:Label>
                <asp:Label ID="lblColRemoved" runat="server" CssClass="fw-bold d-block text-success"></asp:Label>
                <asp:Label ID="lblBlankRowRemoved" runat="server" CssClass="fw-bold d-block text-warning"></asp:Label>
                <asp:Label ID="lblUnwantedRowRemoved" runat="server" CssClass="fw-bold d-block text-danger"></asp:Label>

                <div class="table-responsive mt-3">
                    <asp:GridView ID="GridView1" runat="server"
                        AutoGenerateColumns="true"
                        AllowPaging="true"
                        PageSize="100"
                        CssClass="table table-bordered table-hover table-striped"
                        PagerStyle-CssClass="pagination-container"
                        PagerStyle-HorizontalAlign="Center"
                        OnPageIndexChanging="GridView1_PageIndexChanging">
                    </asp:GridView>
                </div>
            </div>
        </div>

        <!-- Pie Chart -->
        <div class="card shadow mb-4">
            <div class="card-header bg-danger text-white">
                <h5 class="mb-0">Step 2: Job Category</h5>
            </div>
            <div class="card-body">
                <div id="piechart" style="width: 100%; height: 400px;"></div>
            </div>
        </div>

        <!-- ML Accuracy -->
        <div class="card shadow mb-4">
            <div class="card-header bg-info text-white">
                <h5 class="mb-0">Step 3: ML Algorithm Accuracy Comparison</h5>
            </div>
            <div class="card-body">
                <div id="accuracychart" style="width: 100%; height: 400px;"></div>
                <p class="mt-3"><strong>Note:</strong> Accuracy values shown are indicative and can vary based on dataset size and training.</p>
            </div>
        </div>

        <!-- Feature-Based Prediction -->
        <div class="card shadow mb-4" style="display:none">
            <div class="card-header bg-warning">
                <h5 class="mb-0">Step 4: Feature-Based Risk Analysis</h5>
            </div>
            <div class="card-body">
                <div id="featurechart" style="width: 100%; height: 400px;"></div>
                <p>This chart shows how various transaction attributes contribute to fraud predictions.</p>
            </div>
        </div>

        <!-- Predicted Grid -->
        <div class="card shadow mb-5" style="display:none">
            <div class="card-header bg-secondary text-white">
                <h5 class="mb-0">Step 5: Rule-Based Fraud Predictions (Simulated)</h5>
            </div>
            <div class="card-body">
                <asp:GridView ID="GridPredicted" runat="server" CssClass="table table-bordered table-hover table-sm" />
            </div>
        </div>
    </form>
</body>
</html>
