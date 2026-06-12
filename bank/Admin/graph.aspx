<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="graph.aspx.cs" Inherits="bank.Admin.graph" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
         <meta charset="utf-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <title>Admin || CryptCloud</title>
  <!-- Tell the browser to be responsive to screen width -->
  <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
  <!-- Bootstrap 3.3.7 -->
  <link rel="stylesheet" href=" bower_components/bootstrap/dist/css/bootstrap.min.css">
  <!-- Font Awesome -->
  <link rel="stylesheet" href=" bower_components/font-awesome/css/font-awesome.min.css">
  <!-- Ionicons -->
  <link rel="stylesheet" href=" bower_components/Ionicons/css/ionicons.min.css">
  <!-- DataTables -->
  <link rel="stylesheet" href=" bower_components/datatables.net-bs/css/dataTables.bootstrap.min.css">
  <!-- Theme style -->
  <link rel="stylesheet" href=" dist/css/AdminLTE.min.css">
  <!-- AdminLTE Skins. Choose a skin from the css/skins
       folder instead of downloading all of them to reduce the load. -->
  <link rel="stylesheet" href=" dist/css/skins/_all-skins.min.css">

  <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
  <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
  <!--[if lt IE 9]>
  <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
  <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
  <![endif]-->
         <script type="text/javascript" src="https://www.google.com/jsapi"></script>  
  <!-- Google Font -->
  <link rel="stylesheet"
        href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic">
</head>
<body class="hold-transition skin-blue sidebar-mini">
<div class="wrapper">

 <header class="main-header">
    <!-- Logo -->
    <a href="index2.html" class="logo">
      <!-- mini logo for sidebar mini 50x50 pixels -->
      <span class="logo-mini"><b>A</b>LT</span>
      <!-- logo for regular state and mobile devices -->
      <span class="logo-lg"><b>Admin</b></span>
    </a>
    <!-- Header Navbar: style can be found in header.less -->
    <nav class="navbar navbar-static-top">
      <!-- Sidebar toggle button-->
      <a href="#" class="sidebar-toggle" data-toggle="push-menu" role="button">
        <span class="sr-only">Toggle navigation</span>
      </a>

      <div class="navbar-custom-menu">
        <ul class="nav navbar-nav">
          <!-- Messages: style can be found in dropdown.less-->
      
          <!-- Notifications: style can be found in dropdown.less -->

          <!-- Tasks: style can be found in dropdown.less -->
     
          <!-- User Account: style can be found in dropdown.less -->
          <li class="dropdown user user-menu">
            <a href="#" class="dropdown-toggle" data-toggle="dropdown">
             
              <span class="hidden-xs">Admin</span>
            </a>
            <ul class="dropdown-menu">
              <!-- User image -->
          
              <!-- Menu Body -->
          
              <!-- Menu Footer-->
              <li class="user-footer">
              
                <div class="pull-right">
                  <a href="../index.aspx" class="btn btn-default btn-flat">Sign out</a>
                </div>
              </li>
            </ul>
          </li>
          <!-- Control Sidebar Toggle Button -->
     
        </ul>
      </div>
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
		   <li class="active"><a href="#"><i class="fa fa-dashboard"></i> <span>Dashboard</span></a></li>
        <li class="active treeview">
		   
        </li>

        <li class="header">User Option</li>
       
        <%--     <li class="treeview">
          <a href="#">
            <i class="fa fa-files-o"></i>
            <span>Manage</span>
            <span class="pull-right-container">
              <span class="label label-primary pull-right">4</span>
            </span>
          </a>
           <ul class="treeview-menu">--%>
       <%--     <li><a href="Alumini.aspx"><i class="fa fa-circle-o"></i> Alumini</a></li>--%>
       <%--     <li><a href="index.aspx"><i class="fa fa-circle-o"></i> index</a></li>
            <li><a href="Current.aspx"><i class="fa fa-circle-o"></i> Current Students</a></li>--%>
          <%--   <li><a href="ManageJob.aspx"><i class="fa fa-circle-o"></i> Manage Job_Drive</a></li>--%>
         
     <%--     </ul>
        </li>--%>
     
       <%--<li><a href="courses.aspx"><i class="fa fa-circle-o text-yellow"></i> <span>Courses</span></a></li>--%>
       <%--   <li><a href="funds_view.aspx"><i class="fa fa-circle-o text-yellow"></i> <span>Funds</span></a></li>--%>
<%--        <li><a href="view_courses.aspx"><i class="fa fa-circle-o text-yellow"></i> <span>View Courses</span></a></li>--%>
    <%--   <li><a href="view_training.aspx"><i class="fa fa-circle-o text-yellow"></i> <span>Training Session</span></a></li>--%>
   <%--    <li><a href="view_alumuni_meet.aspx"><i class="fa fa-circle-o text-yellow"></i> <span>Alumini meet</span></a></li>--%>
                 <li><a href="index.aspx"><i class="fa fa-circle-o text-yellow"></i> <span>index</span></a></li>
                 <li><a href="graph.aspx"><i class="fa fa-circle-o text-yellow"></i> <span>Visual</span></a></li>
        <li><a href="../user/default.aspx"><i class="fa fa-sign-out text-aqua"></i> <span>Logout</span></a></li>
      </ul>
    </section>
    <!-- /.sidebar -->
  </aside>

  <!-- Content Wrapper. Contains page content -->
  <div class="content-wrapper">
    <!-- Content Header (Page header) -->
    <section class="content-header">
      <h1>
        Chart
        
      </h1>
      <ol class="breadcrumb">
        <li><a href="../index.aspx"><i class="fa fa-home"></i> Home</a></li>
       
      </ol>
    </section>

    <!-- Main content -->
    
    <section class="content">
      <div class="row">
        <div class="col-xs-12">
        
          <!-- /.box -->

          <div class="box">
            <div class="box-header">
              <h3 class="box-title">Chart </h3>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
              <table id="example1" class="table table-bordered table-striped">
         <%--   <div id="piechart" style="width: 900px; height: 500px;"></div>--%>
           <form runat="server">

               <h2 style="text-align: center;">Fraud Transaction Status Graph</h2>
            <div id="chart_divvv" style="width: 800px; height: 500px; margin: 0 auto;"></div>


         <div style="width:20%;float:left;margin:0;margin-left: 100px;">.
            </div>
            <div style="width:30%;float:left;margin:0;"/>
                
                                   <asp:Literal ID="ltScripts" runat="server"></asp:Literal>  
        <div id="chart_div" style="width: 760px; height: 400px;"> 

            </div>  

            <div style="width:30%;float:left;margin:0;">.
            </div>


                      <div style="width:20%;float:left;margin:0;margin-left: 100px;">.
            </div>
            <div style="width:30%;float:left;margin:0;"/>
                
                                   <asp:Literal ID="Literal1" runat="server"></asp:Literal>  
        <div id="chart_div1" style="width: 760px; height: 400px;"> 

            </div>  

            <div style="width:30%;float:left;margin:0;">.
            </div>
        </form> 
              </table>
            </div>
            <!-- /.box-body -->
          </div>
          <!-- /.box -->
        </div>




            <div class="col-xs-12">
        
          <!-- /.box -->

       
          <!-- /.box -->
        </div>
        <!-- /.col -->
      </div>
      <!-- /.row -->
    </section>
         
    <!-- /.content -->
  </div>
  <!-- /.content-wrapper -->
  <footer class="main-footer">
    <div class="pull-right hidden-xs">
     
    </div>
   <center> <strong>Copyright &copy; 2022-2023 <a target="_blank" href=""></a></strong> All rights
    reserved</center>
  </footer>

  <!-- Control Sidebar -->
 
  <!-- /.control-sidebar -->
  <!-- Add the sidebar's background. This div must be placed
       immediately after the control sidebar -->
  <div class="control-sidebar-bg"></div>
</div>
<!-- ./wrapper -->

<!-- jQuery 3 -->
<script src=" bower_components/jquery/dist/jquery.min.js"></script>
<!-- Bootstrap 3.3.7 -->
<script src=" bower_components/bootstrap/dist/js/bootstrap.min.js"></script>
<!-- DataTables -->
<script src=" bower_components/datatables.net/js/jquery.dataTables.min.js"></script>
<script src=" bower_components/datatables.net-bs/js/dataTables.bootstrap.min.js"></script>
<!-- SlimScroll -->
<script src=" bower_components/jquery-slimscroll/jquery.slimscroll.min.js"></script>
<!-- FastClick -->
<script src=" bower_components/fastclick/lib/fastclick.js"></script>
<!-- AdminLTE App -->
<script src=" dist/js/adminlte.min.js"></script>
<!-- AdminLTE for demo purposes -->
<script src=" dist/js/demo.js"></script>
<!-- page script -->
<script>
    $(function () {
        $('#example1').DataTable()
        $('#example3').DataTable()
        $('#example2').DataTable({
            'paging': true,
            'lengthChange': false,
            'searching': false,
            'ordering': true,
            'info': true,
            'autoWidth': false
        })
    })

</script>

     

        <script>


            function TDelete(id) {

                //alert(id);
                $.ajax({
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({ ID: id }),
                    url: "index.aspx/TdeleteClaim",
                    success: function (data) {
                        if (data.d == 1) {

                            alert("User Deleted");
                            //toastr.success("Image Requested.");
                            window.location.reload();

                        } else {
                            toastr.error("Error in deleting record.");
                        }

                    }

                });


            }

            function TtDelete(id) {

                //alert(id);
                $.ajax({
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({ ID: id }),
                    url: "index.aspx/TtdeleteClaim",
                    success: function (data) {
                        if (data.d == 1) {

                            alert("User Deleted");
                            //toastr.success("Image Requested.");
                            window.location.reload();

                        } else {
                            toastr.error("Error in deleting record.");
                        }

                    }

                });


            }


            function reject(id) {

                //alert(id);
                $.ajax({
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({ ID: id }),
                    url: "index.aspx/deleteClaim",
                    success: function (data) {
                        if (data.d == 1) {

                            alert("User Rejected");
                            //toastr.success("Image Requested.");
                            window.location.reload();

                        } else {
                            toastr.error("Error in deleting record.");
                        }

                    }

                });
            }

            function freject(id) {

                //alert(id);
                $.ajax({
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: JSON.stringify({ ID: id }),
                    url: "index.aspx/fdeleteClaim",
                    success: function (data) {
                        if (data.d == 1) {

                            alert("User Rejected");
                            //toastr.success("Image Requested.");
                            window.location.reload();

                        } else {
                            toastr.error("Error in deleting record.");
                        }

                    }

                });
            }
        </script>

       <script>

           function approve(id) {

               //alert(id);
               $.ajax({
                   type: 'POST',
                   contentType: "application/json; charset=utf-8",
                   dataType: "json",
                   data: JSON.stringify({ ID: id }),
                   url: "index.aspx/approve",
                   success: function (data) {
                       if (data.d == 1) {

                           alert("User Approved");
                           //toastr.success("Image Requested.");
                           window.location.reload();

                       } else {
                           toastr.error("Error in deleting record.");
                       }

                   }

               });
           }




           function fapprove(id) {

               //alert(id);
               $.ajax({
                   type: 'POST',
                   contentType: "application/json; charset=utf-8",
                   dataType: "json",
                   data: JSON.stringify({ ID: id }),
                   url: "index.aspx/fapprove",
                   success: function (data) {
                       if (data.d == 1) {

                           alert("User Approved");
                           //toastr.success("Image Requested.");
                           window.location.reload();

                       } else {
                           toastr.error("Error in deleting record.");
                       }

                   }

               });
           }
       </script>
</body>
</html>

