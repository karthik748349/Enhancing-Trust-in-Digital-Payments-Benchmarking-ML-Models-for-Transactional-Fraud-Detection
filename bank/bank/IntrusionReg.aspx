<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IntrusionReg.aspx.cs" Inherits="bank.IntrusionReg" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Register - Bank Intrusion Detection System</title>

    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />

    <style>
        body {
            background: linear-gradient(to bottom right, #0a1f44, #004e92);
            color: #fff;
            font-family: 'Segoe UI', sans-serif;
            height: 100vh;
        }

        .register-box {
            background-color: #ffffff;
            color: #000;
            border-radius: 15px;
            padding: 40px 30px;
            max-width: 500px;
            margin: 100px auto;
            box-shadow: 0 5px 20px rgba(0, 0, 0, 0.3);
        }

        .register-box h2 {
            font-weight: 700;
            color: #004e92;
            margin-bottom: 20px;
            text-align: center;
        }

        .btn-custom {
            background-color: #004e92;
            color: #fff;
            border-radius: 50px;
            padding: 10px 20px;
            font-size: 1rem;
            transition: 0.3s;
        }

        .btn-custom:hover {
            background-color: #0069c0;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="register-box">
                <h2>Create an Account</h2>

                <div class="mb-3">
                    <label for="txtName" class="form-label">Full Name</label>
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Enter your name"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <label for="txtEmail" class="form-label">Email Address</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter your email"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <label for="txtPhone" class="form-label">Phone Number</label>
                    <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" placeholder="Enter your phone number"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <label for="txtAddress" class="form-label">Address</label>
                    <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" placeholder="Enter your address"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <label for="txtPassword" class="form-label">Password</label>
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Create a password"></asp:TextBox>
                </div>

                <div class="d-grid">
                    <asp:Button ID="btnRegister" runat="server" CssClass="btn btn-custom" Text="Register" OnClick="btnRegister_Click" />
                </div>

                <div class="text-center mt-3">
                    <span>Already have an account?</span>
                    <a href="Login.aspx" class="text-decoration-none text-primary">Login here</a>
                </div>
            </div>
        </div>
    </form>

    <!-- Bootstrap JS -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>