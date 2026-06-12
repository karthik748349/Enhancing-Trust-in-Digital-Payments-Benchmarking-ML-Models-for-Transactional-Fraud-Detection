<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="bank.About" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>About - Bank Intrusion Detection System</title>

    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />

    <style>
        body {
            background: linear-gradient(135deg, #0a2a43, #0f5d8d);
            color: white;
            font-family: 'Poppins', sans-serif;
        }
        .about-section {
            padding: 80px 0;
        }
        .about-card {
            background: rgba(255, 255, 255, 0.1);
            border-radius: 15px;
            padding: 40px;
            box-shadow: 0 0 20px rgba(0, 0, 0, 0.3);
        }
        h2 {
            font-weight: 700;
            margin-bottom: 20px;
        }
        p {
            font-size: 16px;
            line-height: 1.7;
        }
        .btn-home {
            background-color: #007bff;
            border: none;
            border-radius: 10px;
        }
        .btn-home:hover {
            background-color: #0056b3;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container about-section text-center">
            <div class="row justify-content-center">
                <div class="col-md-10 col-lg-8 about-card">
                    <h2>About Bank Intrusion Detection System</h2>
                    <p>
                        The <strong>Bank Intrusion Detection System (BIDS)</strong> is a web-based security framework 
                        developed to protect online banking platforms from unauthorized access, cyber-attacks, 
                        and malicious activities. This system continuously monitors user activity, detects 
                        abnormal patterns, and alerts administrators in real time.
                    </p>
                    <p>
                        By integrating machine learning algorithms and advanced analytics, BIDS enhances 
                        cybersecurity by identifying intrusion attempts before they cause damage. 
                        It provides a proactive defense mechanism for both administrators and users, 
                        ensuring safe and secure digital banking operations.
                    </p>
                    <p>
                        The project’s goal is to reduce banking risks by improving the reliability and 
                        resilience of digital systems against external and internal threats.
                    </p>
                    <div class="mt-4">
                        <a href="Default.aspx" class="btn btn-home btn-primary">Back to Home</a>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <!-- Bootstrap JS -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>