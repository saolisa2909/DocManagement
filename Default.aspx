<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
body {
    width: 100%;
    font-family: 'Khmer OS Battambang';
    font-size: 10px;
    text-align: center;
}

.container {
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    background-color: blueviolet;
    border-radius:45px;
    width: 30%;
    height: 50vh;
    padding: 15px;
    border: double;
    border: 1px solid;
    margin: 0 auto;
    box-shadow: 5px 10px 8px 10px #888888;
}

.input-group {
    margin: 10px 0;
}

.label {
    background: linear-gradient(to right, red, blue); /* Direction of gradient */
    -webkit-background-clip: text;
    background-clip: text;
    -webkit-text-fill-color: transparent; /* For Safari */
   text-shadow: 5px 5px #888888;
    font-family: 'Khmer OS Bokor';
    font-size: 30px;
    font-weight: bold;
    margin-bottom: 10px;

}

.button {
    font-family: 'Khmer OS Battambang';
    font-size: 20px;
    background-color:aqua;
    border: 2px solid #008CBA;
    font-weight: bold;
    border-radius:8px;
    margin: 5px;
    transition-duration: 0.4s;
}
.button:hover{
     background-color: chartreuse;
     color: white;
}
</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPage" Runat="Server">
    <div class="container">
    <div class="label">
       កម្មវិធីគ្រប់គ្រងឯកសាររបស់...
    </div>
    <div class="input-group">
        <label style="color:white;">ឈ្មោះចូល</label>
        <asp:TextBox ID="txtUName" runat="server" Height="25px" Width="250px" style="margin-left: 12px"></asp:TextBox>
    </div>
    <div class="input-group">
        <label style="color:white;">លេខសម្ងាត់</label>
        <asp:TextBox ID="txtPw" runat="server" TextMode="Password" MaxLength="12" Height="25px" Width="250px"></asp:TextBox>
    </div>
    <div class="input-group">
         <asp:Label ID="lblBlank" runat="server" Height="25px" Width="350px" ></asp:Label>
    </div>
    <div class="input-group">
        <asp:Button ID="btnAdd" runat="server" Text="ចូល" CssClass="button" Height="50px" Width="100px"  OnClick="btnAdd_Click1"/>
        <asp:Button ID="btnClear" runat="server" Text="សម្អាត" CssClass="button" Height="50px" Width="100px" OnClick="btnClear_Click1" />
    </div>
    <div class="input-group">
        <asp:HyperLink ID="CreateUser" runat="server" Text="បង្កើតគណនីអ្នកប្រើ" NavigateUrl="~/CreateUser.aspx"></asp:HyperLink>
    </div>
</div>
</asp:Content>