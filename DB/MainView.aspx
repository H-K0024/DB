﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainView.aspx.cs" Inherits="DB.MainView" %>

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
            <asp:GridView ID="DB_Grid" runat="server" Height="192px" Width="396px" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" OnSelectedIndexChanged="DB_Grid_SelectedIndexChanged">
                <Columns>
                    <asp:CommandField ButtonType="Button" HeaderText="選択" ShowSelectButton="True" />
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
        <asp:Panel ID="Button_Panel" CssClass="Panel" runat="server" Height="431px" Width="394px">
            <asp:Label ID="ID_Label"  runat="server"></asp:Label>
            <asp:TextBox ID="ID_Text" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="Name_Label" runat="server"></asp:Label>
            <asp:TextBox ID="Name_Text" CssClass="Text" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="Point_Label" runat="server"></asp:Label>
            <asp:TextBox ID="Point_Text" CssClass="Text" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="Add_Button" runat="server" Height="40px" Text="追加" Width="105px" OnClick="Add_Button_Click" />
            <br />
            <br />
            <asp:Label ID="UP_ID" runat="server"></asp:Label>
            <asp:Label ID="UP_ID_Label" runat="server"></asp:Label>
            <br />
            <asp:Label ID="UP_Name" runat="server"></asp:Label>
            <asp:TextBox ID="UP_Name_Text" runat="server" CssClass="Text"></asp:TextBox>
            <br />
            <asp:Label ID="UP_Point" runat="server"></asp:Label>
            <asp:TextBox ID="UP_Point_Text" runat="server" CssClass="Text"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="Update_Button" runat="server"　Height="40px" Text="更新" Width="105px"　 OnClick="Update_Button_Click"/>
            <br />
            <br />
            <asp:Label ID="Del_ID" runat="server"></asp:Label>
            <asp:Label ID="DEL_ID_Label" runat="server"></asp:Label>
            <br />
            <br />
            <asp:Button ID="Delete_Button" runat="server"　Height="40px" Text="削除"　Width="105px" OnClick="Delete_Button_Click" />
        </asp:Panel>
    </form>
</body>
</html>
