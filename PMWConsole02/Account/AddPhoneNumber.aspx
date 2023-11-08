<%@ Page Language="VB" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeBehind="AddPhoneNumber.aspx.vb" Inherits="PMWConsole02.AddPhoneNumber" %>

<asp:content id="ClientArea" contentplaceholderid="MainContent" runat="server">
     
<div class="accountHeader">
    <h2>Add a phone number</h2>
</div>
<p><asp:Literal runat="server" ID="ErrorMessage" /></p>
<dx:ASPxTextBox runat="server" ID="PhoneNumber" TextMode="Phone" Caption="Phone Number" >
    <CaptionSettings Position="Top" />
    <ValidationSettings>
        <RequiredField IsRequired="true" ErrorText="The PhoneNumber field is required."/>
    </ValidationSettings>
</dx:ASPxTextBox><br />
<dx:ASPxButton runat="server" ID="btnSubmit" OnClick="PhoneNumber_Click" Text="Submit" />
 
</asp:content>