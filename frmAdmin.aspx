<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmAdmin.aspx.cs" Inherits="Admin_form" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        body {
            font-family: 'Khmer OS Battambang';
            font-size: 10px;
            border: double;
            margin: 0;
            padding: 5px; 
        }

        .Container {
            display: flex;
            justify-content: center; /* Centers the buttons horizontally */
            align-items: center;     /* Aligns items vertically */
            height: 10vh;           /* Full height of the viewport */
        }

        .session {
            display: flex;          /* Use flexbox to arrange children in a row */
            gap: 20px;             /* Space between buttons */
        }

        .AddorEditU, .AddorEditDep {
            flex: 0 0 auto;        /* Prevents items from shrinking or growing */
        }

        .button {
            border: double;
            border-color: aqua;
            border-radius: 30px;
            background-color: #CC66FF; /* Keep the button background color */
            font-weight: bold;          /* Keep the font bold */
            font-family: 'Kh Muol';     /* Keep the font family */
            height: 50px;               /* Set height */
            width: 343px;               /* Set width */
            color: white;               /* Text color */
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