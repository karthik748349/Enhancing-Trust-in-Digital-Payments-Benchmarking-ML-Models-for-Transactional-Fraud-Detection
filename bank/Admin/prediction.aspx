<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="prediction.aspx.cs" Inherits="bank.Admin.prediction" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>AdminLTE 2 | Starter</title>
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <link rel="stylesheet" href="bower_components/bootstrap/dist/css/bootstrap.min.css">
    <!-- Font Awesome -->
    <link rel="stylesheet" href="bower_components/font-awesome/css/font-awesome.min.css">
    <!-- Ionicons -->
    <link rel="stylesheet" href="bower_components/Ionicons/css/ionicons.min.css">
    <!-- Theme style -->
    <link rel="stylesheet" href="dist/css/AdminLTE.min.css">
    <!-- AdminLTE Skins. We have chosen the skin-blue for this starter
        page. However, you can choose any other skin. Make sure you
        apply the skin class to the body tag so the changes take effect. -->
    <link rel="stylesheet" href="dist/css/skins/skin-blue.min.css">

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
  <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
  <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
  <![endif]-->

    <!-- Google Font -->
    <link rel="stylesheet"
        href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic">





    <!-- Bootstrap & Google Charts -->

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

        function drawFraudPieChart(fraud, genuine) {
            var data = google.visualization.arrayToDataTable([
                ['Transaction Type', 'Count'],
                ['Fraud', fraud],
                ['Genuine', genuine]
            ]);
            var options = {
                title: 'Fraud vs Genuine Transactions',
                is3D: true,
                slices: {
                    0: { color: '#dc3545' },
                    1: { color: '#198754' }
                }
            };
            var chart = new google.visualization.PieChart(document.getElementById('piechart'));
            chart.draw(data, options);
        }

        function drawAccuracyChart(svm, rf, lstm) {
            var data = google.visualization.arrayToDataTable([
                ['Algorithm', 'Accuracy (%)', { role: 'style' }],
                ['SVM', svm, 'color: #007bff'],
                ['Random Forest', rf, 'color: #28a745'],
                ['LSTM', lstm, 'color: #ffc107']
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

<body class="hold-transition skin-blue sidebar-mini">
    <form runat="server">
        <div class="wrapper">

            <!-- Main Header -->
            <header class="main-header">
                <!-- Logo -->
                <a href="index2.html" class="logo">
                    <!-- mini logo for sidebar mini 50x50 pixels -->
                    <span class="logo-mini"><b>A</b>LT</span>
                    <!-- logo for regular state and mobile devices -->
                    <span class="logo-lg"><b>Admin</b>LTE</span>
                </a>

                <!-- Header Navbar -->
                <nav class="navbar navbar-static-top" role="navigation">
                    <!-- Sidebar toggle button-->
                    <a href="#" class="sidebar-toggle" data-toggle="push-menu" role="button">
                        <span class="sr-only">Toggle navigation</span>
                    </a>

                </nav>
            </header>
            <!-- Left side column. contains the logo and sidebar -->
            <aside class="main-sidebar">
                <!-- sidebar: style can be found in sidebar.less -->
                <section class="sidebar">
                    <!-- Sidebar user panel -->


                    <!-- /.search form -->
                    <!-- sidebar menu: : style can be found in sidebar.less -->
                    <ul class="sidebar-menu" data-widget="tree">
                        <li class="header">MAIN NAVIGATION</li>
                        <li class="active"><a href="index.aspx"><i class="fa fa-dashboard"></i><span>Dashboard</span></a></li>
                        <li class="active treeview"></li>

                        <li class="header">User Option</li>



                        <li><a href="predicton.aspx"><i class="fa fa-circle-o text-yellow"></i><span>Canara Bank</span></a></li>
                        <li><a href="karnataka.aspx"><i class="fa fa-circle-o text-yellow"></i><span>Karnataka Bank</span></a></li>
                        <li><a href="hdfc.aspx"><i class="fa fa-circle-o text-yellow"></i><span>Hdfc Bank</span></a></li>
                        <li><a href="sbi.aspx"><i class="fa fa-circle-o text-yellow"></i><span>SBI Bank</span></a></li>

                    </ul>
                </section>
                <!-- /.sidebar -->
            </aside>
            <!-- Content Wrapper. Contains page content -->
            <div class="content-wrapper">
                <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>Manage Shop
     
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="#"><i class="fa fa-dashboard"></i>Level</a></li>
                        <li class="active">Here</li>
                    </ol>
                </section>

                <!-- Main content -->
                <section class="content container-fluid">

                    <!--------------------------
        | Your Page Content Here |
        -------------------------->

                    <!-- ----start-------------- -->


                    <!-- /.box-header -->
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="box">

                                <!-- /.box-header -->
                                <div class="box-body">














                                    <asp:ScriptManager ID="ScriptManager1" runat="server" />

                                    <!-- Introduction -->
                                    <%--     <div class="card mb-4 shadow">
         <div class="card-header bg-primary text-white">
             <h4 class="mb-0">Online Banking Fraud Detection using Machine Learning</h4>
         </div>
         <div class="card-body">
             <p><strong>Problem Statement:</strong> Online banking frauds are increasing rapidly. Accurate detection is critical for saving both banks and customers from major losses.</p>
             <p><strong>Solution:</strong> We use ML algorithms (SVM, Random Forest, LSTM) to identify potentially fraudulent transactions based on patterns found in uploaded Excel data.</p>
            
         </div>
     </div>--%>

                                    <!-- Upload Section -->
                                    <div class="card shadow mb-4">
                                        <div class="box box-default collapsed-box">
                                            <div class="box-header with-border" style="background-color: #e3f2fd;">
                                                <h3 class="box-title">Step 1: Upload and Clean Excel Data</h3>

                                                <div class="box-tools pull-right">
                                                    <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                                        <i class="fa fa-plus"></i>
                                                    </button>
                                                </div>
                                                <!-- /.box-tools -->
                                            </div>
                                            <!-- /.box-header -->
                                            <div class="box-body">
                                                <div class="card shadow mb-4">

                                                    <div class="box-tools pull-right">
                                                        <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                                            <i class="fa fa-plus"></i>
                                                        </button>
                                                    </div>
                                                    <div class="card-body">
                                                        <%--    <div class="mb-3">
                 <label class="form-label">Choose .xlsx File:</label>
                 <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" />
             </div><br /><br />--%>
                                                        <%--   <asp:Button ID="btnUpload" runat="server" Text="Upload & Clean"
                 CssClass="btn btn-success mb-3" OnClick="btnUpload_Click" />--%>

                                                        <asp:Label ID="lblMessage" runat="server" CssClass="text-danger fw-bold d-block"></asp:Label><br />
                                                        <br />
                                                        <asp:Label ID="lblRowCount" runat="server" CssClass="fw-bold d-block text-info"></asp:Label><br />
                                                        <br />
                                                        <asp:Label ID="lblColRemoved" runat="server" CssClass="fw-bold d-block text-success"></asp:Label><br />
                                                        <br />
                                                        <asp:Label ID="lblBlankRowRemoved" runat="server" CssClass="fw-bold d-block text-warning"></asp:Label><br />
                                                        <br />
                                                        <asp:Label ID="lblUnwantedRowRemoved" runat="server" CssClass="fw-bold d-block text-danger"></asp:Label><br />
                                                        <br />

                                                        <div class="table-responsive mt-3">
                                                            <div style="max-height: 400px; overflow-y: auto; border: 1px solid #ccc;">
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
                                                </div>
                                            </div>
                                            <!-- /.box-body -->
                                        </div>
                                        <!-- /.box -->
                                    </div>


                                    <br />


                                    <!-- Pie Chart -->

                                    <div class="card shadow mb-4">
                                        <div class="box box-default collapsed-box">
                                            <div class="box-header with-border" style="background-color: #cddc39;">
                                                <h3 class="box-title">Step 2: Fraud vs Genuine (Pie Chart)</h3>
                                                  <div class="box-tools pull-right">
                                                    <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                                        <i class="fa fa-plus"></i>
                                                    </button>
                                                </div>
                                            </div>
                                            <div class="box-body" style="display: flex; justify-content: center; align-items: center; height: 500px;">
                                                <div id="piechart" style="width: 600px; height: 400px;"></div>
                                            </div>
                                        </div>                                  
                                    </div>

                                    <br />
                                   


                                       <div class="card shadow mb-4">
       <div class="box box-default collapsed-box">
           <div class="box-header with-border" style="background-color: #fff9c4;">
               <h3 class="box-title">Step 3: ML Algorithm Accuracy Comparison</h3>
                 <div class="box-tools pull-right">
                   <button type="button" class="btn btn-box-tool" data-widget="collapse">
                       <i class="fa fa-plus"></i>
                   </button>
               </div>
           </div>
           <div class="box-body" style="display: flex; justify-content: center; align-items: center; height: 500px;">
                <div class="card shadow mb-4">
 
     <div class="card-body">
         <div id="accuracychart" style="width: 100%; height: 400px;"></div>
         <p class="mt-3"><strong>Note:</strong> Accuracy values shown are indicative and can vary based on dataset size and training.</p>
     </div>
 </div>
           </div>
       </div>                                  
   </div>



                                    <!-- ML Accuracy -->
                                   
                                    <br />
                             


                                       <div class="card shadow mb-4">
          <div class="box box-default collapsed-box">
            <div class="box-header with-border" style="background-color: #ffccd5;">
              <h3 class="box-title">Step 4: Feature-Based Risk Analysis</h3>

              <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-plus"></i>
                </button>
              </div>
              <!-- /.box-tools -->
            </div>
            <!-- /.box-header -->
            <div class="box-body" style="display: flex; justify-content: center; align-items: center; height: 500px;">
          
          <div id="featurechart" style="width: 100%; height: 400px;"></div>
          <p>This chart shows how various transaction attributes contribute to fraud predictions.</p>
     
            </div>
            <!-- /.box-body -->
          </div>
          <!-- /.box -->
        </div>


                                    <!-- Feature-Based Prediction -->
                                  
                                    <br />
                


                                       <div class="card shadow mb-4">
          <div class="box box-default collapsed-box">
            <div class="box-header with-border" style="background-color: #e0e0f8;">
              <h3 class="box-title">Step 5: Rule-Based Fraud Predictions (Simulated)</h3>

              <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-plus"></i>
                </button>
              </div>
              <!-- /.box-tools -->
            </div>
            <!-- /.box-header -->
            <div class="box-body">
                <div class="card shadow mb-5">
     
       <div class="card-body">

           <hr style="margin: 30px 0;" />

           <h4 style="color: red;">Fraud Predictions</h4>
           <div style="max-height: 300px; overflow-y: auto; border: 1px solid #ccc; padding: 10px;">
               <asp:GridView ID="GridFraud" runat="server" AutoGenerateColumns="true" CssClass="table table-bordered" />
           </div>

           <hr style="margin: 30px 0;" />

           <h4 style="color: green;">Genuine Predictions</h4>
           <div style="max-height: 300px; overflow-y: auto; border: 1px solid #ccc; padding: 10px;">
               <asp:GridView ID="GridGenuine" runat="server" AutoGenerateColumns="true" CssClass="table table-bordered" />
           </div>

       </div>
   </div>
            </div>
            <!-- /.box-body -->
          </div>
          <!-- /.box -->
        </div>


                                    <!-- Predicted Grid -->
                                 



                                </div>
                                <!-- /.box-body -->
                            </div>
                        </div>
                    </div>
                </section>








                <!-- ----end-------------- -->

                <!-- /.content -->
            </div>
            <!-- /.content-wrapper -->

            <!-- Main Footer -->
            <footer class="main-footer">
                <!-- To the right -->
                <div class="pull-right hidden-xs">
                    Anything you want
                </div>
                <!-- Default to the left -->
                <strong>Copyright &copy; 2025 <a target="_blank" href="">Bank</a>.</strong> All rights
reserved.
            </footer>

            <!-- Control Sidebar -->
            <aside class="control-sidebar control-sidebar-dark">
                <!-- Create the tabs -->
                <ul class="nav nav-tabs nav-justified control-sidebar-tabs">
                    <li class="active"><a href="#control-sidebar-home-tab" data-toggle="tab"><i class="fa fa-home"></i></a></li>
                    <li><a href="#control-sidebar-settings-tab" data-toggle="tab"><i class="fa fa-gears"></i></a></li>
                </ul>
                <!-- Tab panes -->
                <div class="tab-content">
                    <!-- Home tab content -->
                    <div class="tab-pane active" id="control-sidebar-home-tab">
                        <h3 class="control-sidebar-heading">Recent Activity</h3>
                        <ul class="control-sidebar-menu">
                            <li>
                                <a href="javascript:;">
                                    <i class="menu-icon fa fa-birthday-cake bg-red"></i>

                                    <div class="menu-info">
                                        <h4 class="control-sidebar-subheading">Langdon's Birthday</h4>

                                        <p>Will be 23 on April 24th</p>
                                    </div>
                                </a>
                            </li>
                        </ul>
                        <!-- /.control-sidebar-menu -->

                        <h3 class="control-sidebar-heading">Tasks Progress</h3>
                        <ul class="control-sidebar-menu">
                            <li>
                                <a href="javascript:;">
                                    <h4 class="control-sidebar-subheading">Custom Template Design
                <span class="pull-right-container">
                    <span class="label label-danger pull-right">70%</span>
                </span>
                                    </h4>

                                    <div class="progress progress-xxs">
                                        <div class="progress-bar progress-bar-danger" style="width: 70%"></div>
                                    </div>
                                </a>
                            </li>
                        </ul>
                        <!-- /.control-sidebar-menu -->

                    </div>
                    <!-- /.tab-pane -->
                    <!-- Stats tab content -->
                    <div class="tab-pane" id="control-sidebar-stats-tab">Stats Tab Content</div>
                    <!-- /.tab-pane -->
                    <!-- Settings tab content -->
                    <div class="tab-pane" id="control-sidebar-settings-tab">
                        <form method="post">
                            <h3 class="control-sidebar-heading">General Settings</h3>

                            <div class="form-group">
                                <label class="control-sidebar-subheading">
                                    Report panel usage
              <input type="checkbox" class="pull-right" checked>
                                </label>

                                <p>
                                    Some information about this general settings option
                                </p>
                            </div>
                            <!-- /.form-group -->
                        </form>
                    </div>
                    <!-- /.tab-pane -->
                </div>
            </aside>
            <!-- /.control-sidebar -->
            <!-- Add the sidebar's background. This div must be placed
  immediately after the control sidebar -->
            <div class="control-sidebar-bg"></div>
        </div>
        <!-- ./wrapper -->

        <!-- REQUIRED JS SCRIPTS -->

        <!-- jQuery 3 -->
        <script src="bower_components/jquery/dist/jquery.min.js"></script>
        <!-- Bootstrap 3.3.7 -->
        <script src="bower_components/bootstrap/dist/js/bootstrap.min.js"></script>
        <!-- AdminLTE App -->
        <script src="dist/js/adminlte.min.js"></script>

        <!-- Optionally, you can add Slimscroll and FastClick plugins.
     Both of these plugins are recommended to enhance the
     user experience. -->
    </form>
</body>


<script>
    function del(id) {

        window.location.href = "Delshop.aspx?id=" + id;
    }
</script>
<script>
    function edit(id) {

        window.location.href = "AddEditShop.aspx?id=" + id;
    }
</script>
</html>
