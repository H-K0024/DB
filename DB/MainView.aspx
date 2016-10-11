<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainView.aspx.cs" Inherits="DB.MainView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="MainView.css"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
            <asp:GridView ID="DB_Grid" runat="server" Height="192px" Width="396px">
            </asp:GridView>
        <asp:Panel ID="Button_Panel" CssClass="Panel" runat="server" Height="431px" Width="394px">
            <asp:Label ID="ID_Label"  runat="server"></asp:Label>
            <asp:TextBox ID="ID_Text" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="Name_Label" runat="server"></asp:Label>
            <asp:TextBox ID="Label_Text" CssClass="Text" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="Point_Label" runat="server"></asp:Label>
            <asp:TextBox ID="Point_Text" CssClass="Text" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="Add_Button" runat="server" Height="40px" Text="追加" Width="105px" />
            <br />
            <br />
            <asp:Button ID="Update_Button" runat="server"　Height="40px" Text="更新" Width="105px"　/>
            <br />
            <br />
            <asp:Button ID="Delete_Button" runat="server"　Height="40px" Text="削除"　Width="105px" />
        </asp:Panel>
    </form>
</body>
</html>
