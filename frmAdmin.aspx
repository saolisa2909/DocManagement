<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmAdmin.aspx.cs" Inherits="Admin_form" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        body {
            font-family: 'Khmer OS Battambang';
            font-size: 15px;
            border: double;
            margin: 0;
            padding: 5px; 
        }

        .Container {
            display: flex;
            justify-content: center; 
            align-items: center;     
            height: 10vh;         
        }

        .session {
            display: flex;          
            gap: 20px;             
        }

        .AddorEditU, .AddorEditDep {
            flex: 0 0 auto;        
        }

        .button {
            border: 2px solid #008CBA;
            border-radius:20px;
            background-color: #CC66FF; 
            font-weight: bold;          
            font-family: 'Kh Muol';    
            height: 60px;               
            width:250px;
            color: white; 
            transition-duration: 0.4s;
        }
        .button:hover{
             background-color: chartreuse;
            color: black;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPage" Runat="Server">
    <div class="Container">
        <div class="session">
            <div class="AddorEditU">
                <asp:Button ID="btnAddrEditU" runat="server" CssClass="button" Text="បង្កើតឬកែប្រែគណនីអ្នកប្រើប្រាស់" OnClick="btnAddrEditU_Click" />
            </div>
            <div class="AddorEditDep">
                <asp:Button ID="btnAddrEditDep" runat="server" CssClass="button" Text="បង្កើតឬកែប្រែដេប៉ាតេម៉ង" OnClick="btnAddrEditDep_Click" />
            </div>
        </div>
    </div>
</asp:Content>