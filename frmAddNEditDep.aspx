<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmAddNEditDep.aspx.cs" Inherits="frmAddNEditDep" %>

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
            font-size: 10px;
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
            width: 97%; /* Changed to 100% */
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
        <h1  style="font-size:30px; text-shadow: 1px 1px 2px pink;">សូមជ្រើសរើស បញ្ចូល ឬ កែប្រែ គណនីអ្នកប្រើប្រាស់</h1>
        <div style="font-size: 18px;">
    <asp:RadioButton Id="rdAdd" runat="server" Text="បង្កើតគណនី" GroupName="Ratio" OnCheckedChanged="rdAdd_CheckedChanged" AutoPostBack="true" /> 
    &nbsp; &nbsp; &nbsp;
    <asp:RadioButton Id="rdUpdate" runat="server" Text="កែប្រែគណនី" GroupName="Ratio" OnCheckedChanged="rdUpdate_CheckedChanged" AutoPostBack="true"/>
        </div>
    </div>

    <div class="form-section">
       <div class="input-field row">
            <div style="flex: 1; margin-right: 30px; font-family:'Khmer OS'; font-size:15px;">
                     <label>ឈ្មោះដេប៉ាតេម៉ង</label>
                     <asp:TextBox ID="txtDep" runat="server"></asp:TextBox>
             </div>
             <div style="flex: 1; margin-right: 30px; font-family:'Khmer OS'; font-size:15px;">
                    <label>ឈ្មោះកាត់ដេប៉ាតេម៉ង</label>
                    <asp:TextBox ID="txtTDep" runat="server"></asp:TextBox>
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
         <div class="search-section-container search-section">
            <label style="font-weight:bold;">ការស្វែងរកគណនី</label>
            <div class="input-field search-row">
                <label style="width:300px;">លេខដេប៉ាតង់</label>
                <asp:DropDownList ID="drpSearch" runat="server" Height="45px" DataSourceID="SqlDataSource1" DataTextField="DepId" DataValueField="DepId" ></asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DbDoc %>" SelectCommand="SELECT * FROM [tblDepartment]"></asp:SqlDataSource>
                <label style="width:300px;">ឈ្មោះដេប៉ាតេម៉ង</label>
                <asp:TextBox ID="txtSearch" runat="server" Height="40px"></asp:TextBox>
            </div>
            
        </div>
        
        <div class="button-container">
            <asp:Button ID="btnInput" runat="server" Text="បញ្ចូល" CssClass="button-style"  Font-Bold="True" Font-Names="Khmer OS Bokor" Font-Size="13pt" OnClick="btnInput_Click" />
            <asp:Button ID="btnEdit" runat="server" Text="កែប្រែ" CssClass="button-style"  Font-Bold="True" Font-Names="Khmer OS Bokor" Font-Size="13pt" OnClick="btnEdit_Click" />
            <asp:Button ID="btnDelete" runat="server" Text="សម្អាត" CssClass="button-style"  Font-Bold="True" Font-Names="Khmer OS Bokor" Font-Size="13pt" OnClick="btnDelete_Click"/>
            <asp:Button ID="btnSearch" runat="server" Text="ស្វែងរក" CssClass="button-style"  Font-Bold="True" Font-Names="Khmer OS Bokor" Font-Size="13pt" OnClick="btnSearch_Click"/>
        </div>

  

    <div class="data-grid">

        <asp:GridView ID="gridSearch" runat="server" BackColor="Red" BorderColor="#990099" BorderStyle="Double" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="None" OnSelectedIndexChanged="gridSearch_SelectedIndexChanged" AllowCustomPaging="True" AllowPaging="True" CaptionAlign="Bottom" ForeColor="Fuchsia" HorizontalAlign="Center">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
            </Columns>
            <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
            <HeaderStyle BackColor="#FF0066" Font-Bold="True" ForeColor="#E7E7FF" />
            <PagerStyle BackColor="Black" ForeColor="Black" HorizontalAlign="Right" />
            <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
            <SelectedRowStyle BackColor="Black" Font-Bold="True" ForeColor="#CC3300" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#594B9C" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#33276A" />
        </asp:GridView>
    </div>
</asp:Content>
