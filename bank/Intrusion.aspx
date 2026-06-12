<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Intrusion.aspx.cs" Inherits="bank.Intrusion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet">
    <style>
        body {
            background-color: #0a1f44;
            color: #fff;
            font-family: 'Segoe UI', sans-serif;
        }

        /* Hero Section */
        .hero {
            background: linear-gradient(to bottom right, #0a1f44, #004e92);
            padding: 100px 0;
            text-align: center;
        }

        .hero h1 {
            font-size: 2.8rem;
            font-weight: 700;
            color: #fff;
        }

        .hero p {
            font-size: 1.2rem;
            max-width: 750px;
            margin: 20px auto;
            color: #dcdcdc;
        }

        .btn-custom {
            background-color: #00bcd4;
            border: none;
            color: #fff;
            padding: 10px 25px;
            font-size: 1.1rem;
            border-radius: 50px;
            transition: 0.3s;
        }

        .btn-custom:hover {
            background-color: #0288d1;
        }

        /* Features Section */
        .features {
            background-color: #ffffff;
            color: #000;
            padding: 80px 0;
        }

        .feature-card {
            background: #f7f9fc;
            border: none;
            border-radius: 15px;
            box-shadow: 0 4px 10px rgba(0,0,0,0.1);
            transition: transform 0.3s ease;
        }

        .feature-card:hover {
            transform: translateY(-8px);
        }

        .feature-icon {
            font-size: 3rem;
            color: #007bff;
        }

        /* Footer */
        footer {
            background-color: #001f3f;
            color: #ccc;
            padding: 25px 0;
        }

        footer a {
            color: #00bcd4;
            text-decoration: none;
        }

        footer a:hover {
            text-decoration: underline;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <!-- Navbar -->
        <nav class="navbar navbar-expand-lg navbar-dark bg-dark shadow-sm fixed-top">
            <div class="container">
                <a class="navbar-brand fw-bold text-info" href="#">Bank IDS</a>
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav">
                    <span class="navbar-toggler-icon"></span>
                </button>

                <div class="collapse navbar-collapse" id="navbarNav">
                    <ul class="navbar-nav ms-auto">
                        <li class="nav-item"><a class="nav-link active" href="#">Home</a></li>
                        <li class="nav-item"><a class="nav-link" href="Login.aspx">Login</a></li>
                        <li class="nav-item"><a class="nav-link" href="IntrusionReg.aspx">Register</a></li>
                        <li class="nav-item"><a class="nav-link" href="About.aspx">About</a></li>
                    </ul>
                </div>
            </div>
        </nav>

        <!-- Hero Section -->
        <section class="hero mt-5">
            <div class="container">
                <h1 class="mb-3">Bank Intrusion Detection System</h1>
                <p class="lead">
                    A secure, intelligent system designed to monitor, detect, and prevent unauthorized access
                    and cyber threats in real-time within online banking systems.
                </p>
                <a href="Login.aspx" class="btn btn-custom mt-3">Get Started</a>
            </div>
        </section>

        <!-- Features Section -->
        <section class="features text-center">
            <div class="container">
                <h2 class="mb-5 fw-bold text-primary">Key Features</h2>
                <div class="row g-4">
                    <div class="col-md-4">
                        <div class="card feature-card p-4">
                            <div class="feature-icon mb-3">
                                <i class="bi bi-shield-lock-fill"></i>
                            </div>
                            <h5 class="fw-bold">Real-Time Monitoring</h5>
                            <p>Continuously monitors banking networks to detect and block unauthorized access attempts instantly.</p>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="card feature-card p-4">
                            <div class="feature-icon mb-3">
                                <i class="bi bi-cpu-fill"></i>
                            </div>
                            <h5 class="fw-bold">AI-Based Detection</h5>
                            <p>Utilizes intelligent algorithms to analyze activity patterns and identify suspicious behaviors.</p>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="card feature-card p-4">
                            <div class="feature-icon mb-3">
                                <i class="bi bi-blockchain"></i>
                            </div>
                            <h5 class="fw-bold">Blockchain Security</h5>
                            <p>Ensures secure, tamper-proof transaction records and enhances transparency in data validation.</p>
                        </div>
                    </div>
                </div>
            </div>
        </section>

        <!-- Footer -->
        <footer class="text-center">
            <div class="container">
                <p class="mb-1">&copy; 2025 Bank Intrusion Detection System. All rights reserved.</p>
                <p>Developed by <a href="#">Your Name / Team</a></p>
            </div>
        </footer>

    </form>

    <!-- Bootstrap JS & Icons -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.css">
</body>
</html>
