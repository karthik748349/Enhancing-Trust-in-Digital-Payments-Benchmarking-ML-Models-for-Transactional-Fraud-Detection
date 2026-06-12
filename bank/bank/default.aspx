<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="bank._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Oskar | Responsive Bootstrap 4 Landing Template</title>
    <meta name="description" content="" />
    <meta name="keywords" content="" />
    <meta name="author" content="CreataThemes" />

    <link rel="shortcut icon" href="img/fevicon.png">

    <!--Bootstrap Css-->
    <link rel="stylesheet" type="text/css" href="css/bootstrap.min.css" />

    <link rel="stylesheet" href="css/magnific-popup.css">

    <!-- Animate Css -->
    <link rel="stylesheet" href="css/animate.min.css">

    <!-- Materialdesign icons Css -->
    <link href="css/materialdesignicons.min.css" rel="stylesheet">

    <!-- pe-icon-7 css -->
    <link href="css/pe-icon-7.css" rel="stylesheet">

    <!-- Owl Slider -->
    <link rel="stylesheet" href="css/owl.carousel.css" />
    <link rel="stylesheet" href="css/owl.theme.css" />
    <link rel="stylesheet" href="css/owl.transitions.css" />


    <!-- Custom style Css -->
    <link href="css/style.css" rel="stylesheet">
</head>

<body>

    <!--Navbar Start-->
    <nav class="navbar navbar-expand-lg fixed-top custom-nav sticky">
        <div class="container">
            <!-- LOGO -->
            <a class="logo navbar-brand" href="index-2.html">
                <img src="img/logo.png" alt="" class="img-fluid logo-light">
                <img src="img/logo-dark.png" alt="" class="img-fluid logo-dark">
            </a>
            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarCollapse" aria-controls="navbarCollapse" aria-expanded="false" aria-label="Toggle navigation">
                <i class="mdi mdi-menu"></i>
            </button>
            <div class="collapse navbar-collapse" id="navbarCollapse">
                <ul class="navbar-nav ml-auto navbar-center" id="mySidenav">
                    <li class="nav-item active">
                        <a href="#home" class="nav-link">Home</a>
                    </li>

                    <li class="nav-item">
                        <a href="#services" class="nav-link">Services</a>
                    </li>
                    <li class="nav-item">
                        <a href="register.aspx" class="nav-link">Register</a>
                    </li>
                    <li class="nav-item">
                        <a href="#login" class="nav-link">Login</a>
                    </li>
                </ul>
            </div>
        </div>
    </nav>
    <!-- Navbar End -->

    <!--Start Home-->
    <section class="home-bg section h-100vh" id="home">
        <div class="bg-overlay"></div>
        <div class="home-table">
            <div class="home-table-center">
                <div class="container">
                    <div class="row justify-content-center">
                        <div class="col-lg-12">
                            <div class="text-white text-center">
                                <h1 class="header_title mb-0 mt-3 text-center text-white mx-auto">Bank Intrusion Detection System
                                </h1>
                                <p class="header_subtitle text-white mx-auto mt-3 mb-0">
                                    A robust and intelligent security framework designed to monitor, detect, and prevent unauthorized access and malicious activities in online banking environments using advanced analytics and real-time monitoring.
                                </p>
                                <ul class="mb-0 list-inline text-center skill_home mt-5">
                                    <li class="list-inline-item mr-0">Location: India</li>
                                    <li class="list-inline-item mr-0">+91 98765 43210</li>
                                    <li class="list-inline-item mr-0">contact@frauddetection.ai</li>
                                </ul>
                                <ul class="social_home list-unstyled text-center pt-5">
                                    <li class="list-inline-item"><a href="#"><i class="mdi mdi-facebook"></i></a></li>
                                    <li class="list-inline-item"><a href="#"><i class="mdi mdi-linkedin"></i></a></li>
                                    <li class="list-inline-item"><a href="#"><i class="mdi mdi-dribbble"></i></a></li>
                                    <li class="list-inline-item"><a href="#"><i class="mdi mdi-google-plus"></i></a></li>
                                    <li class="list-inline-item"><a href="#"><i class="mdi mdi-twitter"></i></a></li>
                                </ul>
                                <div class="header_btn">
                                    <a href="#about" class="btn btn-outline-custom btn-rounded mt-4 mr-3">Learn More</a>
                                    <a href="#contact" class="btn btn-custom btn-rounded mt-4">Contact Us</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <!--End Home-->



    <!-- Start Fraud Detection Services -->
    <section class="section bg-light" id="services">
        <div class="container">
            <div class="row">
                <div class="col-lg-12">
                    <div class="section_title text-center">
                        <h3>Fraud Detection for <span class="font-weight-bold">Online Banking</span></h3>
                        <div class="vr_line mx-auto d-block"></div>
                        <p class="sec_subtitle mx-auto text-muted pt-2">
                            Our intelligent system ensures secure banking by detecting and preventing fraudulent transactions using rule-based logic and data analytics.
                        </p>
                    </div>
                </div>
            </div>

            <div class="row mt-3">
                <div class="col-lg-4">
                    <div class="services_boxes bg-white mt-3 rounded p-4">
                        <div class="services_icon text-custom">
                            <h2>01.</h2>
                        </div>
                        <div class="services_content mt-3">
                            <h5 class="mb-0 font-weight-bold">Anomaly Detection</h5>
                            <p class="text-muted mb-0 mt-2">Identify suspicious transactions by detecting patterns that deviate from normal user behavior.</p>
                        </div>
                        <div class="read_more">
                            <a href="#" class="text-custom"><i class="pe-7s-search"></i></a>
                        </div>
                    </div>
                </div>

                <div class="col-lg-4">
                    <div class="services_boxes bg-white mt-3 rounded p-4">
                        <div class="services_icon text-custom">
                            <h2>02.</h2>
                        </div>
                        <div class="services_content mt-3">
                            <h5 class="mb-0 font-weight-bold">Risk Scoring</h5>
                            <p class="text-muted mb-0 mt-2">Assign risk scores to transactions based on multiple parameters like IP, device, and amount.</p>
                        </div>
                        <div class="read_more">
                            <a href="#" class="text-custom"><i class="pe-7s-graph"></i></a>
                        </div>
                    </div>
                </div>

                <div class="col-lg-4">
                    <div class="services_boxes bg-white mt-3 rounded p-4">
                        <div class="services_icon text-custom">
                            <h2>03.</h2>
                        </div>
                        <div class="services_content mt-3">
                            <h5 class="mb-0 font-weight-bold">Device & Location Tracking</h5>
                            <p class="text-muted mb-0 mt-2">Monitor device type and geolocation to prevent access from unusual environments.</p>
                        </div>
                        <div class="read_more">
                            <a href="#" class="text-custom"><i class="pe-7s-map-marker"></i></a>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row mt-3">
                <div class="col-lg-4">
                    <div class="services_boxes bg-white mt-3 rounded p-4">
                        <div class="services_icon text-custom">
                            <h2>04.</h2>
                        </div>
                        <div class="services_content mt-3">
                            <h5 class="mb-0 font-weight-bold">Real-time Alerts</h5>
                            <p class="text-muted mb-0 mt-2">Instantly notify users and administrators when high-risk activity is detected.</p>
                        </div>
                        <div class="read_more">
                            <a href="#" class="text-custom"><i class="pe-7s-bell"></i></a>
                        </div>
                    </div>
                </div>

                <div class="col-lg-4">
                    <div class="services_boxes bg-white mt-3 rounded p-4">
                        <div class="services_icon text-custom">
                            <h2>05.</h2>
                        </div>
                        <div class="services_content mt-3">
                            <h5 class="mb-0 font-weight-bold">Historical Data Analysis</h5>
                            <p class="text-muted mb-0 mt-2">Leverage past transaction history to detect repeat fraud patterns.</p>
                        </div>
                        <div class="read_more">
                            <a href="#" class="text-custom"><i class="pe-7s-clock"></i></a>
                        </div>
                    </div>
                </div>

                <div class="col-lg-4">
                    <div class="services_boxes bg-white mt-3 rounded p-4">
                        <div class="services_icon text-custom">
                            <h2>06.</h2>
                        </div>
                        <div class="services_content mt-3">
                            <h5 class="mb-0 font-weight-bold">Rule-Based Prediction</h5>
                            <p class="text-muted mb-0 mt-2">Apply expert-defined rules to identify fraud with high accuracy and transparency.</p>
                        </div>
                        <div class="read_more">
                            <a href="#" class="text-custom"><i class="pe-7s-tools"></i></a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <!-- End Fraud Detection Services -->


    <!-- Start Education -->

    <!-- End Education -->

    <!-- Start Cta -->

    <!-- End Cta -->

    <!-- Start Work -->

    <!-- End Work -->

    <!-- Start Client -->

    <!-- End Client -->

    <!-- Start Great People -->

    <!-- End Great People -->

    <!-- Start Blog -->

    <!-- End Blog -->

    <!-- Start Contact -->
    <section class="section_all bg-light" id="login">
        <div class="container">
            <div class="row justify-content-center">
                <div class="col-lg-6">
                    <div class="section_title text-center mb-4">
                        <h3 class="font-weight-bold">Admin Login</h3>
                        <div class="vr_line mx-auto d-block"></div>
                        <p class="sec_subtitle mx-auto text-muted pt-2">Enter your credentials to access the admin dashboard.</p>
                    </div>
                    <form id="loginForm" runat="server" class="bg-white p-4 rounded shadow-sm">
                        <div class="form-group">
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Placeholder="Email"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" Placeholder="Password" TextMode="Password"></asp:TextBox>
                        </div>
                        <div class="form-group text-center">
                            <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-custom" OnClick="btnLogin_Click" />
                        </div>
                        <div class="form-group text-danger text-center">
                            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </section>

    <!-- End Contact -->

    <!-- Start Footer -->
    <footer class="footer_detail">
        <div class="container">

            <div class="fot_bor"></div>
            <div class="row pt-3 pb-3">
                <div class="col-lg-12">
                    <div class="float-left float_none">
                        <p class="copy-rights mb-0"><a target="_blank" href="#">CodeRed</a></p>
                    </div>
                    <div class="float-right float_none">
                        <ul class="list-inline fot_social mb-0">
                            <li class="list-inline-item"><a href="#" class="social-icon text-muted"><i class="mdi mdi-facebook"></i></a></li>
                            <li class="list-inline-item"><a href="#" class="social-icon text-muted"><i class="mdi mdi-twitter"></i></a></li>
                            <li class="list-inline-item"><a href="#" class="social-icon text-muted"><i class="mdi mdi-linkedin"></i></a></li>
                            <li class="list-inline-item"><a href="#" class="social-icon text-muted"><i class="mdi mdi-google-plus"></i></a></li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </footer>
    <!-- End Footer -->

    <!-- Back to top -->
    <a href="#" class="back_top" style="display: inline;"><i class="pe-7s-up-arrow"></i></a>

    <!-- JAVASCRIPTS -->
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/popper.min.js"></script>
    <script src="js/jquery.easing.min.js"></script>

    <!-- scrollspy js -->
    <script src="js/scrollspy.min.js"></script>

    <!-- isotope js -->
    <script src="js/isotope.js"></script>

    <!-- magnific js -->
    <script src="js/jquery.magnific-popup.min.js"></script>

    <!-- Particles Js -->
    <script src="js/particles.js"></script>
    <script src="js/particles.app.js"></script>

    <!-- isotope js -->
    <script src="js/isotope.js"></script>

    <!-- custom js -->
    <script src="js/custom.js"></script>

    <script>
        $(".element").each(function () {
            var $this = $(this);
            $this.typed({
                strings: $this.attr('data-elements').split(','),
                typeSpeed: 100,
                backDelay: 3000
            });
        });
    </script>

</body>


<!-- images_405:50-->
</html>
