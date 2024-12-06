<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmViewers.aspx.cs" Inherits="Page2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        body {
            font-family: 'Khmer OS Battambang';
            font-size: 10px;
            border: double;
            margin: 0;
            padding: 20px; 
        }
        .container {
            width: 100%;
            display: flex;
            flex-direction: column;
            align-items: center;
        }
        .section {
            background-color:mediumorchid;
            width: 100%; 
            max-width: 1430px;
            padding: 20px;
            border-radius: 5px;
            margin-bottom: 20px;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1); 
        }
        .input-row {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 10px;
            width: 100%; 
        }
        .input-row div {
            flex: 1;
            margin-right: 10px;
        }
        .input-row div:last-child {
            margin-right: 0;
        }
        select, input[type="text"] {
            width: 100%; 
            height: 35px; 
            font-size: 16px; 
            border: 2px solid darkviolet; 
            border-radius: 5px;
            padding: 5px; 
            box-sizing: border-box;
        }
        .button-container {
            text-align: center;
            margin-top: 20px;
        }
        .button-style {
            height: 40px;
            background-color:  chartreuse;
            font-size: 25px;
            width: 100px;
            margin: 10px 0;
            color: black;
            border-radius: 5px; 
            border: 2px solid #008CBA;
            cursor: pointer; 
            transition-duration: 0.4s;
        }
        .button-style:hover{
            background-color: #008CBA;
            color: white;
        }
        .calendar-row {
            display: flex;
            justify-content: center; 
            margin: 20px 0;
        }
        .calendar-container {
            margin: 0 10px; 
            text-align: center; 
        }
        .data-grid2 {
            width: 97%;
            margin: 20px 0;
            border: 1px solid #000;
        }
            .data-grid2 td {
                border: 1px solid #000;
                text-align: center;
            }
           
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPage" Runat="Server">
    <div class="container">
        <div class="section">
            <asp:Label ID="lblTilte" runat="server">ព័ត៌មានឯកសារស្វែងរក</asp:Label>
            <div class="input-row">
                <div>
                    ប្រភេទស្វែងរក
                    <asp:DropDownList ID="drpSearchType" runat="server" CssClass="dropdown-style" OnSelectedIndexChanged="drpSearchType_SelectedIndexChanged" AutoPostBack="true" Height="45px">
                         <asp:ListItem Value="allDoc"> បង្ហាញទាំងអស់ </asp:ListItem>
                        <asp:ListItem Value="numDoc">លេខឯកសារ</asp:ListItem>
                        <asp:ListItem Value="nameDoc">ឈ្មោះឯកសារ</asp:ListItem>
                        <asp:ListItem Value="dateDoc"> កាលបរិច្ឆេទ</asp:ListItem>
                    </asp:DropDownList> 
                </div>
                <div>
                    អង្គភាព
                    <asp:DropDownList ID="drpDep" runat="server" CssClass="dropdown-style" OnSelectedIndexChanged="drpDep_SelectedIndexChanged" AutoPostBack="true" Height="45px">
                    </asp:DropDownList>
                </div>
                <div>
                    ប្រភេទឯកសារទាំងអស់
                   
                    <asp:DropDownList ID="drpDocType" runat="server" CssClass="dropdown-style" OnSelectedIndexChanged="drpDocType_SelectedIndexChanged" DataSourceID="SqlDataSource1" DataTextField="DocType" DataValueField="DocTypeId" Height="45px">
                    </asp:DropDownList> 
                   
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DbDoc %>" SelectCommand="SELECT * FROM [tblDocType]"></asp:SqlDataSource>
                   
                </div>
            </div>
            <div class="input-row">
                <div>
                    <asp:Label ID="lblSType" runat="server" Text="Label">  ឈ្មោះឯកសារ</asp:Label>
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="input-style" Height="45px"></asp:TextBox>
                </div>
            </div>
            <div class="calendar-row">
                <div class="calendar-container">
                    ចាប់ពីថ្ងៃទី
                    <asp:Calendar ID="fromDate" runat="server" Width="400px" Height="220px" 
                        DayHeaderStyle-BackColor="#ffff66" 
                        DayHeaderStyle-Font-Bold="true" TitleStyle-BackColor="#993399" 
                        TitleStyle-VerticalAlign="Middle" PrevMonthText="" BackColor="White" 
                        BorderColor="Black" Font-Names="Times New Roman" Font-Size="10pt" 
                        ForeColor="Black" NextPrevFormat="FullMonth" AutoPostBack="true" DayNameFormat="Shortest" Font-Strikeout="False" Font-Underline="True"  TitleFormat="Month" OnSelectionChanged="fromDate_SelectionChanged">
                        <DayHeaderStyle Font-Bold="True" Font-Size="7pt" ForeColor="#333333" Height="10pt" BackColor="#FF3399"></DayHeaderStyle>
                        <DayStyle Width="14%" BackColor="#CC66FF" ForeColor="White" />
                        <NextPrevStyle Font-Size="8pt" ForeColor="White" BackColor="#CC00FF" />
                        <OtherMonthDayStyle ForeColor="#999999" BackColor="#CC00FF" />
                        <SelectedDayStyle BackColor="#CC3333" ForeColor="White" />
                        <SelectorStyle BackColor="#CCCCCC" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" ForeColor="#333333" Width="1%" />
                        <TitleStyle VerticalAlign="Middle" BackColor="Black" 
                            Font-Bold="True" Font-Size="13pt" ForeColor="White" Height="14pt" HorizontalAlign="Center"></TitleStyle>
                        <TodayDayStyle BackColor="#CCCC99" />
                    </asp:Calendar>
                </div>
                <div class="calendar-container">
                    ដល់ថ្ងៃ
                    <asp:Calendar ID="toDate" runat="server" Width="400px" Height="220px" 
                        DayHeaderStyle-BackColor="#ffff66" 
                        DayHeaderStyle-Font-Bold="true" TitleStyle-BackColor="#993399" 
                        TitleStyle-VerticalAlign="Middle" PrevMonthText="" BackColor="White" 
                        BorderColor="Black" Font-Names="Times New Roman" Font-Size="10pt" 
                        ForeColor="Black" NextPrevFormat="FullMonth" AutoPostBack="true" DayNameFormat="Shortest" Font-Strikeout="False" Font-Underline="True"  TitleFormat="Month" OnSelectionChanged="toDate_SelectionChanged" SelectedDate="11/29/2024 15:33:14">
                        <DayHeaderStyle Font-Bold="True" Font-Size="7pt" ForeColor="#333333" Height="10pt" BackColor="#FF66CC"></DayHeaderStyle>
                        <DayStyle Width="14%" BackColor="#CC33FF" ForeColor="White" />
                        <NextPrevStyle Font-Size="8pt" ForeColor="White" BackColor="#CC66FF" />
                        <OtherMonthDayStyle ForeColor="#999999" />
                        <SelectedDayStyle BackColor="#CC3333" ForeColor="White" />
                        <SelectorStyle BackColor="#CCCCCC" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" ForeColor="#333333" Width="1%" />
                        <TitleStyle VerticalAlign="Middle" BackColor="Black" 
                            Font-Bold="True" Font-Size="13pt" ForeColor="White" Height="14pt" HorizontalAlign="Center"></TitleStyle>
                        <TodayDayStyle BackColor="#CCCC99" />
                    </asp:Calendar>
                    <asp:Label ID="lblMessage" runat="server" Text="Label"></asp:Label>
                </div>
            </div>
            <div class="button-container">
                <asp:Button ID="btnSearch" runat="server" Text="ស្វែងរក" CssClass="button-style" 
                     Font-Bold="True" Font-Names="Khmer OS Bokor" Font-Size="13pt" OnClick="btnSearch_Click" />
                <asp:Button ID="btnOpen" runat="server" Text="បើកមើល" CssClass="button-style" 
                    Font-Bold="True" Font-Names="Khmer OS Bokor" Font-Size="13pt" OnClick="btnOpen_Click" />
                <asp:Button ID="btnDelete" runat="server" Text="សម្អាត" CssClass="button-style" 
                    Font-Bold="True" Font-Names="Khmer OS Bokor" Font-Size="13pt" OnClick="btnDelete_Click"  />
            </div>
            <div class="data-grid2">
            <asp:GridView ID="gridSearch" runat="server" BackColor="#FF99FF" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"  HorizontalAlign="Center" GridLines="Vertical" ForeColor="Black">
                <AlternatingRowStyle BackColor="#CCCCCC" />
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
            </Columns>
                <EditRowStyle BackColor="#FF99FF" ForeColor="Black" HorizontalAlign="Center" VerticalAlign="Middle" />
            <FooterStyle BackColor="#CCCCCC" />
            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="Black" HorizontalAlign="Center" VerticalAlign="Middle" />
            <PagerStyle BackColor="#FFCCCC" ForeColor="Black" HorizontalAlign="Center" />
                <RowStyle BackColor="#6666FF" HorizontalAlign="Center" VerticalAlign="Middle" />
            <SelectedRowStyle BackColor="#9900CC" Font-Bold="True" ForeColor="black" HorizontalAlign="Center" VerticalAlign="Middle" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#808080" HorizontalAlign="Center" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" HorizontalAlign="Center" VerticalAlign="Middle" />
            <SortedDescendingHeaderStyle BackColor="#383838" HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
        </asp:GridView>
        
            </div>
        </div>
    </div>
</asp:Content>