<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.master" CodeBehind="NewRequisition.aspx.vb" Inherits="PMWConsole02.NewRequisition" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <h2>Item Requisition Form</h2>

    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" DataSourceID="ItemsNeededCon"></dx:ASPxFormLayout>




    <asp:SqlDataSource runat="server" ID="ItemsNeededCon"></asp:SqlDataSource>
</asp:Content>

