<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="ado.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="GridView1" runat="server">
            </asp:GridView>

            <asp:Label ID="Label1" runat="server" Text="更新公司名稱"></asp:Label>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
            <asp:GridView ID="GridView2" runat="server"></asp:GridView>

            <p id="p1" runat="server">
            </p>
            <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>

            <asp:HyperLink ID="HyperLink1" runat="server">HyperLink</asp:HyperLink>

            <asp:Repeater ID="Repeater1" runat="server"></asp:Repeater>

            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        </div>
    </form>
</body>
</html>