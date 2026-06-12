<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="qaz.aspx.cs" Inherits="bank.qaz" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>URL Malware Scanner</title>

    <!-- Bootstrap CSS CDN -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />

    <style>
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background: #f5f6fa;
            padding: 50px 0;
        }

        .scanner-container {
            max-width: 700px;
            margin: auto;
            background: #fff;
            padding: 30px 40px;
            border-radius: 12px;
            box-shadow: 0 8px 20px rgba(0,0,0,0.1);
        }

        h2 {
            margin-bottom: 30px;
            font-weight: 600;
            text-align: center;
            color: #333;
        }

        .form-control {
            padding: 12px 15px;
            font-size: 1rem;
            border-radius: 8px;
            border: 1px solid #ced4da;
        }

        .btn-scan {
            background: #007bff;
            color: #fff;
            font-weight: 500;
            padding: 10px 25px;
            border-radius: 8px;
            transition: 0.3s;
        }

        .btn-scan:hover {
            background: #0056b3;
        }

        .result {
            margin-top: 25px;
            padding: 20px;
            border-radius: 8px;
            background: #f8f9fa;
            border: 1px solid #dee2e6;
            font-size: 1rem;
        }

        .loading {
            color: #0d6efd;
            font-weight: 600;
        }

        a.report-link {
            color: #007bff;
            text-decoration: none;
        }

        a.report-link:hover {
            text-decoration: underline;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="scanner-container">
            <h2>URL Malware Scanner</h2>

            <div class="mb-3">
                <asp:TextBox ID="txtUrl" runat="server" CssClass="form-control" Placeholder="Enter URL to scan"></asp:TextBox>
            </div>

            <div class="mb-3 text-center">
                <asp:Button ID="btnScan" runat="server" CssClass="btn btn-scan" Text="Scan URL" OnClick="btnScan_Click" />
            </div>

            <div class="result">
                <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </form>

    <!-- Bootstrap JS CDN (Optional for future enhancements) -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
