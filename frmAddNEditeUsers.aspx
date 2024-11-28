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
            font-size: 18px;
            text-align: center;
            display: inline-block; 
            width: 100px; 
            border-radius: 5px;
            color: black; 
            padding: 5px; 
            border: none;
            border: 2px;
            cursor: pointer; 
        }
        
        .button-container {
            display: flex;
            justify-content: center; 
            gap: 20px; 
            margin-top: 10px; 
        }

        .form-section, .search-section-container {
            background-color: antiquewhite;
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
        <h1>សូមជ្រើសរើស បញ្ចូល ឬ កែប្រែ គណនីអ្នកប្រើប្រាស់</h1>
        <div style="font-size: 18px;">
    <asp:RadioButton Id="rdAdd" runat="server" Text="បង្កើតគណនី" GroupName="Ratio" OnCheckedChanged="rdAdd_CheckedChanged" AutoPostBack="true" /> 
    &nbsp; &nbsp; &nbsp;
    <asp:RadioButton Id="rdUpdate" runat="server" Text="កែប្រែគណនី" GroupName="Ratio" OnCheckedChanged="rdUpdate_CheckedChanged" AutoPostBack="true"/>
        </div>
    </div>

    <div class="form-section">
       <div class="input-field row">
            <div style="flex: 1; margin-right: 20px; font-family:Khmer, 'Khmer OS'; font-size:20px;">
                  <label>គោត្តនាម</label>
                 <asp:TextBox ID="txtFName" runat="server"></asp:TextBox>
                </div>
                 <div style="flex: 1; margin-right: 5px; font-family:Khmer, 'Khmer OS'; font-size:20px;">
                <label>នាម</label>
               <asp:TextBox ID="txtLName" runat="server"></asp:TextBox>
          </div>
        </div>
        
        <div class="input-field row">
            <div style="flex: 1; margin-right: 10px; font-family:Khmer, 'Khmer OS'; font-size:20px;">
                <label>ឈ្មោះគណនី</label>
                <asp:TextBox ID="txtUName" runat="server"></asp:TextBox>
            </div>
           
            <div style="flex: 1; margin-left: 10px;">
                <label>តួនាទី</label>
                <asp:DropDownList ID="drpRoleU" runat="server" Width="400px" Height="40px" Font-Names="Bokor" DataSourceID="SqlDataSource1" DataTextField="Role" DataValueField="RoleId">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DbDoc %>" SelectCommand="SELECT * FROM [tblRoles]"></asp:SqlDataSource>
            </div>
        </div>
        
        <div class="input-field row">
            <div style="flex: 1; margin-right: 20px; font-family:Khmer, 'Khmer OS'; font-size:20px;">
                  <label>លេខសម្ងាត់</label>
                 <asp:TextBox ID="txtPw" runat="server"></asp:TextBox>
                </div>
                 <div style="flex: 1; margin-right: 5px; font-family:Khmer, 'Khmer OS'; font-size:20px;">
                <label>ដេប៉ាតេម៉ង</label>
                <asp:DropDownList ID="drpDepU" runat="server" Width="400px" Height="40px" Font-Names="Bokor" DataSourceID="SqlDataSource2" DataTextField="DepName" DataValueField="DepId">
                </asp:DropDownList>    
                     <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DbDoc %>" SelectCommand="SELECT * FROM [tblDepartment]"></asp:SqlDataSource>
          </div>
        </div>
        <div class="input-field row">
            <div style="flex: 1; margin-right: 20px; font-family:Khmer, 'Khmer OS'; font-size:20px;">
                  <label>កាលបរិច្ចេទបង្កើត</label>
                 <asp:TextBox ID="txtCreateDate" runat="server"></asp:TextBox>
                </div>
                 <div style="flex: 1; margin-right: 5px; font-family:Khmer, 'Khmer OS'; font-size:20px;">
                <label>កាលបរិច្ចេទបញ្ចប់</label>
                <asp:TextBox ID="txtModifyDate" runat="server"></asp:TextBox>
                     <br />
                     +</div>
        </div>
        
        <div class="checkbox-container">
            <asp:CheckBox ID="chkA" runat="server"/>                        
        </div>
        <div class="checkbox-container">
            <asp:Label ID="lblBlank" runat="server" Height="25px" Width="300px" ></asp:Label>                        
        </div>
        
        <div class="button-container">
            <asp:Button ID="btnInput" runat="server" Text="បញ្ចូល" CssClass="button-style"  Font-Bold="True" Font-Names="Khmer OS Bokor" Font-Size="13pt" />
            <asp:Button ID="BtnEdit" runat="server" Text="កែប្រែ" CssClass="button-style"  Font-Bold="True" Font-Names="Khmer OS Bokor" Font-Size="13pt" />
            <asp:Button ID="BtnDelete" runat="server" Text="សម្អាត" CssClass="button-style"  Font-Bold="True" Font-Names="Khmer OS Bokor" Font-Size="13pt"/>
        </div>
    </div>

   <div class="search-section-container search-section">
        <label style="font-weight:bold;">ការស្វែងរកគណនី</label>
        <div class="input-field search-row">
            <label style="width:300px;">ឈ្មោះគណនី</label>
            <asp:TextBox ID="drpSearchType" runat="server"></asp:TextBox>
            <label style="width:300px;">ឈ្មោះដេប៉ាតេម៉ង</label>
            <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="ស្វែងរក" CssClass="button-style" Width="190px" Height="50px" BackColor="#FF3399" Font-Bold="true" Font-Names="Khmer OS Bokor" Font-Size="13pt" />
        </div>
    </div>
</asp:Content>

