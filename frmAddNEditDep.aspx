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
            margin-bottom:10px;
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
        <h1>សូមជ្រើសរើស បញ្ចូល ឬ កែប្រែប៉ាតេម៉ង</h1>
        <div style="font-size: 18px;">
    <asp:RadioButton Id="rdAdd" runat="server" Text="បង្កើតគណនី" GroupName="Ratio" AutoPostBack="true" OnCheckedChanged="rdAdd_CheckedChanged" /> 
    &nbsp; &nbsp; &nbsp;
    <asp:RadioButton Id="rdUpdate" runat="server" Text="កែប្រែគណនី" GroupName="Ratio" OnCheckedChanged="rdUpdate_CheckedChanged" AutoPostBack="true"/>
        </div>
    </div>

    <div class="form-section">
       <div class="input-field row">
            
                 <div style="flex: 1; margin-right: 5px; font-family:Khmer, 'Khmer OS'; font-size:20px;">
                <label>ឈ្មោះដេប៉ាតេម៉ង</label>
               <asp:TextBox ID="txtLName" runat="server"></asp:TextBox>
          </div>
        </div>
        
        <div class="input-field row">
            <div style="flex: 1; margin-right: 10px; font-family:Khmer, 'Khmer OS'; font-size:20px;">
                <label>ឈ្មោះកាត់នៃដេប៉ាតេម៉ង</label>
                <asp:TextBox ID="txtUName" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="input-field row">
            <div style="flex: 1; margin-right: 20px; font-family:Khmer, 'Khmer OS'; font-size:20px;">
                  <label>កាលបរិច្ចេទបង្កើត</label>
                 <asp:TextBox ID="txtCreateDate" runat="server"></asp:TextBox>
                </div>
        </div>
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
 
   <div class="search-section-container search-section">
        <label style="font-weight:bold;">ការស្វែងរដេប៉ាតេម៉ង</label>
        <div class="input-field search-row">
            <label style="width:300px;">ឈ្មោះដេប៉ាតេម៉ង</label>
            <asp:TextBox ID="drpSearchType" runat="server"></asp:TextBox>
            <label style="width:300px;">ឈ្មោះកាត់ដេប៉ាតេម៉ង</label>
            <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="ស្វែងរក" CssClass="button-style" Width="180px" Height="50px" BackColor="#FF3399" Font-Bold="true" Font-Names="Khmer OS Bokor" Font-Size="13pt" />
        </div>
    </div>
</asp:Content>

