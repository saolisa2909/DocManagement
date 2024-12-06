<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmUsers.aspx.cs" Inherits="Page1" %>

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
            background-color:  darkorchid;
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
           // display: block; /* Change to block for full-width usage */
           text-align:center;
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
           border: 2px solid #008CBA;
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
            background-color: darkorchid;
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
        <h1 style="font-size:30px; text-shadow: 1px 1px 2px pink;">សូមជ្រើសរើស បញ្ចូលឯកសារ​ ឬ កែប្រែ</h1>
        <div style="font-size: 18px;">
    <asp:RadioButton Id="rdAdd" runat="server" Text="បញ្ចូលឯកសារ" GroupName="Ratio" AutoPostBack="true" OnCheckedChanged="rdAdd_CheckedChanged"/> 
    &nbsp; &nbsp; &nbsp;
    <asp:RadioButton Id="rdUpdate" runat="server" Text="កែប្រែឯកសារ" GroupName="Ratio" AutoPostBack="true" OnCheckedChanged="rdUpdate_CheckedChanged"/>
        </div>
    </div>

    <div class="form-section">
        <div class="input-field">
            <label style="font-family:Khmer, 'Khmer OS Muol'; font-size:20px; font-weight:bold;">ព៌ត័មានឯកសារ</label>
            ឯកសារភ្ជាប់
            <asp:FileUpload ID="FUpload" runat="server"/>
        </div>
        
        <div class="input-field row">
            <div style="flex: 1; margin-right: 30px; font-family:Khmer, 'Khmer OS'; font-size:20px;">
                <label>លេខឯកសារ</label>
                <asp:TextBox ID="txtFileNum" runat="server"></asp:TextBox>
            </div>
            <div>
                <label>ប្រភេទ</label>
                <asp:DropDownList ID="drpDocTypeId" runat="server" Width="400px" Height="45px" Font-Names="Bokor" DataSourceID="SqlDataSource1" DataTextField="DocType" DataValueField="DocTypeId">
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DbDoc %>" SelectCommand="SELECT * FROM [tblDocType]"></asp:SqlDataSource>
            </div>
        </div>
        
        <div class="input-field">
            <label>ចំណងជើងឯកសារ</label>
            <asp:TextBox ID="txtFileTitle" runat="server"></asp:TextBox>
        </div>
        <div class="input-field">
            <label>កំណត់ចំណាំ</label>
            <asp:TextBox ID="txtFileNote" runat="server"></asp:TextBox>
        </div>
        
       <div class="checkbox-container">
             <asp:CheckBox ID="chkA" runat="server" OnCheckedChanged="ChkA_CheckedChanged" AutoPostBack="true" />
         </div>
        <div class="checkbox-container">
            <asp:Label ID="lblBlank" runat="server" Height="25px" Width="300px" ></asp:Label>                        
        </div>
        
        <div class="button-container">
            <asp:Button ID="btnInput" runat="server" Text="បញ្ចូល" CssClass="button-style"  Font-Bold="True" Font-Names="Khmer OS Bokor" Font-Size="13pt" OnClick="btnInput_Click" BorderColor="#660066"  />
            <asp:Button ID="BtnOpen" runat="server" Text="បើកមើល" CssClass="button-style"  Font-Bold="True" Font-Names="Khmer OS Bokor" Font-Size="13pt" BorderColor="#660066" OnClick="BtnOpen_Click" /> 
            <asp:Button ID="BtnEdit" runat="server" Text="កែប្រែ" CssClass="button-style"  Font-Bold="True" Font-Names="Khmer OS Bokor" Font-Size="13pt" OnClick="BtnEdit_Click" BorderColor="#660066" />
            <asp:Button ID="BtnDelete" runat="server" Text="សម្អាត" CssClass="button-style"  Font-Bold="True" Font-Names="Khmer OS Bokor" Font-Size="13pt" OnClick="BtnDelete_Click" BorderColor="#660066" />
        </div>
    </div>

   <div class="search-section-container search-section">
        <label style="font-weight:bold;">ការស្វែងរក</label>
        <div class="input-field search-row">
            <label style="width:300px;">ប្រភេទឯកសារ</label>
            <asp:DropDownList ID="drpSearch" runat="server"  Placeholder="លេខឯកសារ" DataSourceID="SqlDataSource2" DataTextField="FileNumber" DataValueField="DocId" Height="40px" ></asp:DropDownList>
           
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DbDoc %>" SelectCommand="SELECT * FROM [tblDocument]"></asp:SqlDataSource>
           
            <label style="width:300px;">ឈ្មោះឯកសារ</label>
            <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="ស្វែងរក" CssClass="button-style" Width="190px" Height="50px" BackColor="#FF3399" Font-Bold="true" Font-Names="Khmer OS Bokor" Font-Size="13pt" OnClick="btnSearch_Click"/>
        </div>
    </div>

    <div class="data-grid">
        
        <asp:GridView ID="gridSearch" runat="server" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" OnSelectedIndexChanged="gridSearch_SelectedIndexChanged" HorizontalAlign="Center" CellSpacing="1" GridLines="None">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
            </Columns>
            <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" HorizontalAlign="Center" />
            <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
            <RowStyle ForeColor="Black" BackColor="#DEDFDE" />
            <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#594B9C" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#33276A" HorizontalAlign="Center" VerticalAlign="Middle" Wrap="False" />
        </asp:GridView>
        
    </div>
</asp:Content>