<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmAddNEditeUsers.aspx.cs" Inherits="CreateUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPage" Runat="Server">
    <style>
        body {
            font-family: 'Khmer OS Battambang';
            font-size: 10px;
            margin: 0;
            padding: 20px; 
        }
        .header {
            font-family: 'Khmer OS Muol';
            font-size: 15px;
            padding-bottom: 5px;
            text-align: center; 
        }
        .search-section {
            background-color: cornflowerblue;
            margin-bottom: 10px;
            border: none;
            padding: 10px; 
        }
        .data-grid {
            width: 99%; /* Changed to 100% */
            border: 2px solid #000; 
            border-radius: 5px; 
            padding: 10px; 
            margin-top: 20px; 
            display: block; /* Change to block for full-width usage */
        }
        .data-grid td {
            border: 1px solid #000;
            text-align: center;
           
        }
      
        .button-style {
            background-color: white; 
            color: black; 
            border: 2px solid #008CBA;
            font-size: 18px;
            text-align: center;
            display: inline-block; 
            width: 100px; 
            border-radius: 5px; 
            padding: 5px; 
            cursor: pointer; 
            transition-duration: 0.4s;
        }
        .button-style:hover{
            background-color: #008CBA;
            color: white;
        }
        
        .button-container {
            
            display: flex;
            justify-content: center; 
            gap: 20px; 
            margin-top: 10px; 
            margin-bottom:10px;
        }

        .form-section, .search-section-container {
            background-color:mediumpurple;
            padding: 15px; 
            margin-bottom: 15px;
            border-radius: 5px; 
            font-size: 15px;
        }

        .form-section label {
            display: block; 
            margin-bottom: 5px; 
        }

        .input-field {
            margin-bottom: 10px;
        }
        .input-field input, .input-field select {
            width: 100%; 
            height: 30px; 
            padding: 5px; 
        }

        .row {
            display: flex; 
            justify-content: space-between; 
            align-items: center; 
        }

        .search-row {
            display: flex;
            align-items: center; 
            gap: 10px; 
        }

        .checkbox-container {
            display: flex; 
            align-items: center; 
            margin-bottom: 10px;
        }
    </style>

    <div class="header">
        <h1 style="font-size:30px; text-shadow: 1px 1px 2px pink;">សូមជ្រើសរើស បញ្ចូល ឬ កែប្រែ គណនីអ្នកប្រើប្រាស់</h1>
        <div style="font-size: 18px;">
    <asp:RadioButton Id="rdAdd" runat="server" Text="បង្កើតគណនី" GroupName="Ratio" OnCheckedChanged="rdAdd_CheckedChanged" AutoPostBack="true" /> 
    &nbsp; &nbsp; &nbsp;
    <asp:RadioButton Id="rdUpdate" runat="server" Text="កែប្រែគណនី" GroupName="Ratio" OnCheckedChanged="rdUpdate_CheckedChanged" AutoPostBack="true"/>
        </div>
    </div>

    <div class="form-section">
       <div class="input-field row">
            <div style="flex: 1; margin-right: 30px; font-family:'Khmer OS'; font-size:15px;">
                     <label>អត្តលេខ</label>
                     <asp:TextBox ID="txtNum" runat="server"></asp:TextBox>
             </div>
             <div style="flex: 1; margin-right: 30px; font-family:'Khmer OS'; font-size:15px;">
                    <label>ឈ្មោះអ្នកប្រើ</label>
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
             </div>
           ​​​  <div style="flex: 1; margin-right: 10px; font-family:'Khmer OS'; font-size:15px;">
                    <label>អង្គភាព</label>
                    <asp:DropDownList ID="drpDep" runat="server" Height="45px" Width="500px" DataSourceID="SqlDataSource1" DataTextField="DepName" DataValueField="DepId"> </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DbDoc %>" SelectCommand="SELECT * FROM [tblDepartment]"></asp:SqlDataSource>
             </div>
        </div>
        <div class="input-field row">
             <div style="flex: 1; margin-right: 30px; font-family:'Khmer OS'; font-size:20px;">
                <label>លេខសំងាត់</label>
                <asp:TextBox ID="txtPw" runat="server"  MaxLength="8"></asp:TextBox>
            </div>
            <div style="flex: 1; margin-right: 10px; font-family:'Khmer OS'; font-size:20px;">
                <label>តួនាទី</label>
                <asp:DropDownList ID="drpRole" runat="server" Width="350px" Height="45px" DataSourceID="SqlDataSource2" DataTextField="Role" DataValueField="RoleId">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DbDoc %>" SelectCommand="SELECT * FROM [tblRoles]"></asp:SqlDataSource>
            </div>
            <div style="flex: 1; margin-right: 10px; font-family:'Khmer OS'; font-size:20px;">
                <label>ដំណើរការ</label>
                <asp:DropDownList ID="drpStatus" runat="server" Width="350px" Height="45px" >
                    <asp:ListItem Value="True">True</asp:ListItem>
                     <asp:ListItem Value="False">False</asp:ListItem>
                    <asp:ListItem Value="True">True</asp:ListItem>
                    <asp:ListItem Value="False">False</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
         
         <div class="input-field row">
            <div class="checkbox-container">
               <asp:CheckBox ID="chkA" runat="server" OnCheckedChanged="chkA_CheckedChanged" AutoPostBack="true" Height="20px"/>   
                &nbsp; &nbsp; &nbsp;
                <asp:Label ID="lblBlank" runat="server" Height="25px" Width="500px" ></asp:Label>  
            </div>
        </div>
       </div>
        <div class="button-container">
            <asp:Button ID="BtnInput" runat="server" Text="បញ្ចូល" CssClass="button-style"  Font-Bold="True" Font-Names="Khmer OS Bokor" Font-Size="13pt" OnClick="BtnInput_Click" />
            <asp:Button ID="BtnEdit" runat="server" Text="កែប្រែ" CssClass="button-style"  Font-Bold="True" Font-Names="Khmer OS Bokor" Font-Size="13pt" OnClick="BtnEdit_Click" />
            <asp:Button ID="BtnDelete" runat="server" Text="សម្អាត" CssClass="button-style"  Font-Bold="True" Font-Names="Khmer OS Bokor" Font-Size="13pt" OnClick="BtnDelete_Click"/>
        </div>

   <div class="search-section-container search-section">
        <label style="font-weight:bold;">ការស្វែងរកគណនី</label>
        <div class="input-field search-row">
            <label style="width:300px;">ឈ្មោះគណនី</label>
            <asp:DropDownList ID="drpSearch" runat="server" Placeholder="លេខឯកសារ" Height="45px" DataSourceID="SqlDataSource3" DataTextField="StaffId" DataValueField="UserId"></asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:DbDoc %>" SelectCommand="SELECT * FROM [tblUsers]"></asp:SqlDataSource>
            <label style="width:300px;">ឈ្មោះឯកសារ</label>
             <asp:TextBox ID="txtSearch" runat="server" Height="40px"></asp:TextBox>
            <asp:Button ID="BtnSearch" runat="server" Text="ស្វែងរក" CssClass="button-style" Width="190px" Height="50px" BackColor="#FF3399" Font-Bold="true" Font-Names="Khmer OS Bokor" Font-Size="13pt" OnClick="BtnSearch_Click" />
        </div>
    </div>

    <div class="data-grid">

        <asp:GridView ID="gridSearch" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" CaptionAlign="Bottom" Font-Bold="True" HorizontalAlign="Center" RowHeaderColumn="center" ShowHeaderWhenEmpty="True" OnSelectedIndexChanged="gridSearch_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
            </Columns>
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
            <SelectedRowStyle BackColor="#FFFFCC" Font-Bold="True" ForeColor="Maroon" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#0000A9" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#000065" ForeColor="Black" HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
        </asp:GridView>

    </div>
</asp:Content>

