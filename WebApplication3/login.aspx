<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="WebApplication3.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container" style="width:300px;margin:10px auto;">
        <asp:Login ID="Login1" runat="server" FailureText="ログインに失敗しました。ユーザー名・またはパスワードを確認してください。"  OnAuthenticate="Login1_Authenticate" BackColor="#F7F7DE" BorderColor="#CCCC99" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="10pt"  RememberMeText="次回以降入力を省略">
            <TitleTextStyle BackColor="#6B696B" Font-Bold="True" ForeColor="#FFFFFF" />
        </asp:Login>

    </div>
    </form>
</body>
</html>
