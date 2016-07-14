<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication3.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>OrlaKiely在庫確認システム</title>
    
</head>
<body style="background-image:url(http://free-texture.net/wp-content/uploads/wall04-768x576.jpg)">
    <h1>Orla Kiely在庫確認システム</h1>
    
    
    <form id="form1" runat="server">
        <%--ログアウトリンクの表示--%>
        <div style="position:absolute;top:0;right:0;">
            <h3><asp:LinkButton ID="logout" runat="server" Text="ログアウト" OnClick="logout_Click" >ログアウト</asp:LinkButton></h3>
        </div>
        

        <%--位置の調整--%>
        <div class="container" style="width:900px;margin:10px auto;">

            <%
                var dt = DateTime.Today;
                Response.Write(dt.Year + "年" + dt.Month + "月" + (dt.Day -1) + "日最終の在庫データです" );

            %>
            <br/><br/>

            <asp:Literal ID="Literal3" runat="server" Text="仕入先コード："></asp:Literal>
            <asp:TextBox ID="Isearch" runat="server"></asp:TextBox>

            <asp:Literal ID="Literal1" runat="server" Text="商品コード："></asp:Literal>
            <asp:TextBox ID="Psearch" runat="server" ></asp:TextBox>

            <asp:Literal ID="Literal2" runat="server" Text ="店舗名："></asp:Literal>
            <asp:DropDownList ID="Ssearch" runat="server"></asp:DropDownList>
            
            <asp:Button ID="serchbotan" runat="server" Text="検索"/>
            <br/><br />
            
            <asp:Button ID="resetbutton" runat="server" Text="TOPに戻る" OnCommand="resetbutton_Command" />

            <asp:Button ID="CSVDownload" runat="server" Text="CSVダウンロード" OnClick="CSVDownload_Click" />
            <br/>
            <br/>
            <br/>

            <%Response.Write(Request.Cookies["UserName"].Value); %>
        
            <asp:GridView ID="GridView1" runat="server" ItemType="WebApplication3.tablejoin"  selectmethod="GridView1_GetData"  AllowPaging ="True" PageSize="100" AllowSorting="True"  ForeColor="#333333" GridLines="None" CellPadding="8" AutoGenerateColumns="False" Width="1000px">
                <Columns>
                    <asp:BoundField DataField="JAN" HeaderText="JAN" SortExpression="JAN" />

                    <asp:TemplateField HeaderText="商品コード" sortExpression="商品コード" >                   
                        <ItemTemplate>
                            <asp:LinkButton ID="Pcode" runat="server" Text='<%#Item.商品コード %>' OnCommand="PCode_Command" CommandName="Pcodename" CommandArgument="<%#Item.商品コード %>"></asp:LinkButton> 
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%--PostBackUrl='<%# string.Format("WebForm1.aspx/?P={0}",Item.商品コード) %>'--%>  
                    <asp:BoundField DataField="商品名" HeaderText="商品名" SortExpression="商品名" />

                    <asp:BoundField DataField="標準上代" HeaderText="標準上代" SortExpression="標準上代" />

                    <asp:BoundField DataField="在庫数" HeaderText="在庫数" SortExpression="在庫数" />

                    <asp:TemplateField HeaderText="店舗コード" sortExpression="店舗コード" >
                        <ItemTemplate>
                           <asp:LinkButton ID="Scode" runat="server" Text="<%#Item.店舗コード %>" OnCommand="Scode_Command" CommandName="Scodename" CommandArgument="<%#Item.店舗コード %>"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="店舗名" HeaderText="店舗名" SortExpression="店舗名" />
                    
                </Columns>
                <AlternatingRowStyle BackColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
        
        </div>
       
    <div>
    
    </div>
    </form>
</body>
</html>
